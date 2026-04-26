using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemPrices
{
    public sealed class DeleteItemPriceCommand : BaseCommand, IRequest<ApiResponse<DeleteItemPriceCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteItemPriceCommandValidator : AbstractValidator<DeleteItemPriceCommand>
    {
        public DeleteItemPriceCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Item price ids are required")
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All ids must be greater than 0");
        }
    }

    public sealed class DeleteItemPriceCommandHandler : IRequestHandler<DeleteItemPriceCommand, ApiResponse<DeleteItemPriceCommand.Response>>
    {
        public async Task<ApiResponse<DeleteItemPriceCommand.Response>> Handle(DeleteItemPriceCommand request, CancellationToken ct)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM it_item_price
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, ct);

                    

                    var responseData = new DeleteItemPriceCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}


