using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Application.Utilities;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Services;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Employees
{
    #region Command

    /// <summary>
    /// Command to create a new employee in the system.
    /// </summary>
    public sealed class CreateEmployeeCommand : BaseCommand, IRequest<ApiResponse<CreateEmployeeCommand.Response>>
    {
        /// <summary>
        /// Gets or sets the unique employee code.
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the employee's full name.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the employee's email address.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the employee's phone number.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the employee's gender.
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the employee's date of birth.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the department code the employee belongs to.
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Gets or sets the position/job title code of the employee.
        /// </summary>
        public string? PositionCode { get; set; }

        /// <summary>
        /// Gets or sets the date the employee joined the company.
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Gets or sets the employee's current status (e.g., Active).
        /// </summary>
        public string Status { get; set; } = CoreCommon.Value.Commonstatus.Active;

        /// <summary>
        /// Represents the response data for a successfully created employee.
        /// </summary>
        public class Response
        {
            /// <summary>
            /// Gets or sets the unique database identifier.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the confirmed employee code.
            /// </summary>
            public string EmployeeCode { get; set; } = string.Empty;
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for CreateEmployeeCommand ensuring all mandatory fields are valid.
    /// </summary>
    public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.EmployeeCode)
                .NotEmpty()
                .WithMessage(status => string.Format(CoreResource.validation_required, CoreResource.lbl_employeeCode))
                .MaximumLength(50)
                .WithMessage(status => string.Format(CoreResource.validation_maxLength, CoreResource.lbl_employeeCode, 50))
                .DependentRules(() =>
                {
                    RuleFor(x => x.FullName)
                        .NotEmpty()
                        .WithMessage(status => string.Format(CoreResource.validation_required, "Full Name"))
                        .MaximumLength(150)
                        .WithMessage(status => string.Format(CoreResource.validation_maxLength, "Full Name", 150));

                    RuleFor(x => x.Email)
                        .Must(ValidationHelper.IsValidEmail)
                        .When(x => !string.IsNullOrEmpty(x.Email))
                        .WithMessage(CoreResource.validation_invalidEmail);

                    RuleFor(x => x.PhoneNumber)
                        .Must(ValidationHelper.IsValidPhoneNumber)
                        .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                        .WithMessage(CoreResource.validation_invalidPhone);

                    RuleFor(x => x.Status)
                        .Must(status => CoreCommon.Value.Commonstatus.All.Contains(status))
                        .WithMessage(CoreResource.validation_invalidStatus);
                });
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler xử lý việc lưu trữ hồ sơ nhân viên mới vào cơ sở dữ liệu.
    /// </summary>
    public sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ApiResponse<CreateEmployeeCommand.Response>>
    {
        public async Task<ApiResponse<CreateEmployeeCommand.Response>> Handle(CreateEmployeeCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.EmployeeCode), request.EmployeeCode),
                    new(nameof(request.FullName), request.FullName),
                    new(nameof(request.Email), request.Email),
                    new(nameof(request.DepartmentCode), request.DepartmentCode),
                    new(nameof(request.PositionCode), request.PositionCode)
                }
            };

            try
            {
                using (var dbContext = new UniManage.Core.Database.DbContext(openTransaction: true))
                {
                    // 1. Kiểm tra trùng mã nhân viên
                    var exists = await dbContext.Set<hr_employees>()
                        .AnyAsync(e => e.EmployeeCode == request.EmployeeCode, ct);

                    if (exists)
                    {
                        var errorMsg = string.Format(CoreResource.validation_alreadyExists, CoreResource.lbl_employeeCode);
                        var errorRes = ResponseHelper.Error<CreateEmployeeCommand.Response>(errorMsg);
                        log.Message = errorMsg;
                        log.ReturnCode = errorRes.ReturnCode;
                        return errorRes;
                    }

                    // 2. Map Command sang Entity
                    // [LƯU Ý]: BirthDate map vào DateOfBirth trong Entity.
                    // Thuộc tính DateOfBirth trong Entity hr_employees.
                    var entity = new hr_employees
                    {
                        EmployeeCode = request.EmployeeCode,
                        FullName = request.FullName,
                        Email = request.Email ?? string.Empty,
                        PhoneNumber = request.PhoneNumber ?? string.Empty,
                        Gender = request.Gender ?? string.Empty,
                        DateOfBirth = request.BirthDate,
                        DepartmentCode = request.DepartmentCode ?? string.Empty,
                        PositionCode = request.PositionCode ?? string.Empty,
                        CreatedAt = DateTimeHelper.Now,
                        CreatedBy = request.HeaderInfo?.Username ?? "SYSTEM",
                        DataRowVersion = new byte[8]
                    };

                    // 3. Lưu vào Database
                    dbContext.Set<hr_employees>().Add(entity);
                    await dbContext.SaveChangesAsync(ct);
                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateEmployeeCommand.Response
                    {
                        Id = entity.Id,
                        EmployeeCode = entity.EmployeeCode
                    };

                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_employee));
                    
                    log.Result = responseData;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<CreateEmployeeCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
