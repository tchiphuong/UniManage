using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.IvItemImage.Commands
{
    public sealed class DeleteItemImageCommand : BaseCommand, IRequest<ApiResponse<DeleteItemImageCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteItemImageCommandValidator : AbstractValidator<DeleteItemImageCommand>
    {
        public DeleteItemImageCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Item image ids are required")
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All ids must be greater than 0");
        }
    }

    public sealed class DeleteItemImageCommandHandler : IRequestHandler<DeleteItemImageCommand, ApiResponse<DeleteItemImageCommand.Response>>
    {
        public async Task<ApiResponse<DeleteItemImageCommand.Response>> Handle(DeleteItemImageCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM it_item_image
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, cancellationToken);

                    

                    var responseData = new DeleteItemImageCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}




