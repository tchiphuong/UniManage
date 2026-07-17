namespace UniManage.Shared.Application.Modules.HrDepartment.Commands
{
    #region Command

    /// <summary>
    /// L?nh c?p nh?t th�ng tin ph�ng ban
    /// </summary>
    public sealed class UpdateDepartmentCommand : BaseCommand, IRequest<ApiResponse<UpdateDepartmentCommand.Response>>
    {
        public int Id { get; set; }
        public string Code { get; init; } = default!;
        public string NameVi { get; init; } = default!;
        public string NameEn { get; init; } = default!;
        public string? Description { get; init; }
        public byte[]? RowVersion { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
            public int Id { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// B? ki?m tra d? li?u cho l?nh c?p nh?t ph�ng ban
    /// </summary>
    public sealed class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_required, "Id"))
                .MustAsync(async (id, cancel) => await IsDepartmentExistsByIdAsync(id))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_department));

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_code))
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_code, 50));

            RuleFor(x => x.NameVi)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name))
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_name, 200));

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name))
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_name, 200));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.common_description, 500));
        }

        private static async Task<bool> IsDepartmentExistsByIdAsync(int id)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrDepartments>().AnyAsync(x => x.Id == id);
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l� l?nh c?p nh?t ph�ng ban
    /// </summary>
    public sealed class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ApiResponse<UpdateDepartmentCommand.Response>>
    {
        public async Task<ApiResponse<UpdateDepartmentCommand.Response>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Id), request.Id),
                    new(nameof(request.Code), request.Code),
                    new(nameof(request.NameVi), request.NameVi),
                    new(nameof(request.NameEn), request.NameEn),
                    new(nameof(request.RowVersion), request.RowVersion != null ? Convert.ToBase64String(request.RowVersion) : string.Empty)
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var department = await dbContext.Set<HrDepartments>()
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (department == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<UpdateDepartmentCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_department));
                    log.Message = notFoundResponse.Message;
                    log.ReturnCode = notFoundResponse.ReturnCode;
                    return notFoundResponse;
                }

                // Ki?m tra xung d?t d? li?u (Concurrency)
                if (request.RowVersion != null && !department.DataRowVersion.SequenceEqual(request.RowVersion))
                {
                    var concurrencyResponse = ResponseHelper.Error<UpdateDepartmentCommand.Response>(CoreResource.common_concurrencyError);
                    log.Message = concurrencyResponse.Message;
                    log.ReturnCode = concurrencyResponse.ReturnCode;
                    return concurrencyResponse;
                }

                department.Code = request.Code;
                department.NameVi = request.NameVi;
                department.NameEn = request.NameEn;
                department.Description = request.Description ?? string.Empty;
                department.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                department.UpdatedAt = DateTimeHelper.Now;

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                var responseData = new UpdateDepartmentCommand.Response { Success = true, Id = request.Id };
                var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_department));
                
                log.Result = responseData;
                log.Message = response.Message;
                log.ReturnCode = response.ReturnCode;

                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<UpdateDepartmentCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}



