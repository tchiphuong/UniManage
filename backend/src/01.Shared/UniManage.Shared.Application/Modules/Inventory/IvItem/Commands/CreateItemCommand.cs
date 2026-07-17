using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.IvItem.Commands
{
    public sealed class CreateItemCommand : BaseCommand, IRequest<ApiResponse<CreateItemCommand.Response>>
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
            public int Id { get; init; }
        }
    }

    public sealed class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Item code is required")
                .Length(2, 50).WithMessage("Item code must be between 2 and 50 characters")
                .Must(ValidationHelper.IsValidUserCode).WithMessage("Item code allows only alphanumeric and underscore")
                .MustAsync(async (code, cancel) => !await IsItemCodeExistsAsync(code))
                .WithMessage("Item code already exists");

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

        private static async Task<bool> IsItemCodeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM it_items WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
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

    public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ApiResponse<CreateItemCommand.Response>>
    {
        public async Task<ApiResponse<CreateItemCommand.Response>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        INSERT INTO it_items (Code, Name, Description, BrandCode, CategoryCode, ColorCode, SizeCode, CreatedAt)
                        VALUES (@Code, @Name, @Description, @BrandCode, @CategoryCode, @ColorCode, @SizeCode, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<int>(sql, new
                    {
                        request.Code,
                        request.Name,
                        request.Description,
                        request.BrandCode,
                        request.CategoryCode,
                        request.ColorCode,
                        request.SizeCode
                    }, cancellationToken);

                    

                    var responseData = new CreateItemCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);
return response;
        }
    }
}




