using Dapper;
using UniManage.Core.Database;
using UniManage.Core.Models;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Common database utility functions
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Check if record exists by field value
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="fieldName">Field name to check</param>
        /// <param name="value">Value to check</param>
        /// <param name="excludeId">ID to exclude from check (for updates)</param>
        /// <returns>True if record exists</returns>
        public static async Task<bool> RecordExistsAsync(string tableName, string fieldName, object value, int? excludeId = null)
        {
            using var dbContext = new DbContext();

            var sql = $"SELECT COUNT(*) FROM [{tableName}] WHERE [{fieldName}] = @Value";
            var parameters = new DynamicParameters();
            parameters.Add("Value", value);

            if (excludeId.HasValue)
            {
                sql += " AND [Id] != @ExcludeId";
                parameters.Add("ExcludeId", excludeId.Value);
            }

            var count = await dbContext.connection.ExecuteScalarAsync<int>(sql, parameters);
            return count > 0;
        }

        /// <summary>
        /// Check if user code already exists
        /// </summary>
        /// <param name="userCode">User code to check</param>
        /// <param name="excludeId">ID to exclude (for updates)</param>
        /// <returns>True if exists</returns>
        public static async Task<bool> UserCodeExistsAsync(string userCode, int? excludeId = null)
        {
            return await RecordExistsAsync("sy_users", "UserName", userCode, excludeId);
        }

        /// <summary>
        /// Check if employee code already exists
        /// </summary>
        /// <param name="employeeCode">Employee code to check</param>
        /// <param name="excludeId">ID to exclude (for updates)</param>
        /// <returns>True if exists</returns>
        public static async Task<bool> EmployeeCodeExistsAsync(string employeeCode, int? excludeId = null)
        {
            return await RecordExistsAsync("sy_users", "EmployeeCode", employeeCode, excludeId);
        }

        /// <summary>
        /// Check if email already exists
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <param name="excludeId">ID to exclude (for updates)</param>
        /// <returns>True if exists</returns>
        public static async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await RecordExistsAsync("sy_users", "Email", email, excludeId);
        }

        /// <summary>
        /// Get next sequence number for a table
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="sequenceField">Sequence field name</param>
        /// <returns>Next sequence number</returns>
        public static async Task<int> GetNextSequenceAsync(string tableName, string sequenceField = "Id")
        {
            using var dbContext = new DbContext();

            var sql = $"SELECT ISNULL(MAX([{sequenceField}]), 0) + 1 FROM [{tableName}]";
            return await dbContext.connection.ExecuteScalarAsync<int>(sql);
        }

        /// <summary>
        /// Execute query with transaction
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Number of affected rows</returns>
        public static async Task<int> ExecuteWithTransactionAsync(string sql, object? parameters = null)
        {
            using var dbContext = new DbContext(openTransaction: true);

            try
            {
                var result = await dbContext.connection.ExecuteAsync(sql, parameters, dbContext.transaction);
                await dbContext.CommitAsync();
                return result;
            }
            catch
            {
                await dbContext.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Execute multiple queries in one transaction
        /// </summary>
        /// <param name="queries">List of SQL queries with parameters</param>
        /// <returns>Total affected rows</returns>
        public static async Task<int> ExecuteMultipleWithTransactionAsync(IEnumerable<(string Sql, object? Parameters)> queries)
        {
            using var dbContext = new DbContext(openTransaction: true);

            try
            {
                var totalRows = 0;

                foreach (var (sql, parameters) in queries)
                {
                    totalRows += await dbContext.connection.ExecuteAsync(sql, parameters, dbContext.transaction);
                }

                await dbContext.CommitAsync();
                return totalRows;
            }
            catch
            {
                await dbContext.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Build WHERE clause from filter parameters
        /// </summary>
        /// <param name="filters">Dictionary of field filters</param>
        /// <returns>WHERE clause and parameters</returns>
        public static (string WhereClause, DynamicParameters Parameters) BuildWhereClause(Dictionary<string, object?> filters)
        {
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            foreach (var (field, value) in filters)
            {
                if (value == null) continue;

                if (value is string stringValue)
                {
                    if (!string.IsNullOrWhiteSpace(stringValue))
                    {
                        conditions.Add($"[{field}] LIKE @{field}");
                        parameters.Add(field, $"%{stringValue}%");
                    }
                }
                else
                {
                    conditions.Add($"[{field}] = @{field}");
                    parameters.Add(field, value);
                }
            }

            var whereClause = conditions.Any() ? "WHERE " + string.Join(" AND ", conditions) : "";
            return (whereClause, parameters);
        }

        /// <summary>
        /// Execute paginated query with total count
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <param name="baseQuery">Base SELECT query</param>
        /// <param name="whereClause">WHERE clause</param>
        /// <param name="orderByClause">ORDER BY clause</param>
        /// <param name="parameters">Query parameters</param>
        /// <param name="pageIndex">Page number (1-based)</param>
        /// <param name="pageSize">Items per page</param>
        /// <returns>Paginated results with total count</returns>
        public static async Task<(IReadOnlyList<T> Items, int TotalCount)> QueryPagingAsync<T>(
            string baseQuery,
            string whereClause,
            string orderByClause,
            object parameters,
            int pageIndex,
            int pageSize)
        {
            using (var db = new DbContext())
            {
                // Get total count
                var countQuery = $@"
                    SELECT COUNT(*)
                    FROM ({baseQuery}) t
                    {whereClause}";

                var totalCount = await db.connection.ExecuteScalarAsync<int>(countQuery, parameters);

                // Get paginated data
                var dataQuery = $@"
                    SELECT t.*
                    FROM ({baseQuery}) t
                    {whereClause}
                    {orderByClause} 
                    OFFSET {(pageIndex - 1) * pageSize} ROWS 
                    FETCH NEXT {pageSize} ROWS ONLY";

                var results = await db.connection.QueryAsync<T>(dataQuery, parameters);

                return (results.ToList(), totalCount);
            }
        }
    }
}