using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.Attendance
{
    #region Query

    public sealed class GetAttendanceListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetAttendanceListQuery.Response>>>
    {
        public string? EmployeeCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Status { get; set; }

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
        }
    }

    #endregion

    #region Validator

    public sealed class GetAttendanceListQueryValidator : AbstractValidator<GetAttendanceListQuery>
    {
        public GetAttendanceListQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);

            RuleFor(x => x.ToDate)
                .GreaterThanOrEqualTo(x => x.FromDate)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue)
                .WithMessage("ToDate must be greater than or equal to FromDate");
        }
    }

    #endregion

    #region Handler

    public sealed class GetAttendanceListQueryHandler : IRequestHandler<GetAttendanceListQuery, ApiResponse<PagedResult<GetAttendanceListQuery.Response>>>
    {
        public async Task<ApiResponse<PagedResult<GetAttendanceListQuery.Response>>> Handle(GetAttendanceListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                    new CoreParamModel(nameof(request.FromDate), request.FromDate),
                    new CoreParamModel(nameof(request.ToDate), request.ToDate),
                    new CoreParamModel(nameof(request.Status), request.Status)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var query = new StringBuilder();
                    query.AppendLine($@"SELECT 
                                        a.Id                   AS {nameof(GetAttendanceListQuery.Response.Id)},
                                        a.EmployeeCode         AS {nameof(GetAttendanceListQuery.Response.EmployeeCode)},
                                        e.FullName             AS {nameof(GetAttendanceListQuery.Response.EmployeeName)},
                                        a.AttendanceDate       AS {nameof(GetAttendanceListQuery.Response.AttendanceDate)},
                                        a.CheckInTime          AS {nameof(GetAttendanceListQuery.Response.CheckInTime)},
                                        a.CheckOutTime         AS {nameof(GetAttendanceListQuery.Response.CheckOutTime)},
                                        a.Status               AS {nameof(GetAttendanceListQuery.Response.Status)},
                                        a.CreatedBy            AS {nameof(GetAttendanceListQuery.Response.CreatedBy)},
                                        a.CreatedAt            AS {nameof(GetAttendanceListQuery.Response.CreatedAt)}
                                    FROM hr_attendance a
                                    INNER JOIN hr_employees e ON a.EmployeeCode = e.EmployeeCode
                                    WHERE 1 = 1");

                    if (!string.IsNullOrEmpty(request.EmployeeCode))
                    {
                        query.AppendLine("AND a.EmployeeCode = @EmployeeCode");
                    }

                    if (request.FromDate.HasValue)
                    {
                        query.AppendLine("AND a.AttendanceDate >= @FromDate");
                    }

                    if (request.ToDate.HasValue)
                    {
                        query.AppendLine("AND a.AttendanceDate <= @ToDate");
                    }

                    if (!string.IsNullOrEmpty(request.Status))
                    {
                        query.AppendLine("AND a.Status = @Status");
                    }

                    var result = await dbContext.QueryPagingAsync<GetAttendanceListQuery.Response>(query, request);

                    var response = ResponseHelper.Success(result, CoreResource.crud_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving attendance: {ex.Message}", ex);
                    var response = ResponseHelper.Error<PagedResult<GetAttendanceListQuery.Response>>(CoreResource.common_exceptionOccurred);

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
