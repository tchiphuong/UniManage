using UniManage.Model.Common;

namespace UniManage.Tests.Fixtures;

/// <summary>
/// Base test fixture providing common test utilities
/// </summary>
public abstract class TestBase : IDisposable
{
    protected TestBase()
    {
        // Initialize common test setup
    }

    /// <summary>
    /// Create a mock HeaderInfo for testing
    /// </summary>
    protected HeaderInfo CreateMockHeaderInfo(string username = "test_user", string ip = "127.0.0.1")
    {
        return new HeaderInfo
        {
            Username = username,
            IpAddress = ip,
            Timestamp = DateTime.UtcNow,
            TraceId = Guid.NewGuid().ToString()
        };
    }

    /// <summary>
    /// Create a CancellationToken for testing with optional timeout
    /// </summary>
    protected CancellationToken CreateCancellationToken(int timeoutMs = 5000)
    {
        var cts = new CancellationTokenSource(timeoutMs);
        return cts.Token;
    }

    public virtual void Dispose()
    {
        // Cleanup resources
        GC.SuppressFinalize(this);
    }
}
