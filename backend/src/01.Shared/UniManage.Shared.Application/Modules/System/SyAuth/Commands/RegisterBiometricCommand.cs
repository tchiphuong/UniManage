using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Modules.System.SyAuth.Services;
using UniManage.Shared.Resource;
using UniManage.Shared.Infrastructure.Services;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    #region Command

    /// <summary>
    /// L?nh dang k� th�ng tin sinh tr?c h?c (Public Key) cho thi?t b? ngu?i d�ng
    /// </summary>
    public sealed class RegisterBiometricCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// M� d?nh danh duy nh?t c?a thi?t b?
        /// </summary>
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// M� kh�a c�ng khai (Public Key) do thi?t b? t?o ra (�?nh d?ng Base64)
        /// </summary>
        public string PublicKey { get; set; } = string.Empty;

        /// <summary>
        /// T�n thi?t b? (v� d?: iPhone 15 Pro, Samsung S24 Ultra)
        /// </summary>
        public string? DeviceName { get; set; }
    }

    #endregion

    #region Validator

    /// <summary>
    /// B? ki?m tra d? li?u cho l?nh dang k� sinh tr?c h?c
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
    /// B? x? l� luu tr? m� kh�a sinh tr?c h?c v�o h? th?ng
    /// </summary>
    public sealed class RegisterBiometricCommandHandler : IRequestHandler<RegisterBiometricCommand, ApiResponse<bool>>
    {
        /// <summary>
        /// X? l� y�u c?u dang k� sinh tr?c h?c
        /// </summary>
        public async Task<ApiResponse<bool>> Handle(RegisterBiometricCommand request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log nghi?p v? v?i th�ng tin thi?t b?
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
                // BU?C 1: Ki?m tra tr?ng th�i t�i kho?n ngu?i d�ng (Ph?i Active m?i du?c dang k�)
                var (statusSuccess, statusError, user) = await AuthHelper.ValidateUserStatusAsync(request.HeaderInfo?.Username ?? string.Empty, log, cancellationToken);
                
                if (!statusSuccess)
                {
                    var errorResponse = ResponseHelper.Error<bool>(statusError ?? CoreResource.common_error);
                    log.Message = statusError;
                    log.ReturnCode = errorResponse.ReturnCode;
                    return errorResponse;
                }

                // BU?C 2: Luu tr? ho?c c?p nh?t Kh�a Sinh tr?c h?c cho thi?t b? n�y
                await AuthHelper.RegisterBiometricKeyAsync(user!.Id, request.DeviceId, request.PublicKey, request.DeviceName, log, cancellationToken);
                
                var response = ResponseHelper.Success(true, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_biometric));
                
                log.Result = response.Data;
                log.Message = response.Message;
                log.ReturnCode = response.ReturnCode;
                
                return response;
            }
            catch (Exception ex)
            {
                // Ghi nh?n l?i ngo?i l?
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






