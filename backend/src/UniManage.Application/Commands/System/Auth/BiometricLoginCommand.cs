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
    /// Command đăng nhập bằng sinh trắc học.
    /// </summary>
    public sealed class BiometricLoginCommand : BaseCommand, IRequest<ApiResponse<LoginCommand.Response>>
    {
        public string Username { get; set; } = default!;
        public string DeviceId { get; set; } = default!;
        public string Signature { get; set; } = default! ; // Base64 signature
        public string ChallengeRaw { get; set; } = default!; // Challenge đã gửi trước đó
        
        public string? FcmToken { get; set; }
        public string? DeviceType { get; set; }
    }

    #endregion

    #region Validator

    public sealed class BiometricLoginValidator : AbstractValidator<BiometricLoginCommand>
    {
        public BiometricLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.DeviceId).NotEmpty();
            RuleFor(x => x.Signature).NotEmpty();
            RuleFor(x => x.ChallengeRaw).NotEmpty();
        }
    }

    #endregion

    #region Handler

    public sealed class BiometricLoginCommandHandler : IRequestHandler<BiometricLoginCommand, ApiResponse<LoginCommand.Response>>
    {
        private readonly IIdentityServerClient _identityClient;

        public BiometricLoginCommandHandler(IIdentityServerClient identityClient)
        {
            _identityClient = identityClient;
        }

        public async Task<ApiResponse<LoginCommand.Response>> Handle(BiometricLoginCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Username), request.Username),
                    new CoreParamModel(nameof(request.DeviceId), request.DeviceId)
                }
            };

            try
            {
                // 1. Kiểm tra trạng thái tài khoản
                var (statusSuccess, statusError, userSecurity) = await AuthHelper.ValidateUserStatusAsync(request.Username, log, ct);
                if (!statusSuccess)
                {
                    return ReturnError(statusError!, "Account status check failed");
                }

                // 2. Xác thực chữ ký sinh trắc học
                var isSignatureValid = await AuthHelper.VerifyBiometricSignatureAsync(
                    userSecurity!.Id, request.DeviceId, request.ChallengeRaw, request.Signature, log, ct);

                if (!isSignatureValid)
                {
                    await AuthHelper.HandleLoginFailureAsync(request.Username, log, ct);
                    return ReturnError(CoreResource.auth_invalidBiometric, "Invalid biometric signature");
                }

                // 3. Đăng nhập thành công - Phát hành token
                // Sử dụng custom grant hoặc password flow với dummy (hoặc trao đổi internal)
                var (authSuccess, tokenData, identityError) = await _identityClient.RequestTokenAsync(
                    request.Username, "biometric-dummy-pass", ct);

                if (!authSuccess || tokenData == null)
                {
                    return ReturnError(CoreResource.auth_invalidLogin, identityError);
                }

                await AuthHelper.HandleLoginSuccessAsync(request.Username, log, ct);

                // 4. Cập nhật Device/FCM
                await AuthHelper.UpdateDeviceTokenAsync(userSecurity.Id, request.DeviceId, request.FcmToken, request.DeviceType, log, ct);

                var responseData = new LoginCommand.Response
                {
                    AccessToken = tokenData.access_token,
                    RefreshToken = tokenData.refresh_token,
                    ExpiresIn = tokenData.expires_in,
                    TokenType = tokenData.token_type,
                    User = new LoginCommand.UserInfo { Id = userSecurity.Id, UserCode = request.Username, DisplayName = request.Username }
                };

                var response = ResponseHelper.Success(responseData, CoreResource.auth_loginSuccess);
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
                return ResponseHelper.Error<LoginCommand.Response>(CoreResource.common_error);
            }

            ApiResponse<LoginCommand.Response> ReturnError(string userMsg, string logMsg)
            {
                var errorResponse = ResponseHelper.Error<LoginCommand.Response>(userMsg);
                log.ReturnCode = errorResponse.ReturnCode;
                log.Message = logMsg;
                UniLogManager.WriteApiLog(log);
                return errorResponse;
            }
        }
    }

    #endregion
}
