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

public sealed class DeleteUnitCommand : BaseCommand, IRequest<ApiResponse<DeleteUnitCommand.Response>>
{
    public List<string> Codes { get; init; } = new();

    public sealed class Response
    {
        public bool Success { get; init; }
        public int DeletedCount { get; init; }
    }
}

#endregion

#region Validator

public sealed class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
{
    public DeleteUnitCommandValidator()
    {
        RuleFor(x => x.Codes).NotEmpty().WithMessage("At least one code is required");
    }
}

#endregion

#region Handler

public sealed class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, ApiResponse<DeleteUnitCommand.Response>>
{
    public async Task<ApiResponse<DeleteUnitCommand.Response>> Handle(DeleteUnitCommand request, CancellationToken ct)
    {
        using var dbContext = new DbContext(openTransaction: true);
        
        var unitsToDelete = await dbContext.Set<ms_units>()
            .Where(x => request.Codes.Contains(x.Code))
            .ToListAsync(ct);

        var deletedCount = unitsToDelete.Count;
        if (deletedCount > 0)
        {
            dbContext.Set<ms_units>().RemoveRange(unitsToDelete);
            await dbContext.SaveChangesAsync(ct);
        }

        await dbContext.CommitAsync();

        var responseData = new DeleteUnitCommand.Response { Success = true, DeletedCount = deletedCount };
        var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
        return response;
    }
}

#endregion


