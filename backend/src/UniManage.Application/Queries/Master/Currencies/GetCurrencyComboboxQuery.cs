using MediatR;
using System.Text;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Currencies
{
    #region Query

    public sealed class GetCurrencyComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxModel>>>
    {
        public string? Keyword { get; set; }
    }

    #endregion

    #region Handler

    public sealed class GetCurrencyComboboxQueryHandler : IRequestHandler<GetCurrencyComboboxQuery, ApiResponse<List<ComboboxModel>>>
    {
        public async Task<ApiResponse<List<ComboboxModel>>> Handle(GetCurrencyComboboxQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword)
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
                        FROM ms_currencies
                        WHERE IsActive = 1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine($"AND (Code LIKE @Keyword OR {nameColumn} LIKE @Keyword OR Symbol LIKE @Keyword)");
                    }

                    query.AppendLine($"ORDER BY {nameColumn}");

                    var currencies = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new { Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%" },
                        ct);

                    var items = currencies.Select(c => new ComboboxModel
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
                return ResponseHelper.Error<List<ComboboxModel>>(CoreResource.common_exceptionOccurred);
            }
        }
    }

    #endregion
}
