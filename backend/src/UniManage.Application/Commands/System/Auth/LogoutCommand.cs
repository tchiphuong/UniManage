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
    /// Logout Command - Đăng xuất và revoke token
    /// </summary>
    public sealed class LogoutCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Refresh Token cần revoke
        /// </summary>
        public string? RefreshToken { get; set; }
    }

    /// <summary>
    /// Logout Command Validator
    /// </summary>
    public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            // RefreshToken is optional
        }
    }

    /// <summary>
    /// Logout Command Handler
    /// </summary>
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(LogoutCommand request, CancellationToken ct)
        {
            try
            {
                UniLogger.Info("[Logout] Start processing logout request");

                // Nếu không có refresh token, chỉ return success (client-side logout)
                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    UniLogger.Info("[Logout] No refresh token provided, client-side logout only");
                    return ResponseHelper.Success(true, CoreResource.Auth_msg_LogoutSuccess);
                }

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
                    UniLogger.Error("[Logout] Missing IdentityServer configuration");
                    return ResponseHelper.Error<bool>("System configuration error");
                }

                var revocationEndpoint = $"{authority}/connect/revocation";

                // Bypass SSL validation in Development
                var handler = new HttpClientHandler();
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development")
                {
                    handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                }

                using var client = new HttpClient(handler);

                var revocationResponse = await client.PostAsync(revocationEndpoint, new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("token", request.RefreshToken),
                    new KeyValuePair<string, string>("token_type_hint", "refresh_token"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret)
                }), ct);

                if (!revocationResponse.IsSuccessStatusCode)
                {
                    var errorContent = await revocationResponse.Content.ReadAsStringAsync(ct);
                    UniLogger.Warn($"[Logout] Token revocation failed: {errorContent}");
                    // Still return success as token might already be expired/revoked
                }

                UniLogger.Info("[Logout] Logout successful");
                return ResponseHelper.Success(true, CoreResource.Auth_msg_LogoutSuccess);
            }
            catch (Exception ex)
            {
                UniLogger.Error("[Logout] Error during logout", ex);
                // Return success anyway to allow client to clear tokens
                return ResponseHelper.Success(true, CoreResource.Auth_msg_LogoutSuccess);
            }
        }
    }
}
