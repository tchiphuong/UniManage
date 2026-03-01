using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Workflow.Requests
{
    public sealed class GetRequestByIdQuery : BaseQuery, IRequest<ApiResponse<GetRequestByIdQuery.Result>>
    {
        public int Id { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string RequestTypeKey { get; set; } = default!;
            public string ApplicantEmployeeCode { get; set; } = default!;
            public string ApplicantUsername { get; set; } = default!;
            public string ApplicantEmployeeName { get; set; } = default!;
            public int? ApprovalRouteId { get; set; }
            public string? RouteName { get; set; }
            public string Status { get; set; } = default!;
            public string? RequestData { get; set; }
            public List<ApprovalHistory> ApprovalHistory { get; set; } = new();
            public string CreatedBy { get; set; } = default!;
            public DateTime CreatedAt { get; set; }
            public string UpdatedBy { get; set; } = default!;
            public DateTime? UpdatedAt { get; set; }
        }

        public sealed class ApprovalHistory
        {
            public int Id { get; set; }
            public int LevelOrder { get; set; }
            public string LevelName { get; set; } = default!;
            public string? ApproverEmployeeCode { get; set; }
            public string ApproverUsername { get; set; } = default!;
            public string? ApproverEmployeeName { get; set; }
            public string ApprovalStatus { get; set; } = default!;
            public string? ApprovalComment { get; set; }
            public DateTime? ApprovedAt { get; set; }
        }
    }

    public sealed class GetRequestByIdQueryValidator : AbstractValidator<GetRequestByIdQuery>
    {
        public GetRequestByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }

    public sealed class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, ApiResponse<GetRequestByIdQuery.Result>>
    {
        public async Task<ApiResponse<GetRequestByIdQuery.Result>> Handle(GetRequestByIdQuery request, CancellationToken ct)
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
                            r.Id,
                            r.RequestTypeKey,
                            r.ApplicantEmployeeCode,
                            r.ApplicantUsername,
                            e.DisplayName AS ApplicantEmployeeName,
                            r.ApprovalRouteId,
                            ar.RouteName,
                            r.Status,
                            r.RequestData,
                            r.CreatedBy,
                            r.CreatedAt,
                            r.UpdatedBy,
                            r.UpdatedAt
                        FROM wf_request r
                        LEFT JOIN hr_employees e ON r.ApplicantEmployeeCode = e.EmployeeCode
                        LEFT JOIN wf_approval_route ar ON r.ApprovalRouteId = ar.Id
                        WHERE r.Id = @Id";

                    var result = await dbContext.QueryFirstOrDefaultAsync<GetRequestByIdQuery.Result>(sql, new { request.Id }, ct);

                    if (result == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetRequestByIdQuery.Result>("Request not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var historySql = @"
                        SELECT 
                            ra.Id,
                            arl.LevelOrder,
                            arl.LevelName,
                            ra.ApproverEmployeeCode,
                            ra.ApproverUsername,
                            e.DisplayName AS ApproverEmployeeName,
                            ra.ApprovalStatus,
                            ra.ApprovalComment,
                            ra.ApprovedAt
                        FROM wf_request_approval ra
                        INNER JOIN wf_approval_route_level arl ON ra.ApprovalRouteLevelId = arl.Id
                        LEFT JOIN hr_employees e ON ra.ApproverEmployeeCode = e.EmployeeCode
                        WHERE ra.RequestId = @RequestId
                        ORDER BY arl.LevelOrder";

                    var history = await dbContext.QueryAsync<GetRequestByIdQuery.ApprovalHistory>(historySql, new { RequestId = request.Id }, ct);
                    result.ApprovalHistory = history.ToList();

                    var response = ResponseHelper.Success(result, CoreResource.crud_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving request by id: {ex.Message}", ex);

                    var response = ResponseHelper.Error<GetRequestByIdQuery.Result>("Error occurred while retrieving request");

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
