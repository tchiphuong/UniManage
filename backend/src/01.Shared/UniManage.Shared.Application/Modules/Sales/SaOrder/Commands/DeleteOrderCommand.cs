using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SaOrder.Commands
{
    public sealed class DeleteOrderCommand : BaseCommand, IRequest<ApiResponse<DeleteOrderCommand.Response>>
    {
        public List<string> OrderCodes { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.OrderCodes)
                .NotEmpty().WithMessage("Order codes are required")
                .Must(codes => codes.All(code => !string.IsNullOrEmpty(code)))
                .WithMessage("All order codes must be valid");
        }
    }

    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ApiResponse<DeleteOrderCommand.Response>>
    {
        public async Task<ApiResponse<DeleteOrderCommand.Response>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var deleteItemsSql = @"
                        DELETE FROM sa_order_items
                        WHERE OrderCode IN @OrderCodes";

                    await dbContext.ExecuteAsync(deleteItemsSql, new { OrderCodes = request.OrderCodes }, cancellationToken);

                    var deleteOrderSql = @"
                        DELETE FROM sa_orders
                        WHERE OrderCode IN @OrderCodes";

                    var deletedCount = await dbContext.ExecuteAsync(deleteOrderSql, new { OrderCodes = request.OrderCodes }, cancellationToken);

                    

                    var responseData = new DeleteOrderCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}




