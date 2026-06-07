using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using log4net;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using SysClaim = System.Security.Claims.Claim;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Dịch vụ cung cấp thông tin người dùng (Claims) và kiểm tra trạng thái hoạt động cho IdentityServer.
    /// Tích hợp trực tiếp với IIdentityUserRepository để lấy dữ liệu từ SQL Server.
    /// </summary>
    public class CustomProfileService : IProfileService
    {
        private readonly IIdentityUserRepository _userRepository;

        /// <summary>
        /// Khởi tạo Profile Service với Repository quản lý người dùng
        /// </summary>
        public CustomProfileService(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Lấy danh sách Claims bổ sung cho Access Token và ID Token
        /// </summary>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // Thiết lập thuộc tính 'api' cho log4net
            ThreadContext.Properties["api"] = "IdentityServer-Profile";

            try
            {
                var userId = context.Subject.FindFirst(CoreConstant.Claim.Subject)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    UniLogger.Warn("Không tìm thấy claim 'sub' trong subject");
                    return;
                }

                // Truy vấn thông tin chi tiết người dùng
                var user = await _userRepository.FindByIdAsync(int.Parse(userId));

                if (user == null)
                {
                    UniLogger.WarnFormat("Không tìm thấy người dùng có ID: {0}", userId);
                    return;
                }

                // Thiết lập các Claims tiêu chuẩn và tùy chỉnh của UniManage
                var claims = new List<SysClaim>
                {
                    new SysClaim(CoreConstant.Claim.Subject, user.Id.ToString()),
                    new SysClaim(CoreConstant.Claim.Name, user.UserName),
                    new SysClaim(CoreConstant.Claim.Username, user.UserName),
                    new SysClaim(CoreConstant.Claim.EmployeeCode, user.EmployeeCode ?? ""),
                    new SysClaim(CoreConstant.Claim.Role, user.RoleCode ?? CoreConstant.DefaultRole)
                };

                if (!string.IsNullOrEmpty(user.Email))
                {
                    claims.Add(new SysClaim(CoreConstant.Claim.Email, user.Email));
                }

                // Chỉ trả về các Claims mà Client yêu cầu thông qua Scopes
                var requestedClaims = context.RequestedClaimTypes;
                if (requestedClaims != null && requestedClaims.Any())
                {
                    claims = claims.Where(c => requestedClaims.Contains(c.Type)).ToList();
                }

                context.IssuedClaims = claims;
            }
            catch (Exception ex)
            {
                UniLogger.Error("Lỗi khi lấy dữ liệu hồ sơ người dùng", ex);
            }
        }

        /// <summary>
        /// Kiểm tra tài khoản người dùng có còn ở trạng thái hoạt động (Active) hay không
        /// </summary>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var userId = context.Subject.FindFirst(CoreConstant.Claim.Subject)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    context.IsActive = false;
                    return;
                }

                // Trả về true nếu người dùng đang ở trạng thái Active
                context.IsActive = await _userRepository.IsUserActiveAsync(int.Parse(userId));
            }
            catch (Exception ex)
            {
                UniLogger.Error("Lỗi khi kiểm tra trạng thái hoạt động của người dùng", ex);
                context.IsActive = false;
            }
        }
    }
}


