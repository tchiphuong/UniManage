using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemImages
{
    public sealed class UpdateItemImageCommand : BaseCommand, IRequest<ApiResponse<UpdateItemImageCommand.Response>>
    {
        public int Id { get; init; }
        public string ImageUrl { get; init; } = default!;
        public bool IsThumbnail { get; init; }
        public int SortOrder { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateItemImageCommandValidator : AbstractValidator<UpdateItemImageCommand>
    {
        public UpdateItemImageCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL is required")
                .Length(1, 500).WithMessage("Image URL must be between 1 and 500 characters");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sort order must be greater than or equal to 0");
        }
    }

    public sealed class UpdateItemImageCommandHandler : IRequestHandler<UpdateItemImageCommand, ApiResponse<UpdateItemImageCommand.Response>>
    {
        public async Task<ApiResponse<UpdateItemImageCommand.Response>> Handle(UpdateItemImageCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id.ToString()),
                    new CoreParamModel(nameof(request.ImageUrl), request.ImageUrl),
                    new CoreParamModel(nameof(request.IsThumbnail), request.IsThumbnail.ToString())
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        UPDATE it_item_image
                        SET ImageUrl = @ImageUrl,
                            IsThumbnail = @IsThumbnail,
                            SortOrder = @SortOrder
                        WHERE Id = @Id";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Id,
                        request.ImageUrl,
                        request.IsThumbnail,
                        request.SortOrder
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateItemImageCommand.Response>("Item image not found");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateItemImageCommand.Response { Success = true };
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
                    UniLogger.Error($"Error updating item image: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateItemImageCommand.Response>("Error occurred while updating item image");

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
