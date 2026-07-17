using System.Text.Json.Serialization;

namespace UniManage.Shared.Domain.Interfaces
{
    public class IdentityTokenResponse
    {
        [JsonPropertyName("accessToken")]
        public string access_token { get; set; } = string.Empty;
        
        [JsonPropertyName("expiresIn")]
        public int expires_in { get; set; }
        
        [JsonPropertyName("tokenType")]
        public string token_type { get; set; } = string.Empty;
        
        [JsonPropertyName("refreshToken")]
        public string refresh_token { get; set; } = string.Empty;
        
        [JsonPropertyName("scope")]
        public string scope { get; set; } = string.Empty;
    }

    public interface IIdentityServerClient
    {
        Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RequestTokenAsync(
            string username, string password, CancellationToken ct = default);

        Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RefreshTokenAsync(
            string refreshToken, CancellationToken ct = default);

        Task<(bool Success, string? Error)> RevokeTokenAsync(
            string refreshToken, CancellationToken ct = default);
    }
}
