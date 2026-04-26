using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Sales.Orders
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
        public async Task<ApiResponse<UpdateOrderStatusCommand.Response>> Handle(UpdateOrderStatusCommand request, CancellationToken ct)
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
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateOrderStatusCommand.Response>("Order not found");
return errorResponse;
                    }

                    

                    var responseData = new UpdateOrderStatusCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }
}


