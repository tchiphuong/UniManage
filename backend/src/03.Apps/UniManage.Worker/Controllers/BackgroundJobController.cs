using UniManage.Modules.System.Domain;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;

namespace UniManage.Worker.Controllers;

[ApiController]
[Route("api/v1/background-jobs")]
public class BackgroundJobController : ControllerBase
{
    #region Properties

    private string Username => User?.Identity?.Name ?? "Anonymous";

    #endregion

    #region GET: /api/v1/background-jobs

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<SyBackgroundJobs>>>> GetList()
    {
        using var dbContext = new DbContext();
        var sql = "SELECT * FROM SyBackgroundJobs ORDER BY JobCode";

        var items = await dbContext.QueryAsync<SyBackgroundJobs>(sql);

        return Ok(ResponseHelper.Success(items, "Background jobs retrieved successfully"));
    }

    #endregion

    #region PUT: /api/v1/background-jobs/{id}/update-cron

    [HttpPut("{id}/update-cron")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateCron(int id, [FromBody] UpdateCronRequest request)
    {
        // Validate Cron (Basic check)
        if (string.IsNullOrWhiteSpace(request.CronExpression) || request.CronExpression.Split(' ').Length < 5)
        {
            return BadRequest(ResponseHelper.Error<bool>("Invalid Cron Expression"));
        }

        using var dbContext = new DbContext(openTransaction: true);
        try
        {
            var sqlGet = "SELECT Id, JobCode FROM SyBackgroundJobs WHERE Id = @Id";
            var job = await dbContext.QueryFirstOrDefaultAsync<SyBackgroundJobs>(sqlGet, new { Id = id });

            if (job == null)
            {
                return NotFound(ResponseHelper.NotFound<bool>("Job not found"));
            }

            var sqlUpdate = @"
                UPDATE SyBackgroundJobs 
                SET CronExpression = @Cron, 
                    UpdatedBy = @UpdatedBy, 
                    UpdatedAt = SYSUTCDATETIME() 
                WHERE Id = @Id";

            await dbContext.ExecuteAsync(sqlUpdate, new { Cron = request.CronExpression, Id = id, UpdatedBy = Username });

            // Update Hangfire recurring job immediately
            RecurringJob.AddOrUpdate<Jobs.TokenCleanupJob>(
                job.JobCode,
                x => x.ExecuteAsync(),
                request.CronExpression);

            await dbContext.CommitAsync();
            return Ok(ResponseHelper.Success(true, "Cron expression updated successfully"));
        }
        catch (Exception ex)
        {
            await dbContext.RollbackAsync();
            return StatusCode(500, ResponseHelper.Error<bool>($"Error updating cron: {ex.Message}"));
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
            var sqlGet = "SELECT Id, JobCode, IsDisabled, CronExpression FROM SyBackgroundJobs WHERE Id = @Id";
            var job = await dbContext.QueryFirstOrDefaultAsync<SyBackgroundJobs>(sqlGet, new { Id = id });

            if (job == null)
            {
                return NotFound(ResponseHelper.NotFound<bool>("Job not found"));
            }

            var newStatus = !(job.IsDisabled ?? false);
            var sqlUpdate = @"
                UPDATE SyBackgroundJobs 
                SET IsDisabled = @IsDisabled, 
                    UpdatedBy = @UpdatedBy, 
                    UpdatedAt = SYSUTCDATETIME() 
                WHERE Id = @Id";

            await dbContext.ExecuteAsync(sqlUpdate, new { IsDisabled = newStatus, Id = id, UpdatedBy = Username });

            // Sync with Hangfire
            if (newStatus) // Disabling
            {
                RecurringJob.RemoveIfExists(job.JobCode);
            }
            else // Enabling
            {
                RecurringJob.AddOrUpdate<Jobs.TokenCleanupJob>(
                    job.JobCode,
                    x => x.ExecuteAsync(),
                    job.CronExpression);
            }

            await dbContext.CommitAsync();

            return Ok(ResponseHelper.Success(true, $"Job {(newStatus ? "disabled" : "enabled")} successfully"));
        }
        catch (Exception ex)
        {
            await dbContext.RollbackAsync();
            return StatusCode(500, ResponseHelper.Error<bool>($"Error toggling job: {ex.Message}"));
        }
    }

    #endregion

    #region Models

    public class UpdateCronRequest
    {
        public string CronExpression { get; set; } = default!;
    }

    #endregion
}



