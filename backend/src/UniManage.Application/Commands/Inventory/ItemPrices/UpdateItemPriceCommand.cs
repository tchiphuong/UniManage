using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemPrices
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
        public async Task<ApiResponse<UpdateItemPriceCommand.Response>> Handle(UpdateItemPriceCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id.ToString()),
                    new CoreParamModel(nameof(request.Price), request.Price.ToString()),
                    new CoreParamModel(nameof(request.StartDate), request.StartDate.ToString())
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
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
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateItemPriceCommand.Response>("Item price not found");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateItemPriceCommand.Response { Success = true };
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
                    UniLogger.Error($"Error updating item price: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateItemPriceCommand.Response>("Error occurred while updating item price");

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
