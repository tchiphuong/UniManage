using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Workflow.Requests
{
    public sealed class GetRequestListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetRequestListQuery.Result>>>
    {
        public string? RequestTypeKey { get; set; }
        public string? Status { get; set; }
        public string? ApplicantEmployeeCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string RequestTypeKey { get; set; } = default!;
            public string ApplicantEmployeeCode { get; set; } = default!;
            public string ApplicantUsername { get; set; } = default!;
            public string ApplicantEmployeeName { get; set; } = default!;
            public string? RouteName { get; set; }
            public string Status { get; set; } = default!;
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetRequestListQueryValidator : AbstractValidator<GetRequestListQuery>
    {
        public GetRequestListQueryValidator()
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

    public sealed class GetRequestListQueryHandler : IRequestHandler<GetRequestListQuery, ApiResponse<PagedResult<GetRequestListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetRequestListQuery.Result>>> Handle(GetRequestListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.RequestTypeKey), request.RequestTypeKey),
                    new CoreParamModel(nameof(request.Status), request.Status),
                    new CoreParamModel(nameof(request.ApplicantEmployeeCode), request.ApplicantEmployeeCode),
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
                            r.Id,
                            r.RequestTypeKey,
                            r.ApplicantEmployeeCode,
                            r.ApplicantUsername,
                            e.DisplayName AS ApplicantEmployeeName,
                            ar.RouteName,
                            r.Status,
                            r.CreatedAt
                        FROM wf_request r
                        LEFT JOIN hr_employees e ON r.ApplicantEmployeeCode = e.EmployeeCode
                        LEFT JOIN wf_approval_route ar ON r.ApprovalRouteId = ar.Id
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.RequestTypeKey))
                    {
                        sql.AppendLine("AND r.RequestTypeKey = @RequestTypeKey");
                        parameters.Add("RequestTypeKey", request.RequestTypeKey);
                    }

                    if (!string.IsNullOrEmpty(request.Status))
                    {
                        sql.AppendLine("AND r.Status = @Status");
                        parameters.Add("Status", request.Status);
                    }

                    if (!string.IsNullOrEmpty(request.ApplicantEmployeeCode))
                    {
                        sql.AppendLine("AND r.ApplicantEmployeeCode = @ApplicantEmployeeCode");
                        parameters.Add("ApplicantEmployeeCode", request.ApplicantEmployeeCode);
                    }

                    if (request.FromDate.HasValue)
                    {
                        sql.AppendLine("AND r.CreatedAt >= @FromDate");
                        parameters.Add("FromDate", request.FromDate.Value);
                    }

                    if (request.ToDate.HasValue)
                    {
                        sql.AppendLine("AND r.CreatedAt <= @ToDate");
                        parameters.Add("ToDate", request.ToDate.Value.AddDays(1));
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (e.DisplayName LIKE @Keyword OR r.ApplicantUsername LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "r.CreatedAt DESC" },
                        { "createdAt", "r.CreatedAt" },
                        { "status", "r.Status" },
                        { "requestTypeKey", "r.RequestTypeKey" }
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
                        FROM wf_request r
                        LEFT JOIN hr_employees e ON r.ApplicantEmployeeCode = e.EmployeeCode
                        WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.RequestTypeKey))
                    {
                        countSql.AppendLine("AND r.RequestTypeKey = @RequestTypeKey");
                    }

                    if (!string.IsNullOrEmpty(request.Status))
                    {
                        countSql.AppendLine("AND r.Status = @Status");
                    }

                    if (!string.IsNullOrEmpty(request.ApplicantEmployeeCode))
                    {
                        countSql.AppendLine("AND r.ApplicantEmployeeCode = @ApplicantEmployeeCode");
                    }

                    if (request.FromDate.HasValue)
                    {
                        countSql.AppendLine("AND r.CreatedAt >= @FromDate");
                    }

                    if (request.ToDate.HasValue)
                    {
                        countSql.AppendLine("AND r.CreatedAt <= @ToDate");
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (e.DisplayName LIKE @Keyword OR r.ApplicantUsername LIKE @Keyword)");
                    }

                    var items = await dbContext.QueryAsync<GetRequestListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetRequestListQuery.Result>
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
                    UniLogger.Error($"Error retrieving requests: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetRequestListQuery.Result>>("Error occurred while retrieving requests");

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
