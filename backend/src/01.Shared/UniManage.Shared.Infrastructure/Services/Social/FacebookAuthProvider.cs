using Newtonsoft.Json;
using UniManage.Shared.Domain.Interfaces;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.Shared.Infrastructure.Services.Social
{
    /// <summary>
    /// Triển khai xác thực Facebook Access Token qua Graph API
    /// </summary>
    public class FacebookAuthProvider : UniManage.Shared.Domain.Interfaces.ISocialAuthProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public string ProviderName => "facebook";

        public FacebookAuthProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UniManage.Shared.Domain.Interfaces.SocialUserProfile?> VerifyTokenAsync(string token, CancellationToken ct = default)
        {
            try
            {
                // Gọi API profile của Facebook
                var url = $"https://graph.facebook.com/me?fields=id,name,email,first_name,last_name,picture&access_token={token}";
                
                using var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(url, ct);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync(ct);
                    UniLogger.Warn($"[FacebookAuth] Verify failed: {error}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync(ct);
                var fbUser = JsonConvert.DeserializeObject<dynamic>(json);

                if (fbUser == null) return null;

                return new UniManage.Shared.Domain.Interfaces.SocialUserProfile
                {
                    Provider = ProviderName,
                    ProviderUserId = fbUser.id,
                    Email = fbUser.email ?? $"{fbUser.id}@facebook.com",
                    Name = fbUser.name,
                    FirstName = fbUser.first_name,
                    LastName = fbUser.last_name,
                    Picture = fbUser.picture?.data?.url
                };
            }
            catch (Exception ex)
            {
                UniLogger.Error($"[FacebookAuth] Error: {ex.Message}");
                return null;
            }
        }
    }
}

