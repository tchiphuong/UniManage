using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.IvItemDetail.Commands
{
    public sealed class DeleteItemDetailCommand : BaseCommand, IRequest<ApiResponse<DeleteItemDetailCommand.Response>>
    {
        public List<long> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteItemDetailCommandValidator : AbstractValidator<DeleteItemDetailCommand>
    {
        public DeleteItemDetailCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Item detail ids are required")
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All ids must be greater than 0");
        }
    }

    public sealed class DeleteItemDetailCommandHandler : IRequestHandler<DeleteItemDetailCommand, ApiResponse<DeleteItemDetailCommand.Response>>
    {
        public async Task<ApiResponse<DeleteItemDetailCommand.Response>> Handle(DeleteItemDetailCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM it_item_details
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, cancellationToken);

                    

                    var responseData = new DeleteItemDetailCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}




