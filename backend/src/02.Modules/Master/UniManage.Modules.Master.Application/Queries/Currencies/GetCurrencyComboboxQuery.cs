using MediatR;
using System.Text;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Master.Application.Queries.Currencies
{
    #region Query

    public sealed class GetCurrencyComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
    {
        public string? Keyword { get; set; }
    }

    #endregion

    #region Handler

    public sealed class GetCurrencyComboboxQueryHandler : IRequestHandler<GetCurrencyComboboxQuery, ApiResponse<List<ComboboxItem>>>
    {
        public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetCurrencyComboboxQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Keyword), request.Keyword)
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
                            Symbol
                        FROM MsCurrencies
                        WHERE IsActive = 1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine($"AND (Code LIKE @Keyword OR {nameColumn} LIKE @Keyword OR Symbol LIKE @Keyword)");
                    }

                    query.AppendLine($"ORDER BY {nameColumn}");

                    var currencies = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new { Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%" },
                        cancellationToken);

                    var items = currencies.Select(c => new ComboboxItem
                    {
                        Code = c.Code,
                        Name = c.Symbol != null ? $"{c.Name} ({c.Symbol})" : c.Name,
                        Status = 1,
                        ExtData = new Dictionary<string, object>
                        {
                            ["Symbol"] = c.Symbol ?? ""
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

                UniLogger.Error($"Error getting currency combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItem>>(CoreResource.common_exceptionOccurred);
            }
        }
    }

    #endregion
}




