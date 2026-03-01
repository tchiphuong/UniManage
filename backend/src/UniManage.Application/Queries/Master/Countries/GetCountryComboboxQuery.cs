using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Countries
{
    #region Query

    public sealed class GetCountryComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItemDto>>>
    {
    }

    #endregion

    #region Handler

    public sealed class GetCountryComboboxQueryHandler : IRequestHandler<GetCountryComboboxQuery, ApiResponse<List<ComboboxItemDto>>>
    {
        public async Task<ApiResponse<List<ComboboxItemDto>>> Handle(GetCountryComboboxQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<CoreParamModel>()
            };

            try
            {
                using (var db = new DbContext())
                {
                    var isEnglish = request.HeaderInfo?.Language?.StartsWith("en", StringComparison.OrdinalIgnoreCase) == true;

                    var countries = await db.QueryAsync<dynamic>(
                        $"""
                    SELECT 
                        Code, 
                        {(isEnglish ? "NameEn" : "NameVi")} AS Name,
                        PhoneCode
                    FROM ad_countries
                    WHERE IsActive = 1
                    ORDER BY SortOrder, {(isEnglish ? "NameEn" : "NameVi")}
                    """,
                        cancellationToken: ct);

                    var items = countries.Select(c => new ComboboxItemDto
                    {
                        Value = c.Code,
                        Label = c.Name,
                        Status = 1,
                        Metadata = new Dictionary<string, object>
                        {
                            ["PhoneCode"] = c.PhoneCode ?? ""
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

                UniLogger.Error($"Error getting country combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItemDto>>(CoreResource.common_exceptionOccurred);
            }
        }

    }

    #endregion
}