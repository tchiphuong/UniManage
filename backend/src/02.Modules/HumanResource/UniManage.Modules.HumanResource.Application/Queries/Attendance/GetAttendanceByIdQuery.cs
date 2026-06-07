using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Queries.Attendance
{
    #region Query

    public sealed class GetAttendanceByIdQuery : BaseQuery, IRequest<ApiResponse<GetAttendanceByIdQuery.Response>>
    {
        public long Id { get; init; }

        public sealed class Response
        {
            public long Id { get; set; }
            public long EmployeeId { get; set; }
            public DateTime AttendanceDate { get; set; }
            public TimeSpan? CheckInTime { get; set; }
            public TimeSpan? CheckOutTime { get; set; }
            public byte Status { get; set; }
            public string? Note { get; set; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetAttendanceByIdQueryValidator : AbstractValidator<GetAttendanceByIdQuery>
    {
        public GetAttendanceByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }

    #endregion

    #region Handler

    public sealed class GetAttendanceByIdQueryHandler : IRequestHandler<GetAttendanceByIdQuery, ApiResponse<GetAttendanceByIdQuery.Response>>
    {
        public async Task<ApiResponse<GetAttendanceByIdQuery.Response>> Handle(GetAttendanceByIdQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Method = nameof(GetAttendanceByIdQueryHandler),
                Path = "Attendance",
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Id), request.Id)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = "SELECT Id, EmployeeId, AttendanceDate, CheckInTime, CheckOutTime, Status, Note FROM HrAttendances WHERE Id = @Id;";
                    var item = await dbContext.QueryFirstOrDefaultAsync<GetAttendanceByIdQuery.Response>(sql, new { request.Id });

                    if (item == null)
                    {
                        var notFound = ResponseHelper.NotFound<GetAttendanceByIdQuery.Response>(string.Format(CoreResource.common_notFound, "Attendance"));
                        log.ReturnCode = notFound.ReturnCode;
                        log.Message = notFound.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFound;
                    }

                    var response = ResponseHelper.Success(item, string.Format(CoreResource.common_getSuccess, "Attendance"));

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                    UniLogManager.WriteApiLog(log);
                    return ResponseHelper.Error<GetAttendanceByIdQuery.Response>(CoreResource.common_exceptionOccurred);
                }
            }
        }
    }

    #endregion
}




