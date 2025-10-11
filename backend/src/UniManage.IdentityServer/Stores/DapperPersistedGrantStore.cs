using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using UniManage.Core.Database;

namespace UniManage.IdentityServer.Stores
{
    /// <summary>
    /// Dapper implementation of IPersistedGrantStore using DbContext from Core
    /// Lưu trữ tokens, refresh tokens, authorization codes vào database
    /// </summary>
    public class DapperPersistedGrantStore : IPersistedGrantStore
    {
        public async Task StoreAsync(PersistedGrant grant)
        {
            using var dbContext = new DbContext(openTransaction: true);

            try
            {
                var sql = @"
                    MERGE INTO [dbo].[is_persisted_grants] AS target
                    USING (SELECT @Key AS [Key]) AS source
                    ON target.[Key] = source.[Key]
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

                await dbContext.connection.ExecuteAsync(sql, grant, dbContext.transaction);
                await dbContext.CommitAsync();
            }
            catch
            {
                await dbContext.RollbackAsync();
                throw;
            }
        }

        public async Task<PersistedGrant?> GetAsync(string key)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT TOP 1 [Key], [Type], [SubjectId], [SessionId], [ClientId], [Description], [CreationTime], [Expiration], [ConsumedTime], [Data]
                FROM [dbo].[is_persisted_grants]
                WHERE [Key] = @Key";

            return await dbContext.connection.QueryFirstOrDefaultAsync<PersistedGrant>(sql, new { Key = key });
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            using var dbContext = new DbContext();

            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(filter.SubjectId))
            {
                conditions.Add("[SubjectId] = @SubjectId");
                parameters.Add("SubjectId", filter.SubjectId);
            }

            if (!string.IsNullOrEmpty(filter.SessionId))
            {
                conditions.Add("[SessionId] = @SessionId");
                parameters.Add("SessionId", filter.SessionId);
            }

            if (!string.IsNullOrEmpty(filter.ClientId))
            {
                conditions.Add("[ClientId] = @ClientId");
                parameters.Add("ClientId", filter.ClientId);
            }

            if (!string.IsNullOrEmpty(filter.Type))
            {
                conditions.Add("[Type] = @Type");
                parameters.Add("Type", filter.Type);
            }

            var whereClause = conditions.Any()
                ? "WHERE " + string.Join(" AND ", conditions)
                : "";

            var sql = $@"
                SELECT [Key], [Type], [SubjectId], [SessionId], [ClientId], [Description], [CreationTime], [Expiration], [ConsumedTime], [Data]
                FROM [dbo].[is_persisted_grants]
                {whereClause}
                ORDER BY [CreationTime] DESC";

            return await dbContext.connection.QueryAsync<PersistedGrant>(sql, parameters);
        }

        public async Task RemoveAsync(string key)
        {
            using var dbContext = new DbContext(openTransaction: true);

            try
            {
                var sql = "DELETE FROM [dbo].[is_persisted_grants] WHERE [Key] = @Key";
                await dbContext.connection.ExecuteAsync(sql, new { Key = key }, dbContext.transaction);
                await dbContext.CommitAsync();
            }
            catch
            {
                await dbContext.RollbackAsync();
                throw;
            }
        }

        public async Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            using var dbContext = new DbContext(openTransaction: true);

            try
            {
                var conditions = new List<string>();
                var parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(filter.SubjectId))
                {
                    conditions.Add("[SubjectId] = @SubjectId");
                    parameters.Add("SubjectId", filter.SubjectId);
                }

                if (!string.IsNullOrEmpty(filter.SessionId))
                {
                    conditions.Add("[SessionId] = @SessionId");
                    parameters.Add("SessionId", filter.SessionId);
                }

                if (!string.IsNullOrEmpty(filter.ClientId))
                {
                    conditions.Add("[ClientId] = @ClientId");
                    parameters.Add("ClientId", filter.ClientId);
                }

                if (!string.IsNullOrEmpty(filter.Type))
                {
                    conditions.Add("[Type] = @Type");
                    parameters.Add("Type", filter.Type);
                }

                if (!conditions.Any())
                {
                    // Không cho phép xóa tất cả nếu không có filter
                    throw new InvalidOperationException("Must provide at least one filter condition");
                }

                var whereClause = "WHERE " + string.Join(" AND ", conditions);
                var sql = $"DELETE FROM [dbo].[is_persisted_grants] {whereClause}";

                await dbContext.connection.ExecuteAsync(sql, parameters, dbContext.transaction);
                await dbContext.CommitAsync();
            }
            catch
            {
                await dbContext.RollbackAsync();
                throw;
            }
        }
    }
}
