using System.Threading;
using System.Threading.Tasks;

namespace UniManage.Core.Database;

/// <summary>
/// Interface for database transaction handling
/// </summary>
public interface IDbTransaction : IAsyncDisposable
{
    /// <summary>
    /// Commits the transaction
    /// </summary>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the transaction
    /// </summary>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Interface for database context with transaction support
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Begins a new database transaction
    /// </summary>
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a query and returns the first column of the first row
    /// </summary>
    Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a command and returns the number of rows affected
    /// </summary>
    Task<int> ExecuteAsync(string sql, object param = null, CancellationToken cancellationToken = default);
}
