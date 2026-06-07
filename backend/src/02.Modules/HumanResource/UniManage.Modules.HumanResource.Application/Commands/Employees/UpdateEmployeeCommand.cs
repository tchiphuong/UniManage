using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.HumanResource.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.HumanResource.Application.Commands.Employees
{
    #region Command

    /// <summary>
    /// Lệnh cập nhật thông tin nhân viên
    /// </summary>
    public sealed class UpdateEmployeeCommand : BaseCommand, IRequest<ApiResponse<UpdateEmployeeCommand.Response>>
    {
        /// <summary>
        /// ID của nhân viên cần cập nhật
        /// </summary>
        public long Id { get; init; }

        /// <summary>
        /// Họ và tên đầy đủ
        /// </summary>
        public string FullName { get; init; } = default!;

        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// Số điện thoại liên lạc
        /// </summary>
        public string? PhoneNumber { get; init; }

        /// <summary>
        /// Ngày tháng năm sinh
        /// </summary>
        public DateTime? BirthDate { get; init; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string? Gender { get; init; }

        /// <summary>
        /// Địa chỉ thường trú/tạm trú
        /// </summary>
        public string? Address { get; init; }

        /// <summary>
        /// Mã phòng ban công tác
        /// </summary>
        public string? DepartmentCode { get; init; }

        /// <summary>
        /// Mã chức vụ đảm nhiệm
        /// </summary>
        public string? PositionCode { get; init; }

        /// <summary>
        /// Ngày bắt đầu gia nhập công ty
        /// </summary>
        public DateTime? JoinDate { get; init; }

        /// <summary>
        /// Trạng thái hoạt động (Active/Inactive)
        /// </summary>
        public byte Status { get; init; }

        /// <summary>
        /// Phiên bản dữ liệu để kiểm soát tranh chấp (Concurrency)
        /// </summary>
        public byte[] RowVersion { get; init; } = default!;

        public sealed class Response
        {
            public bool Success => Id > 0;
            public long Id { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh cập nhật thông tin nhân viên
    /// </summary>
    public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_required, "Id"))
                .MustAsync(async (id, cancel) => await IsEmployeeExistsByIdAsync(id))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_employee));

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_fullName))
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_fullName, 200));

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(100).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_email, 100))
                .Must(email => string.IsNullOrEmpty(email) || ValidationHelper.IsValidEmail(email))
                .WithMessage(CoreResource.validation_invalidEmail)
                .MustAsync(async (command, email, cancel) =>
                    string.IsNullOrEmpty(email) || !await IsEmailTakenByAnotherAsync(email, command.Id))
                .WithMessage(CoreResource.validation_alreadyExists);

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_phoneNumber, 20))
                .Must(phone => string.IsNullOrEmpty(phone) || ValidationHelper.IsValidPhoneNumber(phone))
                .WithMessage(CoreResource.validation_invalidPhone);

            RuleFor(x => x.Gender)
                .MaximumLength(20).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_gender, 20));

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_address, 500));

            RuleFor(x => x.DepartmentCode)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_departmentCode, 50))
                .MustAsync(async (code, cancel) => string.IsNullOrEmpty(code) || await DepartmentExistsAsync(code))
                .When(x => !string.IsNullOrEmpty(x.DepartmentCode))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_department));

            RuleFor(x => x.PositionCode)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_positionCode, 50))
                .MustAsync(async (code, cancel) => string.IsNullOrEmpty(code) || await PositionExistsAsync(code))
                .When(x => !string.IsNullOrEmpty(x.PositionCode))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_position));

            RuleFor(x => x.RowVersion)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_rowVersion));
        }

        private static async Task<bool> IsEmployeeExistsByIdAsync(long id)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrEmployees>().AnyAsync(x => x.Id == id);
            }
        }

        private static async Task<bool> IsEmailTakenByAnotherAsync(string email, long id)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrEmployees>().AnyAsync(x => x.Email == email && x.Id != id);
            }
        }

        private static async Task<bool> DepartmentExistsAsync(string departmentCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrDepartments>().AnyAsync(x => x.Code == departmentCode);
            }
        }

        private static async Task<bool> PositionExistsAsync(string positionCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrPositions>().AnyAsync(x => x.Code == positionCode);
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh cập nhật thông tin nhân viên
    /// </summary>
    public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ApiResponse<UpdateEmployeeCommand.Response>>
    {
        public async Task<ApiResponse<UpdateEmployeeCommand.Response>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Id), request.Id),
                    new(nameof(request.FullName), request.FullName),
                    new(nameof(request.Email), request.Email),
                    new(nameof(request.DepartmentCode), request.DepartmentCode),
                    new(nameof(request.PositionCode), request.PositionCode)
                }
            };

            try
            {
                using (var dbContext = new DbContext(openTransaction: true))
                {
                    try
                    {
                        var employee = await dbContext.Set<HrEmployees>()
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                        if (employee == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<UpdateEmployeeCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_employee));
                            log.Message = notFoundResponse.Message;
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // Kiểm tra xung đột dữ liệu (Concurrency) dựa trên RowVersion
                        if (request.RowVersion != null && !employee.DataRowVersion.SequenceEqual(request.RowVersion))
                        {
                            var concurrencyResponse = ResponseHelper.Error<UpdateEmployeeCommand.Response>(CoreResource.common_concurrencyError);
                            log.Message = concurrencyResponse.Message;
                            log.ReturnCode = concurrencyResponse.ReturnCode;
                            return concurrencyResponse;
                        }

                        // Cập nhật thông tin chi tiết
                        employee.FullName = request.FullName;
                        employee.Email = request.Email ?? string.Empty;
                        employee.PhoneNumber = request.PhoneNumber ?? string.Empty;
                        employee.DateOfBirth = request.BirthDate;
                        employee.Gender = request.Gender ?? string.Empty;
                        employee.Address = request.Address ?? string.Empty;
                        employee.DepartmentCode = request.DepartmentCode ?? string.Empty;
                        employee.PositionCode = request.PositionCode ?? string.Empty;
                        employee.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                        employee.UpdatedAt = DateTimeHelper.Now;

                        await dbContext.SaveChangesAsync(cancellationToken);
                        await dbContext.CommitAsync();

                        var responseData = new UpdateEmployeeCommand.Response { Id = request.Id };
                        var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_employee));

                        log.Result = responseData;
                        log.Message = response.Message;
                        log.ReturnCode = response.ReturnCode;

                        return response;
                    }
                    catch (Exception)
                    {
                        await dbContext.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<UpdateEmployeeCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




