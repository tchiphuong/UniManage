using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using DbContext = UniManage.Core.Database.DbContext;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.User;

#region Command

/// <summary>
/// Command to update user information
/// </summary>
public sealed class UpdateUserCommand : BaseCommand, IRequest<ApiResponse<UpdateUserCommand.Response>>
{
    public long Id { get; set; }
    public string Email { get; init; } = default!;
    public string? EmployeeCode { get; init; }
    public string Status { get; init; } = default!;
    public byte[]? RowVersion { get; init; }

    // Note: DisplayName and PhoneNumber should be stored in hr_employees table if needed
    // They don't exist in sy_users table schema

    public sealed class Response
    {
        public long Id { get; init; }
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for UpdateUserCommand
/// </summary>
public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            .EmailAddress().WithMessage(CoreResource.Validation_msg_InvalidEmail)
            .MustAsync(async (cmd, email, cancel) => !await IsEmailExistsByOtherUserAsync(cmd.Id, email))
            .WithMessage(CoreResource.Validation_msg_EmailAlreadyRegisteredByOther);

        RuleFor(x => x.EmployeeCode)
            .MaximumLength(50).WithMessage("Employee code must not exceed 50 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(status => status == "Active" || status == "Inactive")
            .WithMessage("Status must be either 'Active' or 'Inactive'");
    }

    private static async Task<bool> IsEmailExistsByOtherUserAsync(long userId, string email)
    {
        using (var dbContext = new DbContext())
        {
            // Use EF Core LINQ instead of raw SQL
            return await dbContext.Set<sy_users>().AnyAsync(u => u.Email == email && u.Id != userId);
        }
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for UpdateUserCommand
/// </summary>
public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UpdateUserCommand.Response>>
{
    public async Task<ApiResponse<UpdateUserCommand.Response>> Handle(UpdateUserCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Id), request.Id),
                new CoreParamModel(nameof(request.Email), request.Email),
                new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                new CoreParamModel(nameof(request.Status), request.Status)
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                // Find user by Id with RowVersion for optimistic concurrency
                var user = await dbContext.Set<sy_users>()
                    .FirstOrDefaultAsync(u => u.Id == request.Id, ct);

                if (user == null)
                {
                    await dbContext.RollbackAsync(ct);
                    return ResponseHelper.NotFound<UpdateUserCommand.Response>("User not found");
                }

                // Check RowVersion for optimistic concurrency
                if (request.RowVersion != null && !user.RowVersion.SequenceEqual(request.RowVersion))
                {
                    await dbContext.RollbackAsync(ct);
                    return ResponseHelper.Error<UpdateUserCommand.Response>("User has been modified by another process");
                }

                // Update properties
                user.Email = request.Email;
                user.EmployeeCode = request.EmployeeCode;
                user.Status = request.Status;
                user.UpdatedBy = request.HeaderInfo?.Username ?? "SYSTEM";
                user.UpdatedAt = DateTime.Now;

                // Save changes - EF Core will handle RowVersion concurrency check
                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync(ct);

                var response = ResponseHelper.Success(new UpdateUserCommand.Response { Id = user.Id }, CoreResource.User_msg_UpdateSuccess);
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                log.Result = response;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                await dbContext.RollbackAsync(ct);
                log.IsException = 1;
                log.Message = "Concurrency conflict";
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);
                return ResponseHelper.Error<UpdateUserCommand.Response>("User has been modified by another process");
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync(ct);
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);
                return ResponseHelper.Error<UpdateUserCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }
}

#endregion