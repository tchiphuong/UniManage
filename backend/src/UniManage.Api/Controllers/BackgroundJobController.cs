using Hangfire;
using Microsoft.AspNetCore.Mvc;
using UniManage.Core.Database;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using Dapper;

namespace UniManage.Api.Controllers
{
    [ApiController]
    [Route("api/v1/background-jobs")]
    public class BackgroundJobController : BaseController
    {
        #region GET: /api/v1/background-jobs

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UniManage.Model.Entities.BackgroundJob>>>> GetList()
        {
            using var dbContext = new DbContext();
            var sql = "SELECT * FROM sy_background_jobs ORDER BY JobCode";

            var items = await dbContext.QueryAsync<UniManage.Model.Entities.BackgroundJob>(sql);

            return Ok(ResponseHelper.Success(items));
        }

        #endregion

        #region PUT: /api/v1/background-jobs/{id}/update-cron

        [HttpPut("{id}/update-cron")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateCron(int id, [FromBody] string newCronExpression)
        {
            // Validate Cron (Basic check)
            if (string.IsNullOrWhiteSpace(newCronExpression) || newCronExpression.Split(' ').Length < 5)
                return BadRequest(ResponseHelper.Error<bool>("Invalid Cron Expression"));

            using var dbContext = new DbContext(openTransaction: true);
            try
            {
                var sqlGet = "SELECT Id, JobCode FROM sy_background_jobs WHERE Id = @Id";
                var job = await dbContext.QueryFirstOrDefaultAsync<UniManage.Model.Entities.BackgroundJob>(sqlGet, new { Id = id });

                if (job == null) return NotFound(ResponseHelper.NotFound<bool>("Job not found"));

                var sqlUpdate = @"
                    UPDATE sy_background_jobs 
                    SET CronExpression = @Cron, 
                        UpdatedBy = @UpdatedBy, 
                        UpdatedAt = SYSUTCDATETIME() 
                    WHERE Id = @Id";

                await dbContext.ExecuteAsync(sqlUpdate, new { Cron = newCronExpression, Id = id, UpdatedBy = Username });

                // Update Hangfire immediately
                // In real implementation, you would call RecurringJob.AddOrUpdate(job.JobCode, () => ..., newCronExpression);
                // But since API doesn't know the Job Implementation, 
                // we rely on the Worker to Reload or we accept that it connects on next restart.
                // Or better: Publish an Event causing Worker to refresh.

                await dbContext.CommitAsync();
                return Ok(ResponseHelper.Success(true, "Cron updated successfully"));
            }
            catch
            {
                await dbContext.RollbackAsync();
                throw;
            }
        }

        #endregion

        #region PUT: /api/v1/background-jobs/{id}/toggle

        [HttpPut("{id}/toggle")]
        public async Task<ActionResult<ApiResponse<bool>>> ToggleJob(int id)
        {
            using var dbContext = new DbContext(openTransaction: true);
            try
            {
                // 1. Get current status
                var sqlGet = "SELECT IsDisabled, JobCode, CronExpression FROM sy_background_jobs WHERE Id = @Id";
                var job = await dbContext.QueryFirstOrDefaultAsync<UniManage.Model.Entities.BackgroundJob>(sqlGet, new { Id = id });

                if (job == null)
                    return NotFound(ResponseHelper.NotFound<bool>("Job not found"));

                // 2. Toggle status
                var newStatus = !job.IsDisabled;
                var sqlUpdate = @"
                    UPDATE sy_background_jobs 
                    SET IsDisabled = @IsDisabled, 
                        UpdatedBy = @UpdatedBy, 
                        UpdatedAt = SYSUTCDATETIME() 
                    WHERE Id = @Id";

                await dbContext.ExecuteAsync(sqlUpdate, new { IsDisabled = newStatus, Id = id, UpdatedBy = Username });

                // 3. Sync with Hangfire (Real-time update)
                // If Disabled -> Remove from Schedule
                // If Enabled -> AddOrUpdate Schedule
                // Note: The actual Job Logic implementation (Worker) needs to register itself. 
                // Alternatively, we can just use the DB flag check inside the job logic.
                // Here we just update the DB flag.

                if (newStatus) // If we are disabling
                {
                    RecurringJob.RemoveIfExists(job.JobCode);
                }
                else // If we are enabling
                {
                    // For "Real World", triggering a reload or having the Worker watch this table is better.
                    // But for simple "API Switch", we can try to re-schedule if we know the type.
                    // Since API layer doesn't know the Job Type, we rely on the DB Flag check inside the Job Execution,
                    // OR we rely on the Background Worker to "Refresh" its schedule based on this table.

                    // Option A: Just update DB, and Job Logic checks DB.
                    // This is the safest implementation.
                }

                await dbContext.CommitAsync();

                return Ok(ResponseHelper.Success(true, $"Job {(newStatus ? "disabled" : "enabled")} successfully"));
            }
            catch
            {
                await dbContext.RollbackAsync();
                throw;
            }
        }

        #endregion
    }
}
