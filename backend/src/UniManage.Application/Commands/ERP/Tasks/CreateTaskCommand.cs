using Dapper;
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

    /// <summary>Tạo mới một ERP Task</summary>
    public sealed class CreateTaskCommand : BaseCommand, IRequest<ApiResponse<CreateTaskCommand.Response>>
    {
        public string Title { get; init; } = default!;
        public string? Description { get; init; }

        /// <summary>Defaults to Medium if not supplied by client</summary>
        public string Priority { get; init; } = CoreCommon.Value.Taskpriority.Medium;

        public string? AssigneeEmployeeCode { get; init; }
        public string? AssigneeUsername { get; init; }
        public DateTime? DueDate { get; init; }
        public DateTime? StartDate { get; init; }
        public string? Tags { get; init; }
        public decimal? EstimatedHours { get; init; }
        public string? DepartmentCode { get; init; }

        public sealed class Response
        {
            public long Id { get; init; }
            public string TaskCode { get; init; } = default!;
        }
    }

    #endregion

    #region Validator

    public sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
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

            RuleFor(x => x.DepartmentCode)
                .MaximumLength(50).WithMessage("DepartmentCode must not exceed 50 characters")
                .When(x => x.DepartmentCode != null);

            // Due date must be on/after start date when both are provided
            When(x => x.DueDate.HasValue && x.StartDate.HasValue, () =>
            {
                RuleFor(x => x.DueDate)
                    .GreaterThanOrEqualTo(x => x.StartDate)
                    .WithMessage("DueDate must be on or after StartDate");
            });

            // Validate assignee employee exists when code is provided
            When(x => !string.IsNullOrEmpty(x.AssigneeEmployeeCode), () =>
            {
                RuleFor(x => x.AssigneeEmployeeCode!)
                    .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code))
                    .WithMessage("Assignee employee does not exist");
            });
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

    public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, ApiResponse<CreateTaskCommand.Response>>
    {
        public async Task<ApiResponse<CreateTaskCommand.Response>> Handle(CreateTaskCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Title), request.Title),
                    new CoreParamModel(nameof(request.Priority), request.Priority),
                    new CoreParamModel(nameof(request.AssigneeEmployeeCode), request.AssigneeEmployeeCode)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    // Generate a human-readable, date-partitioned task code
                    var taskCode = await GenerateTaskCodeAsync(dbContext, ct);

                    var sql = @"
                        INSERT INTO erp_tasks (
                            TaskCode, Title, Description, Status, Priority,
                            AssigneeEmployeeCode, AssigneeUsername, ReporterUsername,
                            DueDate, StartDate, Tags, EstimatedHours, DepartmentCode,
                            CreatedBy, CreatedAt, UpdatedBy, UpdatedAt
                        ) VALUES (
                            @TaskCode, @Title, @Description, @Status, @Priority,
                            @AssigneeEmployeeCode, @AssigneeUsername, @ReporterUsername,
                            @DueDate, @StartDate, @Tags, @EstimatedHours, @DepartmentCode,
                            @CreatedBy, GETDATE(), @CreatedBy, GETDATE()
                        );
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<long>(sql, new
                    {
                        TaskCode = taskCode,
                        request.Title,
                        request.Description,
                        Status             = CoreCommon.Value.Taskstatus.Todo,
                        request.Priority,
                        request.AssigneeEmployeeCode,
                        request.AssigneeUsername,
                        ReporterUsername   = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                        request.DueDate,
                        request.StartDate,
                        request.Tags,
                        request.EstimatedHours,
                        request.DepartmentCode,
                        CreatedBy          = request.HeaderInfo?.Username ?? CoreConstant.SystemUser
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateTaskCommand.Response { Id = id, TaskCode = taskCode };
                    var response     = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);

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

                    UniLogger.Error($"[CreateTask] Error creating task: {ex.Message}", ex);
                    return ResponseHelper.Error<CreateTaskCommand.Response>("Error occurred while creating task");
                }
                finally
                {
                    UniLogManager.WriteApiLog(log);
                }
            }
        }

        /// <summary>
        /// Generates TASK-YYYYMMDD-NNNN codes that are unique per day.
        /// Runs inside the existing transaction so the count is consistent.
        /// </summary>
        private static async Task<string> GenerateTaskCodeAsync(DbContext dbContext, CancellationToken ct)
        {
            var datePart = DateTime.Now.ToString("yyyyMMdd");
            var count    = await dbContext.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM erp_tasks WHERE TaskCode LIKE @Pattern",
                new { Pattern = $"TASK-{datePart}-%" },
                ct);

            return $"TASK-{datePart}-{(count + 1):D4}";
        }
    }

    #endregion
}
