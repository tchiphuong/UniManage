using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using UniManage.IdentityServer.Interfaces;
using UniManage.Shared.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;

namespace UniManage.IdentityServer.Services
{
    public class KeyManagementService : IKeyManagementService
    {
        private readonly RsaSecurityKey _rsaSecurityKey;

        public KeyManagementService(IConfiguration configuration)
        {
            var rsaKeyPem = configuration["IdentityServer:RsaPrivateKeyPem"];
            var rsa = RSA.Create();

            if (!string.IsNullOrEmpty(rsaKeyPem))
            {
                try
                {
                    rsa.ImportFromPem(rsaKeyPem);
                    UniLogger.Info("Loaded RSA Private Key from configuration.");
                }
                catch (Exception ex)
                {
                    UniLogger.Error("Failed to load RSA Private Key from config. Generating a temporary key.", ex);
                    rsa = RSA.Create(2048);
                }
            }
            else
            {
                UniLogger.Warn("No RSA Private Key found in configuration. Generating a temporary key for this session (NOT FOR PRODUCTION).");
                rsa = RSA.Create(2048);
            }

            _rsaSecurityKey = new RsaSecurityKey(rsa)
            {
                KeyId = "unimanage-auth-key-1"
            };
        }

        public RsaSecurityKey GetSigningKey()
        {
            return _rsaSecurityKey;
        }

        public JsonWebKeySet GetJwks()
        {
            var jwk = JsonWebKeyConverter.ConvertFromRSASecurityKey(_rsaSecurityKey);
            jwk.Use = "sig";
            jwk.Alg = SecurityAlgorithms.RsaSha256;

            var jwks = new JsonWebKeySet();
            jwks.Keys.Add(jwk);
            return jwks;
        }
    }
}
