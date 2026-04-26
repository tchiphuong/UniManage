using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Workflow.Requests
{
    public sealed class DeleteRequestCommand : BaseCommand, IRequest<ApiResponse<DeleteRequestCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteRequestCommandValidator : AbstractValidator<DeleteRequestCommand>
    {
        public DeleteRequestCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Ids are required")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Ids must be greater than 0");

            RuleFor(x => x.Ids)
                .MustAsync(async (ids, cancel) => await AreAllRequestsDraftAsync(ids))
                .WithMessage("Only draft requests can be deleted");
        }

        private static async Task<bool> AreAllRequestsDraftAsync(List<int> ids)
        {
            using (var dbContext = new DbContext())
            {
                var sql = @"
                    SELECT COUNT(*)
                    FROM wf_request
                    WHERE Id IN @Ids AND Status = 'Draft'";

                var draftCount = await dbContext.ExecuteScalarAsync<int>(sql, new { Ids = ids });
                return draftCount == ids.Count;
            }
        }
    }

    public sealed class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, ApiResponse<DeleteRequestCommand.Response>>
    {
        public async Task<ApiResponse<DeleteRequestCommand.Response>> Handle(DeleteRequestCommand request, CancellationToken ct)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM wf_request
                        WHERE Id IN @Ids AND Status = 'Draft'";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, ct);

                    

                    var responseData = new DeleteRequestCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}


