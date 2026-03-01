using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Workflow.ApprovalRoutes
{
    public sealed class CreateApprovalRouteCommand : BaseCommand, IRequest<ApiResponse<CreateApprovalRouteCommand.Response>>
    {
        public string RequestTypeKey { get; init; } = default!;
        public string RouteName { get; init; } = default!;
        public string? Description { get; init; }
        public List<LevelInput> Levels { get; init; } = new();

        public sealed class LevelInput
        {
            public int LevelOrder { get; init; }
            public string LevelName { get; init; } = default!;
            public List<ApproverInput> Approvers { get; init; } = new();
        }

        public sealed class ApproverInput
        {
            public string? ApproverEmployeeCode { get; init; }
            public string ApproverUsername { get; init; } = default!;
        }

        public sealed class Response
        {
            public int Id { get; init; }
        }
    }

    public sealed class CreateApprovalRouteCommandValidator : AbstractValidator<CreateApprovalRouteCommand>
    {
        public CreateApprovalRouteCommandValidator()
        {
            RuleFor(x => x.RequestTypeKey)
                .NotEmpty().WithMessage("Request type key is required")
                .MaximumLength(50).WithMessage("Request type key must not exceed 50 characters");

            RuleFor(x => x.RouteName)
                .NotEmpty().WithMessage("Route name is required")
                .Length(2, 255).WithMessage("Route name must be between 2 and 255 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

            RuleFor(x => x.Levels)
                .NotEmpty().WithMessage("At least one approval level is required");

            RuleForEach(x => x.Levels).ChildRules(level =>
            {
                level.RuleFor(l => l.LevelOrder)
                    .GreaterThan(0).WithMessage("Level order must be greater than 0");

                level.RuleFor(l => l.LevelName)
                    .NotEmpty().WithMessage("Level name is required")
                    .MaximumLength(255).WithMessage("Level name must not exceed 255 characters");

                level.RuleFor(l => l.Approvers)
                    .NotEmpty().WithMessage("At least one approver is required for each level");

                level.RuleForEach(l => l.Approvers).ChildRules(approver =>
                {
                    approver.RuleFor(a => a.ApproverUsername)
                        .NotEmpty().WithMessage("Approver username is required")
                        .MaximumLength(100).WithMessage("Approver username must not exceed 100 characters");
                });
            });
        }
    }

    public sealed class CreateApprovalRouteCommandHandler : IRequestHandler<CreateApprovalRouteCommand, ApiResponse<CreateApprovalRouteCommand.Response>>
    {
        public async Task<ApiResponse<CreateApprovalRouteCommand.Response>> Handle(CreateApprovalRouteCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.RequestTypeKey), request.RequestTypeKey),
                    new CoreParamModel(nameof(request.RouteName), request.RouteName),
                    new CoreParamModel("LevelCount", request.Levels.Count)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var routeSql = @"
                        INSERT INTO wf_approval_route (RequestTypeKey, RouteName, Description, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                        VALUES (@RequestTypeKey, @RouteName, @Description, @CreatedBy, GETDATE(), @CreatedBy, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var routeId = await dbContext.ExecuteScalarAsync<int>(routeSql, new
                    {
                        request.RequestTypeKey,
                        request.RouteName,
                        request.Description,
                        CreatedBy = request.HeaderInfo!.Username
                    }, ct);

                    foreach (var level in request.Levels)
                    {
                        var levelSql = @"
                            INSERT INTO wf_approval_route_level (ApprovalRouteId, LevelOrder, LevelName, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                            VALUES (@ApprovalRouteId, @LevelOrder, @LevelName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE());
                            SELECT SCOPE_IDENTITY();";

                        var levelId = await dbContext.ExecuteScalarAsync<int>(levelSql, new
                        {
                            ApprovalRouteId = routeId,
                            level.LevelOrder,
                            level.LevelName,
                            CreatedBy = request.HeaderInfo!.Username
                        }, ct);

                        foreach (var approver in level.Approvers)
                        {
                            var approverSql = @"
                                INSERT INTO wf_approval_route_approver (ApprovalRouteLevelId, ApproverEmployeeCode, ApproverUsername, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                                VALUES (@ApprovalRouteLevelId, @ApproverEmployeeCode, @ApproverUsername, @CreatedBy, GETDATE(), @CreatedBy, GETDATE());";

                            await dbContext.ExecuteAsync(approverSql, new
                            {
                                ApprovalRouteLevelId = levelId,
                                approver.ApproverEmployeeCode,
                                approver.ApproverUsername,
                                CreatedBy = request.HeaderInfo!.Username
                            }, ct);
                        }
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateApprovalRouteCommand.Response { Id = routeId };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_createSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error creating approval route: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateApprovalRouteCommand.Response>("Error occurred while creating approval route");

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
