namespace UniManage.Shared.Application.Modules.HrEmployeeShift.Commands
{
    public sealed class CreateEmployeeShiftCommand : BaseCommand, IRequest<ApiResponse<CreateEmployeeShiftCommand.Response>>
    {
        public string EmployeeCode { get; init; } = default!;
        public string WorkShiftCode { get; init; } = default!;
        public DateTime WorkDate { get; init; }

        public sealed class Response
        {
            public int Id { get; init; }
        }
    }

    public sealed class CreateEmployeeShiftCommandValidator : AbstractValidator<CreateEmployeeShiftCommand>
    {
        public CreateEmployeeShiftCommandValidator()
        {
            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("Employee code is required")
                .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code))
                .WithMessage("Employee does not exist");

            RuleFor(x => x.WorkShiftCode)
                .NotEmpty().WithMessage("Work shift code is required")
                .MustAsync(async (code, cancel) => await IsWorkShiftExistsAsync(code))
                .WithMessage("Work shift does not exist");

            RuleFor(x => x.WorkDate)
                .NotEmpty().WithMessage("Work date is required");

            RuleFor(x => x)
                .MustAsync(async (cmd, cancel) => !await IsDuplicateShiftAssignmentAsync(cmd.EmployeeCode, cmd.WorkDate))
                .WithMessage("Employee already has a shift assigned for this date");
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

        private static async Task<bool> IsWorkShiftExistsAsync(string workShiftCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM HrWorkShifts WHERE Code = @WorkShiftCode) THEN 1 ELSE 0 END",
                    new { WorkShiftCode = workShiftCode });
            }
        }

        private static async Task<bool> IsDuplicateShiftAssignmentAsync(string employeeCode, DateTime workDate)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM HrEmployeeShifts WHERE EmployeeCode = @EmployeeCode AND WorkDate = @WorkDate) THEN 1 ELSE 0 END",
                    new { EmployeeCode = employeeCode, WorkDate = workDate });
            }
        }
    }

    public sealed class CreateEmployeeShiftCommandHandler : IRequestHandler<CreateEmployeeShiftCommand, ApiResponse<CreateEmployeeShiftCommand.Response>>
    {
        public async Task<ApiResponse<CreateEmployeeShiftCommand.Response>> Handle(CreateEmployeeShiftCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        INSERT INTO HrEmployeeShifts (EmployeeCode, WorkShiftCode, WorkDate, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                        VALUES (@EmployeeCode, @WorkShiftCode, @WorkDate, @CreatedBy, GETDATE(), @CreatedBy, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<int>(sql, new
                    {
                        request.EmployeeCode,
                        request.WorkShiftCode,
                        request.WorkDate,
                        CreatedBy = request.HeaderInfo!.Username
                    }, cancellationToken);

                    

                    var responseData = new CreateEmployeeShiftCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_employeeShift));
return response;
        }
    }
}



