using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using UniManage.Core.Database;
using UniManage.Core.Logging;

namespace UniManage.Api.Authorization
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
    ///   sy_users → sy_user_roles → sy_role_permissions (FunctionCode + ActionCode)
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IMemoryCache _cache;

        // Cache duration for user permissions
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

        public PermissionAuthorizationHandler(IMemoryCache cache)
        {
            _cache = cache;
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
            var cacheKey = $"permissions:{username}";

            if (_cache.TryGetValue(cacheKey, out HashSet<string>? cachedPermissions) && cachedPermissions != null)
            {
                return cachedPermissions;
            }

            // Query permissions from database
            using (var dbContext = new DbContext())
            {
                var sql = @"
                    SELECT DISTINCT CONCAT(rp.[FunctionCode], '.', rp.[ActionCode]) AS PermissionCode
                    FROM [dbo].[sy_users] u
                    INNER JOIN [dbo].[sy_user_roles] ur ON u.[Username] = ur.[Username]
                    INNER JOIN [dbo].[sy_role_permissions] rp ON ur.[RoleCode] = rp.[RoleCode]
                    WHERE u.[Username] = @Username
                        AND rp.[FunctionCode] IS NOT NULL
                        AND rp.[ActionCode] IS NOT NULL";

                var permissions = (await dbContext.QueryAsync<string>(
                    sql, new { Username = username })).ToList();

                var permissionSet = new HashSet<string>(permissions, StringComparer.OrdinalIgnoreCase);

                // Cache for 5 minutes
                _cache.Set(cacheKey, permissionSet, CacheDuration);

                return permissionSet;
            }
        }
    }
}
