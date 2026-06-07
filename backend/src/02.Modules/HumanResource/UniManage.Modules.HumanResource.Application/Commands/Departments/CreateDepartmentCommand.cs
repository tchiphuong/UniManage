using FluentValidation;
using MediatR;
using UniManage.Modules.HumanResource.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Commands.Departments
{
    #region Command

    /// <summary>
    /// Command to create a new department
    /// </summary>
    public sealed class CreateDepartmentCommand : BaseCommand, IRequest<ApiResponse<CreateDepartmentCommand.Response>>
    {
        public string Code { get; init; } = default!;
        public string NameVi { get; init; } = default!;
        public string NameEn { get; init; } = default!;
        public string? Description { get; init; }

        public sealed class Response
        {
            public bool Success => Id > 0;
            public int Id { get; init; }
            public string Code { get; init; } = default!;
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for CreateDepartmentCommand
    /// </summary>
    public sealed class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, 50))
                .Matches("^[A-Z0-9]+$").WithMessage(CoreResource.validation_onlyNumbers)
                .MustAsync(async (code, cancel) => !await IsCodeExistsAsync(code))
                .WithMessage(CoreResource.validation_alreadyExists);

            RuleFor(x => x.NameVi)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, 200));

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, 200));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage(string.Format(CoreResource.validation_maxLength, 500));
        }

        private static async Task<bool> IsCodeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM HrDepartments WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for CreateDepartmentCommand
    /// </summary>
    public sealed class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ApiResponse<CreateDepartmentCommand.Response>>
    {
        public async Task<ApiResponse<CreateDepartmentCommand.Response>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
            {
                new(nameof(request.Code), request.Code),
                new(nameof(request.NameVi), request.NameVi)
            }
            };

            try
            {
                using (var dbContext = new UniManage.Shared.Infrastructure.Database.DbContext(openTransaction: true))
                {
                    var department = new HrDepartments
                    {
                        Code = request.Code,
                        NameVi = request.NameVi,
                        NameEn = request.NameEn,
                        Description = request.Description ?? string.Empty,
                        CreatedBy = request.HeaderInfo?.Username ?? "SYSTEM",
                        CreatedAt = DateTimeHelper.Now,
                        DataRowVersion = new byte[8]
                    };

                    dbContext.Set<HrDepartments>().Add(department);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync(cancellationToken);

                    var responseData = new CreateDepartmentCommand.Response { Id = department.Id, Code = department.Code };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_department));

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
                return ResponseHelper.Error<CreateDepartmentCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
