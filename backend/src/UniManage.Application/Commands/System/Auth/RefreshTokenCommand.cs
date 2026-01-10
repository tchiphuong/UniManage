using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Auth
{
    /// <summary>
    /// Refresh Token Command - Làm mới access token
    /// </summary>
    public sealed class RefreshTokenCommand : BaseCommand, IRequest<ApiResponse<RefreshTokenCommand.Response>>
    {
        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        public class Response
        {
            /// <summary>
            /// Access Token mới
            /// </summary>
            public string AccessToken { get; set; } = string.Empty;
            /// <summary>
            /// Refresh Token mới
            /// </summary>
            public string RefreshToken { get; set; } = string.Empty;
            /// <summary>
            /// Expires In (seconds)
            /// </summary>
            public int ExpiresIn { get; set; }
            /// <summary>
            /// Token Type
            /// </summary>
            public string TokenType { get; set; } = "Bearer";
        }
    }

    /// <summary>
    /// Refresh Token Command Validator
    /// </summary>
    public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required");
        }
    }

    /// <summary>
    /// Refresh Token Command Handler
    /// </summary>
    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<RefreshTokenCommand.Response>>
    {
        public async Task<ApiResponse<RefreshTokenCommand.Response>> Handle(RefreshTokenCommand request, CancellationToken ct)
        {
            try
            {
                UniLogger.Info($"[RefreshToken] Start processing refresh token request");

                // Build configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .Build();

                var authority = configuration["IdentityServer:Authority"] ?? "http://localhost:5001";
                var clientId = configuration["IdentityServer:ClientId"];
                var clientSecret = configuration["IdentityServer:ClientSecret"];

                if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                {
                    UniLogger.Error("[RefreshToken] Missing IdentityServer configuration");
                    return ResponseHelper.Error<RefreshTokenCommand.Response>("System configuration error");
                }

                var tokenEndpoint = $"{authority}/connect/token";

                // Bypass SSL validation in Development
                var handler = new HttpClientHandler();
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development")
                {
                    handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                }

                using var client = new HttpClient(handler);

                var tokenResponse = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("refresh_token", request.RefreshToken)
                }), ct);

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    var errorContent = await tokenResponse.Content.ReadAsStringAsync(ct);
                    UniLogger.Warn($"[RefreshToken] IdentityServer refresh failed: {errorContent}");
                    return ResponseHelper.Error<RefreshTokenCommand.Response>("Invalid or expired refresh token");
                }

                var tokenContent = await tokenResponse.Content.ReadAsStringAsync(ct);
                var tokenData = global::System.Text.Json.JsonSerializer.Deserialize<IdentityTokenResponse>(tokenContent);

                if (tokenData == null)
                {
                    UniLogger.Error("[RefreshToken] Failed to parse token response");
                    return ResponseHelper.Error<RefreshTokenCommand.Response>("Failed to parse token response");
                }

                var response = new RefreshTokenCommand.Response
                {
                    AccessToken = tokenData.access_token,
                    RefreshToken = tokenData.refresh_token,
                    ExpiresIn = tokenData.expires_in,
                    TokenType = tokenData.token_type
                };

                UniLogger.Info("[RefreshToken] Token refreshed successfully");
                return ResponseHelper.Success(response, CoreResource.Auth_msg_TokenRefreshed);
            }
            catch (Exception ex)
            {
                UniLogger.Error("[RefreshToken] Error during token refresh", ex);
                return ResponseHelper.Error<RefreshTokenCommand.Response>("An error occurred during token refresh");
            }
        }

        private class IdentityTokenResponse
        {
            public string access_token { get; set; } = string.Empty;
            public int expires_in { get; set; }
            public string token_type { get; set; } = string.Empty;
            public string refresh_token { get; set; } = string.Empty;
        }
    }
}
