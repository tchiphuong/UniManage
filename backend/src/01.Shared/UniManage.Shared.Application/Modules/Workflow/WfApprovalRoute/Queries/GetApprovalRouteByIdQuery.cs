namespace UniManage.Shared.Application.Modules.WfApprovalRoute.Queries
{
    public sealed class GetApprovalRouteByIdQuery : BaseQuery, IRequest<ApiResponse<GetApprovalRouteByIdQuery.Result>>
    {
        public int Id { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string RequestTypeKey { get; set; } = default!;
            public string RouteName { get; set; } = default!;
            public string? Description { get; set; }
            public List<RouteLevel> Levels { get; set; } = new();
            public string CreatedBy { get; set; } = default!;
            public DateTime CreatedAt { get; set; }
            public string UpdatedBy { get; set; } = default!;
            public DateTime? UpdatedAt { get; set; }
        }

        public sealed class RouteLevel
        {
            public int Id { get; set; }
            public int LevelOrder { get; set; }
            public string LevelName { get; set; } = default!;
            public List<Approver> Approvers { get; set; } = new();
        }

        public sealed class Approver
        {
            public int Id { get; set; }
            public string? ApproverEmployeeCode { get; set; }
            public string ApproverUsername { get; set; } = default!;
            public string? ApproverEmployeeName { get; set; }
        }
    }

    public sealed class GetApprovalRouteByIdQueryValidator : AbstractValidator<GetApprovalRouteByIdQuery>
    {
        public GetApprovalRouteByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }

    public sealed class GetApprovalRouteByIdQueryHandler : IRequestHandler<GetApprovalRouteByIdQuery, ApiResponse<GetApprovalRouteByIdQuery.Result>>
    {
        public async Task<ApiResponse<GetApprovalRouteByIdQuery.Result>> Handle(GetApprovalRouteByIdQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Id), request.Id)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var routeSql = @"
                        SELECT 
                            Id,
                            RequestTypeKey,
                            RouteName,
                            Description,
                            CreatedBy,
                            CreatedAt,
                            UpdatedBy,
                            UpdatedAt
                        FROM wf_approval_route
                        WHERE Id = @Id";

                    var route = await dbContext.QueryFirstOrDefaultAsync<GetApprovalRouteByIdQuery.Result>(routeSql, new { request.Id }, cancellationToken);

                    if (route == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetApprovalRouteByIdQuery.Result>("Approval route not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var levelsSql = @"
                        SELECT 
                            Id,
                            LevelOrder,
                            LevelName
                        FROM wf_approval_route_level
                        WHERE ApprovalRouteId = @RouteId
                        ORDER BY LevelOrder";

                    var levels = await dbContext.QueryAsync<GetApprovalRouteByIdQuery.RouteLevel>(levelsSql, new { RouteId = request.Id }, cancellationToken);
                    route.Levels = levels.ToList();

                    foreach (var level in route.Levels)
                    {
                        var approversSql = @"
                            SELECT 
                                ara.Id,
                                ara.ApproverEmployeeCode,
                                ara.ApproverUsername,
                                e.DisplayName AS ApproverEmployeeName
                            FROM wf_approval_route_approver ara
                            LEFT JOIN HrEmployees e ON ara.ApproverEmployeeCode = e.EmployeeCode
                            WHERE ara.ApprovalRouteLevelId = @LevelId";

                        var approvers = await dbContext.QueryAsync<GetApprovalRouteByIdQuery.Approver>(approversSql, new { LevelId = level.Id }, cancellationToken);
                        level.Approvers = approvers.ToList();
                    }

                    var response = ResponseHelper.Success(route, CoreResource.common_getSuccess);

                    log.Result = route;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving approval route by id: {ex.Message}", ex);

                    var response = ResponseHelper.Error<GetApprovalRouteByIdQuery.Result>("Error occurred while retrieving approval route");

                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}


