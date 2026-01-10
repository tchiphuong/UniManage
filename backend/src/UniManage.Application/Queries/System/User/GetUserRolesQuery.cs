using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Queries.System.User;

/// <summary>
/// Query to get user's roles
/// </summary>
public sealed class GetUserRolesQuery : BaseQuery, IRequest<ApiResponse<List<UserRoleDto>>>
{
    public long Id { get; init; }
}

/// <summary>
/// DTO for user role
/// </summary>
public sealed record UserRoleDto(
    string RoleCode,
    string RoleName,
    DateTime AssignedAt);

/// <summary>
/// Validator for GetUserRolesQuery
/// </summary>
public sealed class GetUserRolesQueryValidator : AbstractValidator<GetUserRolesQuery>
{
    public GetUserRolesQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}

/// <summary>
/// Handler for GetUserRolesQuery
/// </summary>
public sealed class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, ApiResponse<List<UserRoleDto>>>
{
    public async Task<ApiResponse<List<UserRoleDto>>> Handle(GetUserRolesQuery request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
        {
            Parameter = new List<CoreParamModel>
            {
                new() { Name = nameof(request.Id), Value = request.Id.ToString() }
            }
        };

        try
        {
            // Check if user exists
            using (var checkDb = new DbContext())
            {
                var userExists = await checkDb.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Id = @Id) THEN 1 ELSE 0 END",
                    new { request.Id },
                    ct);

                if (!userExists)
                {
                    return ResponseHelper.NotFound<List<UserRoleDto>>("User not found");
                }
            }

            using (var db = new DbContext())
            {
                var roles = await db.QueryAsync<UserRoleDto>(
                    """
                    SELECT ur.RoleCode, r.Name AS RoleName, ur.CreatedAt AS AssignedAt
                    FROM sy_user_roles ur
                    INNER JOIN sy_roles r ON ur.RoleCode = r.Code
                    INNER JOIN sy_users u ON ur.Username = u.Username
                    WHERE u.Id = @Id
                    ORDER BY ur.CreatedAt
                    """,
                    new { request.Id },
                    cancellationToken: ct);

                var response = ResponseHelper.Success(roles.ToList());
                log.Result = new { RoleCount = roles.Count() };
                log.ReturnCode = response.ReturnCode;
                UniLogger.Info(JsonConvert.SerializeObject(log));

                return response;
            }
        }
        catch (Exception ex)
        {
            log.IsException = 1;
            log.Message = ex.Message;
            log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
            UniLogger.Error(JsonConvert.SerializeObject(log));

            return ResponseHelper.Error<List<UserRoleDto>>($"Failed to get user roles: {ex.Message}");
        }
    }
}
