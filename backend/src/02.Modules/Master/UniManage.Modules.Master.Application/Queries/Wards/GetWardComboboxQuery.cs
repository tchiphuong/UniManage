using MediatR;
using System.Text;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Master.Application.Queries.Wards
{
    #region Query

    public sealed class GetWardComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
    {
        public string? ProvinceCode { get; init; }
        public string? Keyword { get; set; }
    }

    #endregion

    #region Handler

    public sealed class GetWardComboboxQueryHandler : IRequestHandler<GetWardComboboxQuery, ApiResponse<List<ComboboxItem>>>
    {
        public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetWardComboboxQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Keyword), request.Keyword),
                    new LogParamModel(nameof(request.ProvinceCode), request.ProvinceCode)
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
                        FROM AdWards
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
                        cancellationToken);

                    var items = wards.Select(w => new ComboboxItem
                    {
                        Code = w.Code,
                        Name = w.Name,
                        Status = 1,
                        ExtData = new Dictionary<string, object>
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
                log.IsException = true;
                log.Message = ex.ToString();
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                UniLogger.Error($"Error getting ward combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItem>>(CoreResource.common_exceptionOccurred);
            }
        }
    }

    #endregion
}




