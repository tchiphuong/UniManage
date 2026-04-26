using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using DbContext = UniManage.Core.Database.DbContext;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;

namespace UniManage.Application.Commands.Master.Units;

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
    public async Task<ApiResponse<UpdateUnitCommand.Response>> Handle(UpdateUnitCommand request, CancellationToken ct)
    {
        using var dbContext = new DbContext(openTransaction: true);
        
        var unit = await dbContext.Set<ms_units>()
            .FirstOrDefaultAsync(x => x.Code == request.Code, ct);

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

        await dbContext.SaveChangesAsync(ct);
        await dbContext.CommitAsync();

        var responseData = new UpdateUnitCommand.Response { Success = true };
        var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
        return response;
    }
}

#endregion


