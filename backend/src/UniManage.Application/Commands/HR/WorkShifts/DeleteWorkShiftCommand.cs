using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.WorkShifts
{
    #region Command

    public sealed class DeleteWorkShiftCommand : BaseCommand, IRequest<ApiResponse<DeleteWorkShiftCommand.Response>>
    {
        public List<int> Ids { get; init; } = default!;

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
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All IDs must be greater than 0");
        }
    }

    #endregion

    #region Handler

    public sealed class DeleteWorkShiftCommandHandler : IRequestHandler<DeleteWorkShiftCommand, ApiResponse<DeleteWorkShiftCommand.Response>>
    {
        public async Task<ApiResponse<DeleteWorkShiftCommand.Response>> Handle(DeleteWorkShiftCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Ids), string.Join(", ", request.Ids))
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var deletedCount = await dbContext.ExecuteAsync(
                        "DELETE FROM hr_work_shifts WHERE Id IN @Ids",
                        new { Ids = request.Ids },
                        ct);

                    await dbContext.CommitAsync();

                    var responseData = new DeleteWorkShiftCommand.Response { Success = true, DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.Common_msg_DeleteSuccess, deletedCount));

                    log.Result = response.Data;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error deleting work shifts: {ex.Message}", ex);

                    var response = ResponseHelper.Error<DeleteWorkShiftCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
                    log.Message = ex.ToString();
                    log.IsException = 1;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }

    #endregion
}
