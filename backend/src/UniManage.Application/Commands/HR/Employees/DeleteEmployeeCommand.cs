using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Employees;

/// <summary>
/// Command to delete (soft delete) an employee
/// </summary>
public sealed class DeleteEmployeeCommand : BaseCommand, IRequest<ApiResponse<DeleteEmployeeCommand.Response>>
{
    public long Id { get; init; }

    // Internal - set by controller from token
    internal string DeletedBy { get; init; } = default!;

    public sealed class Response
    {
        public long Id { get; init; }
    }
}

/// <summary>
/// Validator for DeleteEmployeeCommand
/// </summary>
public sealed class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}

/// <summary>
/// Handler for DeleteEmployeeCommand
/// </summary>
public sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ApiResponse<DeleteEmployeeCommand.Response>>
{
    public async Task<ApiResponse<DeleteEmployeeCommand.Response>> Handle(DeleteEmployeeCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
        {
            Parameter = new List<CoreParamModel>
            {
                new() { Name = nameof(request.Id), Value = request.Id.ToString() },
                new() { Name = nameof(request.DeletedBy), Value = request.DeletedBy }
            }
        };

        try
        {
            // Check if employee exists
            using (var checkDb = new DbContext())
            {
                var exists = await checkDb.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE Id = @Id) THEN 1 ELSE 0 END",
                    new { request.Id },
                    ct);

                if (!exists)
                {
                    return ResponseHelper.NotFound<DeleteEmployeeCommand.Response>("Employee not found");
                }
            }

            using (var db = new DbContext(openTransaction: true))
            {
                try
                {
                    // Soft delete by setting Status = 0
                    var rowsAffected = await db.ExecuteAsync(
                        """
                        UPDATE hr_employees
                        SET Status = 0,
                            UpdatedBy = @DeletedBy,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id
                        """,
                        request,
                        ct);

                    if (rowsAffected == 0)
                    {
                        await db.RollbackAsync(ct);
                        return ResponseHelper.NotFound<DeleteEmployeeCommand.Response>("Employee not found");
                    }

                    await db.CommitAsync(ct);

                var response = ResponseHelper.Success(new DeleteEmployeeCommand.Response { Id = request.Id }, string.Format(string.Format(CoreResource.crud_deleteSuccess, CoreResource.entity_employee), 1));
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogger.Info(JsonConvert.SerializeObject(log));

                    return response;
                }
                catch
                {
                    await db.RollbackAsync(ct);
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            log.IsException = 1;
            log.Message = ex.Message;
            log.ReturnCode = 500;
            UniLogger.Error(JsonConvert.SerializeObject(log));

            return ResponseHelper.Error<DeleteEmployeeCommand.Response>($"Failed to delete employee: {ex.Message}");
        }
    }
}
