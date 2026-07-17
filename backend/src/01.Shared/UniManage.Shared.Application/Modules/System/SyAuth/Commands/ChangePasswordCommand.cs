using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    /// <summary>
    /// Change Password Command - �?i m?t kh?u
    /// </summary>
    public sealed class ChangePasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        [Newtonsoft.Json.JsonIgnore]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// M?t kh?u cu
        /// </summary>
        public string OldPassword { get; set; } = string.Empty;
        /// <summary>
        /// M?t kh?u m?i
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
        /// <summary>
        /// X�c nh?n m?t kh?u m?i
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    /// <summary>
    /// Change Password Command Validator
    /// </summary>
    public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.HeaderInfo.Username)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userIdentity));

            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_oldPassword));

            // [SECURITY] H4 � Enforce consistent password policy via extension method
            RuleFor(x => x.NewPassword)
                .Password(CoreResource.lbl_newPassword)
                .DependentRules(() =>
                {
                    RuleFor(x => x.NewPassword)
                        .NotEqual(x => x.OldPassword)
                        .WithMessage(CoreResource.validation_newPasswordDifferent);
                });

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_confirmPassword))
                .DependentRules(() =>
                {
                    RuleFor(x => x.ConfirmPassword)
                        .Equal(x => x.NewPassword)
                        .WithMessage(CoreResource.validation_confirmPasswordMismatch);
                });
        }
    }

    /// <summary>
    /// Change Password Command Handler
    /// </summary>
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var username = request.Username ?? string.Empty;

            using var dbContext = new DbContext(openTransaction: true);
                    // Get current user with password
                    var getUserSql = @"
                        SELECT TOP 1
                            [Id],
                            [UserName],
                            [Password]
                        FROM [dbo].[SyUsers]
                        WHERE [UserName] = @Username";

                    var user = await dbContext.QueryFirstOrDefaultAsync<UserModel>(
                        getUserSql,
                        new { Username = username },
                        cancellationToken);

                    if (user == null)
                    {
                        var errorResponse = ResponseHelper.Error<bool>(CoreResource.auth_userNotFound);
                        return errorResponse;
                    }

                    // Verify old password
                    if (!PasswordHelper.VerifyPassword(request.OldPassword, user.Password))
                    {
                        var errorResponse = ResponseHelper.Error<bool>(CoreResource.auth_oldPasswordIncorrect);
                        return errorResponse;
                    }

                    // Hash new password
                    var hashedPassword = PasswordHelper.HashPassword(request.NewPassword);

                    // Update password
                    var updateSql = @"
                        UPDATE [dbo].[SyUsers]
                        SET [Password] = @Password,
                            [UpdatedAt] = GETDATE(),
                            [UpdatedBy] = @UpdatedBy
                        WHERE [Id] = @Id";

                    await dbContext.ExecuteAsync(
                        updateSql,
                        new
                        {
                            Id = user.Id,
                            Password = hashedPassword,
                            UpdatedBy = username
                        },
                        cancellationToken);

                    

                    var response = ResponseHelper.Success(true, CoreResource.auth_passwordChanged);
                    return response;
        }

        private class UserModel
        {
            public long Id { get; set; }
            public string UserName { get; set; } = default!;
            public string Password { get; set; } = default!;
        }
    }
}








