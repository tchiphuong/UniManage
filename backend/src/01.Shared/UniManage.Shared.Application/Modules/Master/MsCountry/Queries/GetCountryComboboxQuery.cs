using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.MsCountry.Queries
{
    #region Query

    /// <summary>
    /// Truy v?n l?y danh s�ch c�c qu?c gia du?i d?ng Combobox (Code, Name)
    /// </summary>
    public sealed class GetCountryComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
    {
    }

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l� truy v?n l?y danh s�ch qu?c gia cho Combobox
    /// </summary>
    public sealed class GetCountryComboboxQueryHandler : IRequestHandler<GetCountryComboboxQuery, ApiResponse<List<ComboboxItem>>>
    {
        public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetCountryComboboxQuery request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log nghi?p v?
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>()
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // X�c d?nh t�n c?t da ng�n ng? an to�n (Zero Hardcode)
                    var propertyName = TranslateHelper.GetSafeLocalizedColumnName<AdCountries>(dbContext, CoreConstant.Column.Name, request.HeaderInfo?.Language);

                    // Truy v?n v� s?p x?p d?ng t?i SQL Side b?ng EF.Property
                    var query = dbContext.Set<AdCountries>()
                        .Where(c => c.IsActive)
                        .OrderBy(c => c.SortOrder)
                        .ThenBy(c => EF.Property<string>(c, propertyName));

                    // L?y d? li?u th� v? b? nh?
                    var rawItems = await query.ToListAsync(cancellationToken);

                    // Chuy?n d?i sang ComboboxItem (Memory Side) b?ng Helper
                    var items = rawItems.Select(c => new ComboboxItem
                    {
                        Code = c.Code,
                        Name = TranslateHelper.GetLocalizedValue(c, "Name", request.HeaderInfo?.Language),
                        Status = 1,
                        ExtData = new Dictionary<string, object>
                        {
                            ["PhoneCode"] = c.PhoneCode ?? string.Empty
                        }
                    }).ToList();

                    var response = ResponseHelper.Success(items, string.Format(CoreResource.common_listSuccess, CoreResource.entity_country));
                    
                    log.Result = new { Count = items.Count };
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;

                    return response;
                }
            }
            catch (Exception ex)
            {
                // Ghi nh?n l?i h? th?ng
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<List<ComboboxItem>>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




