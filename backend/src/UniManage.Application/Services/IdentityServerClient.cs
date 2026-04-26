using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using UniManage.Core.Logging;

namespace UniManage.Application.Services
{
    /// <summary>
    /// Response model from IdentityServer token endpoint.
    /// </summary>
    public class IdentityTokenResponse
    {
        /// <summary>
        /// Access token for resource access.
        /// </summary>
        public string access_token { get; set; } = string.Empty;

        /// <summary>
        /// Token lifetime in seconds.
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// Token type (usually Bearer).
        /// </summary>
        public string token_type { get; set; } = string.Empty;

        /// <summary>
        /// Refresh token to renew access token.
        /// </summary>
        public string refresh_token { get; set; } = string.Empty;

        /// <summary>
        /// Granted scopes.
        /// </summary>
        public string scope { get; set; } = string.Empty;
    }

    /// <summary>
    /// Shared interface for IdentityServer HTTP operations.
    /// Eliminates duplicate code in LoginCommand, RefreshTokenCommand, and LogoutCommand.
    /// </summary>
    public interface IIdentityServerClient
    {
        /// <summary>
        /// Requests a token from IdentityServer using password grant (ROPC).
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Result containing success status, token data, or error message.</returns>
        Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RequestTokenAsync(
            string username, string password, CancellationToken ct = default);

        /// <summary>
        /// Refreshes an Access Token using a Refresh Token.
        /// </summary>
        /// <param name="refreshToken">Current refresh token.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Result containing success status, new token data, or error message.</returns>
        Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RefreshTokenAsync(
            string refreshToken, CancellationToken ct = default);

        /// <summary>
        /// Revokes (invalidates) a Refresh Token upon logout.
        /// </summary>
        /// <param name="refreshToken">Refresh token to revoke.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Success status and optional error message.</returns>
        Task<(bool Success, string? Error)> RevokeTokenAsync(
            string refreshToken, CancellationToken ct = default);
    }

    /// <summary>
    /// Centralized HTTP client for interacting with IdentityServer.
    /// 
    /// [SECURITY] Design Decisions:
    /// - Uses IHttpClientFactory named client "IdentityServer" (H1).
    /// - Configuration loaded from injected IConfiguration (H2).
    /// - NO SSL bypass in any environment (C3).
    /// - Generic error messages returned to callers (Domain 9).
    /// </summary>
    public class IdentityServerClient : IIdentityServerClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _authority;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _scope;

        /// <summary>
        /// Initializes the IdentityServerClient with required configuration.
        /// </summary>
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
        /// Parses IdentityServer token response.
        /// [SECURITY] Domain 9 — Raw IdentityServer errors are never exposed to clients.
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
