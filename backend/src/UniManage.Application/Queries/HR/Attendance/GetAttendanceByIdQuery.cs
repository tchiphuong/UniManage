using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.Attendance
{
    #region Query

    public sealed class GetAttendanceByIdQuery : BaseQuery, IRequest<ApiResponse<GetAttendanceByIdQuery.Response>>
    {
        public int Id { get; init; }

        public sealed record Response
        {
            public int Id { get; set; }
            public string EmployeeCode { get; set; } = default!;
            public string EmployeeName { get; set; } = default!;
            public DateTime AttendanceDate { get; set; }
            public TimeSpan? CheckInTime { get; set; }
            public TimeSpan? CheckOutTime { get; set; }
            public string Status { get; set; } = default!;
            public string? CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
            public string? UpdatedBy { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public byte[] DataRowVersion { get; set; } = default!;
        }
    }

    #endregion

    #region Validator

    public sealed class GetAttendanceByIdQueryValidator : AbstractValidator<GetAttendanceByIdQuery>
    {
        public GetAttendanceByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(CoreResource.Validation_msg_Required);
        }
    }

    #endregion

    #region Handler

    public sealed class GetAttendanceByIdQueryHandler : IRequestHandler<GetAttendanceByIdQuery, ApiResponse<GetAttendanceByIdQuery.Response>>
    {
        public async Task<ApiResponse<GetAttendanceByIdQuery.Response>> Handle(GetAttendanceByIdQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var attendance = await dbContext.QueryFirstOrDefaultAsync<GetAttendanceByIdQuery.Response>(
                        @"SELECT a.Id, a.EmployeeCode, e.FullName AS EmployeeName, a.AttendanceDate,
                                 a.CheckInTime, a.CheckOutTime, a.Status,
                                 a.CreatedBy, a.CreatedAt, a.UpdatedBy, a.UpdatedAt, a.DataRowVersion
                          FROM hr_attendance a
                          INNER JOIN hr_employees e ON a.EmployeeCode = e.EmployeeCode
                          WHERE a.Id = @Id",
                        new { request.Id },
                        ct);

                    if (attendance == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetAttendanceByIdQuery.Response>(CoreResource.Common_msg_NotFound);
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(attendance, CoreResource.Common_msg_GetSuccess);
                    log.Result = attendance;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error getting attendance: {ex.Message}", ex);
                    var response = ResponseHelper.Error<GetAttendanceByIdQuery.Response>(CoreResource.Common_msg_ExceptionOccurred);

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
