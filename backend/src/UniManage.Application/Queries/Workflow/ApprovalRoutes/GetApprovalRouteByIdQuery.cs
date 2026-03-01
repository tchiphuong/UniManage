using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Workflow.ApprovalRoutes
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
        public async Task<ApiResponse<GetApprovalRouteByIdQuery.Result>> Handle(GetApprovalRouteByIdQuery request, CancellationToken ct)
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

                    var route = await dbContext.QueryFirstOrDefaultAsync<GetApprovalRouteByIdQuery.Result>(routeSql, new { request.Id }, ct);

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

                    var levels = await dbContext.QueryAsync<GetApprovalRouteByIdQuery.RouteLevel>(levelsSql, new { RouteId = request.Id }, ct);
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
                            LEFT JOIN hr_employees e ON ara.ApproverEmployeeCode = e.EmployeeCode
                            WHERE ara.ApprovalRouteLevelId = @LevelId";

                        var approvers = await dbContext.QueryAsync<GetApprovalRouteByIdQuery.Approver>(approversSql, new { LevelId = level.Id }, ct);
                        level.Approvers = approvers.ToList();
                    }

                    var response = ResponseHelper.Success(route, CoreResource.crud_getSuccess);

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
