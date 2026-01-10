using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemImages
{
    public sealed class CreateItemImageCommand : BaseCommand, IRequest<ApiResponse<CreateItemImageCommand.Response>>
    {
        public string ItemCode { get; init; } = default!;
        public string ImageUrl { get; init; } = default!;
        public bool IsThumbnail { get; init; }
        public int SortOrder { get; init; }

        public sealed class Response
        {
            public int Id { get; init; }
        }
    }

    public sealed class CreateItemImageCommandValidator : AbstractValidator<CreateItemImageCommand>
    {
        public CreateItemImageCommandValidator()
        {
            RuleFor(x => x.ItemCode)
                .NotEmpty().WithMessage("Item code is required")
                .MustAsync(async (code, cancel) => await IsItemExistsAsync(code))
                .WithMessage("Item does not exist");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL is required")
                .Length(1, 500).WithMessage("Image URL must be between 1 and 500 characters");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sort order must be greater than or equal to 0");
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

    public sealed class CreateItemImageCommandHandler : IRequestHandler<CreateItemImageCommand, ApiResponse<CreateItemImageCommand.Response>>
    {
        public async Task<ApiResponse<CreateItemImageCommand.Response>> Handle(CreateItemImageCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.ItemCode), request.ItemCode),
                    new CoreParamModel(nameof(request.ImageUrl), request.ImageUrl),
                    new CoreParamModel(nameof(request.IsThumbnail), request.IsThumbnail.ToString())
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        INSERT INTO it_item_image (ItemCode, ImageUrl, IsThumbnail, SortOrder, CreatedAt)
                        VALUES (@ItemCode, @ImageUrl, @IsThumbnail, @SortOrder, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<int>(sql, new
                    {
                        request.ItemCode,
                        request.ImageUrl,
                        request.IsThumbnail,
                        request.SortOrder
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateItemImageCommand.Response { Id = id };
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
                    UniLogger.Error($"Error creating item image: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateItemImageCommand.Response>("Error occurred while creating item image");

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
