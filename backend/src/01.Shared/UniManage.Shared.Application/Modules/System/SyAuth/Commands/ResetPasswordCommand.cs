using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    #region Command

    /// <summary>
    /// L?nh d?t l?i m?t kh?u s? d?ng m� Token x�c th?c t? Email
    /// </summary>
    public sealed class ResetPasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// M� Token x�c th?c du?c g?i qua Email
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// M?t kh?u m?i mu?n thi?t l?p
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// X�c nh?n l?i m?t kh?u m?i
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    #endregion

    #region Validator

    /// <summary>
    /// B? ki?m tra d? li?u cho l?nh d?t l?i m?t kh?u
    /// </summary>
    public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_resetToken));

            // S? d?ng ch�nh s�ch m?t kh?u th?ng nh?t c?a h? th?ng
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

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l� l?nh d?t l?i m?t kh?u ngu?i d�ng
    /// </summary>
    public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log (kh�ng ghi m?t kh?u m?i v�o log v� l� do b?o m?t)
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Token), "********")
                }
            };

            try
            {
                using (var dbContext = new DbContext(openTransaction: true))
                {
                    // 1. L?y danh s�ch c�c m� Token chua s? d?ng v� chua h?t h?n t? Entity
                    var activeTokens = await dbContext.Set<SyPasswordResetTokens>()
                        .Where(t => t.UsedAt == null && t.ExpiresAt > DateTime.UtcNow)
                        .ToListAsync(cancellationToken);

                    SyPasswordResetTokens? validToken = null;

                    // 2. Ki?m tra Token b?ng PasswordHelper (v� Token trong DB d� du?c hash)
                    foreach (var tokenItem in activeTokens)
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
                        
                        log.Message = "Invalid or expired reset token provided";
                        log.ReturnCode = errorResponse.ReturnCode;
                        return errorResponse;
                    }

                    // 3. T�m ngu?i d�ng tuong ?ng d?a tr�n Username t? Token
                    var user = await dbContext.Set<SyUsers>()
                        .FirstOrDefaultAsync(u => u.Username == validToken.Username, cancellationToken);

                    if (user == null)
                    {
                        await dbContext.RollbackAsync();
                        var errorResponse = ResponseHelper.NotFound<bool>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                        
                        log.Message = $"User '{validToken.Username}' not found for the provided token";
                        log.ReturnCode = errorResponse.ReturnCode;
                        return errorResponse;
                    }

                    // 4. C?p nh?t m?t kh?u m?i v� th�ng tin Audit
                    user.Password = PasswordHelper.HashPassword(request.NewPassword);
                    user.UpdatedAt = DateTimeHelper.Now;
                    user.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;

                    // 5. ��nh d?u Token d� du?c s? d?ng
                    validToken.UsedAt = DateTimeHelper.Now;

                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync();

                    var response = ResponseHelper.Success(true, CoreResource.auth_passwordReset);
                    
                    log.Message = $"Password successfully reset for user '{validToken.Username}'";
                    log.ReturnCode = response.ReturnCode;
                    log.Result = true;

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}






