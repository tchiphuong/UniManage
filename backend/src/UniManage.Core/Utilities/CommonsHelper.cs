using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniManage.Core.Database;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Helper class for working with sy_commons table
    /// Provides dynamic configuration values instead of hardcoded constants
    /// </summary>
    public static class CommonsHelper
    {
        /// <summary>
        /// Get a single value from sy_commons by TypeKey and ValueKey
        /// </summary>
        /// <param name="typeKey">The type key (e.g., "STATUS", "ROLE", "SETTING")</param>
        /// <param name="valueKey">The value key (e.g., "ACTIVE", "ADMIN", "DEFAULT_PASSWORD") - will be converted to UPPER_CASE</param>
        /// <returns>The value or null if not found</returns>
        public static async Task<string?> GetValueAsync(string typeKey, string valueKey)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT TOP 1 [ValueNameVi]
                FROM [dbo].[sy_commons]
                WHERE [TypeKey] = @TypeKey 
                    AND [ValueKey] = @ValueKey 
                    AND [Status] = 1";

            return await dbContext.connection.QueryFirstOrDefaultAsync<string?>(
                sql,
                new
                {
                    TypeKey = typeKey.ToUpper(),
                    ValueKey = valueKey.ToUpper()
                });
        }

        /// <summary>
        /// Get all values for a specific TypeKey
        /// </summary>
        /// <param name="typeKey">The type key - will be converted to UPPER_CASE</param>
        /// <returns>Dictionary of ValueKey -> ValueNameVi</returns>
        public static async Task<Dictionary<string, string>> GetTypeValuesAsync(string typeKey)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT [ValueKey], [ValueNameVi]
                FROM [dbo].[sy_commons]
                WHERE [TypeKey] = @TypeKey 
                    AND [Status] = 1
                ORDER BY [Sort]";

            var results = await dbContext.connection.QueryAsync<(string ValueKey, string ValueNameVi)>(
                sql,
                new { TypeKey = typeKey.ToUpper() });

            return results.ToDictionary(x => x.ValueKey, x => x.ValueNameVi);
        }

        /// <summary>
        /// Get integer value from sy_commons (useful for status codes)
        /// </summary>
        /// <param name="typeKey">The type key - will be converted to UPPER_CASE</param>
        /// <param name="valueKey">The value key - will be converted to UPPER_CASE</param>
        /// <param name="defaultValue">Default value if not found or not a valid integer</param>
        /// <returns>Integer value or default</returns>
        public static async Task<int> GetIntValueAsync(string typeKey, string valueKey, int defaultValue = 0)
        {
            var value = await GetValueAsync(typeKey.ToUpper(), valueKey.ToUpper());
            return int.TryParse(value, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Check if a specific TypeKey/ValueKey combination exists and is active
        /// </summary>
        /// <param name="typeKey">The type key - will be converted to UPPER_CASE</param>
        /// <param name="valueKey">The value key - will be converted to UPPER_CASE</param>
        /// <returns>True if exists and active</returns>
        public static async Task<bool> ExistsAsync(string typeKey, string valueKey)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT COUNT(*)
                FROM [dbo].[sy_commons]
                WHERE [TypeKey] = @TypeKey 
                    AND [ValueKey] = @ValueKey 
                    AND [Status] = 1";

            var count = await dbContext.connection.ExecuteScalarAsync<int>(
                sql,
                new
                {
                    TypeKey = typeKey.ToUpper(),
                    ValueKey = valueKey.ToUpper()
                });

            return count > 0;
        }

        /// <summary>
        /// Common TypeKeys used in the system
        /// </summary>
        public static class TypeKeys
        {
            public const string Status = "STATUS";
            public const string Role = "ROLE";
            public const string Setting = "SETTING";
            public const string Permission = "PERMISSION";
            public const string UserType = "USER_TYPE";
        }

        /// <summary>
        /// Common ValueKeys for STATUS type
        /// </summary>
        public static class StatusKeys
        {
            public const string Active = "ACTIVE";
            public const string Inactive = "INACTIVE";
            public const string Pending = "PENDING";
            public const string Suspended = "SUSPENDED";
            public const string Deleted = "DELETED";
        }
    }
}