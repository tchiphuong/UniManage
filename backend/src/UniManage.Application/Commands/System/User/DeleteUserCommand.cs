using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.System.User;

/// <summary>
/// Command to delete (soft delete) users
/// </summary>
public sealed class DeleteUserCommand : BaseCommand, IRequest<ApiResponse<DeleteUserCommand.Response>>
{
    public List<long> Ids { get; init; } = new();

    public sealed class Response
    {
        public List<long> DeletedIds { get; init; } = new();
        public int Count { get; init; }
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
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Ids), JsonConvert.SerializeObject(request.Ids))
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                // Find users by Ids using EF Core
                var users = await dbContext.Set<sy_users>()
                    .Where(u => request.Ids.Contains(u.Id))
                    .ToListAsync(ct);

                if (users.Count == 0)
                {
                    await dbContext.RollbackAsync(ct);
                    return ResponseHelper.NotFound<DeleteUserCommand.Response>("No users found to delete");
                }

                // Soft delete: Set Status to "Inactive" (or use CoreCommon.Value.Commonstatus.Inactive if available)
                foreach (var user in users)
                {
                    user.Status = "Inactive"; // Soft delete
                    user.UpdatedBy = request.HeaderInfo?.Username ?? "SYSTEM";
                    user.UpdatedAt = DateTime.Now;
                }

                // Save changes
                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync(ct);

                var response = ResponseHelper.Success(
                    new DeleteUserCommand.Response 
                    { 
                        DeletedIds = users.Select(u => u.Id).ToList(),
                        Count = users.Count 
                    }, 
                    $"{users.Count} user(s) deleted successfully");

                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                log.Result = response;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync(ct);

                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                return ResponseHelper.Error<DeleteUserCommand.Response>(
                    $"Failed to delete users: {ex.Message}");
            }
        }
    }
}
