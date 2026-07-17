using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Application.Modules.System.SyAuth.Services;
using UniManage.Shared.Resource;
using UniManage.Shared.Infrastructure.Services;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    #region Command

    /// <summary>
    /// Command dang nh?p b?ng sinh tr?c h?c.
    /// </summary>
    public sealed class BiometricLoginCommand : BaseCommand, IRequest<ApiResponse<LoginCommand.Response>>
    {
        public string Username { get; set; } = default!;
        public string DeviceId { get; set; } = default!;
        public string Signature { get; set; } = default! ; // Base64 signature
        public string ChallengeRaw { get; set; } = default!; // Challenge d� g?i tru?c d�
        
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
                // 1. Ki?m tra tr?ng th�i t�i kho?n
                var (statusSuccess, statusError, userSecurity) = await AuthHelper.ValidateUserStatusAsync(request.Username, log, cancellationToken);
                if (!statusSuccess)
                {
                    return ReturnError(statusError!);
                }

                // 2. X�c th?c ch? k� sinh tr?c h?c
                var isSignatureValid = await AuthHelper.VerifyBiometricSignatureAsync(
                    userSecurity!.Id, request.DeviceId, request.ChallengeRaw, request.Signature, log, cancellationToken);

                if (!isSignatureValid)
                {
                    await AuthHelper.HandleLoginFailureAsync(request.Username, log, cancellationToken);
                    return ReturnError(CoreResource.auth_invalidLogin);
                }

                // 3. �ang nh?p th�nh c�ng - Ph�t h�nh token
                // S? d?ng custom grant ho?c password flow v?i dummy (ho?c trao d?i internal)
                var (authSuccess, tokenData, identityError) = await _identityClient.RequestTokenAsync(
                    request.Username, "biometric-dummy-pass", cancellationToken);

                if (!authSuccess || tokenData == null)
                {
                    return ReturnError(CoreResource.auth_invalidLogin);
                }

                await AuthHelper.HandleLoginSuccessAsync(request.Username, log, cancellationToken);

                // 4. C?p nh?t Device/FCM
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

            ApiResponse<LoginCommand.Response> ReturnError(string userMsg)
            {
                var errorResponse = ResponseHelper.Error<LoginCommand.Response>(userMsg);
return errorResponse;
            }
        }
    }

    #endregion
}









