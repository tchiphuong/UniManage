using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
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
            RuleFor(x => x.HeaderInfo.Username)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userIdentity));
        }
    }

    /// <summary>
    /// Get User Permissions Query Handler
    /// </summary>
    public sealed class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, ApiResponse<GetUserPermissionsQuery.Result>>
    {
        public async Task<ApiResponse<GetUserPermissionsQuery.Result>> Handle(GetUserPermissionsQuery request, CancellationToken ct)
        {
            var username = request.HeaderInfo.Username ?? string.Empty;
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.HeaderInfo.Username), username)
                }
            };

            try
            {
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
                        new { Username = username })).ToList();

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
                        new { Username = username })).ToList();

                    var result = new GetUserPermissionsQuery.Result
                    {
                        Permissions = permissions,
                        Roles = roles
                    };

                    var response = ResponseHelper.Success(result);
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = $"Retrieved {permissions.Count} permissions and {roles.Count} roles for user: {username}";
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<GetUserPermissionsQuery.Result>(CoreResource.common_exceptionOccurred);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }
}
