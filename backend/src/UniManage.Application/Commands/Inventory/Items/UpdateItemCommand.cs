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
        public async Task<ApiResponse<UpdateItemCommand.Response>> Handle(UpdateItemCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Code), request.Code),
                    new CoreParamModel(nameof(request.Name), request.Name),
                    new CoreParamModel(nameof(request.BrandCode), request.BrandCode),
                    new CoreParamModel(nameof(request.CategoryCode), request.CategoryCode)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
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
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateItemCommand.Response>("Item not found");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateItemCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_updateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error updating item: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateItemCommand.Response>("Error occurred while updating item");

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
