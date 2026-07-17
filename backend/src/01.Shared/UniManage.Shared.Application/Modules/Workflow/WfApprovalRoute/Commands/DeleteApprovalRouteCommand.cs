using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.WfApprovalRoute.Commands
{
    public sealed class DeleteApprovalRouteCommand : BaseCommand, IRequest<ApiResponse<DeleteApprovalRouteCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteApprovalRouteCommandValidator : AbstractValidator<DeleteApprovalRouteCommand>
    {
        public DeleteApprovalRouteCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Ids are required")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Ids must be greater than 0");

            RuleFor(x => x.Ids)
                .MustAsync(async (ids, cancel) => !await IsAnyRouteInUseAsync(ids))
                .WithMessage("One or more approval routes are linked to requests and cannot be deleted");
        }

        private static async Task<bool> IsAnyRouteInUseAsync(List<int> ids)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM wf_request WHERE ApprovalRouteId IN @Ids) THEN 1 ELSE 0 END",
                    new { Ids = ids });
            }
        }
    }

    public sealed class DeleteApprovalRouteCommandHandler : IRequestHandler<DeleteApprovalRouteCommand, ApiResponse<DeleteApprovalRouteCommand.Response>>
    {
        public async Task<ApiResponse<DeleteApprovalRouteCommand.Response>> Handle(DeleteApprovalRouteCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var deleteLevelsSql = @"
                        DELETE ara
                        FROM wf_approval_route_approver ara
                        INNER JOIN wf_approval_route_level arl ON ara.ApprovalRouteLevelId = arl.Id
                        WHERE arl.ApprovalRouteId IN @Ids;

                        DELETE FROM wf_approval_route_level
                        WHERE ApprovalRouteId IN @Ids;";

                    await dbContext.ExecuteAsync(deleteLevelsSql, new { Ids = request.Ids }, cancellationToken);

                    var deleteRouteSql = @"
                        DELETE FROM wf_approval_route
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(deleteRouteSql, new { Ids = request.Ids }, cancellationToken);

                    

                    var responseData = new DeleteApprovalRouteCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}




