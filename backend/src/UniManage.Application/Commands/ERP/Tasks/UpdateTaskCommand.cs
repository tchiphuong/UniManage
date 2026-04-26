using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.ERP.Tasks
{
    #region Command

    /// <summary>Cập nhật thông tin chi tiết của ERP Task</summary>
    public sealed class UpdateTaskCommand : BaseCommand, IRequest<ApiResponse<UpdateTaskCommand.Response>>
    {
        /// <summary>Set by the controller from the route parameter — not part of the request body</summary>
        public long Id { get; set; }

        public string Title { get; init; } = default!;
        public string? Description { get; init; }
        public string Priority { get; init; } = CoreCommon.Value.Taskpriority.Medium;
        public string? AssigneeEmployeeCode { get; init; }
        public string? AssigneeUsername { get; init; }
        public DateTime? DueDate { get; init; }
        public DateTime? StartDate { get; init; }
        public string? Tags { get; init; }
        public decimal? EstimatedHours { get; init; }
        public decimal? ActualHours { get; init; }
        public string? DepartmentCode { get; init; }

        /// <summary>
        /// Client should echo back the RowVersion obtained from GET so the server
        /// can detect concurrent modifications (optimistic concurrency).
        /// </summary>
        public byte[]? RowVersion { get; init; }

        public sealed class Response
        {
            public bool Success => Id > 0;
            public long Id { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Id must be greater than 0")
                .MustAsync(async (id, cancel) => await IsTaskExistsAsync(id))
                .WithMessage("Task not found");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(500).WithMessage("Title must not exceed 500 characters");

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required")
                .Must(p => CoreCommon.Value.Taskpriority.All.Contains(p))
                .WithMessage($"Priority must be one of: {string.Join(", ", CoreCommon.Value.Taskpriority.All)}");

            RuleFor(x => x.Tags)
                .MaximumLength(500).WithMessage("Tags must not exceed 500 characters")
                .When(x => x.Tags != null);

            RuleFor(x => x.EstimatedHours)
                .GreaterThan(0).WithMessage("EstimatedHours must be greater than 0")
                .When(x => x.EstimatedHours.HasValue);

            RuleFor(x => x.ActualHours)
                .GreaterThanOrEqualTo(0).WithMessage("ActualHours must be 0 or greater")
                .When(x => x.ActualHours.HasValue);

            When(x => x.DueDate.HasValue && x.StartDate.HasValue, () =>
            {
                RuleFor(x => x.DueDate)
                    .GreaterThanOrEqualTo(x => x.StartDate)
                    .WithMessage("DueDate must be on or after StartDate");
            });

            When(x => !string.IsNullOrEmpty(x.AssigneeEmployeeCode), () =>
            {
                RuleFor(x => x.AssigneeEmployeeCode!)
                    .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code))
                    .WithMessage("Assignee employee does not exist");
            });
        }

        private static async Task<bool> IsTaskExistsAsync(long id)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM erp_tasks WHERE Id = @Id) THEN 1 ELSE 0 END",
                    new { Id = id });
            }
        }

        private static async Task<bool> IsEmployeeExistsAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE EmployeeCode = @Code) THEN 1 ELSE 0 END",
                    new { Code = employeeCode });
            }
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ApiResponse<UpdateTaskCommand.Response>>
    {
        public async Task<ApiResponse<UpdateTaskCommand.Response>> Handle(UpdateTaskCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id),
                    new CoreParamModel(nameof(request.Title), request.Title),
                    new CoreParamModel(nameof(request.Priority), request.Priority)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    // ── Optimistic concurrency: fetch current RowVersion ───────────────
                    if (request.RowVersion != null && request.RowVersion.Length > 0)
                    {
                        var currentRowVersion = await dbContext.ExecuteScalarAsync<byte[]>(
                            "SELECT DataRowVersion FROM erp_tasks WHERE Id = @Id",
                            new { request.Id },
                            ct);

                        if (currentRowVersion == null)
                        {
                            return ResponseHelper.NotFound<UpdateTaskCommand.Response>("Task not found");
                        }

                        if (!currentRowVersion.SequenceEqual(request.RowVersion))
                        {
                            return ResponseHelper.Error<UpdateTaskCommand.Response>(
                                "The record was modified by another user. Please refresh and try again.");
                        }
                    }
                    // ──────────────────────────────────────────────────────────────────

                    var sql = @"
                        UPDATE erp_tasks SET
                            Title                = @Title,
                            Description          = @Description,
                            Priority             = @Priority,
                            AssigneeEmployeeCode = @AssigneeEmployeeCode,
                            AssigneeUsername     = @AssigneeUsername,
                            DueDate              = @DueDate,
                            StartDate            = @StartDate,
                            Tags                 = @Tags,
                            EstimatedHours       = @EstimatedHours,
                            ActualHours          = @ActualHours,
                            DepartmentCode       = @DepartmentCode,
                            UpdatedBy            = @UpdatedBy,
                            UpdatedAt            = GETDATE()
                        WHERE Id = @Id";

                    await dbContext.ExecuteAsync(sql, new
                    {
                        request.Id,
                        request.Title,
                        request.Description,
                        request.Priority,
                        request.AssigneeEmployeeCode,
                        request.AssigneeUsername,
                        request.DueDate,
                        request.StartDate,
                        request.Tags,
                        request.EstimatedHours,
                        request.ActualHours,
                        request.DepartmentCode,
                        UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateTaskCommand.Response { Id = request.Id };
                    var response     = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);

                    log.Result     = responseData;
                    log.ReturnCode = response.ReturnCode;
                    log.Message    = response.Message;

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);

                    log.IsException = true;
                    log.Message     = ex.Message;
                    log.ReturnCode  = CoreApiReturnCode.ExceptionOccurred;

                    UniLogger.Error($"[UpdateTask] Error updating task {request.Id}: {ex.Message}", ex);
                    return ResponseHelper.Error<UpdateTaskCommand.Response>("Error occurred while updating task");
                }
                finally
                {
                    UniLogManager.WriteApiLog(log);
                }
            }
        }
    }

    #endregion
}
