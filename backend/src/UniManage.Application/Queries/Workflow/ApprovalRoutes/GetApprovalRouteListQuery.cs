using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Workflow.ApprovalRoutes
{
    public sealed class GetApprovalRouteListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetApprovalRouteListQuery.Result>>>
    {
        public string? RequestTypeKey { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string RequestTypeKey { get; set; } = default!;
            public string RouteName { get; set; } = default!;
            public string? Description { get; set; }
            public int LevelCount { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetApprovalRouteListQueryValidator : AbstractValidator<GetApprovalRouteListQuery>
    {
        public GetApprovalRouteListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetApprovalRouteListQueryHandler : IRequestHandler<GetApprovalRouteListQuery, ApiResponse<PagedResult<GetApprovalRouteListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetApprovalRouteListQuery.Result>>> Handle(GetApprovalRouteListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.RequestTypeKey), request.RequestTypeKey)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            ar.Id,
                            ar.RequestTypeKey,
                            ar.RouteName,
                            ar.Description,
                            COUNT(arl.Id) AS LevelCount,
                            ar.CreatedAt
                        FROM wf_approval_route ar
                        LEFT JOIN wf_approval_route_level arl ON ar.Id = arl.ApprovalRouteId
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.RequestTypeKey))
                    {
                        sql.AppendLine("AND ar.RequestTypeKey = @RequestTypeKey");
                        parameters.Add("RequestTypeKey", request.RequestTypeKey);
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (ar.RouteName LIKE @Keyword OR ar.Description LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    sql.AppendLine("GROUP BY ar.Id, ar.RequestTypeKey, ar.RouteName, ar.Description, ar.CreatedAt");

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "ar.CreatedAt DESC" },
                        { "routeName", "ar.RouteName" },
                        { "requestTypeKey", "ar.RequestTypeKey" },
                        { "createdAt", "ar.CreatedAt" }
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
                    countSql.AppendLine("SELECT COUNT(*) FROM wf_approval_route ar WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.RequestTypeKey))
                    {
                        countSql.AppendLine("AND ar.RequestTypeKey = @RequestTypeKey");
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (ar.RouteName LIKE @Keyword OR ar.Description LIKE @Keyword)");
                    }

                    var items = await dbContext.QueryAsync<GetApprovalRouteListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetApprovalRouteListQuery.Result>
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
                    UniLogger.Error($"Error retrieving approval routes: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetApprovalRouteListQuery.Result>>("Error occurred while retrieving approval routes");

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
