using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Inventory.Application.Commands.ItemImages
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
        public async Task<ApiResponse<CreateItemImageCommand.Response>> Handle(CreateItemImageCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
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
                    }, cancellationToken);

                    

                    var responseData = new CreateItemImageCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);
return response;
        }
    }
}




