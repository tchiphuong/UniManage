using Dapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Auth
{
    /// <summary>
    /// Login Command - Đăng nhập hệ thống
    /// </summary>
    public sealed class LoginCommand : BaseCommand, IRequest<ApiResponse<LoginCommand.Response>>
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;

        public class Response
        {
            /// <summary>
            /// Access Token
            /// </summary>
            public string AccessToken { get; set; } = string.Empty;
            /// <summary>
            /// Refresh Token
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
            /// <summary>
            /// User Information
            /// </summary>
            public UserInfo User { get; set; } = new UserInfo();
        }

        public class UserInfo
        {
            /// <summary>
            /// User ID
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// User Code
            /// </summary>
            public string UserCode { get; set; } = string.Empty;
            /// <summary>
            /// Display Name
            /// </summary>
            public string DisplayName { get; set; } = string.Empty;
            /// <summary>
            /// Email
            /// </summary>
            public string? Email { get; set; }
            /// <summary>
            /// Role Code
            /// </summary>
            public string? RoleCode { get; set; }
        }
    }

    /// <summary>
    /// Login Command Validator
    /// </summary>
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }

    /// <summary>
    /// Login Command Handler
    /// </summary>
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginCommand.Response>>
    {
        public async Task<ApiResponse<LoginCommand.Response>> Handle(LoginCommand request, CancellationToken ct)
        {
            try
            {
                UniLogger.Info($"[Login] Start processing login for user: {request.Username}");

                // Build configuration manually
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .Build();

                // 1. Call IdentityServer to get token
                UniLogger.Info($"[Login] Calling IdentityServer to get token for user: {request.Username}");

                var authority = configuration["IdentityServer:Authority"] ?? "http://localhost:5001";
                var clientId = configuration["IdentityServer:ClientId"];
                var clientSecret = configuration["IdentityServer:ClientSecret"];
                var scope = configuration["IdentityServer:Scope"];

                if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(scope))
                {
                    UniLogger.Error("[Login] Missing IdentityServer configuration (ClientId, ClientSecret, or Scope)");
                    return ResponseHelper.Error<LoginCommand.Response>("System configuration error");
                }

                var tokenEndpoint = $"{authority}/connect/token";
                UniLogger.Info($"[Login] Token Endpoint: {tokenEndpoint}");

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
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("username", request.Username),
                    new KeyValuePair<string, string>("password", request.Password),
                    new KeyValuePair<string, string>("scope", scope)
                }), ct);

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    var errorContent = await tokenResponse.Content.ReadAsStringAsync(ct);
                    UniLogger.Warn($"[Login] IdentityServer login failed for {request.Username}: {errorContent}");
                    return ResponseHelper.Error<LoginCommand.Response>("Invalid username or password");
                }

                UniLogger.Info($"[Login] IdentityServer returned success. Parsing token...");
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync(ct);
                var tokenData = global::System.Text.Json.JsonSerializer.Deserialize<IdentityTokenResponse>(tokenContent);

                if (tokenData == null)
                {
                    UniLogger.Error($"[Login] Failed to parse token response for {request.Username}");
                    return ResponseHelper.Error<LoginCommand.Response>("Failed to parse token response");
                }

                // 2. Get User Info from Database (to enrich response if needed, or just use token claims)
                UniLogger.Info($"[Login] Querying user info from database for user: {request.Username}");
                using (var dbContext = new DbContext())
                {
                    var sql = @"
                        SELECT TOP 1
                            [Id],
                            [UserName] AS UserCode,
                            [EmployeeCode],
                            [RoleCode],
                            [Email]
                        FROM [dbo].[sy_users]
                        WHERE [UserName] = @Username";

                    var user = await dbContext.connection.QueryFirstOrDefaultAsync<UserDto>(
                        sql,
                        new { request.Username });

                    if (user == null)
                    {
                        UniLogger.Warn($"[Login] User data not found in database for: {request.Username}");
                        return ResponseHelper.Error<LoginCommand.Response>("User data not found");
                    }

                    var response = new LoginCommand.Response
                    {
                        AccessToken = tokenData.access_token,
                        RefreshToken = tokenData.refresh_token,
                        ExpiresIn = tokenData.expires_in,
                        TokenType = tokenData.token_type,
                        User = new LoginCommand.UserInfo
                        {
                            Id = user.Id,
                            UserCode = user.UserCode,
                            DisplayName = user.EmployeeCode ?? user.UserCode,
                            Email = user.Email,
                            RoleCode = user.RoleCode
                        }
                    };

                    UniLogger.Info($"[Login] User {request.Username} logged in successfully via IdentityServer");
                    return ResponseHelper.Success(response, CoreResource.Auth_msg_LoginSuccess);
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"[Login] Error during login for {request.Username}", ex);
                return ResponseHelper.Error<LoginCommand.Response>("An error occurred during login");
            }
        }

        private class IdentityTokenResponse
        {
            public string access_token { get; set; } = string.Empty;
            public int expires_in { get; set; }
            public string token_type { get; set; } = string.Empty;
            public string refresh_token { get; set; } = string.Empty;
            public string scope { get; set; } = string.Empty;
        }

        private class UserDto
        {
            public long Id { get; set; }
            public string UserCode { get; set; } = default!;
            public string? EmployeeCode { get; set; }
            public string? RoleCode { get; set; }
            public string? Email { get; set; }
        }
    }
}
