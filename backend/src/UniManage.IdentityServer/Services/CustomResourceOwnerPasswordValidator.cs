using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using SysClaim = System.Security.Claims.Claim;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Validate username/password từ bảng sy_users using DbContext from Core
    /// </summary>
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IIdentityUserRepository _userRepository;

        public CustomResourceOwnerPasswordValidator(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _userRepository.FindByUsernameAsync(context.UserName);

                if (user == null)
                {
                    UniLogger.WarnFormat("User {0} not found", context.UserName);
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        "Invalid username or password");
                    return;
                }

                // Verify password using PasswordHelper (BCrypt)
                if (!PasswordHelper.VerifyPassword(context.Password, user.Password))
                {
                    UniLogger.WarnFormat("Invalid password for user {0}", context.UserName);
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        "Invalid username or password");
                    return;
                }

                // Create claims
                var claims = new List<SysClaim>
                {
                    new System.Security.Claims.Claim(ClaimConstants.StandardClaims.Subject, user.Id.ToString()),
                    new System.Security.Claims.Claim(ClaimConstants.CustomClaims.Username, user.UserName),
                    new System.Security.Claims.Claim(ClaimConstants.CustomClaims.EmployeeCode, user.EmployeeCode ?? ""),
                    new System.Security.Claims.Claim(ClaimConstants.CustomClaims.Role, user.RoleCode ?? ApplicationConstants.Defaults.DefaultRole)
                };

                if (!string.IsNullOrEmpty(user.Email))
                {
                    claims.Add(new System.Security.Claims.Claim(ClaimConstants.StandardClaims.Email, user.Email));
                }

                UniLogger.InfoFormat("User {0} authenticated successfully", context.UserName);

                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "password",
                    claims: claims);
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error validating user {context.UserName}", ex);
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    "Authentication failed");
            }
        }
    }
}
