using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Employees;

#region Command

/// <summary>
/// Command to create a new employee
/// </summary>
public sealed class CreateEmployeeCommand : BaseCommand, IRequest<ApiResponse<CreateEmployeeCommand.Response>>
{
    public string EmployeeCode { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Gender { get; init; }
    public string? Address { get; init; }
    public string? DepartmentCode { get; init; }
    public string? PositionCode { get; init; }
    public DateTime? JoinDate { get; init; }
    public byte Status { get; init; } = 1;

    public sealed class Response
    {
        public bool Success => Id > 0;
        public long Id { get; init; }
        public string EmployeeCode { get; init; } = default!;
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for CreateEmployeeCommand
/// </summary>
public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Employee code is required")
            .MaximumLength(50).WithMessage("Employee code must not exceed 50 characters")
            .Matches("^[A-Z0-9]+$").WithMessage("Employee code must contain only uppercase letters and numbers")
            .MustAsync(async (code, cancel) => !await IsEmployeeCodeExistsAsync(code))
            .WithMessage("Employee code already exists");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(200).WithMessage("Full name must not exceed 200 characters");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            .Must(email => string.IsNullOrEmpty(email) || ValidationHelper.IsValidEmail(email))
            .WithMessage(CoreResource.Validation_msg_InvalidEmail)
            .MustAsync(async (email, cancel) => string.IsNullOrEmpty(email) || !await IsEmailExistsAsync(email))
            .WithMessage("Email already exists");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters")
            .Must(phone => string.IsNullOrEmpty(phone) || ValidationHelper.IsValidPhoneNumber(phone))
            .WithMessage(CoreResource.Validation_msg_InvalidPhone);

        RuleFor(x => x.Gender)
            .MaximumLength(10).WithMessage("Gender must not exceed 10 characters");

        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters");

        RuleFor(x => x.DepartmentCode)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(50).WithMessage("Department code must not exceed 50 characters")
            .MustAsync(async (code, cancel) => string.IsNullOrEmpty(code) || await DepartmentExistsAsync(code))
            .When(x => !string.IsNullOrEmpty(x.DepartmentCode))
            .WithMessage("Department not found");

        RuleFor(x => x.PositionCode)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(50).WithMessage("Position code must not exceed 50 characters")
            .MustAsync(async (code, cancel) => string.IsNullOrEmpty(code) || await PositionExistsAsync(code))
            .When(x => !string.IsNullOrEmpty(x.PositionCode))
            .WithMessage("Position not found");

        RuleFor(x => x.Status)
            .InclusiveBetween((byte)0, (byte)1).WithMessage("Status must be 0 or 1");
    }

    private static async Task<bool> IsEmployeeCodeExistsAsync(string employeeCode)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                new { EmployeeCode = employeeCode });
        }
    }

    private static async Task<bool> IsEmailExistsAsync(string email)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE Email = @Email) THEN 1 ELSE 0 END",
                new { Email = email });
        }
    }

    private static async Task<bool> DepartmentExistsAsync(string departmentCode)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_departments WHERE Code = @Code) THEN 1 ELSE 0 END",
                new { Code = departmentCode });
        }
    }

    private static async Task<bool> PositionExistsAsync(string positionCode)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_positions WHERE Code = @Code) THEN 1 ELSE 0 END",
                new { Code = positionCode });
        }
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for CreateEmployeeCommand
/// </summary>
public sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ApiResponse<CreateEmployeeCommand.Response>>
{
    public async Task<ApiResponse<CreateEmployeeCommand.Response>> Handle(CreateEmployeeCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                new CoreParamModel(nameof(request.FullName), request.FullName),
                new CoreParamModel(nameof(request.Email), request.Email),
                new CoreParamModel(nameof(request.DepartmentCode), request.DepartmentCode),
                new CoreParamModel(nameof(request.PositionCode), request.PositionCode)
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var id = await dbContext.ExecuteScalarAsync<long>(@"
                    INSERT INTO hr_employees (
                        EmployeeCode, 
                        FullName, 
                        Email, 
                        PhoneNumber, 
                        BirthDate, 
                        Gender, 
                        Address, 
                        DepartmentCode, 
                        PositionCode, 
                        JoinDate, 
                        Status, 
                        CreatedBy, 
                        CreatedAt
                    )
                    VALUES (
                        @EmployeeCode, 
                        @FullName, 
                        @Email, 
                        @PhoneNumber, 
                        @BirthDate, 
                        @Gender, 
                        @Address,
                        @DepartmentCode, 
                        @PositionCode, 
                        @JoinDate, 
                        @Status, 
                        @CreatedBy, 
                        GETDATE()
                    );
                    SELECT SCOPE_IDENTITY();",
                    new
                    {
                        request.EmployeeCode,
                        request.FullName,
                        request.Email,
                        request.PhoneNumber,
                        request.BirthDate,
                        request.Gender,
                        request.Address,
                        request.DepartmentCode,
                        request.PositionCode,
                        request.JoinDate,
                        request.Status,
                        CreatedBy = request.HeaderInfo!.Username
                    },
                    ct);

                await dbContext.transaction.CommitAsync(ct);

            var responseData = new CreateEmployeeCommand.Response { Id = id, EmployeeCode = request.EmployeeCode };
            var response = ResponseHelper.Success(responseData, CoreResource.Employee_msg_CreateSuccess);
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.transaction.RollbackAsync(ct);

                log.IsException = 1;
                log.Message = ex.ToString();
                log.ReturnCode = 500;
                UniLogManager.WriteApiLog(log);

                return ResponseHelper.Error<CreateEmployeeCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }
}

#endregion
