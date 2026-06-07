using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Inventory.Application.Commands.Items
{
    public sealed class UpdateItemCommand : BaseCommand, IRequest<ApiResponse<UpdateItemCommand.Response>>
    {
        public string Code { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public string? BrandCode { get; init; }
        public string? CategoryCode { get; init; }
        public string? ColorCode { get; init; }
        public string? SizeCode { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Item code is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Item name is required")
                .Length(2, 255).WithMessage("Item name must be between 2 and 255 characters");

            When(x => !string.IsNullOrEmpty(x.BrandCode), () =>
            {
                RuleFor(x => x.BrandCode)
                    .MustAsync(async (code, cancel) => await IsBrandExistsAsync(code!))
                    .WithMessage("Brand does not exist");
            });

            When(x => !string.IsNullOrEmpty(x.CategoryCode), () =>
            {
                RuleFor(x => x.CategoryCode)
                    .MustAsync(async (code, cancel) => await IsCategoryExistsAsync(code!))
                    .WithMessage("Category does not exist");
            });

            When(x => !string.IsNullOrEmpty(x.ColorCode), () =>
            {
                RuleFor(x => x.ColorCode)
                    .MustAsync(async (code, cancel) => await IsColorExistsAsync(code!))
                    .WithMessage("Color does not exist");
            });

            When(x => !string.IsNullOrEmpty(x.SizeCode), () =>
            {
                RuleFor(x => x.SizeCode)
                    .MustAsync(async (code, cancel) => await IsSizeExistsAsync(code!))
                    .WithMessage("Size does not exist");
            });
        }

        private static async Task<bool> IsBrandExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM it_item_brand WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }

        private static async Task<bool> IsCategoryExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM it_item_category WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }

        private static async Task<bool> IsColorExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM it_item_color WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }

        private static async Task<bool> IsSizeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM it_item_size WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }
    }

    public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ApiResponse<UpdateItemCommand.Response>>
    {
        public async Task<ApiResponse<UpdateItemCommand.Response>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        UPDATE it_items
                        SET Name = @Name,
                            Description = @Description,
                            BrandCode = @BrandCode,
                            CategoryCode = @CategoryCode,
                            ColorCode = @ColorCode,
                            SizeCode = @SizeCode
                        WHERE Code = @Code";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Code,
                        request.Name,
                        request.Description,
                        request.BrandCode,
                        request.CategoryCode,
                        request.ColorCode,
                        request.SizeCode
                    }, cancellationToken);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(cancellationToken);
                        var errorResponse = ResponseHelper.Error<UpdateItemCommand.Response>("Item not found");
return errorResponse;
                    }

                    

                    var responseData = new UpdateItemCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }
}




