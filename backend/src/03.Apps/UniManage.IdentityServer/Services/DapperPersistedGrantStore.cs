using Dapper;
using UniManage.IdentityServer.Interfaces;
using UniManage.IdentityServer.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Store quản lý các PersistedGrant (Refresh Token) sử dụng Dapper.
    /// </summary>
    public class DapperPersistedGrantStore : IPersistedGrantStore
    {
        /// <summary>
        /// Lưu trữ một PersistedGrant mới hoặc cập nhật nếu đã tồn tại.
        /// </summary>
        /// <param name="grant">Thông tin PersistedGrant cần lưu.</param>
        public async Task StoreAsync(PersistedGrantModel grant)
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
                            [CreationTime] = @CreationTime,
                            [Expiration] = @Expiration,
                            [ConsumedTime] = @ConsumedTime,
                            [Data] = @Data
                    WHEN NOT MATCHED THEN
                        INSERT (
                            [Key], 
                            [Type], 
                            [SubjectId], 
                            [SessionId], 
                            [ClientId], 
                            [CreationTime], 
                            [Expiration], 
                            [ConsumedTime], 
                            [Data]
                        )
                        VALUES (
                            @Key, 
                            @Type, 
                            @SubjectId, 
                            @SessionId, 
                            @ClientId, 
                            @CreationTime, 
                            @Expiration, 
                            @ConsumedTime, 
                            @Data
                        );";

                await dbContext.ExecuteAsync(sql, new
                {
                    grant.Key,
                    grant.Type,
                    grant.SubjectId,
                    grant.SessionId,
                    grant.ClientId,
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
        public async Task<PersistedGrantModel?> GetAsync(string key)
        {
            try
            {
                using var dbContext = new DbContext();
                var sql = @"
                    SELECT 
                        [Key], 
                        [Type], 
                        [SubjectId], 
                        [SessionId], 
                        [ClientId], 
                        [CreationTime], 
                        [Expiration], 
                        [ConsumedTime], 
                        [Data] 
                    FROM [dbo].[is_persisted_grants] 
                    WHERE [Key] = @Key";
                return await dbContext.QueryFirstOrDefaultAsync<PersistedGrantModel>(sql, new { Key = key });
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error getting persisted grant {key}", ex);
                return null;
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
        /// Xóa danh sách PersistedGrant dựa trên SubjectId, ClientId và Type.
        /// </summary>
        /// <param name="subjectId">Id của User.</param>
        /// <param name="clientId">Mã định danh Client.</param>
        /// <param name="type">Loại Token (ví dụ: refresh_token).</param>
        public async Task RemoveAllAsync(string? subjectId, string clientId, string? type = null)
        {
            try
            {
                using var dbContext = new DbContext(openTransaction: true);
                var sql = new System.Text.StringBuilder("DELETE FROM [dbo].[is_persisted_grants] WHERE [ClientId] = @ClientId");
                var parameters = new DynamicParameters();
                parameters.Add("ClientId", clientId);

                if (!string.IsNullOrWhiteSpace(subjectId))
                {
                    sql.Append(" AND [SubjectId] = @SubjectId");
                    parameters.Add("SubjectId", subjectId);
                }
                
                if (!string.IsNullOrWhiteSpace(type))
                {
                    sql.Append(" AND [Type] = @Type");
                    parameters.Add("Type", type);
                }

                await dbContext.ExecuteAsync(sql.ToString(), parameters);
                await dbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error removing all persisted grants with filter", ex);
            }
        }
    }
}


