using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Services;
using UniManage.Core.Services.Social;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;
using UniManage.Application.Services;

namespace UniManage.Application.Commands.System.Auth
{
    #region Command

    /// <summary>
    /// Command thực hiện đăng nhập qua mạng xã hội bằng Social Token từ Client SDK.
    /// </summary>
    public sealed class SocialLoginCommand : BaseCommand, IRequest<ApiResponse<LoginCommand.Response>>
    {
        public string Provider { get; set; } = default!; // google, facebook
        public string AccessToken { get; set; } = default!; // Token từ Social SDK
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

        public async Task<ApiResponse<LoginCommand.Response>> Handle(SocialLoginCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo());
                // 1. Lấy Provider từ Factory
                var provider = _socialProviderFactory.GetProvider(request.Provider);
                if (provider == null)
                {
                    return ReturnError(CoreResource.auth_invalidLogin, $"Unsupported social provider: {request.Provider}");
                }

                // 2. Xác thực Social Token
                var socialProfile = await provider.VerifyTokenAsync(request.AccessToken, ct);
                if (socialProfile == null || string.IsNullOrEmpty(socialProfile.Email))
                {
                    return ReturnError(CoreResource.auth_invalidLogin, "Failed to verify social token or email not found");
                }

                // 3. Tìm hoặc tạo người dùng
                var socialSecret = _configuration["SocialAuth:InternalSecret"] ?? "UniManage_Default_Secure_Secret_2026_@!";
                var user = await AuthHelper.GetOrCreateSocialUserAsync(socialProfile, socialSecret, log, ct);
                if (user == null || user.Status != CoreCommon.Value.Commonstatus.Active)
                {
                    return ReturnError(CoreResource.auth_accountInactive, "Social user account is inactive or could not be created");
                }
                
                // 4. Yêu cầu IdentityServer phát hành token (Sử dụng Secure Secret từ cấu hình)
                var (authSuccess, tokenData, identityError) = await _identityClient.RequestTokenAsync(
                    user.Username!, socialSecret, ct);

                if (!authSuccess || tokenData == null)
                {
                    return ReturnError(CoreResource.auth_invalidLogin, identityError);
                }

                // 5. Thành công - Cập nhật thiết bị/FCM
                if (!string.IsNullOrEmpty(request.DeviceId))
                {
                    await AuthHelper.UpdateDeviceTokenAsync(user.Id, request.DeviceId, request.FcmToken, request.DeviceType, log, ct);
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

            ApiResponse<LoginCommand.Response> ReturnError(string userMsg, string logMsg)
            {
                var errorResponse = ResponseHelper.Error<LoginCommand.Response>(userMsg);
return errorResponse;
            }
        }
    }

    #endregion
}


