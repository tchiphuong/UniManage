using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Commands.System.User;

/// <summary>
/// Command to remove a role from a user
/// </summary>
public sealed class RemoveUserRoleCommand : BaseCommand, IRequest<ApiResponse<RemoveUserRoleCommand.Response>>
{
    public long UserId { get; init; }
    public string RoleCode { get; init; } = default!;

    // Internal - set by controller from token
    internal string RemovedBy { get; init; } = default!;

    public sealed class Response
    {
        public long UserId { get; init; }
        public string RoleCode { get; init; } = default!;
    }
}

/// <summary>
/// Validator for RemoveUserRoleCommand
/// </summary>
public sealed class RemoveUserRoleCommandValidator : AbstractValidator<RemoveUserRoleCommand>
{
    public RemoveUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0");

        RuleFor(x => x.RoleCode)
            .NotEmpty().WithMessage("Role code is required")
            .MaximumLength(50).WithMessage("Role code must not exceed 50 characters");
    }
}

/// <summary>
/// Handler for RemoveUserRoleCommand
/// </summary>
public sealed class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, ApiResponse<RemoveUserRoleCommand.Response>>
{
    public async Task<ApiResponse<RemoveUserRoleCommand.Response>> Handle(RemoveUserRoleCommand request, CancellationToken ct)
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
                    return ResponseHelper.NotFound<RemoveUserRoleCommand.Response>("User not found");
                }
            }

            // Check if user has this role
            using (var checkDb = new DbContext())
            {
                var hasRole = await checkDb.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_user_roles WHERE Username = @Username AND RoleCode = @RoleCode) THEN 1 ELSE 0 END",
                    new { Username = username, request.RoleCode },
                    ct);

                if (!hasRole)
                {
                    return ResponseHelper.NotFound<RemoveUserRoleCommand.Response>("User does not have this role");
                }
            }

            using (var db = new DbContext(openTransaction: true))
            {
                try
                {
                    var rowsAffected = await db.ExecuteAsync(
                        """
                        DELETE FROM sy_user_roles
                        WHERE Username = @Username AND RoleCode = @RoleCode
                        """,
                        new { Username = username, request.RoleCode },
                        ct);

                    if (rowsAffected == 0)
                    {
                        await db.RollbackAsync(ct);
                        return ResponseHelper.NotFound<RemoveUserRoleCommand.Response>("User role not found");
                    }

                    await db.CommitAsync(ct);

                    var responseData = new RemoveUserRoleCommand.Response { UserId = request.UserId, RoleCode = request.RoleCode };
                    var response = ResponseHelper.Success(responseData, "Role removed successfully");
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
            log.ReturnCode = 500;
            UniLogger.Error(JsonConvert.SerializeObject(log));

            return ResponseHelper.Error<RemoveUserRoleCommand.Response>($"Failed to remove role: {ex.Message}");
        }
    }
}
