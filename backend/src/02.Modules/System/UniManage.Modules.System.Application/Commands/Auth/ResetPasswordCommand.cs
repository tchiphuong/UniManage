using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Commands.Auth
{
    #region Command

    /// <summary>
    /// Lệnh đặt lại mật khẩu sử dụng mã Token xác thực từ Email
    /// </summary>
    public sealed class ResetPasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Mã Token xác thực được gửi qua Email
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Mật khẩu mới muốn thiết lập
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Xác nhận lại mật khẩu mới
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh đặt lại mật khẩu
    /// </summary>
    public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_resetToken));

            // Sử dụng chính sách mật khẩu thống nhất của hệ thống
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
    /// Bộ xử lý lệnh đặt lại mật khẩu người dùng
    /// </summary>
    public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            // Khởi tạo log (không ghi mật khẩu mới vào log vì lý do bảo mật)
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
                    // 1. Lấy danh sách các mã Token chưa sử dụng và chưa hết hạn từ Entity
                    var activeTokens = await dbContext.Set<SyPasswordResetTokens>()
                        .Where(t => t.UsedAt == null && t.ExpiresAt > DateTime.UtcNow)
                        .ToListAsync(cancellationToken);

                    SyPasswordResetTokens? validToken = null;

                    // 2. Kiểm tra Token bằng PasswordHelper (vì Token trong DB đã được hash)
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

                    // 3. Tìm người dùng tương ứng dựa trên Username từ Token
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

                    // 4. Cập nhật mật khẩu mới và thông tin Audit
                    user.Password = PasswordHelper.HashPassword(request.NewPassword);
                    user.UpdatedAt = DateTimeHelper.Now;
                    user.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;

                    // 5. Đánh dấu Token đã được sử dụng
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






