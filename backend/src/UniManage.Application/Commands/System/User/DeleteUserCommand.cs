using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Commands.System.User;

/// <summary>
/// Command to delete (soft delete) users
/// </summary>
public sealed class DeleteUserCommand : BaseCommand, IRequest<ApiResponse<DeleteUserCommand.Response>>
{
    public List<long> Ids { get; init; } = new();

    // Internal - set by controller from token
    internal string DeletedBy { get; init; } = default!;

    public sealed class Response
    {
        public List<long> Ids { get; init; } = new();
    }
}

/// <summary>
/// Validator for DeleteUserCommand
/// </summary>
public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Ids)
            .NotEmpty().WithMessage("At least one Id is required")
            .Must(ids => ids.All(id => id > 0)).WithMessage("All Ids must be greater than 0");
    }
}

/// <summary>
/// Handler for DeleteUserCommand
/// </summary>
public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<DeleteUserCommand.Response>>
{
    public async Task<ApiResponse<DeleteUserCommand.Response>> Handle(DeleteUserCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
        {
            Parameter = new List<CoreParamModel>
            {
                new() { Name = nameof(request.Ids), Value = JsonConvert.SerializeObject(request.Ids) },
                new() { Name = nameof(request.DeletedBy), Value = request.DeletedBy }
            }
        };

        using (var db = new DbContext(openTransaction: true))
        {
            try
            {
                // Soft delete by setting Status = 0
                // Dapper handles List<long> for IN clause automatically
                var rowsAffected = await db.ExecuteAsync(
                    """
                    UPDATE sy_users
                    SET Status = 0,
                        UpdatedBy = @DeletedBy,
                        UpdatedAt = GETDATE()
                    WHERE Id IN @Ids
                    """,
                    new
                    {
                        Ids = request.Ids,
                        request.DeletedBy
                    },
                    ct);

                if (rowsAffected == 0)
                {
                    await db.RollbackAsync(ct);
                    return ResponseHelper.NotFound<DeleteUserCommand.Response>("No users found to delete");
                }

                await db.CommitAsync(ct);

                var response = ResponseHelper.Success(new DeleteUserCommand.Response { Ids = request.Ids }, $"{rowsAffected} user(s) deleted successfully");
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogger.Info(JsonConvert.SerializeObject(log));

                return response;
            }
            catch (Exception ex)
            {
                await db.RollbackAsync(ct);

                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = 500;
                UniLogger.Error(JsonConvert.SerializeObject(log));

                return ResponseHelper.Error<DeleteUserCommand.Response>(
                    $"Failed to delete users: {ex.Message}");
            }
        }
    }
}
