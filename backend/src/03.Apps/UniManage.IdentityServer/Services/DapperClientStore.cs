using Dapper;
using UniManage.IdentityServer.Interfaces;
using UniManage.IdentityServer.Models;
using UniManage.Shared.Infrastructure.Database;
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
                        [ClientSecret], 
                        [AllowedGrantTypes], 
                        [AllowedScopes], 
                        [AccessTokenLifetime], 
                        [AllowOfflineAccess],
                        [IdentityTokenLifetime], 
                        [AuthorizationCodeLifetime],
                        [AbsoluteRefreshTokenLifetime], 
                        [SlidingRefreshTokenLifetime],
                        [RefreshTokenUsage], 
                        [RefreshTokenExpiration], 
                        [RequireClientSecret], 
                        [Description]
                    FROM [dbo].[is_clients]
                    WHERE [ClientId] = @ClientId";

                var clientDto = await dbContext.QueryFirstOrDefaultAsync<ClientDbModel>(sql, new { ClientId = clientId });

                if (clientDto == null) return null;

                var client = new ClientModel
                {
                    ClientId = clientDto.ClientId,
                    ClientName = clientDto.ClientName,
                    Description = clientDto.Description,
                    AllowedGrantTypes = clientDto.AllowedGrantTypes?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>(),
                    AllowedScopes = clientDto.AllowedScopes?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>(),
                    AccessTokenLifetime = clientDto.AccessTokenLifetime,
                    AllowOfflineAccess = clientDto.AllowOfflineAccess,
                    RefreshTokenLifetime = clientDto.AbsoluteRefreshTokenLifetime > 0 ? clientDto.AbsoluteRefreshTokenLifetime : 2592000,
                    RequireClientSecret = clientDto.RequireClientSecret,
                    ClientSecret = clientDto.ClientSecret
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


