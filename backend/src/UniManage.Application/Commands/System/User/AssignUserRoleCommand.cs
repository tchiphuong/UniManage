using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.User;

/// <summary>
/// Command to assign a role to a user
/// </summary>
public sealed class AssignUserRoleCommand : BaseCommand, IRequest<ApiResponse<AssignUserRoleCommand.Response>>
{
    public long UserId { get; init; }
    public string RoleCode { get; init; } = default!;

    // Internal - set by controller from token
    internal string AssignedBy { get; init; } = default!;

    public sealed class Response
    {
        public long UserId { get; init; }
        public string RoleCode { get; init; } = default!;
    }
}

/// <summary>
/// Validator for AssignUserRoleCommand
/// </summary>
public sealed class AssignUserRoleCommandValidator : AbstractValidator<AssignUserRoleCommand>
{
    public AssignUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0");

        RuleFor(x => x.RoleCode)
            .NotEmpty().WithMessage("Role code is required")
            .MaximumLength(50).WithMessage("Role code must not exceed 50 characters");
    }
}

/// <summary>
/// Handler for AssignUserRoleCommand
/// </summary>
public sealed class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, ApiResponse<AssignUserRoleCommand.Response>>
{
    public async Task<ApiResponse<AssignUserRoleCommand.Response>> Handle(AssignUserRoleCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
        {
            Parameter = new List<CoreParamModel>
            {
                new() { Name = nameof(request.UserId), Value = request.UserId.ToString() },
                new() { Name = nameof(request.RoleCode), Value = request.RoleCode }
            }
        };

        try
        {
            // Check if user exists and get username
            string username;
            using (var checkDb = new DbContext())
            {
                username = await checkDb.ExecuteScalarAsync<string>(
                    "SELECT Username FROM sy_users WHERE Id = @UserId",
                    new { request.UserId },
                    ct);

                if (string.IsNullOrEmpty(username))
                {
                    return ResponseHelper.NotFound<AssignUserRoleCommand.Response>(string.Format(CoreResource.crud_notFound, CoreResource.entity_user));
                }
            }

            // Check if role exists
            using (var checkDb = new DbContext())
            {
                var roleExists = await checkDb.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_roles WHERE Code = @RoleCode) THEN 1 ELSE 0 END",
                    new { request.RoleCode },
                    ct);

                if (!roleExists)
                {
                    return ResponseHelper.NotFound<AssignUserRoleCommand.Response>("Role not found");
                }
            }

            // Check if user already has this role
            using (var checkDb = new DbContext())
            {
                var roleAlreadyAssigned = await checkDb.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_user_roles WHERE Username = @Username AND RoleCode = @RoleCode) THEN 1 ELSE 0 END",
                    new { Username = username, request.RoleCode },
                    ct);

                if (roleAlreadyAssigned)
                {
                    return ResponseHelper.Error<AssignUserRoleCommand.Response>(
                        "User already has this role",
                        CoreApiReturnCode.InvalidData);
                }
            }

            using (var db = new DbContext(openTransaction: true))
            {
                try
                {
                    await db.ExecuteAsync(
                        """
                        INSERT INTO sy_user_roles (Username, RoleCode, CreatedBy, CreatedAt)
                        VALUES (@Username, @RoleCode, @AssignedBy, GETDATE())
                        """,
                        new { Username = username, request.RoleCode, request.AssignedBy },
                        ct);

                    await db.CommitAsync(ct);

                    var responseData = new AssignUserRoleCommand.Response { UserId = request.UserId, RoleCode = request.RoleCode };
                    var response = ResponseHelper.Success(responseData, "Role assigned successfully");
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogger.Info(JsonConvert.SerializeObject(log));

                    return response;
                }
                catch
                {
                    await db.RollbackAsync(ct);
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            log.IsException = 1;
            log.Message = ex.Message;
            log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
            UniLogger.Error(JsonConvert.SerializeObject(log));

            return ResponseHelper.Error<AssignUserRoleCommand.Response>(CoreResource.common_exceptionOccurred);
        }
    }
}
