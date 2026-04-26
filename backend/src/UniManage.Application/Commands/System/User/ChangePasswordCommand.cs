using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.System.User;

#region Command

/// <summary>
/// Command to change user password
/// </summary>
public sealed class ChangePasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
{
    [Newtonsoft.Json.JsonIgnore]
    public string Username { get; set; } = default!;
    public string OldPassword { get; init; } = default!;
    public string NewPassword { get; init; } = default!;
    public string ConfirmPassword { get; init; } = default!;
}

#endregion

#region Validator

/// <summary>
/// Validator for ChangePasswordCommand
/// </summary>
public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.auth_oldPassword));

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.auth_newPassword))
            .DependentRules(() =>
            {
                RuleFor(x => x.NewPassword)
                    .MinimumLength(PasswordHelper.MinPasswordLength).WithMessage(string.Format(CoreResource.validation_minLength, CoreResource.auth_newPassword, PasswordHelper.MinPasswordLength))
                    .MaximumLength(PasswordHelper.MaxPasswordLength).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.auth_newPassword, PasswordHelper.MaxPasswordLength))
                    .Must(PasswordHelper.IsValidPassword).WithMessage(CoreResource.validation_passwordComplexity)
                    .NotEqual(x => x.OldPassword).WithMessage(CoreResource.validation_newPasswordDifferent);
            });

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.auth_confirmPassword))
            .Equal(x => x.NewPassword).WithMessage(CoreResource.validation_confirmPasswordMismatch);
    }
}
#endregion

#region Handler

/// <summary>
/// Handler for ChangePasswordCommand
/// </summary>
public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new(nameof(request.Username), request.Username)
            }
        };

        try
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var user = await dbContext.Set<sy_users>()
                        .FirstOrDefaultAsync(u => u.Username == request.Username, ct);

                    if (user == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<bool>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                        log.Message = notFoundResponse.Message;
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        return notFoundResponse;
                    }

                    // Verify old password
                    if (!PasswordHelper.VerifyPassword(request.OldPassword, user.Password))
                    {
                        var errorResponse = ResponseHelper.Error<bool>(CoreResource.auth_oldPasswordIncorrect);
                        log.Message = errorResponse.Message;
                        log.ReturnCode = errorResponse.ReturnCode;
                        return errorResponse;
                    }

                    // Update new password
                    user.Password = PasswordHelper.HashPassword(request.NewPassword);
                    user.UpdatedBy = request.Username;
                    user.UpdatedAt = DateTimeHelper.Now;

                    await dbContext.SaveChangesAsync(ct);
                    await dbContext.CommitAsync();

                    var response = ResponseHelper.Success(true, CoreResource.auth_passwordChanged);
                    
                    log.Result = true;
                    log.Message = response.Message;
                    log.ReturnCode = response.ReturnCode;

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                    return ResponseHelper.Error<bool>(CoreResource.common_error);
                }
            }
        }
        finally
        {
            UniLogManager.WriteApiLog(log);
        }
    }
}

#endregion
