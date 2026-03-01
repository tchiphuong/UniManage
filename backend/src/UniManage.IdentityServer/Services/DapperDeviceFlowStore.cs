using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using UniManage.Core.Database;
using UniManage.Core.Logging;

namespace UniManage.IdentityServer.Services
{
    public class DapperDeviceFlowStore : IDeviceFlowStore
    {
        public async Task StoreDeviceAuthorizationAsync(string deviceCode, string userCode, DeviceCode data)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = @"
                    INSERT INTO [dbo].[sy_is_device_flows] 
                        ([UserCode], [DeviceCode], [SubjectId], [SessionId], [ClientId], [Description], [CreationTime], [Expiration], [Data])
                    VALUES 
                        (@UserCode, @DeviceCode, @SubjectId, @SessionId, @ClientId, @Description, @CreationTime, @Expiration, @Data)";

                await dbContext.ExecuteAsync(sql, new
                {
                    UserCode = userCode,
                    DeviceCode = deviceCode,
                    SubjectId = data.Subject?.FindFirst(Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId)?.Value,
                    SessionId = data.SessionId,
                    ClientId = data.ClientId,
                    Description = data.Description,
                    CreationTime = data.CreationTime,
                    Expiration = data.CreationTime.AddSeconds(data.Lifetime), // Giả định
                    Data = System.Text.Json.JsonSerializer.Serialize(data)
                });

                await dbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error storing device authorization {deviceCode}", ex);
            }
        }

        public async Task<DeviceCode?> FindByUserCodeAsync(string userCode)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = "SELECT [Data] FROM [dbo].[sy_is_device_flows] WHERE [UserCode] = @UserCode";
                var dataStr = await dbContext.QueryFirstOrDefaultAsync<string>(sql, new { UserCode = userCode });

                if (string.IsNullOrEmpty(dataStr)) return null;

                return System.Text.Json.JsonSerializer.Deserialize<DeviceCode>(dataStr);
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error finding device code by user code {userCode}", ex);
                return null;
            }
        }

        public async Task<DeviceCode?> FindByDeviceCodeAsync(string deviceCode)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = "SELECT [Data] FROM [dbo].[sy_is_device_flows] WHERE [DeviceCode] = @DeviceCode";
                var dataStr = await dbContext.QueryFirstOrDefaultAsync<string>(sql, new { DeviceCode = deviceCode });

                if (string.IsNullOrEmpty(dataStr)) return null;

                return System.Text.Json.JsonSerializer.Deserialize<DeviceCode>(dataStr);
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error finding device by device code {deviceCode}", ex);
                return null;
            }
        }

        public async Task UpdateByUserCodeAsync(string userCode, DeviceCode data)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = @"
                    UPDATE [dbo].[sy_is_device_flows]
                    SET [Data] = @Data,
                        [SubjectId] = @SubjectId,
                        [SessionId] = @SessionId
                    WHERE [UserCode] = @UserCode";

                await dbContext.ExecuteAsync(sql, new
                {
                    UserCode = userCode,
                    SubjectId = data.Subject?.FindFirst(Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId)?.Value,
                    SessionId = data.SessionId,
                    Data = System.Text.Json.JsonSerializer.Serialize(data)
                });

                await dbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error updating device flow by user code {userCode}", ex);
            }
        }

        public async Task RemoveByDeviceCodeAsync(string deviceCode)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = "DELETE FROM [dbo].[sy_is_device_flows] WHERE [DeviceCode] = @DeviceCode";
                await dbContext.ExecuteAsync(sql, new { DeviceCode = deviceCode });
                await dbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error removing device flow by device code {deviceCode}", ex);
            }
        }
    }
}
