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
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, "Id"));

        /*
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            .EmailAddress().WithMessage(CoreResource.validation_invalidEmail)
            .MustAsync(async (cmd, email, cancel) => !await IsEmailExistsByOtherUserAsync(cmd.Id, email))
            .WithMessage(CoreResource.validation_emailAlreadyRegisteredByOther);
        */

        RuleFor(x => x.EmployeeCode)
            .MaximumLength(50)
            .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_employeeCode, 50));

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_status))
            .DependentRules(() =>
            {
                RuleFor(x => x.Status)
                    .Must(status => CoreCommon.Value.Commonstatus.All.Contains(status))
                    .WithMessage(CoreResource.validation_invalidStatus);
            });

        RuleFor(x => x.RowVersion)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, "RowVersion"));
    }

    // private static async Task<bool> IsEmailExistsByOtherUserAsync... Removed
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
                new CoreParamModel(nameof(request.Id), request.Id),
                // new CoreParamModel(nameof(request.Email), request.Email),
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
                    return ResponseHelper.NotFound<UpdateUserCommand.Response>(string.Format(CoreResource.crud_notFound, CoreResource.entity_user));
                }

                // Check RowVersion for optimistic concurrency
                if (request.RowVersion != null && !user.RowVersion.SequenceEqual(request.RowVersion))
                {
                    await dbContext.RollbackAsync(ct);
                    return ResponseHelper.Error<UpdateUserCommand.Response>(CoreResource.common_concurrencyError);
                }

                // Update properties
                // user.Email = request.Email; // Removed
                user.EmployeeCode = request.EmployeeCode;
                user.Status = request.Status;
                user.UpdatedBy = request.HeaderInfo?.Username ?? ApplicationConstants.Defaults.SystemUser;
                user.UpdatedAt = DateTime.Now;

                // Save changes - EF Core will handle RowVersion concurrency check
                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync(ct);

                var response = ResponseHelper.Success(new UpdateUserCommand.Response { Id = user.Id }, string.Format(CoreResource.crud_updateSuccess, CoreResource.entity_user));
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
                return ResponseHelper.Error<UpdateUserCommand.Response>(CoreResource.common_concurrencyError);
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync(ct);
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);
                return ResponseHelper.Error<UpdateUserCommand.Response>(CoreResource.common_exceptionOccurred);
            }
        }
    }
}

#endregion