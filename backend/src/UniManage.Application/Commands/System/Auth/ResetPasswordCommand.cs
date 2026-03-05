using FluentValidation;
using MediatR;
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
    /// Reset Password Command - Đặt lại mật khẩu với token
    /// </summary>
    public sealed class ResetPasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Reset Token từ email
        /// </summary>
        public string Token { get; set; } = string.Empty;
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
    /// Reset Password Command Validator
    /// </summary>
    public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_resetToken));

            // [SECURITY] H4 — Unified password policy via extension method
            RuleFor(x => x.NewPassword)
                .Password(CoreResource.lbl_newPassword);

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
    /// Reset Password Command Handler
    /// </summary>
    public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Token), StringHelper.MaskSensitiveData(request.Token))
                }
            };

            var dbContext = new DbContext(openTransaction: true);
            try
            {
                using (dbContext)
                {
                    // Tìm tất cả reset token chưa sử dụng của user
                    var getAllTokensSql = @"
                        SELECT [Username], [Token], [ExpiresAt], [UsedAt]
                        FROM [dbo].[sy_password_reset_tokens]
                        WHERE [UsedAt] IS NULL AND [ExpiresAt] > GETUTCDATE()";

                    var allTokens = await dbContext.QueryAsync<TokenDto>(getAllTokensSql, null, ct);
                    TokenDto? validToken = null;

                    foreach (var tokenItem in allTokens)
                    {
                        if (PasswordHelper.VerifyPassword(request.Token, tokenItem.Token))
                        {
                            validToken = tokenItem;
                            break;
                        }
                    }

                    if (validToken == null)
                    {
                        await dbContext.RollbackAsync();
                        var errorResponse = ResponseHelper.Error<bool>(CoreResource.auth_invalidResetToken);
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = "Invalid or expired reset token provided";
                        return errorResponse;
                    }

                    // Hash mật khẩu mới
                    var hashedPassword = PasswordHelper.HashPassword(request.NewPassword);

                    // Update password
                    var updatePasswordSql = @"
                        UPDATE [dbo].[sy_users]
                        SET [Password] = @Password,
                            [UpdatedAt] = GETDATE(),
                            [UpdatedBy] = @SystemUser
                        WHERE [Username] = @Username";

                    await dbContext.ExecuteAsync(
                        updatePasswordSql,
                        new
                        {
                            Password = hashedPassword,
                            Username = validToken.Username,
                            SystemUser = ApplicationConstants.Defaults.SystemUser
                        },
                        ct);

                    // Đánh dấu token đã sử dụng
                    var markTokenUsedSql = @"
                        UPDATE [dbo].[sy_password_reset_tokens]
                        SET [UsedAt] = GETDATE()
                        WHERE [Token] = @TokenHash";

                    await dbContext.ExecuteAsync(
                        markTokenUsedSql,
                        new { TokenHash = validToken.Token },
                        ct);

                    await dbContext.CommitAsync();

                    var response = ResponseHelper.Success(true, CoreResource.auth_passwordReset);
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = $"Password reset successfully for user: {validToken.Username}";
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

        private class TokenDto
        {
            public string Username { get; set; } = default!;
            public string Token { get; set; } = default!;
            public DateTime ExpiresAt { get; set; }
            public DateTime? UsedAt { get; set; }
        }
    }
}
