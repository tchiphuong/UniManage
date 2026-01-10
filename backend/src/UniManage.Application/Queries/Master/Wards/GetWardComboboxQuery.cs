using MediatR;
using System.Text;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Wards
{
    #region Query

    public sealed class GetWardComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItemDto>>>
    {
        public string? ProvinceCode { get; init; }
    }

    #endregion

    #region Handler

    public sealed class GetWardComboboxQueryHandler : IRequestHandler<GetWardComboboxQuery, ApiResponse<List<ComboboxItemDto>>>
    {
        public async Task<ApiResponse<List<ComboboxItemDto>>> Handle(GetWardComboboxQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.ProvinceCode), request.ProvinceCode)
                }
            };

            try
            {
                using (var db = new DbContext())
                {
                    var isEnglish = request.HeaderInfo?.Language?.StartsWith("en", StringComparison.OrdinalIgnoreCase) == true;
                    var nameColumn = isEnglish ? "NameEn" : "NameVi";

                    var query = new StringBuilder();
                    query.AppendLine($@"
                        SELECT 
                            Code, 
                            {nameColumn} AS Name,
                            ProvinceCode
                        FROM ad_wards
                        WHERE IsActive = 1");

                    if (!string.IsNullOrEmpty(request.ProvinceCode))
                    {
                        query.AppendLine("AND ProvinceCode = @ProvinceCode");
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine($"AND (Code LIKE @Keyword OR {nameColumn} LIKE @Keyword)");
                    }

                    query.AppendLine($"ORDER BY SortOrder, {nameColumn}");

                    var wards = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new
                        {
                            request.ProvinceCode,
                            Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%"
                        },
                        ct);

                    var items = wards.Select(w => new ComboboxItemDto
                    {
                        Value = w.Code,
                        Label = w.Name,
                        Status = 1,
                        Metadata = new Dictionary<string, object>
                        {
                            ["ProvinceCode"] = w.ProvinceCode ?? ""
                        }
                    }).ToList();

                    var response = ResponseHelper.Success(items);
                    log.Result = new { Count = items.Count };
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = 1;
                log.Message = ex.ToString();
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                UniLogger.Error($"Error getting ward combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItemDto>>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }

    #endregion
}
