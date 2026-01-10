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
    /// Change Password Command - Đổi mật khẩu
    /// </summary>
    public sealed class ChangePasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; } = string.Empty;
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
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old password is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters")
                .Must(PasswordHelper.IsValidPassword).WithMessage("New password does not meet requirements");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required")
                .Equal(x => x.NewPassword).WithMessage("Confirm password does not match new password");

            RuleFor(x => x.NewPassword)
                .NotEqual(x => x.OldPassword).WithMessage("New password must be different from old password");
        }
    }

    /// <summary>
    /// Change Password Command Handler
    /// </summary>
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken ct)
        {
            try
            {
                UniLogger.Info($"[ChangePassword] Start processing password change for user: {request.Username}");

                using (var dbContext = new DbContext(openTransaction: true))
                {
                    try
                    {
                        // Get current user with password
                        var getUserSql = @"
                            SELECT TOP 1
                                [Id],
                                [UserName],
                                [Password]
                            FROM [dbo].[sy_users]
                            WHERE [UserName] = @Username";

                        var user = await dbContext.connection.QueryFirstOrDefaultAsync<UserDto>(
                            getUserSql,
                            new { request.Username },
                            dbContext.transaction);

                        if (user == null)
                        {
                            UniLogger.Warn($"[ChangePassword] User not found: {request.Username}");
                            return ResponseHelper.Error<bool>("User not found");
                        }

                        // Verify old password
                        if (!PasswordHelper.VerifyPassword(request.OldPassword, user.Password))
                        {
                            UniLogger.Warn($"[ChangePassword] Invalid old password for user: {request.Username}");
                            return ResponseHelper.Error<bool>("Old password is incorrect");
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

                        await dbContext.connection.ExecuteAsync(
                            updateSql,
                            new
                            {
                                Id = user.Id,
                                Password = hashedPassword,
                                UpdatedBy = request.Username
                            },
                            dbContext.transaction);

                        await dbContext.CommitAsync();

                        UniLogger.Info($"[ChangePassword] Password changed successfully for user: {request.Username}");
                        return ResponseHelper.Success(true, CoreResource.Auth_msg_PasswordChanged);
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
                UniLogger.Error($"[ChangePassword] Error changing password for user: {request.Username}", ex);
                return ResponseHelper.Error<bool>("An error occurred while changing password");
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
