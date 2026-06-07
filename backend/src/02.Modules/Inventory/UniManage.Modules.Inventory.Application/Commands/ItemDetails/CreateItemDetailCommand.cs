using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Inventory.Application.Commands.ItemDetails
{
    public sealed class CreateItemDetailCommand : BaseCommand, IRequest<ApiResponse<CreateItemDetailCommand.Response>>
    {
        public string ItemCode { get; init; } = default!;
        public int? Type { get; init; }
        public string Key { get; init; } = default!;
        public string ValueVi { get; init; } = default!;
        public string ValueEn { get; init; } = default!;

        public sealed class Response
        {
            public long Id { get; init; }
        }
    }

    public sealed class CreateItemDetailCommandValidator : AbstractValidator<CreateItemDetailCommand>
    {
        public CreateItemDetailCommandValidator()
        {
            RuleFor(x => x.ItemCode)
                .NotEmpty().WithMessage("Item code is required")
                .MustAsync(async (code, cancel) => await IsItemExistsAsync(code))
                .WithMessage("Item does not exist");

            RuleFor(x => x.Key)
                .NotEmpty().WithMessage("Key is required")
                .Length(1, 50).WithMessage("Key must be between 1 and 50 characters");

            RuleFor(x => x.ValueVi)
                .NotEmpty().WithMessage("Vietnamese value is required")
                .Length(1, 50).WithMessage("Vietnamese value must be between 1 and 50 characters");

            RuleFor(x => x.ValueEn)
                .NotEmpty().WithMessage("English value is required")
                .Length(1, 50).WithMessage("English value must be between 1 and 50 characters");
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

    public sealed class CreateItemDetailCommandHandler : IRequestHandler<CreateItemDetailCommand, ApiResponse<CreateItemDetailCommand.Response>>
    {
        public async Task<ApiResponse<CreateItemDetailCommand.Response>> Handle(CreateItemDetailCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        INSERT INTO it_item_details (ItemCode, Type, [Key], ValueVi, ValueEn, InsertBy, InsertOn)
                        VALUES (@ItemCode, @Type, @Key, @ValueVi, @ValueEn, @InsertBy, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<long>(sql, new
                    {
                        request.ItemCode,
                        request.Type,
                        request.Key,
                        request.ValueVi,
                        request.ValueEn,
                        InsertBy = request.HeaderInfo!.Username
                    }, cancellationToken);

                    

                    var responseData = new CreateItemDetailCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);
return response;
        }
    }
}




