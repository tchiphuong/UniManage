using Microsoft.EntityFrameworkCore.Storage;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace UniManage.Shared.Infrastructure.Database
{
    /// <summary>
    /// Extension methods to enable Dapper operations on EF Core DbContext
    /// </summary>
    public static class DapperExtensions
    {
        #region ExecuteAsync

        /// <summary>
        /// Execute a command asynchronously using Dapper
        /// </summary>
        public static async Task<int> ExecuteAsync(this DbContext context, string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            var connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction?.GetDbTransaction();

            await EnsureConnectionOpenAsync(connection, cancellationToken);

            return await connection.ExecuteAsync(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        #endregion

        #region ExecuteScalarAsync

        /// <summary>
        /// Execute a command and returns the first column of the first row asynchronously using Dapper
        /// </summary>
        public static async Task<T?> ExecuteScalarAsync<T>(this DbContext context, string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            var connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction?.GetDbTransaction();

            await EnsureConnectionOpenAsync(connection, cancellationToken);

            return await connection.ExecuteScalarAsync<T>(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        #endregion

        #region QueryAsync

        /// <summary>
        /// Execute a query asynchronously using Dapper
        /// </summary>
        public static async Task<IEnumerable<T>> QueryAsync<T>(this DbContext context, string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            var connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction?.GetDbTransaction();

            await EnsureConnectionOpenAsync(connection, cancellationToken);

            return await connection.QueryAsync<T>(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        #endregion

        #region QueryFirstOrDefaultAsync

        /// <summary>
        /// Execute a query and return the first result or default asynchronously using Dapper
        /// </summary>
        public static async Task<T?> QueryFirstOrDefaultAsync<T>(this DbContext context, string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            var connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction?.GetDbTransaction();

            await EnsureConnectionOpenAsync(connection, cancellationToken);

            return await connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        #endregion

        #region QuerySingleOrDefaultAsync

        /// <summary>
        /// Execute a query and return a single result or default asynchronously using Dapper
        /// </summary>
        public static async Task<T?> QuerySingleOrDefaultAsync<T>(this DbContext context, string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            var connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction?.GetDbTransaction();

            await EnsureConnectionOpenAsync(connection, cancellationToken);

            return await connection.QuerySingleOrDefaultAsync<T>(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        #endregion

        #region QueryMultipleAsync

        /// <summary>
        /// Execute a query with multiple result sets asynchronously using Dapper
        /// </summary>
        public static async Task<SqlMapper.GridReader> QueryMultipleAsync(this DbContext context, string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            var connection = context.Database.GetDbConnection();
            var transaction = context.Database.CurrentTransaction?.GetDbTransaction();

            await EnsureConnectionOpenAsync(connection, cancellationToken);

            return await connection.QueryMultipleAsync(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        #endregion

        #region Transaction Helpers

        /// <summary>
        /// Begin a database transaction if not already started
        /// </summary>
        public static async Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync(this DbContext context, CancellationToken cancellationToken = default)
        {
            if (context.Database.CurrentTransaction != null)
            {
                return context.Database.CurrentTransaction;
            }

            return await context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// Commit the current transaction
        /// </summary>
        public static async Task CommitTransactionAsync(this DbContext context, CancellationToken cancellationToken = default)
        {
            if (context.Database.CurrentTransaction != null)
            {
                await context.Database.CurrentTransaction.CommitAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Rollback the current transaction
        /// </summary>
        public static async Task RollbackTransactionAsync(this DbContext context, CancellationToken cancellationToken = default)
        {
            if (context.Database.CurrentTransaction != null)
            {
                await context.Database.CurrentTransaction.RollbackAsync(cancellationToken);
            }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Ensure database connection is open
        /// </summary>
        private static async Task EnsureConnectionOpenAsync(DbConnection connection, CancellationToken cancellationToken = default)
        {
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }
        }

        #endregion
    }
}

