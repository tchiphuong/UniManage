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
/// Command to update employee information
/// </summary>
public sealed class UpdateEmployeeCommand : BaseCommand, IRequest<ApiResponse<UpdateEmployeeCommand.Response>>
{
    public long Id { get; init; }
    public string FullName { get; init; } = default!;
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Gender { get; init; }
    public string? Address { get; init; }
    public string? DepartmentCode { get; init; }
    public string? PositionCode { get; init; }
    public DateTime? JoinDate { get; init; }
    public byte Status { get; init; }

    public sealed class Response
    {
        public bool Success => Id > 0;
        public long Id { get; init; }
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for UpdateEmployeeCommand
/// </summary>
public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Id must be greater than 0")
            .MustAsync(async (id, cancel) => await EmployeeExistsAsync(id))
            .WithMessage("Employee not found");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(200).WithMessage("Full name must not exceed 200 characters");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            .Must(email => string.IsNullOrEmpty(email) || ValidationHelper.IsValidEmail(email))
            .WithMessage(CoreResource.validation_invalidEmail)
            .MustAsync(async (command, email, cancel) =>
                string.IsNullOrEmpty(email) || !await IsEmailTakenByAnotherAsync(email, command.Id))
            .WithMessage("Email already exists");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters")
            .Must(phone => string.IsNullOrEmpty(phone) || ValidationHelper.IsValidPhoneNumber(phone))
            .WithMessage(CoreResource.validation_invalidPhone);

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

    private static async Task<bool> EmployeeExistsAsync(long id)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE Id = @Id) THEN 1 ELSE 0 END",
                new { Id = id });
        }
    }

    private static async Task<bool> IsEmailTakenByAnotherAsync(string email, long id)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE Email = @Email AND Id != @Id) THEN 1 ELSE 0 END",
                new { Email = email, Id = id });
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
/// Handler for UpdateEmployeeCommand
/// </summary>
public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ApiResponse<UpdateEmployeeCommand.Response>>
{
    public async Task<ApiResponse<UpdateEmployeeCommand.Response>> Handle(UpdateEmployeeCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Id), request.Id.ToString()),
                new CoreParamModel(nameof(request.FullName), request.FullName),
                new CoreParamModel(nameof(request.Email), request.Email),
                new CoreParamModel(nameof(request.DepartmentCode), request.DepartmentCode),
                new CoreParamModel(nameof(request.PositionCode), request.PositionCode),
                new CoreParamModel(nameof(request.Status), request.Status.ToString())
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var rowsAffected = await dbContext.ExecuteAsync(@"
                    UPDATE hr_employees
                    SET FullName = @FullName,
                        Email = @Email,
                        PhoneNumber = @PhoneNumber,
                        BirthDate = @BirthDate,
                        Gender = @Gender,
                        Address = @Address,
                        DepartmentCode = @DepartmentCode,
                        PositionCode = @PositionCode,
                        JoinDate = @JoinDate,
                        Status = @Status,
                        UpdatedBy = @UpdatedBy,
                        UpdatedAt = GETDATE()
                    WHERE Id = @Id",
                    new
                    {
                        request.Id,
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
                        UpdatedBy = request.HeaderInfo!.Username
                    },
                    ct);

                if (rowsAffected == 0)
                {
                    await dbContext.transaction.RollbackAsync(ct);
                    return ResponseHelper.NotFound<UpdateEmployeeCommand.Response>("Employee not found");
                }

                await dbContext.transaction.CommitAsync(ct);

                var response = ResponseHelper.Success(new UpdateEmployeeCommand.Response { Id = request.Id }, string.Format(CoreResource.crud_updateSuccess, CoreResource.entity_employee));
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

                return ResponseHelper.Error<UpdateEmployeeCommand.Response>(CoreResource.common_exceptionOccurred);
            }
        }
    }
}

#endregion
