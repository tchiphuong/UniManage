using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.Master.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.Master.Application.Queries.Countries
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách các quốc gia dưới dạng Combobox (Code, Name)
    /// </summary>
    public sealed class GetCountryComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
    {
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách quốc gia cho Combobox
    /// </summary>
    public sealed class GetCountryComboboxQueryHandler : IRequestHandler<GetCountryComboboxQuery, ApiResponse<List<ComboboxItem>>>
    {
        public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetCountryComboboxQuery request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>()
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // Xác định tên cột đa ngôn ngữ an toàn (Zero Hardcode)
                    var propertyName = TranslateHelper.GetSafeLocalizedColumnName<AdCountries>(dbContext, CoreConstant.Column.Name, request.HeaderInfo?.Language);

                    // Truy vấn và sắp xếp động tại SQL Side bằng EF.Property
                    var query = dbContext.Set<AdCountries>()
                        .Where(c => c.IsActive)
                        .OrderBy(c => c.SortOrder)
                        .ThenBy(c => EF.Property<string>(c, propertyName));

                    // Lấy dữ liệu thô về bộ nhớ
                    var rawItems = await query.ToListAsync(cancellationToken);

                    // Chuyển đổi sang ComboboxItem (Memory Side) bằng Helper
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
                // Ghi nhận lỗi hệ thống
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




