using Dapper;
using System.Data;
using UniManage.Core.Models;

namespace UniManage.Core.Extensions
{
    public static class DapperExtensions
    {
        public class SqlBuilder
        {
            public required string BaseQuery { get; set; }
            public required string WhereClause { get; set; }
            public required string OrderByClause { get; set; }
            public required object Parameters { get; set; }
        }

        public static async Task<(List<T> Items, PagingResult Paging)> QueryPagingAsync<T>(
            this IDbConnection connection,
            SqlBuilder builder,
            int pageIndex,
            int pageSize)
        {
            try
            {
                // First get total count
                var countQuery = $@"
                    SELECT COUNT(*)
                    FROM ({builder.BaseQuery}) t
                    {builder.WhereClause}";

                var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, builder.Parameters);

                // Then get paginated data
                var dataQuery = $@"
                    SELECT t.*
                    FROM ({builder.BaseQuery}) t
                    {builder.WhereClause}
                    {builder.OrderByClause} 
                    OFFSET {(pageIndex - 1) * pageSize} ROWS 
                    FETCH NEXT {pageSize} ROWS ONLY";

                var results = (await connection.QueryAsync<T>(dataQuery, builder.Parameters)).AsList();

                var paging = new PagingResult(pageIndex, pageSize, (int)totalCount);

                return (results, paging);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
