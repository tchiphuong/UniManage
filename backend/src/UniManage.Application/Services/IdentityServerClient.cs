using Microsoft.Extensions.Configuration;
using UniManage.Core.Logging;

namespace UniManage.Application.Services
{
    /// <summary>
    /// Response model for IdentityServer token endpoint
    /// </summary>
    public class IdentityTokenResponse
    {
        public string access_token { get; set; } = string.Empty;
        public int expires_in { get; set; }
        public string token_type { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
    }

    /// <summary>
    /// Shared interface for all IdentityServer HTTP operations.
    /// Eliminates duplicate code across LoginCommand, RefreshTokenCommand, LogoutCommand.
    /// </summary>
    public interface IIdentityServerClient
    {
        /// <summary>
        /// Request token from IdentityServer using ROPC grant.
        /// </summary>
        Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RequestTokenAsync(
            string username, string password, CancellationToken ct = default);

        /// <summary>
        /// Refresh token from IdentityServer.
        /// </summary>
        Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RefreshTokenAsync(
            string refreshToken, CancellationToken ct = default);

        /// <summary>
        /// Revoke refresh token from IdentityServer.
        /// </summary>
        Task<(bool Success, string? Error)> RevokeTokenAsync(
            string refreshToken, CancellationToken ct = default);
    }

    /// <summary>
    /// Centralized IdentityServer HTTP client.
    /// 
    /// [SECURITY] Design decisions:
    /// - Uses IHttpClientFactory named client "IdentityServer" (H1)
    /// - Reads config from injected IConfiguration (H2)
    /// - NO SSL bypass in any environment (C3)
    /// - Generic error messages returned to caller (Domain 9)
    /// 
    /// SECURITY-TODO (C2): This class will be replaced when migrating
    /// from ROPC to Authorization Code + PKCE flow.
    /// </summary>
    public class IdentityServerClient : IIdentityServerClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _authority;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _scope;

        public IdentityServerClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;

            _authority = configuration["IdentityServer:Authority"] ?? "http://localhost:5001";
            _clientId = configuration["IdentityServer:ClientId"]
                ?? throw new InvalidOperationException("IdentityServer:ClientId is not configured");
            _clientSecret = configuration["IdentityServer:ClientSecret"]
                ?? throw new InvalidOperationException("IdentityServer:ClientSecret is not configured");
            _scope = configuration["IdentityServer:Scope"]
                ?? throw new InvalidOperationException("IdentityServer:Scope is not configured");
        }

        /// <inheritdoc />
        public async Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RequestTokenAsync(
            string username, string password, CancellationToken ct = default)
        {
            var tokenEndpoint = $"{_authority}/connect/token";

            using var client = _httpClientFactory.CreateClient("IdentityServer");

            var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("scope", _scope)
            }), ct);

            return await ParseTokenResponseAsync(response, ct);
        }

        /// <inheritdoc />
        public async Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RefreshTokenAsync(
            string refreshToken, CancellationToken ct = default)
        {
            var tokenEndpoint = $"{_authority}/connect/token";

            using var client = _httpClientFactory.CreateClient("IdentityServer");

            var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            }), ct);

            return await ParseTokenResponseAsync(response, ct);
        }

        /// <inheritdoc />
        public async Task<(bool Success, string? Error)> RevokeTokenAsync(
            string refreshToken, CancellationToken ct = default)
        {
            var revocationEndpoint = $"{_authority}/connect/revocation";

            using var client = _httpClientFactory.CreateClient("IdentityServer");

            var response = await client.PostAsync(revocationEndpoint, new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("token", refreshToken),
                new KeyValuePair<string, string>("token_type_hint", "refresh_token"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret)
            }), ct);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(ct);
                UniLogger.Warn($"[IdentityServerClient] Token revocation failed: {errorContent}");
                return (false, "Token revocation failed");
            }

            return (true, null);
        }

        /// <summary>
        /// Parse IdentityServer token response.
        /// [SECURITY] Domain 9 — Never expose raw IdentityServer errors to client.
        /// </summary>
        private static async Task<(bool Success, IdentityTokenResponse? Token, string? Error)> ParseTokenResponseAsync(
            HttpResponseMessage response, CancellationToken ct)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(ct);
                UniLogger.Warn($"[IdentityServerClient] Request failed: {errorContent}");
                return (false, null, "Authentication failed");
            }

            var content = await response.Content.ReadAsStringAsync(ct);
            var tokenData = System.Text.Json.JsonSerializer.Deserialize<IdentityTokenResponse>(content);

            if (tokenData == null)
            {
                UniLogger.Error("[IdentityServerClient] Failed to parse token response");
                return (false, null, "Failed to parse token response");
            }

            return (true, tokenData, null);
        }
    }
}
