using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Inventory.Application.Commands.ItemPrices
{
    public sealed class UpdateItemPriceCommand : BaseCommand, IRequest<ApiResponse<UpdateItemPriceCommand.Response>>
    {
        public int Id { get; init; }
        public decimal Price { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime? EndDate { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateItemPriceCommandValidator : AbstractValidator<UpdateItemPriceCommand>
    {
        public UpdateItemPriceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

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
    }

    public sealed class UpdateItemPriceCommandHandler : IRequestHandler<UpdateItemPriceCommand, ApiResponse<UpdateItemPriceCommand.Response>>
    {
        public async Task<ApiResponse<UpdateItemPriceCommand.Response>> Handle(UpdateItemPriceCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        UPDATE it_item_price
                        SET Price = @Price,
                            StartDate = @StartDate,
                            EndDate = @EndDate
                        WHERE Id = @Id";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Id,
                        request.Price,
                        request.StartDate,
                        request.EndDate
                    }, cancellationToken);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(cancellationToken);
                        var errorResponse = ResponseHelper.Error<UpdateItemPriceCommand.Response>("Item price not found");
return errorResponse;
                    }

                    

                    var responseData = new UpdateItemPriceCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }
}




