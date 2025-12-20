using Dapper;
using UniManage.Core.Database;
using UniManage.Model.Common;

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

        public static async Task<PagedResult> QueryPagingAsync(DbContext dbContext, string baseQuery, string whereClause, string orderByClause, object parameters, int pageIndex, int pageSize)
        {
            // 1. Validation cơ bản
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            // 2. Xử lý bắt buộc có ORDER BY cho hàm OFFSET
            if (string.IsNullOrWhiteSpace(orderByClause))
            {
                // Fallback: Nếu không có sort, SQL Server cần một cái gì đó để order trước khi offset
                // Lưu ý: Cách này có thể không ổn định thứ tự, tốt nhất nên bắt buộc truyền order by
                orderByClause = "ORDER BY (SELECT NULL)";
                // Hoặc throw exception yêu cầu dev phải truyền vào
                // throw new ArgumentException("ORDER BY clause is required for pagination.");
            }

            // 3. Get total count
            // Lưu ý: Thêm "WHERE 1=1" hoặc xử lý logic để đảm bảo cú pháp whereClause đúng (có chữ WHERE hay chưa)
            // Giả sử whereClause bạn truyền vào đã bao gồm chữ "WHERE" hoặc rỗng.
            var countQuery = $@"
                                SELECT COUNT(1)
                                FROM ({baseQuery}) AS TotalQuery
                                {whereClause}";

            var totalCount = await dbContext.connection.ExecuteScalarAsync<int>(countQuery, parameters, transaction: dbContext.transaction);

            // 4. Get data (chỉ chạy nếu có dữ liệu)
            List<dynamic> items = new List<dynamic>();
            if (totalCount > 0)
            {
                var dataQuery = $@"
                                    SELECT t.*
                                    FROM ({baseQuery}) AS DataQuery
                                    {whereClause}
                                    {orderByClause} 
                                    OFFSET @Skip ROWS 
                                    FETCH NEXT @Take ROWS ONLY";

                // Sử dụng DynamicParameters để merge tham số phân trang an toàn
                var dynamicParams = new DynamicParameters(parameters);
                dynamicParams.Add("@Skip", (pageIndex - 1) * pageSize);
                dynamicParams.Add("@Take", pageSize);

                var results = await dbContext.connection.QueryAsync(dataQuery, dynamicParams, transaction: dbContext.transaction);
                items = results.ToList();
            }

            return new PagedResult
            {
                Items = items,
                Paging = new PagingInfo
                {
                    TotalItems = totalCount,
                    PageSize = pageSize,
                    PageIndex = pageIndex // Nên trả về cả trang hiện tại
                }
            };
        }
    }
}