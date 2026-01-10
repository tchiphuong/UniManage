using FluentValidation;
using MediatR;
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
        private readonly string[] _validStatuses = { "Draft", "Pending", "Processing", "Completed", "Cancelled" };

        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderCode)
                .NotEmpty().WithMessage("Order code is required");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => _validStatuses.Contains(status))
                .WithMessage($"Status must be one of: {string.Join(", ", _validStatuses)}");
        }
    }

    public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, ApiResponse<UpdateOrderStatusCommand.Response>>
    {
        public async Task<ApiResponse<UpdateOrderStatusCommand.Response>> Handle(UpdateOrderStatusCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.OrderCode), request.OrderCode),
                    new CoreParamModel(nameof(request.Status), request.Status)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
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
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateOrderStatusCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_UpdateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error updating order status: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateOrderStatusCommand.Response>("Error occurred while updating order status");

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
