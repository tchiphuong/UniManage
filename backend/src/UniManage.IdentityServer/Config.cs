using Duende.IdentityServer.Models;

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
                new ApiScope("unimanage.api", "UniManage API"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "unimanage-client",
                    ClientName = "UniManage Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    // Hỗ trợ LoginCommand (Password) và Frontend (Code)
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes = { "openid", "profile", "unimanage.api" },
                    
                    // Cấu hình cho Frontend (nếu dùng Code flow sau này)
                    RedirectUris = { "http://localhost:5173/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5173/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600
                }
            };
    }
}
