using Dapper;
using System.Text;
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

            var count = await dbContext.ExecuteScalarAsync<int>(sql, parameters);
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
            return await dbContext.ExecuteScalarAsync<int>(sql);
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
                var result = await dbContext.ExecuteAsync(sql, parameters);
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
                    totalRows += await dbContext.ExecuteAsync(sql, parameters);
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
        /// <param name="keyword">Keyword for searching (optional)</param>
        /// <param name="searchFields">Fields to search when keyword is provided (optional, comma-separated)</param>
        /// <returns>WHERE clause and parameters</returns>
        public static (string WhereClause, DynamicParameters Parameters) BuildWhereClause(
            Dictionary<string, object?> filters,
            string? keyword = null,
            string? searchFields = null)
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

            // Handle keyword search with multiple fields (OR logic)
            if (!string.IsNullOrEmpty(keyword))
            {
                var fieldsArray = string.IsNullOrEmpty(searchFields)
                    ? new[] { "Username", "Email", "DisplayName" } // Default search fields
                    : searchFields.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var searchConditions = fieldsArray
                    .Select(field => $"[{QueryHelper.EscapeSqlIdentifier(field)}] LIKE @Keyword")
                    .ToList();

                if (searchConditions.Any())
                {
                    conditions.Add($"({string.Join(" OR ", searchConditions)})");
                    parameters.Add("Keyword", $"%{QueryHelper.SanitizeSearchTerm(keyword)}%");
                }
            }

            var whereClause = conditions.Any() ? "WHERE " + string.Join(" AND ", conditions) : "";
            return (whereClause, parameters);
        }

        /// <summary>
        /// Execute paginated query with automatic filtering, sorting, and pagination
        /// </summary>
        /// <typeparam name="TResult">Result type for auto-generating column mappings</typeparam>
        /// <param name="dbContext">Database context</param>
        /// <param name="baseQuery">Base SELECT query without WHERE/ORDER BY</param>
        /// <param name="request">Query request with pagination and filtering info</param>
        /// <param name="filters">Additional custom filters (optional)</param>
        /// <param name="defaultSortColumn">Default sort column name (optional, auto-detect CreatedAt if not provided)</param>
        /// <returns>Paged result with items and paging info</returns>
        public static async Task<PagedResult<TResult>> QueryPagingAsync<TResult>(this DbContext dbContext, StringBuilder baseQuery, BaseListQuery request, Dictionary<string, object?>? filters = null, string? defaultSortColumn = null) where TResult : class
        {
            // Build WHERE clause from request and custom filters
            filters ??= new Dictionary<string, object?>();
            var (whereClause, parameters) = BuildWhereClause(
                filters,
                request.Keyword,
                request.SearchFields);

            // Extract pagination/sorting from request
            var pageIndex = request.PageIndex < 1 ? 1 : request.PageIndex;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            // Auto-generate ORDER BY clause from TResult type
            var columnMappings = typeof(TResult)
                .GetProperties()
                .ToDictionary(
                    p => char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1), // camelCase
                    p => p.Name); // PascalCase

            // Set default sort column (auto-detect CreatedAt if not provided)
            if (!string.IsNullOrEmpty(defaultSortColumn))
            {
                columnMappings["default"] = defaultSortColumn;
            }
            else if (columnMappings.ContainsKey("createdAt"))
            {
                columnMappings["default"] = columnMappings["createdAt"];
            }
            else
            {
                // Fallback to first property
                columnMappings["default"] = columnMappings.Values.FirstOrDefault() ?? "Id";
            }

            var (orderByClause, _) = QueryHelper.BuildOrderByClause(
                request.SortBy,
                request.SortDirection ?? "DESC",
                columnMappings);

            // Get total count
            var countQuery = $@"
                SELECT COUNT(1)
                FROM ({baseQuery}) AS TotalQuery
                {whereClause}";

            var totalCount = await dbContext.ExecuteScalarAsync<int>(countQuery, parameters);

            // Get data (only if there's data)
            List<TResult> items = new List<TResult>();
            if (totalCount > 0)
            {
                var dataQuery = $@"
                    SELECT *
                    FROM ({baseQuery}) AS DataQuery
                    {whereClause}
                    {orderByClause}
                    OFFSET @Skip ROWS
                    FETCH NEXT @Take ROWS ONLY";

                // Use DynamicParameters to merge pagination parameters safely
                var dynamicParams = new DynamicParameters(parameters);
                dynamicParams.Add("@Skip", (pageIndex - 1) * pageSize);
                dynamicParams.Add("@Take", pageSize);

                // Use Generic QueryAsync<TResult> to ensure Dapper maps to Class (enabling JSON CamelCase)
                var results = await dbContext.QueryAsync<TResult>(dataQuery, dynamicParams);
                items = results.ToList();
            }

            return new PagedResult<TResult>
            {
                Items = items,
                Paging = new PagingInfo
                {
                    TotalItems = totalCount,
                    PageSize = pageSize,
                    PageIndex = pageIndex
                }
            };
        }

        /// <summary>
        /// Execute paginated query with explicit WHERE clause and ORDER BY (Dynamic)
        /// </summary>
        public static async Task<PagedResult<dynamic>> QueryPagingAsync(DbContext dbContext, string baseQuery, string whereClause, object parameters, BaseListQuery request, Type? resultType = null, string? defaultSortColumn = null)
        {
            // 1. Validation cơ bản - extract from request
            var pageIndex = request.PageIndex < 1 ? 1 : request.PageIndex;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            // 2. Auto-generate ORDER BY clause
            string orderByClause;
            if (resultType != null)
            {
                // Auto-generate column mappings from result type properties
                var columnMappings = resultType
                    .GetProperties()
                    .ToDictionary(
                        p => char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1), // camelCase
                        p => p.Name); // PascalCase

                // Set default sort column
                if (!string.IsNullOrEmpty(defaultSortColumn))
                {
                    columnMappings["default"] = defaultSortColumn;
                }
                else if (columnMappings.ContainsKey("createdAt"))
                {
                    columnMappings["default"] = columnMappings["createdAt"];
                }

                var (generatedOrderBy, _) = QueryHelper.BuildOrderByClause(
                    request.SortBy,
                    request.SortDirection ?? "DESC",
                    columnMappings);
                orderByClause = generatedOrderBy;
            }
            else
            {
                // Fallback: Nếu không có sort, SQL Server cần một cái gì đó để order trước khi offset
                orderByClause = "ORDER BY (SELECT NULL)";
            }

            // 3. Get total count
            // Lưu ý: Thêm "WHERE 1=1" hoặc xử lý logic để đảm bảo cú pháp whereClause đúng (có chữ WHERE hay chưa)
            // Giả sử whereClause bạn truyền vào đã bao gồm chữ "WHERE" hoặc rỗng.
            var countQuery = $@"
                                SELECT COUNT(1)
                                FROM ({baseQuery}) AS TotalQuery
                                {whereClause}";

            var totalCount = await dbContext.ExecuteScalarAsync<int>(countQuery, parameters);

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

                var results = await dbContext.QueryAsync<dynamic>(dataQuery, dynamicParams);
                items = results.ToList();
            }

            return new PagedResult<dynamic>
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
        /// <summary>
        /// Execute paginated query with explicit WHERE clause and ORDER BY (Generic)
        /// </summary>
        public static async Task<PagedResult<TResult>> QueryPagingAsync<TResult>(
            this DbContext dbContext, 
            string baseQuery, 
            string whereClause, 
            object parameters, 
            BaseListQuery request, 
            string? defaultSortColumn = null) where TResult : class
        {
            var pageIndex = request.PageIndex < 1 ? 1 : request.PageIndex;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            var columnMappings = typeof(TResult)
                .GetProperties()
                .ToDictionary(
                    p => char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1), 
                    p => p.Name);

            if (!string.IsNullOrEmpty(defaultSortColumn))
            {
                columnMappings["default"] = defaultSortColumn;
            }
            else if (columnMappings.ContainsKey("createdAt"))
            {
                columnMappings["default"] = columnMappings["createdAt"];
            }
            else
            {
                columnMappings["default"] = columnMappings.Values.FirstOrDefault() ?? "Id";
            }

            var (orderByClause, _) = QueryHelper.BuildOrderByClause(
                request.SortBy,
                request.SortDirection ?? "DESC",
                columnMappings);

            var countQuery = $@"
                SELECT COUNT(1)
                FROM ({baseQuery}) AS TotalQuery
                {whereClause}";

            var totalCount = await dbContext.ExecuteScalarAsync<int>(countQuery, parameters);

            List<TResult> items = new List<TResult>();
            if (totalCount > 0)
            {
                var dataQuery = $@"
                    SELECT *
                    FROM ({baseQuery}) AS DataQuery
                    {whereClause}
                    {orderByClause} 
                    OFFSET @Skip ROWS 
                    FETCH NEXT @Take ROWS ONLY";

                var dynamicParams = new DynamicParameters(parameters);
                dynamicParams.Add("@Skip", (pageIndex - 1) * pageSize);
                dynamicParams.Add("@Take", pageSize);

                var results = await dbContext.QueryAsync<TResult>(dataQuery, dynamicParams);
                items = results.ToList();
            }

            return new PagedResult<TResult>
            {
                Items = items,
                Paging = new PagingInfo
                {
                    TotalItems = totalCount,
                    PageSize = pageSize,
                    PageIndex = pageIndex
                }
            };
        }
    }
}