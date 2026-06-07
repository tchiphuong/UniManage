using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Queries.User
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách người dùng rút gọn cho các thành phần Combobox/Dropdown
    /// </summary>
    public sealed class GetUserComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem<long>>>>, ICacheable
    {
        /// <summary>
        /// Từ khóa tìm kiếm theo Username
        /// </summary>
        public string? Keyword { get; init; }

        public string CacheKey => CacheHelper.BuildKey(string.Format(CacheKeyConstant.System.ComboboxUsers, Keyword ?? "all"));
        public int? CacheTTLMinutes => CacheTimeConstant.Normal;
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách người dùng cho Combobox
    /// </summary>
    public sealed class GetUserComboboxQueryHandler : IRequestHandler<GetUserComboboxQuery, ApiResponse<List<ComboboxItem<long>>>>
    {
        /// <summary>
        /// Hàm xử lý lấy dữ liệu từ Database
        /// </summary>
        public async Task<ApiResponse<List<ComboboxItem<long>>>> Handle(GetUserComboboxQuery request, CancellationToken cancellationToken)
        {
            // Khởi tạo log API với thông tin header và tham số keyword
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Keyword), request.Keyword)
                }
            };

            try
            {
                using var dbContext = new DbContext();

                // Chỉ lấy những người dùng đang hoạt động (Active)
                var query = dbContext.Set<SyUsers>()
                    .Include(u => u.HrEmployees) // Eager load thông tin nhân viên để lấy FullName
                    .Where(u => u.Status == CoreCommon.Value.Commonstatus.Active);

                // Lọc theo từ khóa nếu có
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    var kw = request.Keyword.Trim();
                    query = query.Where(u => u.Username.Contains(kw));
                }

                // Chuyển đổi dữ liệu sang định dạng ComboboxItem chuẩn
                var users = await query
                    .OrderBy(u => u.Username)
                    .Take(100) // Giới hạn 100 bản ghi để đảm bảo hiệu năng UI
                    .Select(u => new ComboboxItem<long>
                    {
                        Code = u.Id,
                        // Hiển thị dạng "Username - FullName" nếu có thông tin nhân viên, ngược lại chỉ hiện Username
                        Name = u.HrEmployees != null ? $"{u.Username} - {u.HrEmployees.FullName}" : u.Username,
                        ExtData = u.Username
                    })
                    .ToListAsync(cancellationToken);

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







