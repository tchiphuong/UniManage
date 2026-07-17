using UniManage.Shared.Infrastructure.Database;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Interfaces;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;

namespace UniManage.WebApi.Authorization
{
    /// <summary>
    /// Authorization handler that checks user permissions from database.
    /// 
    /// Flow:
    ///   1. Extract username from JWT claims
    ///   2. Load user permissions from DB (cached 5 minutes per user)
    ///   3. Check if required FunctionCode.ActionCode exists in user's permissions
    ///   4. Succeed or fail the requirement
    /// 
    /// Database tables involved:
    ///   SyUsers → SyUserRoles → SyRolePermissions (FunctionCode + ActionCode)
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        // Cache duration for user permissions
        private static readonly int CacheDurationMinutes = CacheTimeConstant.Short;

        public PermissionAuthorizationHandler()
        {
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            // 1. Extract username from JWT claims
            var username = context.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                // Not authenticated — let [Authorize] handle this
                return;
            }

            try
            {
                // 2. Load permissions from DB (with cache)
                var permissions = await GetUserPermissionsAsync(username);

                // 3. Check if user has the required permission
                var requiredPermission = requirement.PermissionCode;

                if (permissions.Contains(requiredPermission))
                {
                    context.Succeed(requirement);
                }
                // else: requirement fails silently → returns 403 Forbidden
            }
            catch (Exception ex)
            {
                UniLogManager.WriteApiLog(
                    "Authorization",
                    $"Error checking permission {requirement.PermissionCode} for user {username}",
                    null,
                    true,
                    ex);
                // On error, do NOT succeed — deny access (fail-closed principle)
            }
        }

        /// <summary>
        /// Load user permissions from database with in-memory caching.
        /// Cache key: "permissions:{username}"
        /// Cache duration: 5 minutes
        /// 
        /// To invalidate cache (e.g. after role change), clear the key:
        ///   _cache.Remove($"permissions:{username}");
        /// </summary>
        private async Task<HashSet<string>> GetUserPermissionsAsync(string username)
        {
            var cacheKey = string.Format(CacheKeyConstant.System.Permissions, username);

            var permissionSet = await CacheHelper.GetOrSetAsync(cacheKey, async () =>
            {
                // Query permissions from database
                using (var dbContext = new DbContext())
                {
                    var sql = @"
                        SELECT DISTINCT CONCAT(rp.[FunctionCode], '.', rp.[ActionCode]) AS PermissionCode
                        FROM [dbo].[SyUsers] u
                        INNER JOIN [dbo].[SyUserRoles] ur ON u.[Username] = ur.[Username]
                        INNER JOIN [dbo].[SyRolePermissions] rp ON ur.[RoleCode] = rp.[RoleCode]
                        WHERE u.[Username] = @Username
                            AND rp.[FunctionCode] IS NOT NULL
                            AND rp.[ActionCode] IS NOT NULL";

                    var permissions = (await dbContext.QueryAsync<string>(
                        sql, new { Username = username })).ToList();

                    return new HashSet<string>(permissions, StringComparer.OrdinalIgnoreCase);
                }
            }, CacheDurationMinutes);

            return permissionSet ?? new HashSet<string>();
        }
    }
}

