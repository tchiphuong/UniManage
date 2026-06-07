using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManage.IdentityServer.Services;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.IdentityServer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IIdentityUserRepository _userRepository;

        public AccountController(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(ExternalLoginCallback)),
                Items =
                {
                    { "returnUrl", returnUrl },
                    { "scheme", provider },
                }
            };

            return Challenge(props, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync(Duende.IdentityServer.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            var externalUser = result.Principal;
            if (externalUser == null)
            {
                throw new Exception("External authentication error");
            }

            var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                              externalUser.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            var provider = result.Properties.Items["scheme"] ?? throw new Exception("Unknown provider");
            var providerUserId = userIdClaim.Value;
            var email = externalUser.FindFirst(JwtClaimTypes.Email)?.Value ?? 
                        externalUser.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var displayName = externalUser.FindFirst(JwtClaimTypes.Name)?.Value ?? 
                              externalUser.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            // 1. Tìm người dùng đã liên kết
            var user = await _userRepository.FindByExternalProviderAsync(provider, providerUserId);

            if (user == null && !string.IsNullOrEmpty(email))
            {
                // 2. Nếu chưa liên kết, thử tìm theo Email để liên kết tự động
                // Cần thêm FindByEmail vào Repository
                using var dbContext = new DbContext();
                user = await dbContext.QueryFirstOrDefaultAsync<IdentityUserDto>(
                    "SELECT [Id], [UserName], [Password], [EmployeeCode], [RoleCode], [Email], [Status] FROM [dbo].[SyUsers] WHERE [Email] = @Email AND [Status] = @Status",
                    new { Email = email, Status = CoreCommon.Value.Commonstatus.Active });

                if (user != null)
                {
                    await _userRepository.LinkExternalLoginAsync(user.Id, provider, providerUserId, displayName);
                }
            }

            if (user == null)
            {
                // 3. Nếu vẫn không thấy, có thể tạo mới hoặc yêu cầu đăng ký (tùy policy)
                // Ở đây ta tạm thời redirect về trang lỗi hoặc yêu cầu liên kết thủ công
                // UniLogger.Warn($"External user {email} not found and no auto-registration implemented");
                // return RedirectToAction("Login", "Account", new { error = "User not found" });
                
                // Demo: Tạo user thô (trong thực tế nên có bước xác nhận)
                // TODO: Triển khai luồng tạo user từ External Provider
            }
            
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            // Dọn dẹp cookie ngoại vi
            await HttpContext.SignOutAsync(Duende.IdentityServer.IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // Đăng nhập người dùng vào IdentityServer (Social flow thường dùng External Cookie)
            // IdentityServer sẽ handle việc phát hành token sau khi redirect về client
            return Redirect(returnUrl);
        }
    }
}



