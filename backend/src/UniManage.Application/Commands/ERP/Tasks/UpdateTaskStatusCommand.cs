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

    /// <summary>Cập nhật trạng thái của ERP Task (kanban-style move)</summary>
    public sealed class UpdateTaskStatusCommand : BaseCommand, IRequest<ApiResponse<UpdateTaskStatusCommand.Response>>
    {
        /// <summary>Set by controller from route — not in request body</summary>
        public long Id { get; set; }

        public string Status { get; init; } = default!;

        /// <summary>Optionally record actual hours when closing the task</summary>
        public decimal? ActualHours { get; init; }

        public sealed class Response
        {
            public long Id { get; init; }
            public string Status { get; init; } = default!;
        }
    }

    #endregion

    #region Validator

    public sealed class UpdateTaskStatusCommandValidator : AbstractValidator<UpdateTaskStatusCommand>
    {
        public UpdateTaskStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Id must be greater than 0")
                .MustAsync(async (id, cancel) => await IsTaskExistsAsync(id))
                .WithMessage("Task not found");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(s => CoreCommon.Value.Taskstatus.All.Contains(s))
                .WithMessage($"Status must be one of: {string.Join(", ", CoreCommon.Value.Taskstatus.All)}");

            RuleFor(x => x.ActualHours)
                .GreaterThanOrEqualTo(0).WithMessage("ActualHours must be 0 or greater")
                .When(x => x.ActualHours.HasValue);
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
    }

    #endregion

    #region Handler

    public sealed class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, ApiResponse<UpdateTaskStatusCommand.Response>>
    {
        public async Task<ApiResponse<UpdateTaskStatusCommand.Response>> Handle(UpdateTaskStatusCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id),
                    new CoreParamModel(nameof(request.Status), request.Status)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    // CompletedAt is stamped only when transitioning INTO "done"
                    var isDone = string.Equals(request.Status, CoreCommon.Value.Taskstatus.Done,
                        StringComparison.OrdinalIgnoreCase);

                    var sql = @"
                        UPDATE erp_tasks SET
                            Status      = @Status,
                            CompletedAt = CASE WHEN @IsDone = 1 THEN GETDATE() ELSE CompletedAt END,
                            ActualHours = CASE WHEN @ActualHours IS NOT NULL THEN @ActualHours ELSE ActualHours END,
                            UpdatedBy   = @UpdatedBy,
                            UpdatedAt   = GETDATE()
                        WHERE Id = @Id";

                    await dbContext.ExecuteAsync(sql, new
                    {
                        request.Id,
                        request.Status,
                        IsDone      = isDone ? 1 : 0,
                        request.ActualHours,
                        UpdatedBy   = request.HeaderInfo?.Username ?? CoreConstant.SystemUser
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateTaskStatusCommand.Response
                    {
                        Id     = request.Id,
                        Status = request.Status
                    };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);

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

                    UniLogger.Error($"[UpdateTaskStatus] Error updating status for task {request.Id}: {ex.Message}", ex);
                    return ResponseHelper.Error<UpdateTaskStatusCommand.Response>("Error occurred while updating task status");
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
