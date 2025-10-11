namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Helper functions for building SQL queries dynamically
    /// </summary>
    public static class QueryHelper
    {
        /// <summary>
        /// Builds ORDER BY clause with column mapping and direction validation
        /// </summary>
        /// <param name="sortBy">Column name to sort by</param>
        /// <param name="sortDirection">Sort direction (ASC/DESC)</param>
        /// <param name="columnMappings">Dictionary mapping sort keys to actual column names</param>
        /// <returns>Tuple containing ORDER BY clause and parameters</returns>
        public static (string orderBy, object parameters) BuildOrderByClause(
            string? sortBy,
            string? sortDirection,
            Dictionary<string, string> columnMappings)
        {
            var sortDir = (sortDirection?.ToUpper() == "DESC") ? "DESC" : "ASC";
            var defaultColumn = columnMappings.GetValueOrDefault("default", "(SELECT NULL)");

            if (string.IsNullOrEmpty(sortBy) || !columnMappings.ContainsKey(sortBy))
            {
                return ($"ORDER BY {defaultColumn} {sortDir}", new { });
            }

            return ($"ORDER BY {columnMappings[sortBy]} {sortDir}", new { });
        }

        /// <summary>
        /// Builds WHERE clause from filter dictionary
        /// </summary>
        /// <param name="filters">Dictionary of field names and values</param>
        /// <param name="tableName">Table name prefix for columns</param>
        /// <returns>WHERE clause string and parameters</returns>
        public static (string whereClause, Dictionary<string, object> parameters) BuildWhereClause(
            Dictionary<string, object> filters,
            string? tableName = null)
        {
            if (filters == null || filters.Count == 0)
                return ("", new Dictionary<string, object>());

            var whereConditions = new List<string>();
            var parameters = new Dictionary<string, object>();

            foreach (var filter in filters)
            {
                if (filter.Value != null)
                {
                    var columnName = string.IsNullOrEmpty(tableName)
                        ? filter.Key
                        : $"{tableName}.{filter.Key}";

                    if (filter.Value is string stringValue && !string.IsNullOrWhiteSpace(stringValue))
                    {
                        whereConditions.Add($"{columnName} LIKE @{filter.Key}");
                        parameters.Add(filter.Key, $"%{stringValue}%");
                    }
                    else if (filter.Value is not string)
                    {
                        whereConditions.Add($"{columnName} = @{filter.Key}");
                        parameters.Add(filter.Key, filter.Value);
                    }
                }
            }

            var whereClause = whereConditions.Count > 0
                ? "WHERE " + string.Join(" AND ", whereConditions)
                : "";

            return (whereClause, parameters);
        }

        /// <summary>
        /// Builds pagination SQL with OFFSET/FETCH
        /// </summary>
        /// <param name="pageIndex">Page number (1-based)</param>
        /// <param name="pageSize">Items per page</param>
        /// <returns>Pagination SQL clause</returns>
        public static string BuildPaginationClause(int pageIndex, int pageSize)
        {
            var offset = (pageIndex - 1) * pageSize;
            return $"OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }

        /// <summary>
        /// Builds complete SELECT query with filters, sorting, and pagination
        /// </summary>
        /// <param name="selectClause">SELECT columns</param>
        /// <param name="fromClause">FROM tables</param>
        /// <param name="filters">Filter conditions</param>
        /// <param name="sortBy">Sort column</param>
        /// <param name="sortDirection">Sort direction</param>
        /// <param name="columnMappings">Column mappings for sorting</param>
        /// <param name="pageIndex">Page number</param>
        /// <param name="pageSize">Items per page</param>
        /// <returns>Complete SQL query and parameters</returns>
        public static (string sql, Dictionary<string, object> parameters) BuildSelectQuery(
            string selectClause,
            string fromClause,
            Dictionary<string, object>? filters = null,
            string? sortBy = null,
            string? sortDirection = null,
            Dictionary<string, string>? columnMappings = null,
            int pageIndex = 1,
            int pageSize = 20)
        {
            var sql = $"{selectClause} {fromClause}";
            var parameters = new Dictionary<string, object>();

            // Add WHERE clause
            if (filters != null && filters.Count > 0)
            {
                var (whereClause, whereParams) = BuildWhereClause(filters);
                if (!string.IsNullOrEmpty(whereClause))
                {
                    sql += $" {whereClause}";
                    foreach (var param in whereParams)
                    {
                        parameters.Add(param.Key, param.Value);
                    }
                }
            }

            // Add ORDER BY clause
            if (columnMappings != null)
            {
                var (orderBy, _) = BuildOrderByClause(sortBy, sortDirection, columnMappings);
                sql += $" {orderBy}";
            }

            // Add pagination
            sql += $" {BuildPaginationClause(pageIndex, pageSize)}";

            return (sql, parameters);
        }

        /// <summary>
        /// Builds COUNT query for pagination
        /// </summary>
        /// <param name="fromClause">FROM tables</param>
        /// <param name="filters">Filter conditions</param>
        /// <returns>COUNT SQL query and parameters</returns>
        public static (string sql, Dictionary<string, object> parameters) BuildCountQuery(
            string fromClause,
            Dictionary<string, object>? filters = null)
        {
            var sql = $"SELECT COUNT(*) {fromClause}";
            var parameters = new Dictionary<string, object>();

            if (filters != null && filters.Count > 0)
            {
                var (whereClause, whereParams) = BuildWhereClause(filters);
                if (!string.IsNullOrEmpty(whereClause))
                {
                    sql += $" {whereClause}";
                    foreach (var param in whereParams)
                    {
                        parameters.Add(param.Key, param.Value);
                    }
                }
            }

            return (sql, parameters);
        }

        /// <summary>
        /// Validates and sanitizes column name for SQL injection prevention
        /// </summary>
        /// <param name="columnName">Column name to validate</param>
        /// <param name="allowedColumns">List of allowed column names</param>
        /// <returns>True if column name is safe</returns>
        public static bool IsValidColumnName(string columnName, IEnumerable<string> allowedColumns)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                return false;

            // Check if column name contains only alphanumeric, underscore, and dot
            if (!System.Text.RegularExpressions.Regex.IsMatch(columnName, @"^[a-zA-Z0-9_.]+$"))
                return false;

            // Check if column is in allowed list
            return allowedColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sanitizes search term for LIKE queries
        /// </summary>
        /// <param name="searchTerm">Search term to sanitize</param>
        /// <returns>Sanitized search term</returns>
        public static string SanitizeSearchTerm(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return string.Empty;

            // Escape SQL LIKE wildcards
            return searchTerm
                .Replace("[", "[[]")
                .Replace("%", "[%]")
                .Replace("_", "[_]");
        }
    }
}