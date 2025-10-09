namespace UniManage.Core.Helpers
{
    public static class QueryHelper
    {
        public static (string orderBy, object parameters) BuildOrderByClause(string sortBy, string sortDirection, Dictionary<string, string> columnMappings)
        {
            var sortDir = (sortDirection?.ToUpper() == "DESC") ? "DESC" : "ASC";
            var defaultColumn = columnMappings.GetValueOrDefault("default", "(SELECT NULL)");

            if (string.IsNullOrEmpty(sortBy) || !columnMappings.ContainsKey(sortBy))
            {
                return ($"ORDER BY {defaultColumn} {sortDir}", new { });
            }

            return ($"ORDER BY {columnMappings[sortBy]} {sortDir}", new { });
        }
    }
}
