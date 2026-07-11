using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UniManage.IdentityServer.Interfaces;

namespace UniManage.IdentityServer.Controllers
{
    [ApiController]
    [Route(".well-known")]
    public class DiscoveryController : ControllerBase
    {
        private readonly IKeyManagementService _keyManagementService;
        private readonly IConfiguration _configuration;
        private readonly string _issuer;

        public DiscoveryController(IKeyManagementService keyManagementService, IConfiguration configuration)
        {
            _keyManagementService = keyManagementService;
            _configuration = configuration;
            _issuer = _configuration["IdentityServer:Authority"] ?? "https://identity.unimanage.com";
        }

        [HttpGet("openid-configuration")]
        public IActionResult GetConfiguration()
        {
            var config = new
            {
                issuer = _issuer,
                jwks_uri = $"{_issuer}/.well-known/jwks",
                authorization_endpoint = $"{_issuer}/connect/authorize",
                token_endpoint = $"{_issuer}/connect/token",
                userinfo_endpoint = $"{_issuer}/connect/userinfo",
                end_session_endpoint = $"{_issuer}/connect/endsession",
                scopes_supported = new[] { "openid", "profile", "email", "offline_access" },
                claims_supported = new[] { "sub", "name", "email", "role" },
                grant_types_supported = new[] { "password", "client_credentials", "refresh_token" },
                response_types_supported = new[] { "code", "token", "id_token" },
                token_endpoint_auth_methods_supported = new[] { "client_secret_post" }
            };

            return Ok(config);
        }

        [HttpGet("jwks")]
        public IActionResult GetJwks()
        {
            var jwks = _keyManagementService.GetJwks();
            return Ok(jwks);
        }
    }
}
