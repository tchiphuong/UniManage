using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Queries.Master.Countries
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách các quốc gia dưới dạng Combobox (Code, Name)
    /// </summary>
    public sealed class GetCountryComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxModel>>>
    {
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách quốc gia cho Combobox
    /// </summary>
    public sealed class GetCountryComboboxQueryHandler : IRequestHandler<GetCountryComboboxQuery, ApiResponse<List<ComboboxModel>>>
    {
        public async Task<ApiResponse<List<ComboboxModel>>> Handle(GetCountryComboboxQuery request, CancellationToken ct)
        {
            // Khởi tạo log nghiệp vụ
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>()
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // Xác định tên cột đa ngôn ngữ an toàn (Zero Hardcode)
                    var propertyName = TranslateHelper.GetSafeLocalizedColumnName<ad_countries>(dbContext, CoreConstant.Column.Name, request.HeaderInfo?.Language);

                    // Truy vấn và sắp xếp động tại SQL Side bằng EF.Property
                    var query = dbContext.Set<ad_countries>()
                        .Where(c => c.IsActive)
                        .OrderBy(c => c.SortOrder)
                        .ThenBy(c => EF.Property<string>(c, propertyName));

                    // Lấy dữ liệu thô về bộ nhớ
                    var rawItems = await query.ToListAsync(ct);

                    // Chuyển đổi sang ComboboxModel (Memory Side) bằng Helper
                    var items = rawItems.Select(c => new ComboboxModel
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
                
                return ResponseHelper.Error<List<ComboboxModel>>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}