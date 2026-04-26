using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Core.Services;
using UniManage.Model.Common;
using UniManage.Resource;
using SysClaim = System.Security.Claims.Claim;
using log4net;
using Microsoft.Identity.Web;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Bộ xác thực tài khoản người dùng (Username/Password) cho IdentityServer.
    /// Sử dụng AuthHelper để đảm bảo quy trình kiểm tra trạng thái tài khoản đồng nhất với toàn hệ thống.
    /// </summary>
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IIdentityUserRepository _userRepository;

        /// <summary>
        /// Khởi tạo Validator với Repository quản lý thông tin định danh
        /// </summary>
        public CustomResourceOwnerPasswordValidator(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Thực hiện quy trình xác thực thông tin đăng nhập
        /// </summary>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // Thiết lập thuộc tính 'api' cho log4net để phân loại file log
            ThreadContext.Properties["api"] = "IdentityServer-Login";

            try
            {
                // Khởi tạo log nghiệp vụ cho IdentityServer
                var headerInfo = new HeaderInfo 
                { 
                    ApiName = "IdentityServer-Login", 
                    Username = context.UserName,
                    CorrelationId = Guid.NewGuid().ToString()
                };
                var log = new CoreLogModel(headerInfo);

                // 1. Kiểm tra trạng thái tài khoản (Active/Locked/Disabled) thông qua AuthHelper
                var (statusSuccess, statusError, userSecurity) = await AuthHelper.ValidateUserStatusAsync(context.UserName, log);
                if (!statusSuccess)
                {
                    log.ReturnCode = 1;
                    log.Message = statusError;
                    UniLogManager.WriteApiLog(log);

                    UniLogger.WarnFormat("Đăng nhập bị chặn cho người dùng {0}: {1}", context.UserName, statusError);
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, statusError);
                    return;
                }

                // 2. Lấy thông tin chi tiết người dùng để kiểm tra mật khẩu
                var user = await _userRepository.FindByUsernameAsync(context.UserName);

                if (user == null || !PasswordHelper.VerifyPassword(context.Password, user.Password))
                {
                    UniLogger.WarnFormat("Thông tin đăng nhập không hợp lệ cho người dùng {0}", context.UserName);
                    
                    // Ghi nhận lịch sử đăng nhập thất bại
                    await AuthHelper.HandleLoginFailureAsync(context.UserName, log);

                    log.ReturnCode = 1;
                    log.Message = CoreResource.auth_invalidCredentials;
                    UniLogManager.WriteApiLog(log);

                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        CoreResource.auth_invalidCredentials);
                    return;
                }

                // 3. Xử lý khi xác thực thành công
                await AuthHelper.HandleLoginSuccessAsync(context.UserName, log);

                log.ReturnCode = 0;
                log.Message = "Xác thực người dùng thành công";
                log.Result = new { UserId = user.Id, context.UserName };
                UniLogManager.WriteApiLog(log);

                // 4. Thiết lập danh sách Claims để đưa vào Token
                var claims = new List<SysClaim>
                {
                    new SysClaim(CoreConstant.Claim.Subject, user.Id.ToString()),
                    new SysClaim(CoreConstant.Claim.Username, user.UserName),
                    new SysClaim(CoreConstant.Claim.EmployeeCode, user.EmployeeCode ?? ""),
                    new SysClaim(CoreConstant.Claim.Role, user.RoleCode ?? CoreConstant.DefaultRole)
                };

                if (!string.IsNullOrEmpty(user.Email))
                {
                    claims.Add(new SysClaim(CoreConstant.Claim.Email, user.Email));
                }

                UniLogger.InfoFormat("Người dùng {0} đã xác thực thành công qua IdentityServer", context.UserName);

                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "password",
                    claims: claims);
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Lỗi khi xác thực người dùng {context.UserName}", ex);
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    CoreResource.common_error);
            }
        }
    }
}
