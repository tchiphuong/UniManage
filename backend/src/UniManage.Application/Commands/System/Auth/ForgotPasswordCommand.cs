using FluentValidation;
using MediatR;
using UniManage.Application.Services;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Auth
{
    #region Command

    /// <summary>
    /// Forgot Password Command - Gửi link reset mật khẩu
    /// </summary>
    public sealed class ForgotPasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public string EmailOrUsername { get; set; } = string.Empty;
    }

    #endregion

    #region Validator

    /// <summary>
    /// Forgot Password Command Validator
    /// </summary>
    public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.EmailOrUsername)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_emailOrUsername))
                .DependentRules(() =>
                {
                    RuleFor(x => x.EmailOrUsername)
                        .MaximumLength(255)
                        .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_emailOrUsername, 255));
                });
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Forgot Password Command Handler
    /// </summary>
    public sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.EmailOrUsername), request.EmailOrUsername)
                }
            };

            var dbContext = new DbContext(openTransaction: true);
            try
            {
                using (dbContext)
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
                        await dbContext.CommitAsync();
                        var successResponse = ResponseHelper.Success(true, CoreResource.auth_resetLinkSent);
                        log.Result = successResponse;
                        log.ReturnCode = successResponse.ReturnCode;
                        log.Message = "User not found (Success returned for security)";
                        return successResponse;
                    }

                    if (string.IsNullOrEmpty(user.Email))
                    {
                        await dbContext.CommitAsync();
                        var noEmailResponse = ResponseHelper.Success(true, CoreResource.auth_resetLinkSent);
                        log.Result = noEmailResponse;
                        log.ReturnCode = noEmailResponse.ReturnCode;
                        log.Message = "User has no email (Success returned for security)";
                        return noEmailResponse;
                    }

                    // Generate reset token
                    var resetToken = StringHelper.GenerateCode(32);
                    var hashedToken = PasswordHelper.HashPassword(resetToken);
                    var expiresAt = DateTime.UtcNow.AddHours(24);

                    // Lưu reset token vào database (hash token để bảo mật)
                    var insertSql = @"
                        INSERT INTO [dbo].[sy_password_reset_tokens]
                        ([Username], [Token], [ExpiresAt], [CreatedAt])
                        VALUES (@Username, @Token, @ExpiresAt, GETDATE())";

                    await dbContext.ExecuteAsync(
                        insertSql,
                        new
                        {
                            Username = user.Username,
                            Token = hashedToken,
                            ExpiresAt = expiresAt
                        },
                        ct);

                    await dbContext.CommitAsync();

                    // TODO: Gửi email với reset link
                    // var resetLink = $"{baseUrl}/reset-password?token={resetToken}";
                    // await emailService.SendPasswordResetEmail(user.Email, resetLink);

                    var response = ResponseHelper.Success(true, CoreResource.auth_resetLinkSent);
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = $"Reset token generated for user: {user.Username}";
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
            public string Username { get; set; } = string.Empty;
            public string? Email { get; set; }
        }
    }

    #endregion
}
