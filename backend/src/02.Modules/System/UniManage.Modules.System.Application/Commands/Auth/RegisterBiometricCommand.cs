using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Services;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.System.Application.Commands.Auth
{
    #region Command

    /// <summary>
    /// Lệnh đăng ký thông tin sinh trắc học (Public Key) cho thiết bị người dùng
    /// </summary>
    public sealed class RegisterBiometricCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Mã định danh duy nhất của thiết bị
        /// </summary>
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// Mã khóa công khai (Public Key) do thiết bị tạo ra (Định dạng Base64)
        /// </summary>
        public string PublicKey { get; set; } = string.Empty;

        /// <summary>
        /// Tên thiết bị (ví dụ: iPhone 15 Pro, Samsung S24 Ultra)
        /// </summary>
        public string? DeviceName { get; set; }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh đăng ký sinh trắc học
    /// </summary>
    public sealed class RegisterBiometricCommandValidator : AbstractValidator<RegisterBiometricCommand>
    {
        public RegisterBiometricCommandValidator()
        {
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_deviceId));

            RuleFor(x => x.PublicKey)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_publicKey));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lưu trữ mã khóa sinh trắc học vào hệ thống
    /// </summary>
    public sealed class RegisterBiometricCommandHandler : IRequestHandler<RegisterBiometricCommand, ApiResponse<bool>>
    {
        /// <summary>
        /// Xử lý yêu cầu đăng ký sinh trắc học
        /// </summary>
        public async Task<ApiResponse<bool>> Handle(RegisterBiometricCommand request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ với thông tin thiết bị
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.DeviceId), request.DeviceId),
                    new(nameof(request.DeviceName), request.DeviceName ?? "Unknown")
                }
            };

            try
            {
                // BƯỚC 1: Kiểm tra trạng thái tài khoản người dùng (Phải Active mới được đăng ký)
                var (statusSuccess, statusError, user) = await AuthHelper.ValidateUserStatusAsync(request.HeaderInfo?.Username ?? string.Empty, log, cancellationToken);
                
                if (!statusSuccess)
                {
                    var errorResponse = ResponseHelper.Error<bool>(statusError ?? CoreResource.common_error);
                    log.Message = statusError;
                    log.ReturnCode = errorResponse.ReturnCode;
                    return errorResponse;
                }

                // BƯỚC 2: Lưu trữ hoặc cập nhật Khóa Sinh trắc học cho thiết bị này
                await AuthHelper.RegisterBiometricKeyAsync(user!.Id, request.DeviceId, request.PublicKey, request.DeviceName, log, cancellationToken);
                
                var response = ResponseHelper.Success(true, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_biometric));
                
                log.Result = response.Data;
                log.Message = response.Message;
                log.ReturnCode = response.ReturnCode;
                
                return response;
            }
            catch (Exception ex)
            {
                // Ghi nhận lỗi ngoại lệ
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






