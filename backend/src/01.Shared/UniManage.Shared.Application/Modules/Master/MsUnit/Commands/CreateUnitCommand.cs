using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.MsUnit.Commands;

#region Command

public sealed class CreateUnitCommand : BaseCommand, IRequest<ApiResponse<CreateUnitCommand.Response>>
{
    public string Code { get; init; } = default!;
    public string NameVi { get; init; } = default!;
    public string NameEn { get; init; } = default!;

    public sealed class Response
    {
        public long Id { get; init; }
        public string Code { get; init; } = default!;
    }
}

#endregion

#region Validator

public sealed class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitCommandValidator()
    {
        RuleFor(x => x.Code).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Code is required").Length(1, 50).WithMessage("Code must be between 1 and 50 characters").Must(ValidationHelper.IsValidUserCode).WithMessage("Code allows only alphanumeric and underscore").MustAsync(async (code, cancel) => !await IsCodeExistsAsync(code)).WithMessage("Code already exists");

        RuleFor(x => x.NameVi).NotEmpty().WithMessage("Vietnamese name is required").Length(1, 100).WithMessage("Vietnamese name must be between 1 and 100 characters");

        RuleFor(x => x.NameEn).NotEmpty().WithMessage("English name is required").Length(1, 100).WithMessage("English name must be between 1 and 100 characters");
    }

    private static async Task<bool> IsCodeExistsAsync(string code)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.Set<MsUnits>().AnyAsync(x => x.Code == code);
        }
    }
}

#endregion

#region Handler

public sealed class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, ApiResponse<CreateUnitCommand.Response>>
{
    public async Task<ApiResponse<CreateUnitCommand.Response>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        using var dbContext = new DbContext(openTransaction: true);
        
        var unit = new MsUnits
        {
            Code = request.Code,
            NameVi = request.NameVi,
            NameEn = request.NameEn,
            CreatedBy = request.HeaderInfo!.Username,
            CreatedAt = DateTimeHelper.Now
        };

        dbContext.Set<MsUnits>().Add(unit);
        await dbContext.SaveChangesAsync(cancellationToken);
        await dbContext.CommitAsync();

        var responseData = new CreateUnitCommand.Response { Id = unit.Id, Code = unit.Code };
        var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);
        return response;
    }
}

#endregion






