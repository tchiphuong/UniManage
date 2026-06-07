using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;

namespace UniManage.Shared.Infrastructure.Utilities
{
    /// <summary>
    /// Shared utility functions for database interactions.
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Checks if a record exists by a specific field and value.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="fieldName">Field name to check.</param>
        /// <param name="value">Value to look for.</param>
        /// <param name="excludeId">Optional ID to exclude (e.g., for updates).</param>
        /// <returns>True if at least one matching record exists.</returns>
        public static async Task<bool> RecordExistsAsync(string tableName, string fieldName, object value, int? excludeId = null)
        {
            using var dbContext = new UniManage.Shared.Infrastructure.Database.DbContext();

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
        /// Checks if a username already exists in the system.
        /// </summary>
        /// <param name="userCode">Username to check.</param>
        /// <param name="excludeId">Optional ID to exclude.</param>
        /// <returns>True if the username exists.</returns>
        public static async Task<bool> UserCodeExistsAsync(string userCode, int? excludeId = null)
        {
            return await RecordExistsAsync("SyUsers", "UserName", userCode, excludeId);
        }

        /// <summary>
        /// Checks if an employee code already exists in the system.
        /// </summary>
        /// <param name="employeeCode">Employee code to check.</param>
        /// <param name="excludeId">Optional ID to exclude.</param>
        /// <returns>True if the employee code exists.</returns>
        public static async Task<bool> EmployeeCodeExistsAsync(string employeeCode, int? excludeId = null)
        {
            return await RecordExistsAsync("SyUsers", "EmployeeCode", employeeCode, excludeId);
        }

        /// <summary>
        /// Checks if an email address already exists in the system.
        /// </summary>
        /// <param name="email">Email address to check.</param>
        /// <param name="excludeId">Optional ID to exclude.</param>
        /// <returns>True if the email exists.</returns>
        public static async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await RecordExistsAsync("SyUsers", "Email", email, excludeId);
        }

        /// <summary>
        /// Retrieves the next sequence number for a specific table's field.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="sequenceField">Sequence field name (defaults to Id).</param>
        /// <returns>The calculated next increment value.</returns>
        public static async Task<int> GetNextSequenceAsync(string tableName, string sequenceField = "Id")
        {
            using var dbContext = new UniManage.Shared.Infrastructure.Database.DbContext();

            var sql = $"SELECT ISNULL(MAX([{sequenceField}]), 0) + 1 FROM [{tableName}]";
            return await dbContext.ExecuteScalarAsync<int>(sql);
        }

        /// <summary>
        /// Executes a single SQL command within a database transaction.
        /// </summary>
        /// <param name="sql">SQL command string.</param>
        /// <param name="parameters">Command parameters.</param>
        /// <returns>Number of rows affected.</returns>
        public static async Task<int> ExecuteWithTransactionAsync(string sql, object? parameters = null)
        {
            using var dbContext = new UniManage.Shared.Infrastructure.Database.DbContext(openTransaction: true);

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
        /// Executes multiple SQL commands within a single database transaction.
        /// </summary>
        /// <param name="queries">Collection of SQL commands and their corresponding parameters.</param>
        /// <returns>Total number of rows affected across all commands.</returns>
        public static async Task<int> ExecuteMultipleWithTransactionAsync(IEnumerable<(string Sql, object? Parameters)> queries)
        {
            using var dbContext = new UniManage.Shared.Infrastructure.Database.DbContext(openTransaction: true);

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
        /// Constructs a SQL WHERE clause and parameters from filter dictionaries.
        /// </summary>
        /// <param name="filters">Dictionary of fields and values to filter by.</param>
        /// <param name="keyword">Optional search keyword.</param>
        /// <param name="searchFields">Optional comma-separated fields for keyword searching.</param>
        /// <returns>A tuple containing the WHERE clause string and the populated DynamicParameters.</returns>
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

            // Keyword search with OR logic across multiple fields
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
        /// Generic paginated database query with automatic filtering, sorting, and pagination logic.
        /// </summary>
        /// <typeparam name="TResult">Model type for result mapping.</typeparam>
        /// <param name="dbContext">Database context instance.</param>
        /// <param name="baseQuery">Base SELECT query (excluding WHERE/ORDER BY).</param>
        /// <param name="request">Query request containing pagination and filtering metadata.</param>
        /// <param name="filters">Optional additional custom filters.</param>
        /// <param name="defaultSortColumn">Optional default sort column (auto-detects CreatedAt if omitted).</param>
        /// <returns>A paginated result containing items and paging metadata.</returns>
        public static async Task<PagedResult<TResult>> QueryPagingAsync<TResult>(this UniManage.Shared.Infrastructure.Database.DbContext dbContext, StringBuilder baseQuery, BaseListQuery request, Dictionary<string, object?>? filters = null, string? defaultSortColumn = null) where TResult : class
        {
            // Build WHERE clause
            filters ??= new Dictionary<string, object?>();
            var (whereClause, parameters) = BuildWhereClause(
                filters,
                request.Keyword,
                request.SearchFields);

            // Extract pagination metadata
            var pageIndex = request.PageIndex < 1 ? 1 : request.PageIndex;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            // Generate ORDER BY clause
            var columnMappings = typeof(TResult)
                .GetProperties()
                .ToDictionary(
                    p => char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1), // camelCase
                    p => p.Name); // PascalCase

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

            // Fetch total record count
            var countQuery = $@"
                SELECT COUNT(1)
                FROM ({baseQuery}) AS TotalQuery
                {whereClause}";

            var totalCount = await dbContext.ExecuteScalarAsync<int>(countQuery, parameters);

            // Map columns for selection
            var selectCols = string.Join(", ", typeof(TResult).GetProperties().Select(p => $"[{p.Name}]"));

            // Fetch data
            List<TResult> items = new List<TResult>();
            if (totalCount > 0)
            {
                var dataQuery = $@"
                    SELECT {selectCols}
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

        /// <summary>
        /// Explicit paginated database query for dynamic result types.
        /// </summary>
        public static async Task<PagedResult<dynamic>> QueryPagingAsync(UniManage.Shared.Infrastructure.Database.DbContext dbContext, string baseQuery, string whereClause, object parameters, BaseListQuery request, Type? resultType = null, string? defaultSortColumn = null)
        {
            var pageIndex = request.PageIndex < 1 ? 1 : request.PageIndex;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            string orderByClause;
            if (resultType != null)
            {
                var columnMappings = resultType
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

                var (generatedOrderBy, _) = QueryHelper.BuildOrderByClause(
                    request.SortBy,
                    request.SortDirection ?? "DESC",
                    columnMappings);
                orderByClause = generatedOrderBy;
            }
            else
            {
                orderByClause = "ORDER BY (SELECT NULL)";
            }

            var countQuery = $@"
                                SELECT COUNT(1)
                                FROM ({baseQuery}) AS TotalQuery
                                {whereClause}";

            var totalCount = await dbContext.ExecuteScalarAsync<int>(countQuery, parameters);

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
                    PageIndex = pageIndex
                }
            };
        }

        /// <summary>
        /// Generic explicit paginated database query.
        /// </summary>
        public static async Task<PagedResult<TResult>> QueryPagingAsync<TResult>(
            this UniManage.Shared.Infrastructure.Database.DbContext dbContext, 
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


