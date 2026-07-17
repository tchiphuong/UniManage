using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.IvItemPrice.Commands
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
        public async Task<ApiResponse<CreateItemPriceCommand.Response>> Handle(CreateItemPriceCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
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
                    }, cancellationToken);

                    

                    var responseData = new CreateItemPriceCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);
return response;
        }
    }
}




