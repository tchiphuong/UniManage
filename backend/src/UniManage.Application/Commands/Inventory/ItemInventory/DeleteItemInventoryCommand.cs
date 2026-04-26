using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemInventory
{
    public sealed class DeleteItemInventoryCommand : BaseCommand, IRequest<ApiResponse<DeleteItemInventoryCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteItemInventoryCommandValidator : AbstractValidator<DeleteItemInventoryCommand>
    {
        public DeleteItemInventoryCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Item inventory ids are required")
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All ids must be greater than 0");
        }
    }

    public sealed class DeleteItemInventoryCommandHandler : IRequestHandler<DeleteItemInventoryCommand, ApiResponse<DeleteItemInventoryCommand.Response>>
    {
        public async Task<ApiResponse<DeleteItemInventoryCommand.Response>> Handle(DeleteItemInventoryCommand request, CancellationToken ct)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM it_item_inventory
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, ct);

                    

                    var responseData = new DeleteItemInventoryCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}


