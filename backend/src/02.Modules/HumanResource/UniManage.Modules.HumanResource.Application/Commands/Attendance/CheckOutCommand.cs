using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Commands.Attendance
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
                .NotEmpty().WithMessage(CoreResource.validation_required);
        }
    }

    #endregion

    #region Handler

    public sealed class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, ApiResponse<CheckOutCommand.Response>>
    {
        public async Task<ApiResponse<CheckOutCommand.Response>> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var attendanceDate = request.AttendanceDate ?? DateTime.Today;
                    var checkOutTime = request.CheckOutTime ?? DateTimeHelper.Now.TimeOfDay;

                    var attendance = await dbContext.QueryFirstOrDefaultAsync<dynamic>(
                        @"SELECT Id, CheckInTime FROM HrAttendance 
                          WHERE EmployeeCode = @EmployeeCode AND AttendanceDate = @AttendanceDate",
                        new { request.EmployeeCode, AttendanceDate = attendanceDate },
                        cancellationToken);

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
                        @"UPDATE HrAttendance 
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
                        cancellationToken);

                    

                    var responseData = new CheckOutCommand.Response { Success = true, Id = attendance.Id, CheckOutTime = checkOutTime };
                    var response = ResponseHelper.Success(responseData, "Check-out successful");
return response;
        }
    }

    #endregion
}





