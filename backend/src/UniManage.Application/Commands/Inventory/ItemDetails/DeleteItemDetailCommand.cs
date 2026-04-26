using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemDetails
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
        public async Task<ApiResponse<DeleteItemDetailCommand.Response>> Handle(DeleteItemDetailCommand request, CancellationToken ct)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM it_item_details
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, ct);

                    

                    var responseData = new DeleteItemDetailCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}


