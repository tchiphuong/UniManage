using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
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
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Codes), string.Join(", ", request.Codes))
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var deletedCount = await dbContext.ExecuteAsync("DELETE FROM ms_units WHERE Code IN @Codes", new { Codes = request.Codes }, ct);

                await dbContext.transaction.CommitAsync(ct);

                var responseData = new DeleteUnitCommand.Response { Success = true, DeletedCount = deletedCount };
                var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_DeleteSuccess);

                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.transaction.RollbackAsync(ct);

                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                return ResponseHelper.Error<DeleteUnitCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }
}

#endregion
