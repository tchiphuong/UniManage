using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Store quản lý các yêu cầu đăng nhập theo luồng Device Flow trong IdentityServer sử dụng Dapper.
    /// </summary>
    public class DapperDeviceFlowStore : IDeviceFlowStore
    {
        /// <summary>
        /// Lưu trữ thông tin xác thực thiết bị mới.
        /// </summary>
        /// <param name="deviceCode">Mã thiết bị.</param>
        /// <param name="userCode">Mã người dùng hiển thị.</param>
        /// <param name="data">Dữ liệu DeviceCode đi kèm.</param>
        public async Task StoreDeviceAuthorizationAsync(string deviceCode, string userCode, DeviceCode data)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = @"
                    INSERT INTO [dbo].[is_device_flows] 
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

        /// <summary>
        /// Tìm kiếm DeviceCode dựa trên mã người dùng (UserCode).
        /// </summary>
        /// <param name="userCode">Mã người dùng cung cấp.</param>
        /// <returns>Dữ liệu DeviceCode nếu tìm thấy, ngược lại trả về null.</returns>
        public async Task<DeviceCode?> FindByUserCodeAsync(string userCode)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = "SELECT [Data] FROM [dbo].[is_device_flows] WHERE [UserCode] = @UserCode";
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

        /// <summary>
        /// Tìm kiếm DeviceCode dựa trên mã thiết bị (DeviceCode).
        /// </summary>
        /// <param name="deviceCode">Mã thiết bị cung cấp.</param>
        /// <returns>Dữ liệu DeviceCode nếu tìm thấy, ngược lại trả về null.</returns>
        public async Task<DeviceCode?> FindByDeviceCodeAsync(string deviceCode)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = "SELECT [Data] FROM [dbo].[is_device_flows] WHERE [DeviceCode] = @DeviceCode";
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

        /// <summary>
        /// Cập nhật thông tin DeviceCode dựa trên mã người dùng.
        /// </summary>
        /// <param name="userCode">Mã người dùng.</param>
        /// <param name="data">Dữ liệu DeviceCode mới cần cập nhật.</param>
        public async Task UpdateByUserCodeAsync(string userCode, DeviceCode data)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = @"
                    UPDATE [dbo].[is_device_flows]
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

        /// <summary>
        /// Xóa thông tin Device Flow dựa trên mã thiết bị.
        /// </summary>
        /// <param name="deviceCode">Mã thiết bị cần xóa.</param>
        public async Task RemoveByDeviceCodeAsync(string deviceCode)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = "DELETE FROM [dbo].[is_device_flows] WHERE [DeviceCode] = @DeviceCode";
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


