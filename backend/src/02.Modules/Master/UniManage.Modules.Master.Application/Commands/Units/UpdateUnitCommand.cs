using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.Master.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.Master.Application.Commands.Units;

#region Command

public sealed class UpdateUnitCommand : BaseCommand, IRequest<ApiResponse<UpdateUnitCommand.Response>>
{
    public string Code { get; set; } = default!;
    public string NameVi { get; init; } = default!;
    public string NameEn { get; init; } = default!;
    public byte[] DataRowVersion { get; init; } = default!;

    public sealed class Response
    {
        public bool Success { get; init; }
    }
}

#endregion

#region Validator

public sealed class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");

        RuleFor(x => x.NameVi).NotEmpty().WithMessage("Vietnamese name is required").Length(1, 100).WithMessage("Vietnamese name must be between 1 and 100 characters");

        RuleFor(x => x.NameEn).NotEmpty().WithMessage("English name is required").Length(1, 100).WithMessage("English name must be between 1 and 100 characters");

        RuleFor(x => x.DataRowVersion).NotEmpty().WithMessage("DataRowVersion is required");
    }
}

#endregion

#region Handler

public sealed class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, ApiResponse<UpdateUnitCommand.Response>>
{
    public async Task<ApiResponse<UpdateUnitCommand.Response>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        using var dbContext = new DbContext(openTransaction: true);
        
        var unit = await dbContext.Set<MsUnits>()
            .FirstOrDefaultAsync(x => x.Code == request.Code, cancellationToken);

        if (unit == null)
        {
            return ResponseHelper.NotFound<UpdateUnitCommand.Response>(CoreResource.common_notFound);
        }

        // Optimistic concurrency check
        if (request.DataRowVersion != null && !unit.DataRowVersion.SequenceEqual(request.DataRowVersion))
        {
            return ResponseHelper.Error<UpdateUnitCommand.Response>(CoreResource.common_concurrencyError);
        }

        unit.NameVi = request.NameVi;
        unit.NameEn = request.NameEn;
        unit.UpdatedBy = request.HeaderInfo!.Username;
        unit.UpdatedAt = DateTimeHelper.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        await dbContext.CommitAsync();

        var responseData = new UpdateUnitCommand.Response { Success = true };
        var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
        return response;
    }
}

#endregion






