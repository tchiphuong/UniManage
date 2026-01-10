using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.Items
{
    public sealed class DeleteItemCommand : BaseCommand, IRequest<ApiResponse<DeleteItemCommand.Response>>
    {
        public List<string> Codes { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator()
        {
            RuleFor(x => x.Codes)
                .NotEmpty().WithMessage("Item codes are required")
                .Must(codes => codes.All(code => !string.IsNullOrEmpty(code)))
                .WithMessage("All item codes must be valid");
        }
    }

    public sealed class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, ApiResponse<DeleteItemCommand.Response>>
    {
        public async Task<ApiResponse<DeleteItemCommand.Response>> Handle(DeleteItemCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Codes), string.Join(",", request.Codes))
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        DELETE FROM it_items
                        WHERE Code IN @Codes";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Codes = request.Codes }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new DeleteItemCommand.Response { DeletedCount = deletedCount };
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
                    UniLogger.Error($"Error deleting items: {ex.Message}", ex);

                    var response = ResponseHelper.Error<DeleteItemCommand.Response>("Error occurred while deleting items");

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
