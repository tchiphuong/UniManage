using MediatR;
using System.Text;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Provinces
{
    #region Query

    public sealed class GetProvinceComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItemDto>>>
    {
        public string? CountryCode { get; init; }
    }

    #endregion

    #region Handler

    public sealed class GetProvinceComboboxQueryHandler : IRequestHandler<GetProvinceComboboxQuery, ApiResponse<List<ComboboxItemDto>>>
    {
        public async Task<ApiResponse<List<ComboboxItemDto>>> Handle(GetProvinceComboboxQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.CountryCode), request.CountryCode)
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
                            CountryCode
                        FROM ad_provinces
                        WHERE IsActive = 1");

                    if (!string.IsNullOrEmpty(request.CountryCode))
                    {
                        query.AppendLine("AND CountryCode = @CountryCode");
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine($"AND (Code LIKE @Keyword OR {nameColumn} LIKE @Keyword)");
                    }

                    query.AppendLine($"ORDER BY SortOrder, {nameColumn}");

                    var provinces = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new
                        {
                            request.CountryCode,
                            Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%"
                        },
                        ct);

                    var items = provinces.Select(p => new ComboboxItemDto
                    {
                        Value = p.Code,
                        Label = p.Name,
                        Status = 1,
                        Metadata = new Dictionary<string, object>
                        {
                            ["CountryCode"] = p.CountryCode ?? ""
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

                UniLogger.Error($"Error getting province combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItemDto>>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }

    #endregion
}
