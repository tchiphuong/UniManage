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

    public sealed class CheckOutCommand : BaseCommand, IRequest<ApiResponse<CheckOutCommand.Response>>
    {
        public string EmployeeCode { get; init; } = default!;
        public DateTime? AttendanceDate { get; init; }
        public TimeSpan? CheckOutTime { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
            public int Id { get; init; }
            public TimeSpan CheckOutTime { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class CheckOutCommandValidator : AbstractValidator<CheckOutCommand>
    {
        public CheckOutCommandValidator()
        {
            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required);
        }
    }

    #endregion

    #region Handler

    public sealed class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, ApiResponse<CheckOutCommand.Response>>
    {
        public async Task<ApiResponse<CheckOutCommand.Response>> Handle(CheckOutCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                    new CoreParamModel(nameof(request.AttendanceDate), request.AttendanceDate),
                    new CoreParamModel(nameof(request.CheckOutTime), request.CheckOutTime)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var attendanceDate = request.AttendanceDate ?? DateTime.Today;
                    var checkOutTime = request.CheckOutTime ?? DateTime.Now.TimeOfDay;

                    var attendance = await dbContext.QueryFirstOrDefaultAsync<dynamic>(
                        @"SELECT Id, CheckInTime FROM hr_attendance 
                          WHERE EmployeeCode = @EmployeeCode AND AttendanceDate = @AttendanceDate",
                        new { request.EmployeeCode, AttendanceDate = attendanceDate },
                        ct);

                    if (attendance == null)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.Error<CheckOutCommand.Response>("No check-in record found for today");
                    }

                    if (attendance.CheckInTime == null)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.Error<CheckOutCommand.Response>("Please check-in first");
                    }

                    await dbContext.ExecuteAsync(
                        @"UPDATE hr_attendance 
                          SET CheckOutTime = @CheckOutTime,
                              UpdatedBy = @UpdatedBy,
                              UpdatedAt = GETDATE()
                          WHERE Id = @Id",
                        new
                        {
                            Id = (int)attendance.Id,
                            CheckOutTime = checkOutTime,
                            UpdatedBy = request.HeaderInfo!.Username
                        },
                        ct);

                    await dbContext.CommitAsync();

                    var responseData = new CheckOutCommand.Response { Success = true, Id = attendance.Id, CheckOutTime = checkOutTime };
                    var response = ResponseHelper.Success(responseData, "Check-out successful");

                    log.Result = response.Data;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error checking out: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CheckOutCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
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
