using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Departments;

#region Command

public sealed class DeleteDepartmentCommand : BaseCommand, IRequest<ApiResponse<DeleteDepartmentCommand.Response>>
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

public sealed class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(x => x.Ids).NotEmpty().WithMessage("At least one Id is required");
    }
}

#endregion

#region Handler

public sealed class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ApiResponse<DeleteDepartmentCommand.Response>>
{
    public async Task<ApiResponse<DeleteDepartmentCommand.Response>> Handle(DeleteDepartmentCommand request, CancellationToken ct)
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
                    "DELETE FROM hr_departments WHERE Id IN @Ids",
                    new { Ids = request.Ids },
                    ct);

                await dbContext.CommitAsync();

                var responseData = new DeleteDepartmentCommand.Response { Success = true, DeletedCount = rowsAffected };
                var response = ResponseHelper.Success(responseData, string.Format(string.Format(CoreResource.crud_deleteSuccess, CoreResource.entity_department), rowsAffected));
                UniLogManager.WriteApiLog(logData);
                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync();
                UniLogger.Error($"Error deleting departments: {ex.Message}", ex);
                var response = ResponseHelper.Error<DeleteDepartmentCommand.Response>(CoreResource.common_exceptionOccurred);
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
