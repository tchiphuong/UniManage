using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Attendance
{
    #region Command

    public sealed class CheckInCommand : BaseCommand, IRequest<ApiResponse<CheckInCommand.Response>>
    {
        public string EmployeeCode { get; init; } = default!;
        public DateTime? AttendanceDate { get; init; }
        public TimeSpan? CheckInTime { get; init; }

        public sealed class Response
        {
            public bool Success => Id > 0;
            public int Id { get; init; }
            public TimeSpan CheckInTime { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class CheckInCommandValidator : AbstractValidator<CheckInCommand>
    {
        public CheckInCommandValidator()
        {
            RuleFor(x => x.EmployeeCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code))
                .WithMessage("Employee not found");
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
    }

    #endregion

    #region Handler

    public sealed class CheckInCommandHandler : IRequestHandler<CheckInCommand, ApiResponse<CheckInCommand.Response>>
    {
        public async Task<ApiResponse<CheckInCommand.Response>> Handle(CheckInCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                    new CoreParamModel(nameof(request.AttendanceDate), request.AttendanceDate),
                    new CoreParamModel(nameof(request.CheckInTime), request.CheckInTime)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var attendanceDate = request.AttendanceDate ?? DateTime.Today;
                    var checkInTime = request.CheckInTime ?? DateTime.Now.TimeOfDay;

                    var existingId = await dbContext.ExecuteScalarAsync<int?>(
                        @"SELECT Id FROM hr_attendance 
                          WHERE EmployeeCode = @EmployeeCode AND AttendanceDate = @AttendanceDate",
                        new { request.EmployeeCode, AttendanceDate = attendanceDate },
                        ct);

                    int id;
                    if (existingId.HasValue)
                    {
                        await dbContext.ExecuteAsync(
                            @"UPDATE hr_attendance 
                              SET CheckInTime = @CheckInTime,
                                  Status = 'Present',
                                  UpdatedBy = @UpdatedBy,
                                  UpdatedAt = GETDATE()
                              WHERE Id = @Id",
                            new
                            {
                                Id = existingId.Value,
                                CheckInTime = checkInTime,
                                UpdatedBy = request.HeaderInfo!.Username
                            },
                            ct);
                        id = existingId.Value;
                    }
                    else
                    {
                        id = await dbContext.ExecuteScalarAsync<int>(
                            @"INSERT INTO hr_attendance (EmployeeCode, AttendanceDate, CheckInTime, Status, CreatedBy, CreatedAt, DataRowVersion)
                              VALUES (@EmployeeCode, @AttendanceDate, @CheckInTime, 'Present', @CreatedBy, GETDATE(), 0x00000000000000000001);
                              SELECT SCOPE_IDENTITY();",
                            new
                            {
                                request.EmployeeCode,
                                AttendanceDate = attendanceDate,
                                CheckInTime = checkInTime,
                                CreatedBy = request.HeaderInfo!.Username
                            },
                            ct);
                    }

                    await dbContext.CommitAsync();

                    var responseData = new CheckInCommand.Response { Id = id, CheckInTime = checkInTime };
                    var response = ResponseHelper.Success(responseData, "Check-in successful");

                    log.Result = response.Data;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error checking in: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CheckInCommand.Response>(CoreResource.common_exceptionOccurred);
                    log.Message = ex.ToString();
                    log.IsException = 1;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }

    #endregion
}
