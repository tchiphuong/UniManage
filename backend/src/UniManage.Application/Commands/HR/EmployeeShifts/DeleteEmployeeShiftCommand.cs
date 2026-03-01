using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.EmployeeShifts
{
    public sealed class DeleteEmployeeShiftCommand : BaseCommand, IRequest<ApiResponse<DeleteEmployeeShiftCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteEmployeeShiftCommandValidator : AbstractValidator<DeleteEmployeeShiftCommand>
    {
        public DeleteEmployeeShiftCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Ids are required")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Ids must be greater than 0");
        }
    }

    public sealed class DeleteEmployeeShiftCommandHandler : IRequestHandler<DeleteEmployeeShiftCommand, ApiResponse<DeleteEmployeeShiftCommand.Response>>
    {
        public async Task<ApiResponse<DeleteEmployeeShiftCommand.Response>> Handle(DeleteEmployeeShiftCommand request, CancellationToken ct)
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
                        DELETE FROM hr_employee_shifts
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new DeleteEmployeeShiftCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_deleteSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error deleting employee shifts: {ex.Message}", ex);

                    var response = ResponseHelper.Error<DeleteEmployeeShiftCommand.Response>("Error occurred while deleting employee shifts");

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
