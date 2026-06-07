using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Sales.Application.Commands.Orders
{
    public sealed class UpdateOrderStatusCommand : BaseCommand, IRequest<ApiResponse<UpdateOrderStatusCommand.Response>>
    {
        public string OrderCode { get; init; } = default!;
        public string Status { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderCode)
                .NotEmpty().WithMessage("Order code is required");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => CoreCommon.Value.Invoicestatus.All.Contains(status))
                .WithMessage("Invalid order status");
        }
    }

    public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, ApiResponse<UpdateOrderStatusCommand.Response>>
    {
        public async Task<ApiResponse<UpdateOrderStatusCommand.Response>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        UPDATE sa_orders
                        SET Status = @Status,
                            UpdatedBy = @UpdatedBy,
                            UpdatedAt = GETDATE()
                        WHERE OrderCode = @OrderCode";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.OrderCode,
                        request.Status,
                        UpdatedBy = request.HeaderInfo!.Username
                    }, cancellationToken);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(cancellationToken);
                        var errorResponse = ResponseHelper.Error<UpdateOrderStatusCommand.Response>("Order not found");
return errorResponse;
                    }

                    

                    var responseData = new UpdateOrderStatusCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }
}




