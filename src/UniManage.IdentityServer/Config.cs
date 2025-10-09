using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Collections.Generic;

namespace UniManage.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "api1" },
                    AllowOfflineAccess = true ,// Cho phép refresh token
                    AccessTokenLifetime = 3600, // Token sống 1 tiếng
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 2592000 // 30 ngày
                },
                // Cho phép user test đăng nhập với ResourceOwnerPassword
                new Client
                {
                    ClientId = "testuser",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "api1", "openid", "profile", "offline_access" },
                    AllowOfflineAccess = true ,// Cho phép refresh token
                    AccessTokenLifetime = 3600, // Token sống 1 tiếng
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 2592000 // 30 ngày
                }
            };

        // Thêm user test cho ResourceOwnerPassword grant type
        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "testuser",
                    Password = "testpassword",
                    Claims =
                    {
                        new System.Security.Claims.Claim("name", "Test User"),
                        new System.Security.Claims.Claim("role", "Admin"),
                        new System.Security.Claims.Claim("email", "testuser@unimanage.local")
                    }
                }
            };
    }
}
