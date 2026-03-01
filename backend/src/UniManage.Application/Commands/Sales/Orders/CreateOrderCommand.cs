using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Sales.Orders
{
    public sealed class CreateOrderCommand : BaseCommand, IRequest<ApiResponse<CreateOrderCommand.Response>>
    {
        public string OrderCode { get; init; } = default!;
        public string CustomerCode { get; init; } = default!;
        public string EmployeeCode { get; init; } = default!;
        public DateTime? OrderDate { get; init; }
        public string Status { get; init; } = "Draft";
        public List<OrderItemDto> Items { get; init; } = new();

        public sealed class OrderItemDto
        {
            public string ItemCode { get; init; } = default!;
            public int Quantity { get; init; }
            public decimal UnitPrice { get; init; }
        }

        public sealed class Response
        {
            public int Id { get; init; }
            public string OrderCode { get; init; } = default!;
        }
    }

    public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.OrderCode)
                .NotEmpty().WithMessage("Order code is required")
                .Length(2, 50).WithMessage("Order code must be between 2 and 50 characters")
                .Must(ValidationHelper.IsValidUserCode).WithMessage("Order code allows only alphanumeric and underscore")
                .MustAsync(async (code, cancel) => !await IsOrderCodeExistsAsync(code))
                .WithMessage("Order code already exists");

            RuleFor(x => x.CustomerCode)
                .NotEmpty().WithMessage("Customer code is required")
                .MustAsync(async (code, cancel) => await IsCustomerExistsAsync(code))
                .WithMessage("Customer does not exist");

            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("Employee code is required")
                .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code))
                .WithMessage("Employee does not exist");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must have at least one item");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.ItemCode)
                    .NotEmpty().WithMessage("Item code is required")
                    .MustAsync(async (code, cancel) => await IsItemExistsAsync(code))
                    .WithMessage("Item does not exist");

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0");

                item.RuleFor(x => x.UnitPrice)
                    .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to 0");
            });
        }

        private static async Task<bool> IsOrderCodeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sa_orders WHERE OrderCode = @OrderCode) THEN 1 ELSE 0 END",
                    new { OrderCode = code });
            }
        }

        private static async Task<bool> IsCustomerExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sa_customers WHERE CustomerCode = @CustomerCode) THEN 1 ELSE 0 END",
                    new { CustomerCode = code });
            }
        }

        private static async Task<bool> IsEmployeeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                    new { EmployeeCode = code });
            }
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

    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse<CreateOrderCommand.Response>>
    {
        public async Task<ApiResponse<CreateOrderCommand.Response>> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.OrderCode), request.OrderCode),
                    new CoreParamModel(nameof(request.CustomerCode), request.CustomerCode),
                    new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var totalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);

                    var orderSql = @"
                        INSERT INTO sa_orders (OrderCode, CustomerCode, EmployeeCode, OrderDate, TotalAmount, Status, CreatedBy, CreatedAt)
                        VALUES (@OrderCode, @CustomerCode, @EmployeeCode, @OrderDate, @TotalAmount, @Status, @CreatedBy, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var orderId = await dbContext.ExecuteScalarAsync<int>(orderSql, new
                    {
                        request.OrderCode,
                        request.CustomerCode,
                        request.EmployeeCode,
                        OrderDate = request.OrderDate ?? DateTime.Now,
                        TotalAmount = totalAmount,
                        request.Status,
                        CreatedBy = request.HeaderInfo!.Username
                    }, ct);

                    foreach (var item in request.Items)
                    {
                        var itemSql = @"
                            INSERT INTO sa_order_items (OrderCode, ItemCode, Quantity, UnitPrice, TotalPrice)
                            VALUES (@OrderCode, @ItemCode, @Quantity, @UnitPrice, @TotalPrice);";

                        await dbContext.ExecuteAsync(itemSql, new
                        {
                            request.OrderCode,
                            item.ItemCode,
                            item.Quantity,
                            item.UnitPrice,
                            TotalPrice = item.Quantity * item.UnitPrice
                        }, ct);
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateOrderCommand.Response
                    {
                        Id = orderId,
                        OrderCode = request.OrderCode
                    };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_createSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error creating order: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateOrderCommand.Response>("Error occurred while creating order");

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
