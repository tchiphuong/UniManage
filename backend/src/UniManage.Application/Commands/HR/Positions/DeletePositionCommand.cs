using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Positions;

#region Command

public sealed class DeletePositionCommand : BaseCommand, IRequest<ApiResponse<DeletePositionCommand.Response>>
{
    public List<int> Ids { get; set; } = new();

    public sealed class Response
    {
        public bool Success { get; init; }
        public int DeletedCount { get; init; }
    }
}

#endregion

#region Validator

public sealed class DeletePositionCommandValidator : AbstractValidator<DeletePositionCommand>
{
    public DeletePositionCommandValidator()
    {
        RuleFor(x => x.Ids).NotEmpty().WithMessage("At least one Id is required");
    }
}

#endregion

#region Handler

public sealed class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, ApiResponse<DeletePositionCommand.Response>>
{
    public async Task<ApiResponse<DeletePositionCommand.Response>> Handle(DeletePositionCommand request, CancellationToken ct)
    {
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Ids), string.Join(",", request.Ids))
            }
        };

        using (DbContext dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var rowsAffected = await dbContext.ExecuteAsync(
                    "DELETE FROM hr_positions WHERE Id IN @Ids",
                    new { Ids = request.Ids },
                    ct);

                await dbContext.CommitAsync();

                var responseData = new DeletePositionCommand.Response { Success = true, DeletedCount = rowsAffected };
                var response = ResponseHelper.Success(responseData, string.Format(CoreResource.Position_msg_DeleteSuccess, rowsAffected));
                UniLogManager.WriteApiLog(logData);
                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync();
                UniLogger.Error($"Error deleting positions: {ex.Message}", ex);
                var response = ResponseHelper.Error<DeletePositionCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
                logData.Message = ex.ToString();
                logData.IsException = 1;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);
                return response;
            }
        }
    }
}

#endregion
