using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Salaries
{
    #region Command

    public sealed class CreateSalaryCommand : BaseCommand, IRequest<ApiResponse<CreateSalaryCommand.Response>>
    {
        public string EmployeeCode { get; init; } = default!;
        public decimal SalaryAmount { get; init; }

        public sealed class Response
        {
            public bool Success => Id > 0;
            public int Id { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class CreateSalaryCommandValidator : AbstractValidator<CreateSalaryCommand>
    {
        public CreateSalaryCommandValidator()
        {
            RuleFor(x => x.EmployeeCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code))
                .WithMessage("Employee not found")
                .MustAsync(async (code, cancel) => !await HasActiveSalaryAsync(code))
                .WithMessage("Employee already has an active salary record");

            RuleFor(x => x.SalaryAmount)
                .GreaterThan(0).WithMessage("Salary amount must be greater than 0");
        }

        private static async Task<bool> IsEmployeeExistsAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                    new { EmployeeCode = employeeCode });
            }
        }

        private static async Task<bool> HasActiveSalaryAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_salaries WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                    new { EmployeeCode = employeeCode });
            }
        }
    }

    #endregion

    #region Handler

    public sealed class CreateSalaryCommandHandler : IRequestHandler<CreateSalaryCommand, ApiResponse<CreateSalaryCommand.Response>>
    {
        public async Task<ApiResponse<CreateSalaryCommand.Response>> Handle(CreateSalaryCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                    new CoreParamModel(nameof(request.SalaryAmount), request.SalaryAmount)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var id = await dbContext.ExecuteScalarAsync<int>(
                        @"INSERT INTO hr_salaries (EmployeeCode, SalaryAmount, CreatedBy, CreatedAt, DataRowVersion)
                          VALUES (@EmployeeCode, @SalaryAmount, @CreatedBy, GETDATE(), 0x00000000000000000001);
                          SELECT SCOPE_IDENTITY();",
                        new
                        {
                            request.EmployeeCode,
                            request.SalaryAmount,
                            CreatedBy = request.HeaderInfo!.Username
                        },
                        ct);

                    await dbContext.ExecuteAsync(
                        @"INSERT INTO hr_salary_history (SalaryId, SalaryAmount, FromDate, CreatedBy, CreatedAt, DataRowVersion)
                          VALUES (@SalaryId, @SalaryAmount, GETDATE(), @CreatedBy, GETDATE(), 0x00000000000000000001)",
                        new
                        {
                            SalaryId = id,
                            request.SalaryAmount,
                            CreatedBy = request.HeaderInfo!.Username
                        },
                        ct);

                    await dbContext.CommitAsync();

                    var response = ResponseHelper.Success(new CreateSalaryCommand.Response { Id = id }, CoreResource.crud_createSuccess);

                    log.Result = response.Data;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error creating salary: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateSalaryCommand.Response>(CoreResource.common_exceptionOccurred);
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
