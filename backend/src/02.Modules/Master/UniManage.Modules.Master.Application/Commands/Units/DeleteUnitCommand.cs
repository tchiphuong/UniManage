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
    public async Task<ApiResponse<DeleteUnitCommand.Response>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        using var dbContext = new DbContext(openTransaction: true);
        
        var unitsToDelete = await dbContext.Set<MsUnits>()
            .Where(x => request.Codes.Contains(x.Code))
            .ToListAsync(cancellationToken);

        var deletedCount = unitsToDelete.Count;
        if (deletedCount > 0)
        {
            dbContext.Set<MsUnits>().RemoveRange(unitsToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        await dbContext.CommitAsync();

        var responseData = new DeleteUnitCommand.Response { Success = true, DeletedCount = deletedCount };
        var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
        return response;
    }
}

#endregion






