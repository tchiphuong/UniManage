using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Services;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Auth
{
    #region Command

    /// <summary>
    /// Command đăng ký thông tin sinh trắc học cho thiết bị.
    /// </summary>
    public sealed class RegisterBiometricCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public string DeviceId { get; set; } = default!;
        public string PublicKey { get; set; } = default!; // Base64 Public Key
        public string? DeviceName { get; set; }
        public long UserId { get; set; } // Lấy từ context/token
    }

    #endregion

    #region Validator

    public sealed class RegisterBiometricValidator : AbstractValidator<RegisterBiometricCommand>
    {
        public RegisterBiometricValidator()
        {
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "DeviceId"));

            RuleFor(x => x.PublicKey)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "PublicKey"));
        }
    }

    #endregion

    #region Handler

    public sealed class RegisterBiometricCommandHandler : IRequestHandler<RegisterBiometricCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(RegisterBiometricCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.DeviceId), request.DeviceId),
                    new CoreParamModel(nameof(request.DeviceName), request.DeviceName)
                }
            };

            try
            {
                await AuthHelper.RegisterBiometricKeyAsync(request.UserId, request.DeviceId, request.PublicKey, request.DeviceName, log, ct);
                
                var response = ResponseHelper.Success(true, CoreResource.common_success);
                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
        }
    }

    #endregion
}
