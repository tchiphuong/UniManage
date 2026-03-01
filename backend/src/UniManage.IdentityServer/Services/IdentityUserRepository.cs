using Dapper;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;

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
                    FROM [dbo].[sy_users]
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
                    FROM [dbo].[sy_users]
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
                    FROM [dbo].[sy_users]
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
    }
}
