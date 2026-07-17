using MediatR;
using System.Text;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.MsProvince.Queries
{
    #region Query

    public sealed class GetProvinceComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
    {
        public string? CountryCode { get; init; }
        public string? Keyword { get; set; }
    }

    #endregion

    #region Handler

    public sealed class GetProvinceComboboxQueryHandler : IRequestHandler<GetProvinceComboboxQuery, ApiResponse<List<ComboboxItem>>>
    {
        public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetProvinceComboboxQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Keyword), request.Keyword),
                    new LogParamModel(nameof(request.CountryCode), request.CountryCode)
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
                        FROM AdProvinces
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
                        cancellationToken);

                    var items = provinces.Select(p => new ComboboxItem
                    {
                        Code = p.Code,
                        Name = p.Name,
                        Status = 1,
                        ExtData = new Dictionary<string, object>
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
                log.IsException = true;
                log.Message = ex.ToString();
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                UniLogger.Error($"Error getting province combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItem>>(CoreResource.common_exceptionOccurred);
            }
        }
    }

    #endregion
}




