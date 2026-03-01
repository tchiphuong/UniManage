using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemInventory
{
    public sealed class UpdateItemInventoryCommand : BaseCommand, IRequest<ApiResponse<UpdateItemInventoryCommand.Response>>
    {
        public int Id { get; init; }
        public int Quantity { get; init; }
        public string WarehouseLocation { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateItemInventoryCommandValidator : AbstractValidator<UpdateItemInventoryCommand>
    {
        public UpdateItemInventoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0");

            RuleFor(x => x.WarehouseLocation)
                .NotEmpty().WithMessage("Warehouse location is required")
                .Length(1, 100).WithMessage("Warehouse location must be between 1 and 100 characters");
        }
    }

    public sealed class UpdateItemInventoryCommandHandler : IRequestHandler<UpdateItemInventoryCommand, ApiResponse<UpdateItemInventoryCommand.Response>>
    {
        public async Task<ApiResponse<UpdateItemInventoryCommand.Response>> Handle(UpdateItemInventoryCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id.ToString()),
                    new CoreParamModel(nameof(request.Quantity), request.Quantity.ToString()),
                    new CoreParamModel(nameof(request.WarehouseLocation), request.WarehouseLocation)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        UPDATE it_item_inventory
                        SET Quantity = @Quantity,
                            WarehouseLocation = @WarehouseLocation,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Id,
                        request.Quantity,
                        request.WarehouseLocation
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateItemInventoryCommand.Response>("Item inventory not found");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateItemInventoryCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_updateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error updating item inventory: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateItemInventoryCommand.Response>("Error occurred while updating item inventory");

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
