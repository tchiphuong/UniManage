using System.Data;
using UniManage.Core.Database;

namespace UniManage.Tests.Fixtures;

/// <summary>
/// Test fixture for database integration tests
/// Uses shared database with transaction rollback for isolation
/// </summary>
public class DbContextFixture : IDisposable
{
    private readonly List<IDbTransaction> _activeTransactions = new();
    private readonly List<DbContext> _activeContexts = new();

    public DbContextFixture()
    {
        // Uses shared database (production/development DB)
        // Tests are isolated using transactions that rollback after each test
    }

    /// <summary>
    /// Create a new DbContext instance for testing with automatic transaction rollback
    /// </summary>
    public DbContext CreateDbContext(bool openTransaction = true)
    {
        var dbContext = new DbContext(openTransaction);
        
        if (openTransaction && dbContext.transaction != null)
        {
            _activeTransactions.Add(dbContext.transaction);
            _activeContexts.Add(dbContext);
        }
        
        return dbContext;
    }

    /// <summary>
    /// Execute SQL script for test setup
    /// </summary>
    public async Task ExecuteScriptAsync(string sql)
    {
        using var dbContext = CreateDbContext();
        await dbContext.ExecuteAsync(sql);
    }

    /// <summary>
    /// Execute SQL for test data setup (within transaction)
    /// </summary>
    public async Task SetupTestDataAsync(string sql)
    {
        using var dbContext = CreateDbContext(openTransaction: true);
        await dbContext.ExecuteAsync(sql);
        // Transaction will be rolled back in Dispose
    }

    /// <summary>
    /// Begin a transaction for test isolation
    /// </summary>
    public async Task<(DbContext Context, IDbTransaction Transaction)> BeginTransactionAsync()
    {
        var dbContext = CreateDbContext(openTransaction: true);
        return (dbContext, dbContext.transaction!);
    }

    /// <summary>
    /// Create test data that will be automatically cleaned up
    /// </summary>
    public async Task<T> CreateTestEntityAsync<T>(string tableName, object data) where T : class
    {
        using var dbContext = CreateDbContext(openTransaction: true);
        // Implementation would insert and return entity
        // Transaction ensures automatic cleanup
        throw new NotImplementedException("Implement based on your needs");
    }

    public void Dispose()
    {
        // Rollback all active transactions (automatic cleanup)
        foreach (var transaction in _activeTransactions)
        {
            try
            {
                transaction.Rollback();
                transaction.Dispose();
            }
            catch
            {
                // Ignore rollback errors (transaction may already be completed)
            }
        }
        
        // Dispose all active contexts
        foreach (var context in _activeContexts)
        {
            try
            {
                context.Dispose();
            }
            catch
            {
                // Ignore disposal errors
            }
        }
        
        _activeTransactions.Clear();
        _activeContexts.Clear();
        
        GC.SuppressFinalize(this);
    }
}
        {
            try
            {
                context.Dispose();
            }
            catch
            {
                // Ignore disposal errors
                    dbContext.ExecuteAsync(script).Wait();
                }
                catch
                {
                    // Log but don't fail on cleanup
                }
            }
        }

        GC.SuppressFinalize(this);
    }
}
