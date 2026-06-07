using Dapper;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.IdentityServer.Services
{
    public class IdentityUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? EmployeeCode { get; set; }
        public string? RoleCode { get; set; }
        public string? Email { get; set; }
        public string Status { get; set; } = default!;
    }

    public interface IIdentityUserRepository
    {
        Task<IdentityUserDto?> FindByUsernameAsync(string username);
        Task<IdentityUserDto?> FindByIdAsync(int userId);
        Task<bool> IsUserActiveAsync(int userId);
        Task<IdentityUserDto?> FindByExternalProviderAsync(string provider, string providerKey);
        Task LinkExternalLoginAsync(long userId, string provider, string providerKey, string? displayName);
    }

    public class IdentityUserRepository : IIdentityUserRepository
    {
        public async Task<IdentityUserDto?> FindByUsernameAsync(string username)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = @"
                    SELECT TOP 1
                        [Id], [UserName], [Password], [EmployeeCode], [RoleCode], [Email], [Status]
                    FROM [dbo].[SyUsers]
                    WHERE [UserName] = @UserName AND [Status] = @ActiveStatus";

                return await dbContext.QueryFirstOrDefaultAsync<IdentityUserDto>(
                    sql,
                    new { UserName = username, ActiveStatus = CoreCommon.Value.Commonstatus.Active });
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error finding user by username {username}", ex);
                return null;
            }
        }

        public async Task<IdentityUserDto?> FindByIdAsync(int userId)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = @"
                    SELECT TOP 1
                        [Id], [UserName], [Password], [EmployeeCode], [RoleCode], [Email], [Status]
                    FROM [dbo].[SyUsers]
                    WHERE [Id] = @UserId AND [Status] = @ActiveStatus";

                return await dbContext.QueryFirstOrDefaultAsync<IdentityUserDto>(
                    sql,
                    new { UserId = userId, ActiveStatus = CoreCommon.Value.Commonstatus.Active });
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error finding user by id {userId}", ex);
                return null;
            }
        }

        public async Task<bool> IsUserActiveAsync(int userId)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = @"
                    SELECT COUNT(*)
                    FROM [dbo].[SyUsers]
                    WHERE [Id] = @UserId AND [Status] = @ActiveStatus";

                var count = await dbContext.ExecuteScalarAsync<int>(
                    sql,
                    new { UserId = userId, ActiveStatus = CoreCommon.Value.Commonstatus.Active });

                return count > 0;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error checking if user {userId} is active", ex);
                return false;
            }
        }

        public async Task<IdentityUserDto?> FindByExternalProviderAsync(string provider, string providerKey)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = @"
                    SELECT u.[Id], u.[UserName], u.[Password], u.[EmployeeCode], u.[RoleCode], u.[Email], u.[Status]
                    FROM [dbo].[SyUsers] u
                    INNER JOIN [dbo].[SyUserLogins] l ON u.[Id] = l.[UserId]
                    WHERE l.[LoginProvider] = @Provider AND l.[ProviderKey] = @ProviderKey 
                    AND u.[Status] = @ActiveStatus";

                return await dbContext.QueryFirstOrDefaultAsync<IdentityUserDto>(
                    sql,
                    new { Provider = provider, ProviderKey = providerKey, ActiveStatus = CoreCommon.Value.Commonstatus.Active });
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error finding user by external provider {provider}:{providerKey}", ex);
                return null;
            }
        }

        public async Task LinkExternalLoginAsync(long userId, string provider, string providerKey, string? displayName)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = @"
                    IF NOT EXISTS (SELECT 1 FROM [dbo].[SyUserLogins] WHERE [LoginProvider] = @Provider AND [ProviderKey] = @ProviderKey)
                    BEGIN
                        INSERT INTO [dbo].[SyUserLogins] ([UserId], [LoginProvider], [ProviderKey], [ProviderDisplayName], [CreatedAt])
                        VALUES (@UserId, @Provider, @ProviderKey, @DisplayName, GETDATE())
                    END";

                await dbContext.ExecuteAsync(sql, new { UserId = userId, Provider = provider, ProviderKey = providerKey, DisplayName = displayName });
                await dbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error linking external login {provider} for user {userId}", ex);
                throw;
            }
        }
    }
}



