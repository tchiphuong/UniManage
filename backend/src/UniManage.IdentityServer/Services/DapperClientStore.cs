using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using UniManage.Core.Database;
using UniManage.Core.Logging;

namespace UniManage.IdentityServer.Services
{
    public class DapperClientStore : IClientStore
    {
        public async Task<Client?> FindClientByIdAsync(string clientId)
        {
            try
            {
                using var dbContext = new DbContext();
                
                var sql = @"
                    SELECT TOP 1 
                        [ClientId], [ClientName], [ClientSecret], [AllowedGrantTypes], 
                        [AllowedScopes], [AccessTokenLifetime], [AllowOfflineAccess],
                        [IdentityTokenLifetime], [AuthorizationCodeLifetime],
                        [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime],
                        [RefreshTokenUsage], [RefreshTokenExpiration], [RequireClientSecret], [Description]
                    FROM [dbo].[sy_is_clients]
                    WHERE [ClientId] = @ClientId";

                var clientDto = await dbContext.QueryFirstOrDefaultAsync<ClientDto>(sql, new { ClientId = clientId });

                if (clientDto == null) return null;

                var client = new Client
                {
                    ClientId = clientDto.ClientId,
                    ClientName = clientDto.ClientName,
                    Description = clientDto.Description,
                    AllowedGrantTypes = clientDto.AllowedGrantTypes?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>(),
                    AllowedScopes = clientDto.AllowedScopes?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>(),
                    AccessTokenLifetime = clientDto.AccessTokenLifetime,
                    AllowOfflineAccess = clientDto.AllowOfflineAccess,
                    IdentityTokenLifetime = clientDto.IdentityTokenLifetime,
                    AuthorizationCodeLifetime = clientDto.AuthorizationCodeLifetime,
                    AbsoluteRefreshTokenLifetime = clientDto.AbsoluteRefreshTokenLifetime,
                    SlidingRefreshTokenLifetime = clientDto.SlidingRefreshTokenLifetime,
                    RefreshTokenUsage = (TokenUsage)clientDto.RefreshTokenUsage,
                    RefreshTokenExpiration = (TokenExpiration)clientDto.RefreshTokenExpiration,
                    RequireClientSecret = clientDto.RequireClientSecret
                };

                if (!string.IsNullOrEmpty(clientDto.ClientSecret))
                {
                    client.ClientSecrets = new List<Secret> { new Secret(clientDto.ClientSecret) };
                }

                return client;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error fetching client {clientId} from Dapper store", ex);
                return null;
            }
        }

        private class ClientDto
        {
            public string ClientId { get; set; } = default!;
            public string? ClientName { get; set; }
            public string? ClientSecret { get; set; }
            public string? AllowedGrantTypes { get; set; }
            public string? AllowedScopes { get; set; }
            public int AccessTokenLifetime { get; set; }
            public bool AllowOfflineAccess { get; set; }
            public int IdentityTokenLifetime { get; set; }
            public int AuthorizationCodeLifetime { get; set; }
            public int AbsoluteRefreshTokenLifetime { get; set; }
            public int SlidingRefreshTokenLifetime { get; set; }
            public int RefreshTokenUsage { get; set; }
            public int RefreshTokenExpiration { get; set; }
            public bool RequireClientSecret { get; set; }
            public string? Description { get; set; }
        }
    }
}
