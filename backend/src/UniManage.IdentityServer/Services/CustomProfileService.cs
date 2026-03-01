using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using SysClaim = System.Security.Claims.Claim;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Cung cấp claims cho user trong tokens using DbContext from Core
    /// </summary>
    public class CustomProfileService : IProfileService
    {
        private readonly IIdentityUserRepository _userRepository;

        public CustomProfileService(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var userId = context.Subject.FindFirst(ClaimConstants.StandardClaims.Subject)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    UniLogger.Warn("No sub claim found in subject");
                    return;
                }

                var user = await _userRepository.FindByIdAsync(int.Parse(userId));

                if (user == null)
                {
                    UniLogger.WarnFormat("User {0} not found", userId);
                    return;
                }

                var claims = new List<SysClaim>
                {
                    new System.Security.Claims.Claim(ClaimConstants.StandardClaims.Subject, user.Id.ToString()),
                    new System.Security.Claims.Claim(ClaimConstants.StandardClaims.Name, user.UserName), // Add standard name claim
                    new System.Security.Claims.Claim(ClaimConstants.CustomClaims.Username, user.UserName),
                    new System.Security.Claims.Claim(ClaimConstants.CustomClaims.EmployeeCode, user.EmployeeCode ?? ""),
                    new System.Security.Claims.Claim(ClaimConstants.CustomClaims.Role, user.RoleCode ?? ApplicationConstants.Defaults.DefaultRole)
                };

                if (!string.IsNullOrEmpty(user.Email))
                {
                    claims.Add(new System.Security.Claims.Claim(ClaimConstants.StandardClaims.Email, user.Email));
                }

                // Only include requested claims
                var requestedClaims = context.RequestedClaimTypes;
                if (requestedClaims != null && requestedClaims.Any())
                {
                    claims = claims.Where(c => requestedClaims.Contains(c.Type)).ToList();
                }

                context.IssuedClaims = claims;
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error getting profile data", ex);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var userId = context.Subject.FindFirst(ClaimConstants.StandardClaims.Subject)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    context.IsActive = false;
                    return;
                }

                context.IsActive = await _userRepository.IsUserActiveAsync(int.Parse(userId));
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error checking if user is active", ex);
                context.IsActive = false;
            }
        }


    }
}
