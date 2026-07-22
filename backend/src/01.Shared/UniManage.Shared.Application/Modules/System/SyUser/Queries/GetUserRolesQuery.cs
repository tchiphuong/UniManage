using MediatR;
using UniManage.Shared.Domain;
using UniManage.Shared.Application.Interfaces;

namespace UniManage.Shared.Application.Modules.SyUser.Queries
{
    #region Query

    /// <summary>
    /// Query to get the role list of a specific user
    /// </summary>
    public sealed class GetUserRolesQuery : BaseQuery, IRequest<ApiResponse<List<GetUserRolesQuery.Response>>>, ILoggableCommand
    {
        /// <summary>
        /// Uuid of the user to get roles for
        /// </summary>
        public Guid Uuid { get; init; }

        /// <summary>
        /// Response DTO containing user role information
        /// </summary>
        public sealed record Response(
            string RoleCode,
            string RoleName,
            DateTime AssignedAt);
    }

    #endregion

    #region Handler

    /// <summary>
    /// Query handler to get user role list
    /// </summary>
    public sealed class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, ApiResponse<List<GetUserRolesQuery.Response>>>
    {
        public async Task<ApiResponse<List<GetUserRolesQuery.Response>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext())
            {
                // Step 1: Check if user exists in the system
                var userExists = await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM SyUsers WHERE Uuid = @Uuid) THEN 1 ELSE 0 END",
                    new { request.Uuid },
                    cancellationToken: cancellationToken);

                if (!userExists)
                {
                    var notFoundResponse = ResponseHelper.NotFound<List<GetUserRolesQuery.Response>>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                    return notFoundResponse;
                }

                // Determine role name column by language
                var suffix = TranslateHelper.GetLanguageSuffix(request.HeaderInfo?.Language);
                var roleNameColumn = $"r.Name{suffix}";

                // Step 2: Query role list using Dapper to optimize performance
                var roles = await dbContext.QueryAsync<GetUserRolesQuery.Response>(
                    $"""
                    SELECT ur.RoleCode, {roleNameColumn} AS RoleName, ur.CreatedAt AS AssignedAt
                    FROM SyUserRoles ur
                    INNER JOIN SyRoles r ON ur.RoleCode = r.Code
                    INNER JOIN SyUsers u ON ur.Username = u.Username
                    WHERE u.Uuid = @Uuid
                    ORDER BY ur.CreatedAt
                    """,
                    new { request.Uuid },
                    cancellationToken: cancellationToken);

                var result = roles.ToList();
                var response = ResponseHelper.Success(result, string.Format(CoreResource.common_listSuccess, CoreResource.entity_role));

                return response;
            }
        }
    }

    #endregion
}