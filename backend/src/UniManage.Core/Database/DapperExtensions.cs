using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;

namespace UniManage.Core.Database;

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
        // In EF Core, the transaction is stored as IDbContextTransaction
        // We need to get the underlying DbTransaction
        var transaction = context.Database.CurrentTransaction as IDbContextTransaction;
        var dbTransaction = transaction != null ? (transaction.GetType().GetProperty("DbTransaction")?.GetValue(transaction) as DbTransaction) : null;

        await EnsureConnectionOpenAsync(connection, cancellationToken);

        return await connection.ExecuteAsync(new CommandDefinition(sql, param, dbTransaction, cancellationToken: cancellationToken));
    }

    #endregion

    #region ExecuteScalarAsync

    /// <summary>
    /// Execute a command and returns the first column of the first row asynchronously using Dapper
    /// </summary>
    public static async Task<T?> ExecuteScalarAsync<T>(this DbContext context, string sql, object? param = null, CancellationToken cancellationToken = default)
    {
        var connection = context.Database.GetDbConnection();
        var transaction = context.Database.CurrentTransaction as IDbContextTransaction;
        var dbTransaction = transaction != null ? (transaction.GetType().GetProperty("DbTransaction")?.GetValue(transaction) as DbTransaction) : null;

        await EnsureConnectionOpenAsync(connection, cancellationToken);

        return await connection.ExecuteScalarAsync<T>(new CommandDefinition(sql, param, dbTransaction, cancellationToken: cancellationToken));
    }

    #endregion

    #region QueryAsync

    /// <summary>
    /// Execute a query asynchronously using Dapper
    /// </summary>
    public static async Task<IEnumerable<T>> QueryAsync<T>(this DbContext context, string sql, object? param = null, CancellationToken cancellationToken = default)
    {
        var connection = context.Database.GetDbConnection();
        var transaction = context.GetDbTransaction();

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
        var transaction = context.GetDbTransaction();

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
        var transaction = context.GetDbTransaction();

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
        var transaction = context.GetDbTransaction();

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

    /// <summary>
    /// Get the underlying DbTransaction from EF Core IDbContextTransaction
    /// </summary>
    private static DbTransaction? GetDbTransaction(this DbContext context)
    {
        var transaction = context.Database.CurrentTransaction;
        if (transaction == null)
            return null;

        // Get the underlying DbTransaction using reflection
        var dbTransactionProp = transaction.GetType().GetProperty("DbTransaction");
        return dbTransactionProp?.GetValue(transaction) as DbTransaction;
    }

    #endregion

    #region Bulk Operations (Optional)

    /// <summary>
    /// Bulk insert records using Dapper
    /// </summary>
    public static async Task<int> BulkInsertAsync<T>(this DbContext context, string tableName, IEnumerable<T> records, CancellationToken cancellationToken = default) where T : class
    {
        if (!records.Any())
            return 0;

        var properties = typeof(T).GetProperties()
            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
            .ToList();

        var columnNames = string.Join(", ", properties.Select(p => p.Name));
        var valueParams = string.Join(", ", properties.Select(p => $"@{p.Name}"));

        var sql = $"INSERT INTO {tableName} ({columnNames}) VALUES ({valueParams})";

        var connection = context.Database.GetDbConnection();
        var transaction = context.GetDbTransaction();

        await EnsureConnectionOpenAsync(connection, cancellationToken);

        return await connection.ExecuteAsync(new CommandDefinition(sql, records, transaction, cancellationToken: cancellationToken));
    }

    /// <summary>
    /// Execute stored procedure using Dapper
    /// </summary>
    public static async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(this DbContext context, string storedProcName, object? param = null, CancellationToken cancellationToken = default)
    {
        var connection = context.Database.GetDbConnection();
        var transaction = context.GetDbTransaction();

        await EnsureConnectionOpenAsync(connection, cancellationToken);

        return await connection.QueryAsync<T>(new CommandDefinition(storedProcName, param, transaction, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
    }

    #endregion
}
