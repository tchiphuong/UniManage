namespace UniManage.Shared.Application.Modules.HrEmployee.Commands
{
    #region Command

    /// <summary>
    /// L?nh c?p nh?t th�ng tin nh�n vi�n
    /// </summary>
    public sealed class UpdateEmployeeCommand : BaseCommand, IRequest<ApiResponse<UpdateEmployeeCommand.Response>>
    {
        /// <summary>
        /// ID c?a nh�n vi�n c?n c?p nh?t
        /// </summary>
        public long Id { get; init; }

        /// <summary>
        /// H? v� t�n d?y d?
        /// </summary>
        public string FullName { get; init; } = default!;

        /// <summary>
        /// �?a ch? Email
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// S? di?n tho?i li�n l?c
        /// </summary>
        public string? PhoneNumber { get; init; }

        /// <summary>
        /// Ng�y th�ng nam sinh
        /// </summary>
        public DateTime? BirthDate { get; init; }

        /// <summary>
        /// Gi?i t�nh
        /// </summary>
        public string? Gender { get; init; }

        /// <summary>
        /// �?a ch? thu?ng tr�/t?m tr�
        /// </summary>
        public string? Address { get; init; }

        /// <summary>
        /// M� ph�ng ban c�ng t�c
        /// </summary>
        public string? DepartmentCode { get; init; }

        /// <summary>
        /// M� ch?c v? d?m nhi?m
        /// </summary>
        public string? PositionCode { get; init; }

        /// <summary>
        /// Ng�y b?t d?u gia nh?p c�ng ty
        /// </summary>
        public DateTime? JoinDate { get; init; }

        /// <summary>
        /// Tr?ng th�i ho?t d?ng (Active/Inactive)
        /// </summary>
        public byte Status { get; init; }

        /// <summary>
        /// Phi�n b?n d? li?u d? ki?m so�t tranh ch?p (Concurrency)
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
    /// B? ki?m tra d? li?u cho l?nh c?p nh?t th�ng tin nh�n vi�n
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
    /// B? x? l� l?nh c?p nh?t th�ng tin nh�n vi�n
    /// </summary>
    public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ApiResponse<UpdateEmployeeCommand.Response>>
    {
        public async Task<ApiResponse<UpdateEmployeeCommand.Response>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log nghi?p v?
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

                        // Ki?m tra xung d?t d? li?u (Concurrency) d?a tr�n RowVersion
                        if (request.RowVersion != null && !employee.DataRowVersion.SequenceEqual(request.RowVersion))
                        {
                            var concurrencyResponse = ResponseHelper.Error<UpdateEmployeeCommand.Response>(CoreResource.common_concurrencyError);
                            log.Message = concurrencyResponse.Message;
                            log.ReturnCode = concurrencyResponse.ReturnCode;
                            return concurrencyResponse;
                        }

                        // C?p nh?t th�ng tin chi ti?t
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




