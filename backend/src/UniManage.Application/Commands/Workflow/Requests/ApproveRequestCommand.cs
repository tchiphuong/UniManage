using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Commands.Workflow.Requests
{
    public sealed class ApproveRequestCommand : BaseCommand, IRequest<ApiResponse<ApproveRequestCommand.Response>>
    {
        public int RequestId { get; init; }
        public string? ApprovalComment { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
            public string Message { get; init; } = default!;
        }
    }

    public sealed class ApproveRequestCommandValidator : AbstractValidator<ApproveRequestCommand>
    {
        public ApproveRequestCommandValidator()
        {
            RuleFor(x => x.RequestId)
                .GreaterThan(0).WithMessage("Request id must be greater than 0");

            RuleFor(x => x.ApprovalComment)
                .MaximumLength(1000).WithMessage("Approval comment must not exceed 1000 characters");
        }
    }

    public sealed class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand, ApiResponse<ApproveRequestCommand.Response>>
    {
        public async Task<ApiResponse<ApproveRequestCommand.Response>> Handle(ApproveRequestCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.RequestId), request.RequestId),
                    new CoreParamModel(nameof(request.ApprovalComment), request.ApprovalComment)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var currentUsername = request.HeaderInfo!.Username;

                    var getPendingApprovalSql = @"
                        SELECT TOP 1 
                            ra.Id,
                            ra.ApprovalRouteLevelId,
                            arl.LevelOrder
                        FROM wf_request_approval ra
                        INNER JOIN wf_approval_route_level arl ON ra.ApprovalRouteLevelId = arl.Id
                        WHERE ra.RequestId = @RequestId
                            AND ra.ApproverUsername = @ApproverUsername
                            AND ra.ApprovalStatus = 'Pending'
                        ORDER BY arl.LevelOrder";

                    var pendingApproval = await dbContext.QueryFirstOrDefaultAsync<dynamic>(getPendingApprovalSql, new { RequestId = request.RequestId, ApproverUsername = currentUsername }, ct);

                    if (pendingApproval == null)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<ApproveRequestCommand.Response>("No pending approval found for this user");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    var updateApprovalSql = @"
                        UPDATE wf_request_approval
                        SET ApprovalStatus = @ApprovalStatus,
                            ApprovalComment = @ApprovalComment,
                            ApprovedAt = GETDATE(),
                            UpdatedBy = @UpdatedBy,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id";

                    await dbContext.ExecuteAsync(updateApprovalSql, new
                    {
                        Id = (int)pendingApproval.Id,
                        ApprovalStatus = "Approved",
                        request.ApprovalComment,
                        UpdatedBy = currentUsername
                    }, ct);

                    var checkLevelCompleteSql = @"
                        SELECT COUNT(*)
                        FROM wf_request_approval
                        WHERE ApprovalRouteLevelId = @ApprovalRouteLevelId
                            AND ApprovalStatus = 'Pending'";

                    var remainingPending = await dbContext.ExecuteScalarAsync<int>(checkLevelCompleteSql, new { ApprovalRouteLevelId = (int)pendingApproval.ApprovalRouteLevelId }, ct);

                    if (remainingPending == 0)
                    {
                        var getNextLevelSql = @"
                            SELECT TOP 1 Id
                            FROM wf_approval_route_level
                            WHERE ApprovalRouteId = (
                                SELECT ApprovalRouteId 
                                FROM wf_approval_route_level 
                                WHERE Id = @CurrentLevelId
                            )
                            AND LevelOrder > @CurrentLevelOrder
                            ORDER BY LevelOrder";

                        var nextLevelId = await dbContext.ExecuteScalarAsync<int?>(getNextLevelSql, new { CurrentLevelId = (int)pendingApproval.ApprovalRouteLevelId, CurrentLevelOrder = (int)pendingApproval.LevelOrder }, ct);

                        if (nextLevelId.HasValue)
                        {
                            var getNextApproversSql = @"
                                SELECT ApproverEmployeeCode, ApproverUsername
                                FROM wf_approval_route_approver
                                WHERE ApprovalRouteLevelId = @LevelId";

                            var nextApprovers = await dbContext.QueryAsync<dynamic>(getNextApproversSql, new { LevelId = nextLevelId.Value }, ct);

                            foreach (var approver in nextApprovers)
                            {
                                var insertNextApprovalSql = @"
                                    INSERT INTO wf_request_approval (RequestId, ApprovalRouteLevelId, ApproverEmployeeCode, ApproverUsername, ApprovalStatus, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                                    VALUES (@RequestId, @ApprovalRouteLevelId, @ApproverEmployeeCode, @ApproverUsername, @ApprovalStatus, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())";

                                await dbContext.ExecuteAsync(insertNextApprovalSql, new
                                {
                                    RequestId = request.RequestId,
                                    ApprovalRouteLevelId = nextLevelId.Value,
                                    ApproverEmployeeCode = approver.ApproverEmployeeCode,
                                    ApproverUsername = approver.ApproverUsername,
                                    ApprovalStatus = "Pending",
                                    CreatedBy = currentUsername
                                }, ct);
                            }
                        }
                        else
                        {
                            var updateRequestStatusSql = @"
                                UPDATE wf_request
                                SET Status = @Status,
                                    UpdatedBy = @UpdatedBy,
                                    UpdatedAt = GETDATE()
                                WHERE Id = @Id";

                            await dbContext.ExecuteAsync(updateRequestStatusSql, new
                            {
                                Id = request.RequestId,
                                Status = "Approved",
                                UpdatedBy = currentUsername
                            }, ct);
                        }
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new ApproveRequestCommand.Response { Success = true, Message = "Request approved successfully" };
                    var response = ResponseHelper.Success(responseData, "Request approved successfully");

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error approving request: {ex.Message}", ex);

                    var response = ResponseHelper.Error<ApproveRequestCommand.Response>("Error occurred while approving request");

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
