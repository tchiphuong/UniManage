namespace UniManage.Shared.Application.Modules.HrPosition.Commands;

#region Command

public sealed class DeletePositionCommand : BaseCommand, IRequest<ApiResponse<DeletePositionCommand.Response>>
{
    public List<int> Ids { get; init; } = new();

    public sealed class Response 
    {
        public int DeletedCount { get; init; }
    }
}

#endregion

#region Validator

public sealed class DeletePositionValidator : AbstractValidator<DeletePositionCommand>
{
    public DeletePositionValidator()
    {
        RuleFor(x => x.Ids)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "Ids"))
            .MustAsync(async (ids, cancel) => await ArePositionsExistByIdsAsync(ids))
            .WithMessage(string.Format(CoreResource.validation_notFound, CoreResource.entity_position));
    }

    private static async Task<bool> ArePositionsExistByIdsAsync(List<int> ids)
    {
        if (ids == null || !ids.Any()) return false;
        using (var dbContext = new DbContext())
        {
            var count = await dbContext.Set<HrPositions>().CountAsync(x => ids.Contains(x.Id));
            return count == ids.Distinct().Count();
        }
    }
}

#endregion

#region Handler

public sealed class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, ApiResponse<DeletePositionCommand.Response>>
{
    public async Task<ApiResponse<DeletePositionCommand.Response>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
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

            var positionsToDelete = await dbContext.Set<HrPositions>()
                .Where(x => request.Ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var deletedCount = positionsToDelete.Count;
            if (deletedCount > 0)
            {
                dbContext.Set<HrPositions>().RemoveRange(positionsToDelete);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            await dbContext.CommitAsync();

            var responseData = new DeletePositionCommand.Response { DeletedCount = deletedCount };
            var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, CoreResource.entity_position, deletedCount));
            
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




