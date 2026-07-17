using UniManage.Shared.Infrastructure.Database;
using Dapper;
using UniManage.IdentityServer.Interfaces;
using UniManage.IdentityServer.Models;
using UniManage.Shared.Domain.Interfaces;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Store quản lý Client trong IdentityServer sử dụng Dapper.
    /// </summary>
    public class DapperClientStore : IClientStore
    {
        /// <summary>
        /// Tìm kiếm thông tin Client dựa trên ClientId.
        /// </summary>
        /// <param name="clientId">Mã định danh của Client.</param>
        /// <returns>Thông tin Client nếu tìm thấy, ngược lại trả về null.</returns>
        public async Task<ClientModel?> FindClientByIdAsync(string clientId)
        {
            try
            {
                using var dbContext = new DbContext();
                
                var sql = @"
                    SELECT TOP 1 
                        [ClientId], 
                        [ClientName], 
                        [ClientSecrets] as ClientSecret, 
                        [AllowedGrantTypes], 
                        [AllowedScopes], 
                        [AccessTokenLifetime], 
                        [AllowOfflineAccess],
                        [AbsoluteRefreshTokenLifetime], 
                        [SlidingRefreshTokenLifetime],
                        [RefreshTokenUsage], 
                        [RefreshTokenExpiration]
                    FROM [dbo].[is_clients]
                    WHERE [ClientId] = @ClientId";

                var clientModel = await dbContext.QueryFirstOrDefaultAsync<ClientDbModel>(sql, new { ClientId = clientId });

                if (clientModel == null) return null;

                var client = new ClientModel
                {
                    ClientId = clientModel.ClientId,
                    ClientName = clientModel.ClientName,
                    Description = "",
                    AllowedGrantTypes = clientModel.AllowedGrantTypes?.Replace("[", "").Replace("]", "").Replace("\"", "").Split(',').Select(s => s.Trim()).ToList() ?? new List<string>(),
                    AllowedScopes = clientModel.AllowedScopes?.Replace("[", "").Replace("]", "").Replace("\"", "").Split(',').Select(s => s.Trim()).ToList() ?? new List<string>(),
                    AccessTokenLifetime = clientModel.AccessTokenLifetime,
                    AllowOfflineAccess = clientModel.AllowOfflineAccess,
                    RefreshTokenLifetime = clientModel.AbsoluteRefreshTokenLifetime > 0 ? clientModel.AbsoluteRefreshTokenLifetime : 2592000,
                    RequireClientSecret = false, // Not in DB, default to false or implement logic
                    ClientSecret = "secret" // Temp workaround or parse from JSON
                };

                return client;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error fetching client {clientId} from Dapper store", ex);
                return null;
            }
        }

        /// <summary>
        /// DTO thu gọn để map dữ liệu từ bảng is_clients.
        /// </summary>
        private class ClientDbModel
        {
            public string ClientId { get; set; } = default!;
            public string? ClientName { get; set; }
            public string? ClientSecret { get; set; }
            public string? AllowedGrantTypes { get; set; }
            public string? AllowedScopes { get; set; }
            public int AccessTokenLifetime { get; set; }
            public bool AllowOfflineAccess { get; set; }
            public int AbsoluteRefreshTokenLifetime { get; set; }
            public int SlidingRefreshTokenLifetime { get; set; }
            public int RefreshTokenUsage { get; set; }
            public int RefreshTokenExpiration { get; set; }
        }
    }
}


