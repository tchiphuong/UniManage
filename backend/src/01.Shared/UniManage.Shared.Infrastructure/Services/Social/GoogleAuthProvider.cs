using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using UniManage.Shared.Domain.Interfaces;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.Shared.Infrastructure.Services.Social
{
    /// <summary>
    /// Triển khai xác thực Google ID Token
    /// </summary>
    public class GoogleAuthProvider : UniManage.Shared.Domain.Interfaces.ISocialAuthProvider
    {
        private readonly string _googleClientId;

        public string ProviderName => "google";

        public GoogleAuthProvider(IConfiguration configuration)
        {
            _googleClientId = configuration["SocialAuth:GoogleClientId"] ?? string.Empty;
        }

        public async Task<UniManage.Shared.Domain.Interfaces.SocialUserProfile?> VerifyTokenAsync(string token, CancellationToken ct = default)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);

                if (payload == null) return null;

                return new UniManage.Shared.Domain.Interfaces.SocialUserProfile
                {
                    Provider = ProviderName,
                    ProviderUserId = payload.Subject,
                    Email = payload.Email,
                    Name = payload.Name,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    Picture = payload.Picture
                };
            }
            catch (InvalidJwtException ex)
            {
                UniLogger.Warn($"[GoogleAuth] Invalid ID Token: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"[GoogleAuth] Unexpected error verify token: {ex.Message}");
                return null;
            }
        }
    }
}
