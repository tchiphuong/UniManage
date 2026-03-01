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

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("UniManage.Api", "UniManage API")
                {
                    Scopes = { "unimanage.api" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "unimanage-client",
                    ClientName = "UniManage Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    // Hỗ trợ LoginCommand (Password)
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes = { "openid", "profile", "unimanage.api" },
                    
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600
                }
            };
    }
}
