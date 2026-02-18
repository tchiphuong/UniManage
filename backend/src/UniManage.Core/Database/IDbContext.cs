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
/// <remarks>
/// This interface supports hybrid EF Core + Dapper approach.
/// 
/// Implementation:
/// - <see cref="DbContext"/>: Hybrid EF Core + Dapper implementation with auto-discovery and LINQ support
/// 
/// Usage:
///   // Read-only (no transaction)
///   using (var db = new DbContext()) 
///   {
///     var users = await db.Set&lt;sy_users&gt;().Where(u => u.Status == "active").ToListAsync();
///   }
///   
///   // Write operations (with transaction)
///   using (var db = new DbContext(openTransaction: true))
///   {
///     var user = await db.Set&lt;sy_users&gt;().FindAsync(userId);
///     user.Status = "inactive";
///     await db.SaveChangesAsync();
///     await db.CommitAsync(); // Must commit, otherwise auto-rollback on dispose
///   }
/// </remarks>
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

    /// <summary>
    /// Executes a query and returns the results
    /// </summary>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a query and returns the first result or default
    /// </summary>
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CancellationToken cancellationToken = default);
}

/// <summary>
/// Extended interface for hybrid DbContext supporting both EF Core and Dapper
/// </summary>
/// <remarks>
/// Use this interface when you need to support both EF Core LINQ queries and Dapper raw SQL in the same context.
/// Implemented by <see cref="DbContext"/> with <see cref="DapperExtensions"/> support.
/// </remarks>
public interface IHybridDbContext : IDbContext
{
    /// <summary>
    /// Execute Dapper query on EF Core context
    /// </summary>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute Dapper command on EF Core context
    /// </summary>
    Task<int> ExecuteAsync(string sql, object? param = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute Dapper scalar query on EF Core context
    /// </summary>
    Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);
}
