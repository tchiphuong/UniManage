using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using UniManage.Core.Logging;

namespace UniManage.Core.Services.Social
{
    /// <summary>
    /// Triển khai xác thực Google ID Token
    /// </summary>
    public class GoogleAuthProvider : ISocialAuthProvider
    {
        private readonly string _googleClientId;

        public string ProviderName => "google";

        public GoogleAuthProvider(IConfiguration configuration)
        {
            _googleClientId = configuration["SocialAuth:GoogleClientId"] ?? string.Empty;
        }

        public async Task<SocialUserProfile?> VerifyTokenAsync(string token, CancellationToken ct = default)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);

                if (payload == null) return null;

                return new SocialUserProfile
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
