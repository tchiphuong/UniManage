namespace UniManage.Shared.Application.Modules.HrSalary.Commands
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
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM HrEmployees WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                    new { EmployeeCode = employeeCode });
            }
        }

        private static async Task<bool> HasActiveSalaryAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM HrSalaries WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                    new { EmployeeCode = employeeCode });
            }
        }
    }

    #endregion

    #region Handler

    public sealed class CreateSalaryCommandHandler : IRequestHandler<CreateSalaryCommand, ApiResponse<CreateSalaryCommand.Response>>
    {
        public async Task<ApiResponse<CreateSalaryCommand.Response>> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var id = await dbContext.ExecuteScalarAsync<int>(
                        @"INSERT INTO HrSalaries (EmployeeCode, SalaryAmount, CreatedBy, CreatedAt, DataRowVersion)
                          VALUES (@EmployeeCode, @SalaryAmount, @CreatedBy, GETDATE(), 0x00000000000000000001);
                          SELECT SCOPE_IDENTITY();",
                        new
                        {
                            request.EmployeeCode,
                            request.SalaryAmount,
                            CreatedBy = request.HeaderInfo!.Username
                        },
                        cancellationToken);

                    await dbContext.ExecuteAsync(
                        @"INSERT INTO HrSalaryHistory (SalaryId, SalaryAmount, FromDate, CreatedBy, CreatedAt, DataRowVersion)
                          VALUES (@SalaryId, @SalaryAmount, GETDATE(), @CreatedBy, GETDATE(), 0x00000000000000000001)",
                        new
                        {
                            SalaryId = id,
                            request.SalaryAmount,
                            CreatedBy = request.HeaderInfo!.Username
                        },
                        cancellationToken);

                    

                    var response = ResponseHelper.Success(new CreateSalaryCommand.Response { Id = id }, CoreResource.common_createSuccess);
return response;
        }
    }

    #endregion
}





