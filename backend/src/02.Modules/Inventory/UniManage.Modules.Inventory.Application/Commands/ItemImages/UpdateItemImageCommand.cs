using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Inventory.Application.Commands.ItemImages
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
        public async Task<ApiResponse<UpdateItemImageCommand.Response>> Handle(UpdateItemImageCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
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
                    }, cancellationToken);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(cancellationToken);
                        var errorResponse = ResponseHelper.Error<UpdateItemImageCommand.Response>("Item image not found");
return errorResponse;
                    }

                    

                    var responseData = new UpdateItemImageCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }
}




