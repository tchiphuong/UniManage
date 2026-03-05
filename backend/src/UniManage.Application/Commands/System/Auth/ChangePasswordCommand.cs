using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Application.Utilities;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Auth
{
    /// <summary>
    /// Change Password Command - Đổi mật khẩu
    /// </summary>
    public sealed class ChangePasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Mật khẩu cũ
        /// </summary>
        public string OldPassword { get; set; } = string.Empty;
        /// <summary>
        /// Mật khẩu mới
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
        /// <summary>
        /// Xác nhận mật khẩu mới
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

            // [SECURITY] H4 — Enforce consistent password policy via extension method
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
        public async Task<ApiResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken ct)
        {
            var username = request.HeaderInfo.Username ?? string.Empty;
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.HeaderInfo.Username), username),
                    new CoreParamModel(nameof(request.OldPassword), StringHelper.MaskSensitiveData(request.OldPassword))
                }
            };

            var dbContext = new DbContext(openTransaction: true);
            try
            {
                using (dbContext)
                {
                    // Get current user with password
                    var getUserSql = @"
                        SELECT TOP 1
                            [Id],
                            [UserName],
                            [Password]
                        FROM [dbo].[sy_users]
                        WHERE [UserName] = @Username";

                    var user = await dbContext.QueryFirstOrDefaultAsync<UserDto>(
                        getUserSql,
                        new { Username = username },
                        ct);

                    if (user == null)
                    {
                        var errorResponse = ResponseHelper.Error<bool>(CoreResource.auth_userNotFound);
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = "User not found in database";
                        return errorResponse;
                    }

                    // Verify old password
                    if (!PasswordHelper.VerifyPassword(request.OldPassword, user.Password))
                    {
                        var errorResponse = ResponseHelper.Error<bool>(CoreResource.auth_oldPasswordIncorrect);
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = "Invalid old password provided";
                        return errorResponse;
                    }

                    // Hash new password
                    var hashedPassword = PasswordHelper.HashPassword(request.NewPassword);

                    // Update password
                    var updateSql = @"
                        UPDATE [dbo].[sy_users]
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
                        ct);

                    await dbContext.CommitAsync();

                    var response = ResponseHelper.Success(true, CoreResource.auth_passwordChanged);
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    return response;
                }
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync();
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }

        private class UserDto
        {
            public long Id { get; set; }
            public string UserName { get; set; } = default!;
            public string Password { get; set; } = default!;
        }
    }
}
