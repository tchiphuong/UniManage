using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Auth
{
    /// <summary>
    /// Get User Permissions Query - Lấy danh sách permissions của user
    /// </summary>
    public sealed class GetUserPermissionsQuery : BaseQuery, IRequest<ApiResponse<GetUserPermissionsQuery.Result>>
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; } = string.Empty;

        public class Result
        {
            /// <summary>
            /// Danh sách permissions
            /// </summary>
            public List<string> Permissions { get; set; } = new();
            /// <summary>
            /// Danh sách roles
            /// </summary>
            public List<string> Roles { get; set; } = new();
        }
    }

    /// <summary>
    /// Get User Permissions Query Validator
    /// </summary>
    public sealed class GetUserPermissionsQueryValidator : AbstractValidator<GetUserPermissionsQuery>
    {
        public GetUserPermissionsQueryValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");
        }
    }

    /// <summary>
    /// Get User Permissions Query Handler
    /// </summary>
    public sealed class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, ApiResponse<GetUserPermissionsQuery.Result>>
    {
        public async Task<ApiResponse<GetUserPermissionsQuery.Result>> Handle(GetUserPermissionsQuery request, CancellationToken ct)
        {
            try
            {
                UniLogger.Info($"[GetUserPermissions] Getting permissions for user: {request.Username}");

                using (var dbContext = new DbContext())
                {
                    // Get user roles
                    var rolesSql = @"
                        SELECT DISTINCT r.[Code] AS RoleCode
                        FROM [dbo].[sy_users] u
                        INNER JOIN [dbo].[sy_user_roles] ur ON u.[Username] = ur.[Username]
                        INNER JOIN [dbo].[sy_roles] r ON ur.[RoleCode] = r.[Code]
                        WHERE u.[Username] = @Username";

                    var roles = (await dbContext.QueryAsync<string>(
                        rolesSql,
                        new { request.Username })).ToList();

                    // Get user permissions (FunctionCode + ActionCode)
                    var permissionsSql = @"
                        SELECT DISTINCT CONCAT(rp.[FunctionCode], '.', rp.[ActionCode]) AS PermissionCode
                        FROM [dbo].[sy_users] u
                        INNER JOIN [dbo].[sy_user_roles] ur ON u.[Username] = ur.[Username]
                        INNER JOIN [dbo].[sy_role_permissions] rp ON ur.[RoleCode] = rp.[RoleCode]
                        WHERE u.[Username] = @Username
                            AND rp.[FunctionCode] IS NOT NULL
                            AND rp.[ActionCode] IS NOT NULL";

                    var permissions = (await dbContext.QueryAsync<string>(
                        permissionsSql,
                        new { request.Username })).ToList();

                    var result = new GetUserPermissionsQuery.Result
                    {
                        Permissions = permissions,
                        Roles = roles
                    };

                    UniLogger.Info($"[GetUserPermissions] Retrieved {permissions.Count} permissions and {roles.Count} roles for user: {request.Username}");
                    return ResponseHelper.Success(result);
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"[GetUserPermissions] Error getting permissions for user: {request.Username}", ex);
                return ResponseHelper.Error<GetUserPermissionsQuery.Result>(CoreResource.common_exceptionOccurred);
            }
        }
    }
}
