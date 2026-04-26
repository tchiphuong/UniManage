using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using Newtonsoft.Json;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Store quản lý các PersistedGrant (Authorization Code, Refresh Token, ...) trong IdentityServer sử dụng Dapper.
    /// </summary>
    public class DapperPersistedGrantStore : IPersistedGrantStore
    {
        /// <summary>
        /// Lưu trữ một PersistedGrant mới hoặc cập nhật nếu đã tồn tại.
        /// </summary>
        /// <param name="grant">Thông tin PersistedGrant cần lưu.</param>
        public async Task StoreAsync(PersistedGrant grant)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = @"
                    MERGE [dbo].[is_persisted_grants] AS Target
                    USING (SELECT @Key AS [Key]) AS Source
                    ON (Target.[Key] = Source.[Key])
                    WHEN MATCHED THEN
                        UPDATE SET
                            [Type] = @Type,
                            [SubjectId] = @SubjectId,
                            [SessionId] = @SessionId,
                            [ClientId] = @ClientId,
                            [Description] = @Description,
                            [CreationTime] = @CreationTime,
                            [Expiration] = @Expiration,
                            [ConsumedTime] = @ConsumedTime,
                            [Data] = @Data
                    WHEN NOT MATCHED THEN
                        INSERT ([Key], [Type], [SubjectId], [SessionId], [ClientId], [Description], [CreationTime], [Expiration], [ConsumedTime], [Data])
                        VALUES (@Key, @Type, @SubjectId, @SessionId, @ClientId, @Description, @CreationTime, @Expiration, @ConsumedTime, @Data);";

                await dbContext.ExecuteAsync(sql, new
                {
                    grant.Key,
                    grant.Type,
                    grant.SubjectId,
                    grant.SessionId,
                    grant.ClientId,
                    grant.Description,
                    grant.CreationTime,
                    grant.Expiration,
                    grant.ConsumedTime,
                    Data = grant.Data
                });

                await dbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error storing persisted grant {grant.Key}", ex);
            }
        }

        /// <summary>
        /// Lấy thông tin PersistedGrant dựa trên Key.
        /// </summary>
        /// <param name="key">Mã định danh duy nhất của grant.</param>
        /// <returns>Thông tin grant nếu tìm thấy, ngược lại trả về null.</returns>
        public async Task<PersistedGrant?> GetAsync(string key)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = "SELECT * FROM [dbo].[is_persisted_grants] WHERE [Key] = @Key";
                return await dbContext.QueryFirstOrDefaultAsync<PersistedGrant>(sql, new { Key = key });
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error getting persisted grant {key}", ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách PersistedGrant dựa trên bộ lọc.
        /// </summary>
        /// <param name="filter">Bộ lọc chứa SubjectId, ClientId, Type, ...</param>
        /// <returns>Danh sách các grant khớp với bộ lọc.</returns>
        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            try
            {
                using var dbContext = new DbContext();
                var conditions = new List<string>();
                var parameters = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(filter.SubjectId))
                {
                    conditions.Add("[SubjectId] = @SubjectId");
                    parameters.Add("SubjectId", filter.SubjectId);
                }
                if (!string.IsNullOrWhiteSpace(filter.SessionId))
                {
                    conditions.Add("[SessionId] = @SessionId");
                    parameters.Add("SessionId", filter.SessionId);
                }
                if (!string.IsNullOrWhiteSpace(filter.ClientId))
                {
                    conditions.Add("[ClientId] = @ClientId");
                    parameters.Add("ClientId", filter.ClientId);
                }
                if (!string.IsNullOrWhiteSpace(filter.Type))
                {
                    conditions.Add("[Type] = @Type");
                    parameters.Add("Type", filter.Type);
                }

                var sql = "SELECT * FROM [dbo].[is_persisted_grants]";
                if (conditions.Any())
                {
                    sql += " WHERE " + string.Join(" AND ", conditions);
                }

                return await dbContext.QueryAsync<PersistedGrant>(sql, parameters);
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error getting all persisted grants with filter", ex);
                return Enumerable.Empty<PersistedGrant>();
            }
        }

        /// <summary>
        /// Xóa một PersistedGrant dựa trên Key.
        /// </summary>
        /// <param name="key">Mã định danh duy nhất của grant cần xóa.</param>
        public async Task RemoveAsync(string key)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = "DELETE FROM [dbo].[is_persisted_grants] WHERE [Key] = @Key";
                await dbContext.ExecuteAsync(sql, new { Key = key });
                await dbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error removing persisted grant {key}", ex);
            }
        }

        /// <summary>
        /// Xóa danh sách PersistedGrant dựa trên bộ lọc.
        /// </summary>
        /// <param name="filter">Bộ lọc xác định các grant cần xóa.</param>
        public async Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var conditions = new List<string>();
                var parameters = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(filter.SubjectId))
                {
                    conditions.Add("[SubjectId] = @SubjectId");
                    parameters.Add("SubjectId", filter.SubjectId);
                }
                if (!string.IsNullOrWhiteSpace(filter.SessionId))
                {
                    conditions.Add("[SessionId] = @SessionId");
                    parameters.Add("SessionId", filter.SessionId);
                }
                if (!string.IsNullOrWhiteSpace(filter.ClientId))
                {
                    conditions.Add("[ClientId] = @ClientId");
                    parameters.Add("ClientId", filter.ClientId);
                }
                if (!string.IsNullOrWhiteSpace(filter.Type))
                {
                    conditions.Add("[Type] = @Type");
                    parameters.Add("Type", filter.Type);
                }

                if (!conditions.Any()) return; // Prevent deleting all records if no filter is provided

                var sql = "DELETE FROM [dbo].[is_persisted_grants] WHERE " + string.Join(" AND ", conditions);
                await dbContext.ExecuteAsync(sql, parameters);
                await dbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error removing all persisted grants with filter", ex);
            }
        }
    }
}
