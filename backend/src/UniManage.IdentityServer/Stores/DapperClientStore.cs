using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Newtonsoft.Json;
using UniManage.Core.Database;

namespace UniManage.IdentityServer.Stores
{
    /// <summary>
    /// Dapper implementation of IClientStore
    /// </summary>
    public class DapperClientStore : IClientStore
    {
        public async Task<Client?> FindClientByIdAsync(string clientId)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT TOP 1
                    [ClientId],
                    [ClientName],
                    [ClientSecrets],
                    [AllowedGrantTypes],
                    [AllowedScopes],
                    [RedirectUris],
                    [PostLogoutRedirectUris],
                    [AllowOfflineAccess],
                    [AccessTokenLifetime],
                    [RefreshTokenUsage],
                    [RefreshTokenExpiration],
                    [SlidingRefreshTokenLifetime],
                    [AbsoluteRefreshTokenLifetime],
                    [Enabled],
                    [RequirePkce],
                    [AllowPlainTextPkce]
                FROM [dbo].[is_clients]
                WHERE [ClientId] = @ClientId
                    AND [Enabled] = 1";

            var clientDto = await dbContext.connection.QueryFirstOrDefaultAsync<ClientDto>(sql, new { ClientId = clientId });

            if (clientDto == null)
                return null;

            return MapToClient(clientDto);
        }

        private Client MapToClient(ClientDto dto)
        {
            var client = new Client
            {
                ClientId = dto.ClientId,
                ClientName = dto.ClientName,
                Enabled = dto.Enabled,
                RequirePkce = dto.RequirePkce,
                AllowPlainTextPkce = dto.AllowPlainTextPkce,
                AllowOfflineAccess = dto.AllowOfflineAccess,
                AccessTokenLifetime = dto.AccessTokenLifetime,
                RefreshTokenUsage = (TokenUsage)dto.RefreshTokenUsage,
                RefreshTokenExpiration = (TokenExpiration)dto.RefreshTokenExpiration,
                SlidingRefreshTokenLifetime = dto.SlidingRefreshTokenLifetime,
                AbsoluteRefreshTokenLifetime = dto.AbsoluteRefreshTokenLifetime
            };

            // Parse JSON arrays
            if (!string.IsNullOrEmpty(dto.ClientSecrets))
            {
                var secrets = JsonConvert.DeserializeObject<List<SecretDto>>(dto.ClientSecrets);
                client.ClientSecrets = secrets?.Select(s => new Secret(s.Value, s.Description ?? "")).ToList()
                    ?? new List<Secret>();
            }

            if (!string.IsNullOrEmpty(dto.AllowedGrantTypes))
            {
                client.AllowedGrantTypes = JsonConvert.DeserializeObject<List<string>>(dto.AllowedGrantTypes)
                    ?? new List<string>();
            }

            if (!string.IsNullOrEmpty(dto.AllowedScopes))
            {
                client.AllowedScopes = JsonConvert.DeserializeObject<List<string>>(dto.AllowedScopes)
                    ?? new List<string>();
            }

            if (!string.IsNullOrEmpty(dto.RedirectUris))
            {
                client.RedirectUris = JsonConvert.DeserializeObject<List<string>>(dto.RedirectUris)
                    ?? new List<string>();
            }

            if (!string.IsNullOrEmpty(dto.PostLogoutRedirectUris))
            {
                client.PostLogoutRedirectUris = JsonConvert.DeserializeObject<List<string>>(dto.PostLogoutRedirectUris)
                    ?? new List<string>();
            }

            return client;
        }

        private class ClientDto
        {
            public string ClientId { get; set; } = default!;
            public string? ClientName { get; set; }
            public string? ClientSecrets { get; set; }
            public string AllowedGrantTypes { get; set; } = default!;
            public string? AllowedScopes { get; set; }
            public string? RedirectUris { get; set; }
            public string? PostLogoutRedirectUris { get; set; }
            public bool AllowOfflineAccess { get; set; }
            public int AccessTokenLifetime { get; set; }
            public int RefreshTokenUsage { get; set; }
            public int RefreshTokenExpiration { get; set; }
            public int SlidingRefreshTokenLifetime { get; set; }
            public int AbsoluteRefreshTokenLifetime { get; set; }
            public bool Enabled { get; set; }
            public bool RequirePkce { get; set; }
            public bool AllowPlainTextPkce { get; set; }
        }

        private class SecretDto
        {
            public string Value { get; set; } = default!;
            public string? Type { get; set; }
            public string? Description { get; set; }
        }
    }
}
