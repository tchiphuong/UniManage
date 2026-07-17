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
    /// Command th?c hi?n dang nh?p qua m?ng x� h?i b?ng Social Token t? Client SDK.
    /// </summary>
    public sealed class SocialLoginCommand : BaseCommand, IRequest<ApiResponse<LoginCommand.Response>>
    {
        public string Provider { get; set; } = default!; // google, facebook
        public string AccessToken { get; set; } = default!; // Token t? Social SDK
        public string? DeviceId { get; set; }
        public string? DeviceType { get; set; }
        public string? FcmToken { get; set; }
    }

    #endregion

    #region Validator

    public sealed class SocialLoginValidator : AbstractValidator<SocialLoginCommand>
    {
        public SocialLoginValidator()
        {
            RuleFor(x => x.Provider)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "Provider"))
                .Must(p => p.ToLower() == "google" || p.ToLower() == "facebook")
                .WithMessage(CoreResource.auth_invalidLogin);

            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "AccessToken"));
        }
    }

    #endregion

    #region Handler

    public sealed class SocialLoginCommandHandler : IRequestHandler<SocialLoginCommand, ApiResponse<LoginCommand.Response>>
    {
        private readonly IIdentityServerClient _identityClient;
        private readonly SocialAuthProviderFactory _socialProviderFactory;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public SocialLoginCommandHandler(IIdentityServerClient identityClient, SocialAuthProviderFactory socialProviderFactory, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _identityClient = identityClient;
            _socialProviderFactory = socialProviderFactory;
            _configuration = configuration;
        }

        public async Task<ApiResponse<LoginCommand.Response>> Handle(SocialLoginCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo());
            // 1. L?y Provider t? Factory
            var provider = _socialProviderFactory.GetProvider(request.Provider);
            if (provider == null)
            {
                return ReturnError(CoreResource.auth_invalidLogin);
            }

            // 2. X�c th?c Social Token
            var socialProfile = await provider.VerifyTokenAsync(request.AccessToken, cancellationToken);
            if (socialProfile == null || string.IsNullOrEmpty(socialProfile.Email))
            {
                return ReturnError(CoreResource.auth_invalidLogin);
            }

            // 3. T�m ho?c t?o ngu?i d�ng
            var socialSecret = _configuration["SocialAuth:InternalSecret"] ?? "UniManage_Default_Secure_Secret_2026_@!";
            var user = await AuthHelper.GetOrCreateSocialUserAsync(socialProfile, socialSecret, log, cancellationToken);
            if (user == null || user.Status != CoreCommon.Value.Commonstatus.Active)
            {
                return ReturnError(CoreResource.auth_accountInactive);
            }

            // 4. Y�u c?u IdentityServer ph�t h�nh token (S? d?ng Secure Secret t? c?u h�nh)
            var (authSuccess, tokenData, identityError) = await _identityClient.RequestTokenAsync(
                user.Username!, socialSecret, cancellationToken);

            if (!authSuccess || tokenData == null)
            {
                return ReturnError(CoreResource.auth_invalidLogin);
            }

            // 5. Th�nh c�ng - C?p nh?t thi?t b?/FCM
            if (!string.IsNullOrEmpty(request.DeviceId))
            {
                await AuthHelper.UpdateDeviceTokenAsync(user.Id, request.DeviceId, request.FcmToken, request.DeviceType, log, cancellationToken);
            }

            var responseData = new LoginCommand.Response
            {
                AccessToken = tokenData.access_token,
                RefreshToken = tokenData.refresh_token,
                ExpiresIn = tokenData.expires_in,
                TokenType = tokenData.token_type,
                User = new LoginCommand.UserInfo
                {
                    Id = user.Id,
                    UserCode = user.Username!,
                    DisplayName = user.EmployeeCode ?? user.Username!,
                    Email = user.Email,
                    RoleCode = user.RoleCode
                }
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