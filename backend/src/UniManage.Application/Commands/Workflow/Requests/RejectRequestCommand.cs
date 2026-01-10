using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Commands.Workflow.Requests
{
    public sealed class RejectRequestCommand : BaseCommand, IRequest<ApiResponse<RejectRequestCommand.Response>>
    {
        public int RequestId { get; init; }
        public string ApprovalComment { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
            public string Message { get; init; } = default!;
        }
    }

    public sealed class RejectRequestCommandValidator : AbstractValidator<RejectRequestCommand>
    {
        public RejectRequestCommandValidator()
        {
            RuleFor(x => x.RequestId)
                .GreaterThan(0).WithMessage("Request id must be greater than 0");

            RuleFor(x => x.ApprovalComment)
                .NotEmpty().WithMessage("Approval comment is required for rejection")
                .MaximumLength(1000).WithMessage("Approval comment must not exceed 1000 characters");
        }
    }

    public sealed class RejectRequestCommandHandler : IRequestHandler<RejectRequestCommand, ApiResponse<RejectRequestCommand.Response>>
    {
        public async Task<ApiResponse<RejectRequestCommand.Response>> Handle(RejectRequestCommand request, CancellationToken ct)
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
                        SELECT TOP 1 Id
                        FROM wf_request_approval
                        WHERE RequestId = @RequestId
                            AND ApproverUsername = @ApproverUsername
                            AND ApprovalStatus = 'Pending'";

                    var approvalId = await dbContext.ExecuteScalarAsync<int?>(getPendingApprovalSql, new { RequestId = request.RequestId, ApproverUsername = currentUsername }, ct);

                    if (!approvalId.HasValue)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<RejectRequestCommand.Response>("No pending approval found for this user");
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
                        Id = approvalId.Value,
                        ApprovalStatus = "Rejected",
                        request.ApprovalComment,
                        UpdatedBy = currentUsername
                    }, ct);

                    var updateRequestStatusSql = @"
                        UPDATE wf_request
                        SET Status = @Status,
                            UpdatedBy = @UpdatedBy,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id";

                    await dbContext.ExecuteAsync(updateRequestStatusSql, new
                    {
                        Id = request.RequestId,
                        Status = "Rejected",
                        UpdatedBy = currentUsername
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new RejectRequestCommand.Response { Success = true, Message = "Request rejected successfully" };
                    var response = ResponseHelper.Success(responseData, "Request rejected successfully");

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error rejecting request: {ex.Message}", ex);

                    var response = ResponseHelper.Error<RejectRequestCommand.Response>("Error occurred while rejecting request");

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
