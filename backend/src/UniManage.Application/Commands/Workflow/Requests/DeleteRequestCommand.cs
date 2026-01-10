using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Workflow.Requests
{
    public sealed class DeleteRequestCommand : BaseCommand, IRequest<ApiResponse<DeleteRequestCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteRequestCommandValidator : AbstractValidator<DeleteRequestCommand>
    {
        public DeleteRequestCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Ids are required")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Ids must be greater than 0");

            RuleFor(x => x.Ids)
                .MustAsync(async (ids, cancel) => await AreAllRequestsDraftAsync(ids))
                .WithMessage("Only draft requests can be deleted");
        }

        private static async Task<bool> AreAllRequestsDraftAsync(List<int> ids)
        {
            using (var dbContext = new DbContext())
            {
                var sql = @"
                    SELECT COUNT(*)
                    FROM wf_request
                    WHERE Id IN @Ids AND Status = 'Draft'";

                var draftCount = await dbContext.ExecuteScalarAsync<int>(sql, new { Ids = ids });
                return draftCount == ids.Count;
            }
        }
    }

    public sealed class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, ApiResponse<DeleteRequestCommand.Response>>
    {
        public async Task<ApiResponse<DeleteRequestCommand.Response>> Handle(DeleteRequestCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Ids), string.Join(",", request.Ids))
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        DELETE FROM wf_request
                        WHERE Id IN @Ids AND Status = 'Draft'";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new DeleteRequestCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_DeleteSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error deleting requests: {ex.Message}", ex);

                    var response = ResponseHelper.Error<DeleteRequestCommand.Response>("Error occurred while deleting requests");

                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}
