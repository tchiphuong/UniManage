using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemInventory
{
    public sealed class CreateItemInventoryCommand : BaseCommand, IRequest<ApiResponse<CreateItemInventoryCommand.Response>>
    {
        public string ItemCode { get; init; } = default!;
        public int Quantity { get; init; }
        public string WarehouseLocation { get; init; } = default!;

        public sealed class Response
        {
            public int Id { get; init; }
        }
    }

    public sealed class CreateItemInventoryCommandValidator : AbstractValidator<CreateItemInventoryCommand>
    {
        public CreateItemInventoryCommandValidator()
        {
            RuleFor(x => x.ItemCode)
                .NotEmpty().WithMessage("Item code is required")
                .MustAsync(async (code, cancel) => await IsItemExistsAsync(code))
                .WithMessage("Item does not exist");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0");

            RuleFor(x => x.WarehouseLocation)
                .NotEmpty().WithMessage("Warehouse location is required")
                .Length(1, 100).WithMessage("Warehouse location must be between 1 and 100 characters");
        }

        private static async Task<bool> IsItemExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM it_items WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }
    }

    public sealed class CreateItemInventoryCommandHandler : IRequestHandler<CreateItemInventoryCommand, ApiResponse<CreateItemInventoryCommand.Response>>
    {
        public async Task<ApiResponse<CreateItemInventoryCommand.Response>> Handle(CreateItemInventoryCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.ItemCode), request.ItemCode),
                    new CoreParamModel(nameof(request.Quantity), request.Quantity.ToString()),
                    new CoreParamModel(nameof(request.WarehouseLocation), request.WarehouseLocation)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        INSERT INTO it_item_inventory (ItemCode, Quantity, WarehouseLocation, UpdatedAt)
                        VALUES (@ItemCode, @Quantity, @WarehouseLocation, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<int>(sql, new
                    {
                        request.ItemCode,
                        request.Quantity,
                        request.WarehouseLocation
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateItemInventoryCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_CreateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error creating item inventory: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateItemInventoryCommand.Response>("Error occurred while creating item inventory");

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
