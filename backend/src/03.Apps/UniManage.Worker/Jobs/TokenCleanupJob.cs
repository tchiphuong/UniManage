using UniManage.Shared.Infrastructure.Database;
using Hangfire;
using UniManage.Shared.Domain.Interfaces;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.Worker.Jobs;

public class TokenCleanupJob
{
    public TokenCleanupJob()
    {
    }

    [DisableConcurrentExecution(timeoutInSeconds: 60 * 5)]
    [AutomaticRetry(Attempts = 0)]
    public async Task ExecuteAsync()
    {
        // Use UniManage.Core.Database.DbContext like in API
        // It automatically handles connection string and configuration
        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                // 1. Delete expired PersistedGrants
                var grantsSql = "DELETE FROM PersistedGrants WHERE Expiration < SYSUTCDATETIME()";
                var grantsAffected = await dbContext.ExecuteAsync(grantsSql, cancellationToken: CancellationToken.None);

                // 2. Delete expired DeviceCodes
                var devicesSql = "DELETE FROM DeviceCodes WHERE Expiration < SYSUTCDATETIME()";
                var devicesAffected = await dbContext.ExecuteAsync(devicesSql, cancellationToken: CancellationToken.None);

                await dbContext.CommitAsync(CancellationToken.None);

                if (grantsAffected > 0 || devicesAffected > 0)
                {
                    var log = new UniManage.Shared.Domain.Models.ApiLogModel()
                    {
                        Message = $"TokenCleanupJob: Deleted {grantsAffected} grants and {devicesAffected} device codes.",
                        Method = nameof(ExecuteAsync),
                        Path = "BackgroundJob",
                        ReturnCode = 0
                    };
                    UniLogManager.WriteApiLog(log);
                }
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync(CancellationToken.None);

                var log = new UniManage.Shared.Domain.Models.ApiLogModel()
                {
                    Message = $"TokenCleanupJob Error: {ex.Message}",
                    Method = nameof(ExecuteAsync),
                    Path = "BackgroundJob",
                    ReturnCode = -1,
                    IsException = true
                };
                UniLogManager.WriteApiLog(log);
                throw; // Let Hangfire handle the retry/failure state
            }
        }
    }
}



