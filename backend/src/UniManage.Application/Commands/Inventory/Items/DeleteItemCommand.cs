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
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM it_items
                        WHERE Code IN @Codes";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Codes = request.Codes }, ct);

                    

                    var responseData = new DeleteItemCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}


