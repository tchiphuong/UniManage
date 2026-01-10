using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.EmployeeShifts
{
    public sealed class GetEmployeeShiftByIdQuery : BaseQuery, IRequest<ApiResponse<GetEmployeeShiftByIdQuery.Result>>
    {
        public int Id { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string EmployeeCode { get; set; } = default!;
            public string EmployeeName { get; set; } = default!;
            public string WorkShiftCode { get; set; } = default!;
            public string WorkShiftName { get; set; } = default!;
            public DateTime WorkDate { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string CreatedBy { get; set; } = default!;
            public DateTime CreatedAt { get; set; }
            public string UpdatedBy { get; set; } = default!;
            public DateTime UpdatedAt { get; set; }
            public byte[] DataRowVersion { get; set; } = default!;
        }
    }

    public sealed class GetEmployeeShiftByIdQueryValidator : AbstractValidator<GetEmployeeShiftByIdQuery>
    {
        public GetEmployeeShiftByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }

    public sealed class GetEmployeeShiftByIdQueryHandler : IRequestHandler<GetEmployeeShiftByIdQuery, ApiResponse<GetEmployeeShiftByIdQuery.Result>>
    {
        public async Task<ApiResponse<GetEmployeeShiftByIdQuery.Result>> Handle(GetEmployeeShiftByIdQuery request, CancellationToken ct)
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
                    var sql = @"
                        SELECT 
                            es.Id,
                            es.EmployeeCode,
                            e.DisplayName AS EmployeeName,
                            es.WorkShiftCode,
                            ws.Name AS WorkShiftName,
                            es.WorkDate,
                            ws.StartTime,
                            ws.EndTime,
                            es.CreatedBy,
                            es.CreatedAt,
                            es.UpdatedBy,
                            es.UpdatedAt,
                            es.DataRowVersion
                        FROM hr_employee_shifts es
                        INNER JOIN hr_employees e ON es.EmployeeCode = e.EmployeeCode
                        INNER JOIN hr_work_shifts ws ON es.WorkShiftCode = ws.Code
                        WHERE es.Id = @Id";

                    var result = await dbContext.QueryFirstOrDefaultAsync<GetEmployeeShiftByIdQuery.Result>(sql, new { request.Id }, ct);

                    if (result == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetEmployeeShiftByIdQuery.Result>("Employee shift not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(result, CoreResource.Common_msg_GetSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving employee shift by id: {ex.Message}", ex);

                    var response = ResponseHelper.Error<GetEmployeeShiftByIdQuery.Result>("Error occurred while retrieving employee shift");

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
