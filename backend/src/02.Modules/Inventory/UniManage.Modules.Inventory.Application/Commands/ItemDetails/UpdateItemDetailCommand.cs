using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Inventory.Application.Commands.ItemDetails
{
    public sealed class UpdateItemDetailCommand : BaseCommand, IRequest<ApiResponse<UpdateItemDetailCommand.Response>>
    {
        public long Id { get; init; }
        public int? Type { get; init; }
        public string Key { get; init; } = default!;
        public string ValueVi { get; init; } = default!;
        public string ValueEn { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateItemDetailCommandValidator : AbstractValidator<UpdateItemDetailCommand>
    {
        public UpdateItemDetailCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

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
    }

    public sealed class UpdateItemDetailCommandHandler : IRequestHandler<UpdateItemDetailCommand, ApiResponse<UpdateItemDetailCommand.Response>>
    {
        public async Task<ApiResponse<UpdateItemDetailCommand.Response>> Handle(UpdateItemDetailCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        UPDATE it_item_details
                        SET Type = @Type,
                            [Key] = @Key,
                            ValueVi = @ValueVi,
                            ValueEn = @ValueEn,
                            UpdateBy = @UpdateBy,
                            UpdateOn = GETDATE()
                        WHERE Id = @Id";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Id,
                        request.Type,
                        request.Key,
                        request.ValueVi,
                        request.ValueEn,
                        UpdateBy = request.HeaderInfo!.Username
                    }, cancellationToken);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(cancellationToken);
                        var errorResponse = ResponseHelper.Error<UpdateItemDetailCommand.Response>("Item detail not found");
return errorResponse;
                    }

                    

                    var responseData = new UpdateItemDetailCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }
}




