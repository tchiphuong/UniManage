using Dapper;
using FluentValidation;
using MediatR;
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
                .NotEmpty().WithMessage("Reset token is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters")
                .Must(PasswordHelper.IsValidPassword).WithMessage("New password does not meet requirements");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
        }
    }

    /// <summary>
    /// Reset Password Command Handler
    /// </summary>
    public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken ct)
        {
            try
            {
                UniLogger.Info("[ResetPassword] Processing password reset request");

                using (var dbContext = new DbContext(openTransaction: true))
                {
                    try
                    {
                        // Tìm reset token
                        var tokenSql = @"
                            SELECT TOP 1
                                [Username],
                                [Token],
                                [ExpiresAt],
                                [UsedAt]
                            FROM [dbo].[sy_password_reset_tokens]
                            WHERE [Token] = @Token";

                        var tokenData = await dbContext.connection.QueryFirstOrDefaultAsync<TokenDto>(
                            tokenSql,
                            new { request.Token },
                            dbContext.transaction);

                        if (tokenData == null)
                        {
                            UniLogger.Warn("[ResetPassword] Invalid reset token");
                            return ResponseHelper.Error<bool>("Invalid or expired reset token");
                        }

                        if (tokenData.UsedAt.HasValue)
                        {
                            UniLogger.Warn("[ResetPassword] Token already used");
                            return ResponseHelper.Error<bool>("Reset token has already been used");
                        }

                        if (tokenData.ExpiresAt < DateTime.UtcNow)
                        {
                            UniLogger.Warn("[ResetPassword] Token expired");
                            return ResponseHelper.Error<bool>("Reset token has expired");
                        }

                        // Hash mật khẩu mới
                        var hashedPassword = PasswordHelper.HashPassword(request.NewPassword);

                        // Update password
                        var updatePasswordSql = @"
                            UPDATE [dbo].[sy_users]
                            SET [Password] = @Password,
                                [UpdatedAt] = GETDATE(),
                                [UpdatedBy] = 'system'
                            WHERE [Username] = @Username";

                        await dbContext.connection.ExecuteAsync(
                            updatePasswordSql,
                            new
                            {
                                Password = hashedPassword,
                                Username = tokenData.Username
                            },
                            dbContext.transaction);

                        // Đánh dấu token đã sử dụng
                        var markTokenUsedSql = @"
                            UPDATE [dbo].[sy_password_reset_tokens]
                            SET [UsedAt] = GETDATE()
                            WHERE [Token] = @Token";

                        await dbContext.connection.ExecuteAsync(
                            markTokenUsedSql,
                            new { request.Token },
                            dbContext.transaction);

                        await dbContext.CommitAsync();

                        UniLogger.Info($"[ResetPassword] Password reset successfully for user: {tokenData.Username}");
                        return ResponseHelper.Success(true, CoreResource.Auth_msg_PasswordReset);
                    }
                    catch
                    {
                        await dbContext.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error("[ResetPassword] Error during password reset", ex);
                return ResponseHelper.Error<bool>("An error occurred while resetting password");
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
