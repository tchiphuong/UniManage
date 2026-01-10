using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemPrices
{
    public sealed class DeleteItemPriceCommand : BaseCommand, IRequest<ApiResponse<DeleteItemPriceCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteItemPriceCommandValidator : AbstractValidator<DeleteItemPriceCommand>
    {
        public DeleteItemPriceCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Item price ids are required")
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All ids must be greater than 0");
        }
    }

    public sealed class DeleteItemPriceCommandHandler : IRequestHandler<DeleteItemPriceCommand, ApiResponse<DeleteItemPriceCommand.Response>>
    {
        public async Task<ApiResponse<DeleteItemPriceCommand.Response>> Handle(DeleteItemPriceCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Ids), string.Join(",", request.Ids))
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        DELETE FROM it_item_price
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new DeleteItemPriceCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_DeleteSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error deleting item prices: {ex.Message}", ex);

                    var response = ResponseHelper.Error<DeleteItemPriceCommand.Response>("Error occurred while deleting item prices");

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
