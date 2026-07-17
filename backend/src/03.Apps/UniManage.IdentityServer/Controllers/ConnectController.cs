using Microsoft.AspNetCore.Mvc;
using UniManage.IdentityServer.Interfaces;
using UniManage.IdentityServer.Services;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;

namespace UniManage.IdentityServer.Controllers
{
    [ApiController]
    [Route("connect")]
    public class ConnectController : ControllerBase
    {
        private readonly IClientStore _clientStore;
        private readonly ITokenService _tokenService;
        private readonly IIdentityUserRepository _userRepository;

        public ConnectController(
            IClientStore clientStore,
            ITokenService tokenService,
            IIdentityUserRepository userRepository)
        {
            _clientStore = clientStore;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost("token")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Token([FromForm] IFormCollection form)
        {
            var grantType = form["grant_type"].FirstOrDefault();
            var clientId = form["client_id"].FirstOrDefault();
            var clientSecret = form["client_secret"].FirstOrDefault();

            if (string.IsNullOrEmpty(grantType) || string.IsNullOrEmpty(clientId))
            {
                Console.WriteLine("DEBUG: invalid_request - grantType or clientId missing");
                return BadRequest(new { error = "invalid_request" });
            }

            var client = await _clientStore.FindClientByIdAsync(clientId);
            if (client == null)
            {
                Console.WriteLine($"DEBUG: invalid_client - client {clientId} not found");
                return BadRequest(new { error = "invalid_client" });
            }

            if (client.RequireClientSecret && client.ClientSecret != clientSecret)
            {
                Console.WriteLine("DEBUG: invalid_client - invalid client secret");
                return BadRequest(new { error = "invalid_client", error_description = "Invalid client secret" });
            }

            if (!client.AllowedGrantTypes.Contains(grantType))
            {
                Console.WriteLine($"DEBUG: unsupported_grant_type - {grantType} not allowed for client {clientId}");
                return BadRequest(new { error = "unsupported_grant_type" });
            }

            var requestedScopes = (form["scope"].FirstOrDefault() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

            try
            {
                switch (grantType)
                {
                    case "password":
                        return await HandlePasswordGrantAsync(form, client, requestedScopes);
                    
                    case "client_credentials":
                        return await HandleClientCredentialsGrantAsync(client, requestedScopes);
                    
                    case "refresh_token":
                        return await HandleRefreshTokenGrantAsync(form, client);

                    case "social":
                        return await HandleSocialGrantAsync(form, client, requestedScopes);

                    default:
                        Console.WriteLine("DEBUG: unsupported_grant_type - default switch case");
                        return BadRequest(new { error = "unsupported_grant_type" });
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error processing token request for client {clientId} grant_type {grantType}", ex);
                return StatusCode(500, new { error = "server_error" });
            }
        }

        private async Task<IActionResult> HandlePasswordGrantAsync(IFormCollection form, Models.ClientModel client, string[] requestedScopes)
        {
            var username = form["username"].FirstOrDefault();
            var password = form["password"].FirstOrDefault();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("DEBUG: invalid_request - missing username or password");
                return BadRequest(new { error = "invalid_request", error_description = "Missing username or password" });
            }

            var user = await _userRepository.FindByUsernameAsync(username);
            if (user == null)
            {
                Console.WriteLine($"DEBUG: invalid_grant - user {username} not found");
                return BadRequest(new { error = "invalid_grant", error_description = "Invalid username or password" });
            }
            
            if (!PasswordHelper.VerifyPassword(password, user.Password))
            {
                Console.WriteLine($"DEBUG: invalid_grant - password mismatch for user {username}");
                return BadRequest(new { error = "invalid_grant", error_description = "Invalid username or password" });
            }

            var response = await _tokenService.IssueTokenForUserAsync(user, client, requestedScopes);
            return Ok(response);
        }

        private async Task<IActionResult> HandleClientCredentialsGrantAsync(Models.ClientModel client, string[] requestedScopes)
        {
            var response = await _tokenService.IssueTokenForClientAsync(client, requestedScopes);
            return Ok(response);
        }

        private async Task<IActionResult> HandleRefreshTokenGrantAsync(IFormCollection form, Models.ClientModel client)
        {
            var refreshToken = form["refresh_token"].FirstOrDefault();
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(new { error = "invalid_request", error_description = "Missing refresh token" });
            }

            var response = await _tokenService.RenewTokenAsync(refreshToken, client);
            if (!string.IsNullOrEmpty(response.Error))
            {
                return BadRequest(new { error = response.Error, error_description = response.ErrorDescription });
            }

            return Ok(response);
        }

        private async Task<IActionResult> HandleSocialGrantAsync(IFormCollection form, Models.ClientModel client, string[] requestedScopes)
        {
            var provider = form["provider"].FirstOrDefault();
            var providerToken = form["provider_token"].FirstOrDefault();

            if (string.IsNullOrEmpty(provider) || string.IsNullOrEmpty(providerToken))
            {
                return BadRequest(new { error = "invalid_request", error_description = "Missing provider or provider_token" });
            }

            // Note: In a full implementation, you would verify the providerToken via an ISocialAuthProvider here.
            // For now, we return bad request as it requires the Strategy Pattern implementation to verify the token.
            return BadRequest(new { error = "unsupported_grant_type", error_description = "Social login verification not implemented in this controller yet" });
        }

        [HttpGet("userinfo")]
        [Authorize]
        public IActionResult UserInfo()
        {
            var claims = User.Claims.ToDictionary(c => c.Type, c => c.Value);
            return Ok(claims);
        }
    }
}
