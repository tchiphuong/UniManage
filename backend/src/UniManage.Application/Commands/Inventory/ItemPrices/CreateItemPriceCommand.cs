using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemPrices
{
    public sealed class CreateItemPriceCommand : BaseCommand, IRequest<ApiResponse<CreateItemPriceCommand.Response>>
    {
        public string ItemCode { get; init; } = default!;
        public decimal Price { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime? EndDate { get; init; }

        public sealed class Response
        {
            public int Id { get; init; }
        }
    }

    public sealed class CreateItemPriceCommandValidator : AbstractValidator<CreateItemPriceCommand>
    {
        public CreateItemPriceCommandValidator()
        {
            RuleFor(x => x.ItemCode)
                .NotEmpty().WithMessage("Item code is required")
                .MustAsync(async (code, cancel) => await IsItemExistsAsync(code))
                .WithMessage("Item does not exist");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required");

            When(x => x.EndDate.HasValue, () =>
            {
                RuleFor(x => x.EndDate)
                    .GreaterThan(x => x.StartDate).WithMessage("End date must be greater than start date");
            });
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

    public sealed class CreateItemPriceCommandHandler : IRequestHandler<CreateItemPriceCommand, ApiResponse<CreateItemPriceCommand.Response>>
    {
        public async Task<ApiResponse<CreateItemPriceCommand.Response>> Handle(CreateItemPriceCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.ItemCode), request.ItemCode),
                    new CoreParamModel(nameof(request.Price), request.Price.ToString()),
                    new CoreParamModel(nameof(request.StartDate), request.StartDate.ToString())
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        INSERT INTO it_item_price (ItemCode, Price, StartDate, EndDate, CreatedAt)
                        VALUES (@ItemCode, @Price, @StartDate, @EndDate, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<int>(sql, new
                    {
                        request.ItemCode,
                        request.Price,
                        request.StartDate,
                        request.EndDate
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateItemPriceCommand.Response { Id = id };
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
                    UniLogger.Error($"Error creating item price: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateItemPriceCommand.Response>("Error occurred while creating item price");

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
