using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Services;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.System.Application.Commands.Auth
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

        public async Task<ApiResponse<LoginCommand.Response>> Handle(BiometricLoginCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo());
                // 1. Kiểm tra trạng thái tài khoản
                var (statusSuccess, statusError, userSecurity) = await AuthHelper.ValidateUserStatusAsync(request.Username, log, cancellationToken);
                if (!statusSuccess)
                {
                    return ReturnError(statusError!, "Account status check failed");
                }

                // 2. Xác thực chữ ký sinh trắc học
                var isSignatureValid = await AuthHelper.VerifyBiometricSignatureAsync(
                    userSecurity!.Id, request.DeviceId, request.ChallengeRaw, request.Signature, log, cancellationToken);

                if (!isSignatureValid)
                {
                    await AuthHelper.HandleLoginFailureAsync(request.Username, log, cancellationToken);
                    return ReturnError(CoreResource.auth_invalidLogin, "Invalid biometric signature");
                }

                // 3. Đăng nhập thành công - Phát hành token
                // Sử dụng custom grant hoặc password flow với dummy (hoặc trao đổi internal)
                var (authSuccess, tokenData, identityError) = await _identityClient.RequestTokenAsync(
                    request.Username, "biometric-dummy-pass", cancellationToken);

                if (!authSuccess || tokenData == null)
                {
                    return ReturnError(CoreResource.auth_invalidLogin, identityError);
                }

                await AuthHelper.HandleLoginSuccessAsync(request.Username, log, cancellationToken);

                // 4. Cập nhật Device/FCM
                await AuthHelper.UpdateDeviceTokenAsync(userSecurity.Id, request.DeviceId, request.FcmToken, request.DeviceType, log, cancellationToken);

                var responseData = new LoginCommand.Response
                {
                    AccessToken = tokenData.access_token,
                    RefreshToken = tokenData.refresh_token,
                    ExpiresIn = tokenData.expires_in,
                    TokenType = tokenData.token_type,
                    User = new LoginCommand.UserInfo { Id = userSecurity.Id, UserCode = request.Username, DisplayName = request.Username }
                };

                var response = ResponseHelper.Success(responseData, CoreResource.auth_loginSuccess);
                return response;

            ApiResponse<LoginCommand.Response> ReturnError(string userMsg, string logMsg)
            {
                var errorResponse = ResponseHelper.Error<LoginCommand.Response>(userMsg);
return errorResponse;
            }
        }
    }

    #endregion
}









