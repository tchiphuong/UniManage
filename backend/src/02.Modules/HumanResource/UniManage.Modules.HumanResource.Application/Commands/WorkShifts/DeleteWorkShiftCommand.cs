using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.HumanResource.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.HumanResource.Application.Commands.WorkShifts;

#region Command

public sealed class DeleteWorkShiftCommand : BaseCommand, IRequest<ApiResponse<DeleteWorkShiftCommand.Response>>
{
    public List<int> Ids { get; init; } = new();

    public sealed class Response
    {
        public bool Success { get; init; }
        public int DeletedCount { get; init; }
    }
}

#endregion

#region Validator

public sealed class DeleteWorkShiftCommandValidator : AbstractValidator<DeleteWorkShiftCommand>
{
    public DeleteWorkShiftCommandValidator()
    {
        RuleFor(x => x.Ids)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "Ids"))
            .MustAsync(async (ids, cancel) => await AreWorkShiftsExistByIdsAsync(ids))
            .WithMessage(string.Format(CoreResource.validation_notFound, CoreResource.entity_workShift));
    }

    private static async Task<bool> AreWorkShiftsExistByIdsAsync(List<int> ids)
    {
        if (ids == null || !ids.Any()) return false;
        using (var dbContext = new DbContext())
        {
            var count = await dbContext.Set<HrWorkShifts>().CountAsync(x => ids.Contains(x.Id));
            return count == ids.Distinct().Count();
        }
    }
}

#endregion

#region Handler

public sealed class DeleteWorkShiftCommandHandler : IRequestHandler<DeleteWorkShiftCommand, ApiResponse<DeleteWorkShiftCommand.Response>>
{
    public async Task<ApiResponse<DeleteWorkShiftCommand.Response>> Handle(DeleteWorkShiftCommand request, CancellationToken cancellationToken)
    {
        var log = new ApiLogModel(request.HeaderInfo)
        {
            Parameters = new List<LogParamModel>
            {
                new(nameof(request.Ids), string.Join(",", request.Ids))
            }
        };

        try
        {
            using var dbContext = new DbContext(openTransaction: true);

            var workShiftsToDelete = await dbContext.Set<HrWorkShifts>()
                .Where(x => request.Ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var deletedCount = workShiftsToDelete.Count;
            if (deletedCount > 0)
            {
                dbContext.Set<HrWorkShifts>().RemoveRange(workShiftsToDelete);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            await dbContext.CommitAsync();

            var responseData = new DeleteWorkShiftCommand.Response { Success = true, DeletedCount = deletedCount };
            var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, CoreResource.entity_workShift, deletedCount));
            
            log.Result = responseData;
            log.Message = response.Message;
            log.ReturnCode = response.ReturnCode;

            return response;
        }
        catch (Exception ex)
        {
            log.IsException = true;
            log.Message = ex.Message;
            log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
            throw;
        }
        finally
        {
            UniLogManager.WriteApiLog(log);
        }
    }
}

#endregion




