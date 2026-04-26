using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using DbContext = UniManage.Core.Database.DbContext;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Core.Constant;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Employees
{
    #region Command

    /// <summary>
    /// Command to delete (soft delete) an employee
    /// </summary>
    public sealed class DeleteEmployeeCommand : BaseCommand, IRequest<ApiResponse<DeleteEmployeeCommand.Response>>
    {
        public long Id { get; init; }

        public sealed class Response
        {
            public long Id { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for DeleteEmployeeCommand
    /// </summary>
    public sealed class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_required, "Id"))
                .MustAsync(async (id, cancel) => await IsEmployeeExistsByIdAsync(id))
                .WithMessage(string.Format(CoreResource.validation_notFound, CoreResource.entity_employee));
        }

        private static async Task<bool> IsEmployeeExistsByIdAsync(long id)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<hr_employees>().AnyAsync(x => x.Id == id);
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for DeleteEmployeeCommand
    /// </summary>
    public sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ApiResponse<DeleteEmployeeCommand.Response>>
    {
        public async Task<ApiResponse<DeleteEmployeeCommand.Response>> Handle(DeleteEmployeeCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.Id), request.Id)
                }
            };

            try
            {
                using (var dbContext = new DbContext(openTransaction: true))
                {
                    try
                    {
                        var employee = await dbContext.Set<hr_employees>()
                            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

                        if (employee == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<DeleteEmployeeCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_employee));
                            log.Message = notFoundResponse.Message;
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // Thực hiện xóa thực thể
                        dbContext.Set<hr_employees>().Remove(employee);
                        await dbContext.SaveChangesAsync(ct);
                        await dbContext.CommitAsync();

                        var responseData = new DeleteEmployeeCommand.Response { Id = request.Id };
                        var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, CoreResource.entity_employee));

                        log.Result = responseData;
                        log.Message = response.Message;
                        log.ReturnCode = response.ReturnCode;

                        return response;
                    }
                    catch (Exception ex)
                    {
                        await dbContext.RollbackAsync();
                        log.IsException = true;
                        log.Message = ex.Message;
                        log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                        return ResponseHelper.Error<DeleteEmployeeCommand.Response>(CoreResource.common_error);
                    }
                }
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
