using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.EmployeeShifts
{
    public sealed class GetEmployeeShiftListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetEmployeeShiftListQuery.Result>>>
    {
        public string? EmployeeCode { get; set; }
        public string? WorkShiftCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

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
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetEmployeeShiftListQueryValidator : AbstractValidator<GetEmployeeShiftListQuery>
    {
        public GetEmployeeShiftListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");

            When(x => x.FromDate.HasValue && x.ToDate.HasValue, () =>
            {
                RuleFor(x => x.ToDate)
                    .GreaterThanOrEqualTo(x => x.FromDate)
                    .WithMessage("ToDate must be greater than or equal to FromDate");
            });
        }
    }

    public sealed class GetEmployeeShiftListQueryHandler : IRequestHandler<GetEmployeeShiftListQuery, ApiResponse<PagedResult<GetEmployeeShiftListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetEmployeeShiftListQuery.Result>>> Handle(GetEmployeeShiftListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                    new CoreParamModel(nameof(request.WorkShiftCode), request.WorkShiftCode),
                    new CoreParamModel(nameof(request.FromDate), request.FromDate?.ToString("yyyy-MM-dd")),
                    new CoreParamModel(nameof(request.ToDate), request.ToDate?.ToString("yyyy-MM-dd"))
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            es.Id,
                            es.EmployeeCode,
                            e.DisplayName AS EmployeeName,
                            es.WorkShiftCode,
                            ws.Name AS WorkShiftName,
                            es.WorkDate,
                            ws.StartTime,
                            ws.EndTime,
                            es.CreatedAt
                        FROM hr_employee_shifts es
                        INNER JOIN hr_employees e ON es.EmployeeCode = e.EmployeeCode
                        INNER JOIN hr_work_shifts ws ON es.WorkShiftCode = ws.Code
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.EmployeeCode))
                    {
                        sql.AppendLine("AND es.EmployeeCode = @EmployeeCode");
                        parameters.Add("EmployeeCode", request.EmployeeCode);
                    }

                    if (!string.IsNullOrEmpty(request.WorkShiftCode))
                    {
                        sql.AppendLine("AND es.WorkShiftCode = @WorkShiftCode");
                        parameters.Add("WorkShiftCode", request.WorkShiftCode);
                    }

                    if (request.FromDate.HasValue)
                    {
                        sql.AppendLine("AND es.WorkDate >= @FromDate");
                        parameters.Add("FromDate", request.FromDate.Value);
                    }

                    if (request.ToDate.HasValue)
                    {
                        sql.AppendLine("AND es.WorkDate <= @ToDate");
                        parameters.Add("ToDate", request.ToDate.Value);
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (e.DisplayName LIKE @Keyword OR ws.Name LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "es.WorkDate DESC, es.CreatedAt" },
                        { "workDate", "es.WorkDate" },
                        { "employeeName", "e.DisplayName" },
                        { "workShiftName", "ws.Name" },
                        { "createdAt", "es.CreatedAt" }
                    };

                    var (orderBy, _) = QueryHelper.BuildOrderByClause(
                        request.SortBy,
                        request.SortDirection ?? "DESC",
                        columnMappings);

                    sql.AppendLine($"ORDER BY {orderBy}");
                    sql.AppendLine("OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

                    parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);
                    parameters.Add("PageSize", request.PageSize);

                    var countSql = new StringBuilder();
                    countSql.AppendLine(@"
                        SELECT COUNT(*)
                        FROM hr_employee_shifts es
                        INNER JOIN hr_employees e ON es.EmployeeCode = e.EmployeeCode
                        INNER JOIN hr_work_shifts ws ON es.WorkShiftCode = ws.Code
                        WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.EmployeeCode))
                    {
                        countSql.AppendLine("AND es.EmployeeCode = @EmployeeCode");
                    }

                    if (!string.IsNullOrEmpty(request.WorkShiftCode))
                    {
                        countSql.AppendLine("AND es.WorkShiftCode = @WorkShiftCode");
                    }

                    if (request.FromDate.HasValue)
                    {
                        countSql.AppendLine("AND es.WorkDate >= @FromDate");
                    }

                    if (request.ToDate.HasValue)
                    {
                        countSql.AppendLine("AND es.WorkDate <= @ToDate");
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (e.DisplayName LIKE @Keyword OR ws.Name LIKE @Keyword)");
                    }

                    var items = await dbContext.QueryAsync<GetEmployeeShiftListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetEmployeeShiftListQuery.Result>
                    {
                        Items = items.ToList(),
                        Paging = new PagingInfo
                        {
                            PageIndex = request.PageIndex,
                            PageSize = request.PageSize,
                            TotalItems = totalItems
                        }
                    };

                    var response = ResponseHelper.Success(result, CoreResource.crud_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving employee shifts: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetEmployeeShiftListQuery.Result>>("Error occurred while retrieving employee shifts");

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
