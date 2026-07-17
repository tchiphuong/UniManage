using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    #region Command

    /// <summary>
    /// Forgot Password Command - G?i link reset m?t kh?u
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
        public async Task<ApiResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    // T�m user theo email ho?c username
                    var sql = @"
                        SELECT TOP 1
                            [Username],
                            [Email]
                        FROM [dbo].[SyUsers]
                        WHERE [Username] = @EmailOrUsername 
                            OR [Email] = @EmailOrUsername";

                    var user = await dbContext.QueryFirstOrDefaultAsync<UserModel>(
                        sql,
                        new { request.EmailOrUsername },
                        cancellationToken);

                    // Kh�ng b�o l?i n?u kh�ng t�m th?y user (security best practice)
                    if (user == null)
                    {
                        
                        var successResponse = ResponseHelper.Success(true, CoreResource.auth_resetLinkSent);
                        return successResponse;
                    }

                    if (string.IsNullOrEmpty(user.Email))
                    {
                        
                        var noEmailResponse = ResponseHelper.Success(true, CoreResource.auth_resetLinkSent);
                        return noEmailResponse;
                    }

                    // Generate reset token
                    var resetToken = StringHelper.GenerateCode(32);
                    var hashedToken = PasswordHelper.HashPassword(resetToken);
                    var expiresAt = DateTime.UtcNow.AddHours(24);

                    // Luu reset token v�o database (hash token d? b?o m?t)
                    var insertSql = @"
                        INSERT INTO [dbo].[SyPasswordResetTokens]
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
                        cancellationToken);

                    

                    // TODO: G?i email v?i reset link
                    // var resetLink = $"{baseUrl}/reset-password?token={resetToken}";
                    // await emailService.SendPasswordResetEmail(user.Email, resetLink);

                    var response = ResponseHelper.Success(true, CoreResource.auth_resetLinkSent);
                    return response;
        }

        private class UserModel
        {
            public string Username { get; set; } = string.Empty;
            public string? Email { get; set; }
        }
    }

    #endregion
}








