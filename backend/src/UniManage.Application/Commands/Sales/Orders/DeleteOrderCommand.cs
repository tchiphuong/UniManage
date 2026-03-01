using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Sales.Orders
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
        public async Task<ApiResponse<DeleteOrderCommand.Response>> Handle(DeleteOrderCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.OrderCodes), string.Join(",", request.OrderCodes))
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var deleteItemsSql = @"
                        DELETE FROM sa_order_items
                        WHERE OrderCode IN @OrderCodes";

                    await dbContext.ExecuteAsync(deleteItemsSql, new { OrderCodes = request.OrderCodes }, ct);

                    var deleteOrderSql = @"
                        DELETE FROM sa_orders
                        WHERE OrderCode IN @OrderCodes";

                    var deletedCount = await dbContext.ExecuteAsync(deleteOrderSql, new { OrderCodes = request.OrderCodes }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new DeleteOrderCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_deleteSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error deleting orders: {ex.Message}", ex);

                    var response = ResponseHelper.Error<DeleteOrderCommand.Response>("Error occurred while deleting orders");

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
