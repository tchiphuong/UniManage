using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.EmployeeShifts
{
    public sealed class UpdateEmployeeShiftCommand : BaseCommand, IRequest<ApiResponse<UpdateEmployeeShiftCommand.Response>>
    {
        public int Id { get; init; }
        public string EmployeeCode { get; init; } = default!;
        public string WorkShiftCode { get; init; } = default!;
        public DateTime WorkDate { get; init; }
        public byte[] DataRowVersion { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateEmployeeShiftCommandValidator : AbstractValidator<UpdateEmployeeShiftCommand>
    {
        public UpdateEmployeeShiftCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("Employee code is required")
                .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code))
                .WithMessage("Employee does not exist");

            RuleFor(x => x.WorkShiftCode)
                .NotEmpty().WithMessage("Work shift code is required")
                .MustAsync(async (code, cancel) => await IsWorkShiftExistsAsync(code))
                .WithMessage("Work shift does not exist");

            RuleFor(x => x.WorkDate)
                .NotEmpty().WithMessage("Work date is required");

            RuleFor(x => x.DataRowVersion)
                .NotEmpty().WithMessage("DataRowVersion is required for concurrency check");

            RuleFor(x => x)
                .MustAsync(async (cmd, cancel) => !await IsDuplicateShiftAssignmentAsync(cmd.Id, cmd.EmployeeCode, cmd.WorkDate))
                .WithMessage("Employee already has a shift assigned for this date");
        }

        private static async Task<bool> IsEmployeeExistsAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                    new { EmployeeCode = employeeCode });
            }
        }

        private static async Task<bool> IsWorkShiftExistsAsync(string workShiftCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_work_shifts WHERE Code = @WorkShiftCode) THEN 1 ELSE 0 END",
                    new { WorkShiftCode = workShiftCode });
            }
        }

        private static async Task<bool> IsDuplicateShiftAssignmentAsync(int id, string employeeCode, DateTime workDate)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employee_shifts WHERE EmployeeCode = @EmployeeCode AND WorkDate = @WorkDate AND Id != @Id) THEN 1 ELSE 0 END",
                    new { Id = id, EmployeeCode = employeeCode, WorkDate = workDate });
            }
        }
    }

    public sealed class UpdateEmployeeShiftCommandHandler : IRequestHandler<UpdateEmployeeShiftCommand, ApiResponse<UpdateEmployeeShiftCommand.Response>>
    {
        public async Task<ApiResponse<UpdateEmployeeShiftCommand.Response>> Handle(UpdateEmployeeShiftCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id),
                    new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                    new CoreParamModel(nameof(request.WorkShiftCode), request.WorkShiftCode),
                    new CoreParamModel(nameof(request.WorkDate), request.WorkDate.ToString("yyyy-MM-dd"))
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        UPDATE hr_employee_shifts
                        SET EmployeeCode = @EmployeeCode,
                            WorkShiftCode = @WorkShiftCode,
                            WorkDate = @WorkDate,
                            UpdatedBy = @UpdatedBy,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id AND DataRowVersion = @DataRowVersion";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Id,
                        request.EmployeeCode,
                        request.WorkShiftCode,
                        request.WorkDate,
                        UpdatedBy = request.HeaderInfo!.Username,
                        request.DataRowVersion
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateEmployeeShiftCommand.Response>("Employee shift has been modified by another user or does not exist");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateEmployeeShiftCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_updateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error updating employee shift: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateEmployeeShiftCommand.Response>("Error occurred while updating employee shift");

                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}
