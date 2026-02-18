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
    /// Forgot Password Command - Gửi email reset password
    /// </summary>
    public sealed class ForgotPasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Email hoặc Username
        /// </summary>
        public string EmailOrUsername { get; set; } = string.Empty;
    }

    /// <summary>
    /// Forgot Password Command Validator
    /// </summary>
    public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.EmailOrUsername)
                .NotEmpty().WithMessage("Email or username is required");
        }
    }

    /// <summary>
    /// Forgot Password Command Handler
    /// </summary>
    public sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken ct)
        {
            try
            {
                UniLogger.Info($"[ForgotPassword] Processing request for: {request.EmailOrUsername}");

                using (var dbContext = new DbContext(openTransaction: true))
                {
                    try
                    {
                        // Tìm user theo email hoặc username
                        var sql = @"
                            SELECT TOP 1
                                [Username],
                                [Email]
                            FROM [dbo].[sy_users]
                            WHERE [Username] = @EmailOrUsername 
                                OR [Email] = @EmailOrUsername";

                        var user = await dbContext.QueryFirstOrDefaultAsync<UserDto>(
                            sql,
                            new { request.EmailOrUsername },
                            ct);

                        // Không báo lỗi nếu không tìm thấy user (security best practice)
                        if (user == null)
                        {
                            UniLogger.Info($"[ForgotPassword] User not found: {request.EmailOrUsername}");
                            return ResponseHelper.Success(true, CoreResource.Auth_msg_ResetLinkSent);
                        }

                        if (string.IsNullOrEmpty(user.Email))
                        {
                            UniLogger.Warn($"[ForgotPassword] User has no email: {user.Username}");
                            return ResponseHelper.Success(true, CoreResource.Auth_msg_ResetLinkSent);
                        }

                        // Generate reset token
                        var resetToken = StringHelper.GenerateCode(32);
                        var expiresAt = DateTime.UtcNow.AddHours(24);

                        // Lưu reset token vào database
                        var insertSql = @"
                            INSERT INTO [dbo].[sy_password_reset_tokens]
                            ([Username], [Token], [ExpiresAt], [CreatedAt])
                            VALUES (@Username, @Token, @ExpiresAt, GETDATE())";

                        await dbContext.ExecuteAsync(
                            insertSql,
                            new
                            {
                                Username = user.Username,
                                Token = resetToken,
                                ExpiresAt = expiresAt
                            },
                            ct);

                        await dbContext.CommitAsync();

                        // TODO: Gửi email với reset link
                        // var resetLink = $"{baseUrl}/reset-password?token={resetToken}";
                        // await emailService.SendPasswordResetEmail(user.Email, resetLink);

                        UniLogger.Info($"[ForgotPassword] Reset token generated for user: {user.Username}");
                        return ResponseHelper.Success(true, CoreResource.Auth_msg_ResetLinkSent);
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
                UniLogger.Error($"[ForgotPassword] Error processing request for: {request.EmailOrUsername}", ex);
                return ResponseHelper.Error<bool>("An error occurred while processing your request");
            }
        }

        private class UserDto
        {
            public string Username { get; set; } = default!;
            public string? Email { get; set; }
        }
    }
}
