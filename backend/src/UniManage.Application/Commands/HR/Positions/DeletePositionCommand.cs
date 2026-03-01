using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Positions
{
    #region Command

    public sealed class DeletePositionCommand : BaseCommand, IRequest<ApiResponse<DeletePositionCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response { }
    }

    #endregion

    #region Validator

    public sealed class DeletePositionValidator : AbstractValidator<DeletePositionCommand>
    {
        public DeletePositionValidator()
        {
            RuleFor(x => x.Ids).NotEmpty();
        }
    }

    #endregion

    #region Handler

    public sealed class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, ApiResponse<DeletePositionCommand.Response>>
    {
        public async Task<ApiResponse<DeletePositionCommand.Response>> Handle(DeletePositionCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Method = nameof(DeletePositionCommandHandler),
                Path = "Position" // removed Parameter logic for batch
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = "DELETE FROM hr_positions WHERE Id IN @Ids;";
                    var rows = await dbContext.ExecuteAsync(sql, new { request.Ids });

                    if (rows == 0)
                    {
                        var notFound = ResponseHelper.NotFound<DeletePositionCommand.Response>(string.Format(CoreResource.crud_notFound, CoreResource.entity_position));
                        log.ReturnCode = notFound.ReturnCode;
                        log.Message = notFound.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFound;
                    }

                    await dbContext.CommitAsync(ct);

                    var response = ResponseHelper.Success(new DeletePositionCommand.Response(), string.Format(CoreResource.crud_deleteSuccess, CoreResource.entity_position));
                    
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                    UniLogManager.WriteApiLog(log);
                    return ResponseHelper.Error<DeletePositionCommand.Response>(CoreResource.common_exceptionOccurred);
                }
            }
        }
    }

    #endregion
}
