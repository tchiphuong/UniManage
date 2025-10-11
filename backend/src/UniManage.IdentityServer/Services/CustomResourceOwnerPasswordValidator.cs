using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using SysClaim = System.Security.Claims.Claim;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Validate username/password từ bảng sy_users using DbContext from Core
    /// </summary>
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                using var dbContext = new DbContext();

                var sql = @"
                    SELECT TOP 1
                        [Id],
                        [UserName],
                        [Password],
                        [EmployeeCode],
                        [RoleCode],
                        [Email],
                        [Status]
                    FROM [dbo].[sy_users]
                    WHERE [UserName] = @UserName
                        AND [Status] = 1"; // Status = 1 = ACTIVE

                var user = await dbContext.connection.QueryFirstOrDefaultAsync<UserDto>(
                    sql,
                    new { UserName = context.UserName });

                if (user == null)
                {
                    UniLogger.WarnFormat("User {0} not found", context.UserName);
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        "Invalid username or password");
                    return;
                }

                // Verify password (base64 encoded for now)
                // TODO: Use proper password hashing (BCrypt, PBKDF2, etc.)
                var expectedPassword = Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes(context.Password));

                if (user.Password != expectedPassword)
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
                    new SysClaim("sub", user.Id.ToString()),
                    new SysClaim("username", user.UserName),
                    new SysClaim("employeeCode", user.EmployeeCode ?? ""),
                    new SysClaim("role", user.RoleCode ?? "User")
                };

                if (!string.IsNullOrEmpty(user.Email))
                {
                    claims.Add(new SysClaim("email", user.Email));
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

        private class UserDto
        {
            public int Id { get; set; }
            public string UserName { get; set; } = default!;
            public string Password { get; set; } = default!;
            public string? EmployeeCode { get; set; }
            public string? RoleCode { get; set; }
            public string? Email { get; set; }
            public int Status { get; set; }
        }
    }
}
