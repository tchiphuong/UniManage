using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;

namespace UniManage.Modules.Workflow.Application.Commands.Requests
{
    public sealed class SubmitRequestCommand : BaseCommand, IRequest<ApiResponse<SubmitRequestCommand.Response>>
    {
        public int RequestId { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
            public string Message { get; init; } = default!;
        }
    }

    public sealed class SubmitRequestCommandValidator : AbstractValidator<SubmitRequestCommand>
    {
        public SubmitRequestCommandValidator()
        {
            RuleFor(x => x.RequestId)
                .GreaterThan(0).WithMessage("Request id must be greater than 0")
                .MustAsync(async (id, cancel) => await IsRequestDraftAsync(id))
                .WithMessage("Request must be in Draft status to submit");
        }

        private static async Task<bool> IsRequestDraftAsync(int requestId)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM wf_request WHERE Id = @Id AND Status = 'Draft') THEN 1 ELSE 0 END",
                    new { Id = requestId });
            }
        }
    }

    public sealed class SubmitRequestCommandHandler : IRequestHandler<SubmitRequestCommand, ApiResponse<SubmitRequestCommand.Response>>
    {
        public async Task<ApiResponse<SubmitRequestCommand.Response>> Handle(SubmitRequestCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var getRequestSql = @"
                        SELECT RequestTypeKey
                        FROM wf_request
                        WHERE Id = @Id";

                    var requestTypeKey = await dbContext.ExecuteScalarAsync<string>(getRequestSql, new { Id = request.RequestId }, cancellationToken);

                    if (string.IsNullOrEmpty(requestTypeKey))
                    {
                        await dbContext.RollbackAsync(cancellationToken);
                        var errorResponse = ResponseHelper.Error<SubmitRequestCommand.Response>("Request not found");
return errorResponse;
                    }

                    var getRouteSql = @"
                        SELECT TOP 1 Id
                        FROM wf_approval_route
                        WHERE RequestTypeKey = @RequestTypeKey
                        ORDER BY CreatedAt DESC";

                    var approvalRouteId = await dbContext.ExecuteScalarAsync<int?>(getRouteSql, new { RequestTypeKey = requestTypeKey }, cancellationToken);

                    if (!approvalRouteId.HasValue)
                    {
                        await dbContext.RollbackAsync(cancellationToken);
                        var errorResponse = ResponseHelper.Error<SubmitRequestCommand.Response>("No approval route found for this request type");
return errorResponse;
                    }

                    var updateRequestSql = @"
                        UPDATE wf_request
                        SET Status = @Status,
                            ApprovalRouteId = @ApprovalRouteId,
                            UpdatedBy = @UpdatedBy,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id";

                    await dbContext.ExecuteAsync(updateRequestSql, new
                    {
                        Id = request.RequestId,
                        Status = "Pending",
                        ApprovalRouteId = approvalRouteId.Value,
                        UpdatedBy = request.HeaderInfo!.Username
                    }, cancellationToken);

                    var getFirstLevelSql = @"
                        SELECT TOP 1 Id
                        FROM wf_approval_route_level
                        WHERE ApprovalRouteId = @ApprovalRouteId
                        ORDER BY LevelOrder";

                    var firstLevelId = await dbContext.ExecuteScalarAsync<int>(getFirstLevelSql, new { ApprovalRouteId = approvalRouteId.Value }, cancellationToken);

                    var getApproversSql = @"
                        SELECT ApproverEmployeeCode, ApproverUsername
                        FROM wf_approval_route_approver
                        WHERE ApprovalRouteLevelId = @LevelId";

                    var approvers = await dbContext.QueryAsync<dynamic>(getApproversSql, new { LevelId = firstLevelId }, cancellationToken);

                    foreach (var approver in approvers)
                    {
                        var insertApprovalSql = @"
                            INSERT INTO wf_request_approval (RequestId, ApprovalRouteLevelId, ApproverEmployeeCode, ApproverUsername, ApprovalStatus, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                            VALUES (@RequestId, @ApprovalRouteLevelId, @ApproverEmployeeCode, @ApproverUsername, @ApprovalStatus, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())";

                        await dbContext.ExecuteAsync(insertApprovalSql, new
                        {
                            RequestId = request.RequestId,
                            ApprovalRouteLevelId = firstLevelId,
                            ApproverEmployeeCode = approver.ApproverEmployeeCode,
                            ApproverUsername = approver.ApproverUsername,
                            ApprovalStatus = "Pending",
                            CreatedBy = request.HeaderInfo!.Username
                        }, cancellationToken);
                    }

                    

                    var responseData = new SubmitRequestCommand.Response { Success = true, Message = "Request submitted successfully" };
                    var response = ResponseHelper.Success(responseData, "Request submitted successfully");
return response;
        }
    }
}




