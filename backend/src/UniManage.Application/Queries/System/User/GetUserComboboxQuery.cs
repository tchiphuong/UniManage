using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Core.Constant;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Queries.System.User
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách người dùng rút gọn cho các thành phần Combobox/Dropdown
    /// </summary>
    public sealed class GetUserComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxModel<long>>>>
    {
        /// <summary>
        /// Từ khóa tìm kiếm theo Username
        /// </summary>
        public string? Keyword { get; init; }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách người dùng cho Combobox
    /// </summary>
    public sealed class GetUserComboboxQueryHandler : IRequestHandler<GetUserComboboxQuery, ApiResponse<List<ComboboxModel<long>>>>
    {
        /// <summary>
        /// Hàm xử lý lấy dữ liệu từ Database
        /// </summary>
        public async Task<ApiResponse<List<ComboboxModel<long>>>> Handle(GetUserComboboxQuery request, CancellationToken ct)
        {
            // Khởi tạo log API với thông tin header và tham số keyword
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.Keyword), request.Keyword)
                }
            };

            try
            {
                using var dbContext = new DbContext();

                // Chỉ lấy những người dùng đang hoạt động (Active)
                var query = dbContext.Set<sy_users>()
                    .Include(u => u.hr_employees) // Eager load thông tin nhân viên để lấy FullName
                    .Where(u => u.Status == CoreCommon.Value.Commonstatus.Active);

                // Lọc theo từ khóa nếu có
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    var kw = request.Keyword.Trim();
                    query = query.Where(u => u.Username.Contains(kw));
                }

                // Chuyển đổi dữ liệu sang định dạng ComboboxModel chuẩn
                var users = await query
                    .OrderBy(u => u.Username)
                    .Take(100) // Giới hạn 100 bản ghi để đảm bảo hiệu năng UI
                    .Select(u => new ComboboxModel<long>
                    {
                        Code = u.Id,
                        // Hiển thị dạng "Username - FullName" nếu có thông tin nhân viên, ngược lại chỉ hiện Username
                        Name = u.hr_employees != null ? $"{u.Username} - {u.hr_employees.FullName}" : u.Username,
                        ExtData = u.Username
                    })
                    .ToListAsync(ct);

                var response = ResponseHelper.Success(users);
                
                log.Result = new { Count = users.Count };
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;

                return response;
            }
            catch (Exception ex)
            {
                // Ghi nhận ngoại lệ vào log
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                throw;
            }
            finally
            {
                // Ghi log tập trung
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
