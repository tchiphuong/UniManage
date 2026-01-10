using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.User;

#region Command

/// <summary>
/// Command to update user information
/// </summary>
public sealed class UpdateUserCommand : BaseCommand, IRequest<ApiResponse<UpdateUserCommand.Response>>
{
    public long Id { get; set; }
    public string DisplayName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string? PhoneNumber { get; init; }
    public string? EmployeeCode { get; init; }
    public string Status { get; init; } = default!;
    public byte[]? DataRowVersion { get; init; }

    // Internal - set by controller from token
    internal string UpdatedBy { get; init; } = default!;

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

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Display name is required")
            .MaximumLength(200).WithMessage("Display name must not exceed 200 characters");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            .EmailAddress().WithMessage(CoreResource.Validation_msg_InvalidEmail)
            .MustAsync(async (cmd, email, cancel) => !await IsEmailExistsByOtherUserAsync(cmd.Id, email))
            .WithMessage(CoreResource.Validation_msg_EmailAlreadyRegisteredByOther);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters")
            .Must(phone => string.IsNullOrEmpty(phone) || ValidationHelper.IsValidPhoneNumber(phone!))
            .WithMessage(CoreResource.Validation_msg_InvalidPhone);

        RuleFor(x => x.EmployeeCode)
            .MaximumLength(50).WithMessage("Employee code must not exceed 50 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(status => status == "Active" || status == "Inactive")
            .WithMessage("Status must be either 'Active' or 'Inactive'");
    }

    private static Task<bool> IsEmailExistsByOtherUserAsync(long userId, string email)
    {
        using (var dbContext = new DbContext())
        {
            return dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Email = @Email AND Id != @UserId) THEN 1 ELSE 0 END",
                new { Email = email, UserId = userId });
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
                new CoreParamModel(nameof(request.DisplayName), request.DisplayName),
                new CoreParamModel(nameof(request.Email), request.Email),
                new CoreParamModel(nameof(request.Status), request.Status)
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var rowsAffected = await dbContext.ExecuteAsync(
                    """
                        UPDATE sy_users
                        SET DisplayName = @DisplayName,
                            Email = @Email,
                            PhoneNumber = @PhoneNumber,
                            EmployeeCode = @EmployeeCode,
                            Status = @Status,
                            UpdatedBy = @UpdatedBy,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id
                        AND (@DataRowVersion IS NULL OR DataRowVersion = @DataRowVersion)
                        """,
                    new
                    {
                        request.Id,
                        request.DisplayName,
                        request.Email,
                        request.PhoneNumber,
                        request.EmployeeCode,
                        request.Status,
                        UpdatedBy = request.HeaderInfo!.Username,
                        request.DataRowVersion
                    },
                    ct);

                if (rowsAffected == 0)
                {
                    await dbContext.transaction.RollbackAsync(ct);
                    return ResponseHelper.Error<UpdateUserCommand.Response>("User not found or has been modified by another user");
                }

                await dbContext.transaction.CommitAsync(ct);

                var response = ResponseHelper.Success(new UpdateUserCommand.Response { Id = request.Id }, CoreResource.User_msg_UpdateSuccess);
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.transaction.RollbackAsync(ct);

                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;

                return ResponseHelper.Error<UpdateUserCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }
}

#endregion