using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;


namespace UniManage.Shared.Application.Modules.SyUser.Commands
{
    #region Command

    /// <summary>
    /// Command to update user information
    /// </summary>
    public sealed class UpdateUserCommand : BaseCommand, IRequest<ApiResponse<UpdateUserCommand.Response>>
    {
        public Guid Uuid { get; set; }
        public string Email { get; init; } = default!;
        public string? EmployeeCode { get; init; }
        public string Status { get; init; } = default!;
        public byte[]? RowVersion { get; init; }

        public sealed class Response
        {
            public Guid Uuid { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for UpdateUserCommand
    /// </summary>
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Uuid)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.user_userIdentity))
                .MustAsync(async (uuid, cancel) => await IsUserExistsAsync(uuid))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_user));

            RuleFor(x => x.EmployeeCode)
                .MaximumLength(50)
                .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.user_employeeCode, 50));

            When(x => !string.IsNullOrEmpty(x.EmployeeCode), () =>
            {
                RuleFor(x => x.EmployeeCode)
                    .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code!))
                    .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_employee));
            });

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.user_status))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Status)
                        .Must(status => CoreCommon.Value.Commonstatus.All.Contains(status))
                        .WithMessage(CoreResource.validation_invalidStatus);
                });

            RuleFor(x => x.RowVersion)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, "RowVersion"));
        }

        private static async Task<bool> IsUserExistsAsync(Guid uuid)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<SyUsers>().AnyAsync(x => x.Uuid == uuid);
            }
        }

        private static async Task<bool> IsEmployeeExistsAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrEmployees>().AnyAsync(x => x.EmployeeCode == employeeCode);
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for UpdateUserCommand
    /// </summary>
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UpdateUserCommand.Response>>
    {
        public async Task<ApiResponse<UpdateUserCommand.Response>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var user = await dbContext.Set<SyUsers>()
                        .FirstOrDefaultAsync(u => u.Uuid == request.Uuid, cancellationToken);

                    if (user == null)
                    {
                        return ResponseHelper.NotFound<UpdateUserCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                    }

                    // Check RowVersion for optimistic concurrency
                    if (request.RowVersion != null && !user.RowVersion.SequenceEqual(request.RowVersion))
                    {
                        return ResponseHelper.Error<UpdateUserCommand.Response>(CoreResource.common_concurrencyError);
                    }

                    user.EmployeeCode = request.EmployeeCode;
                    user.Status = request.Status;
                    user.Email = request.Email;
                    user.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                    user.UpdatedAt = DateTimeHelper.Now;

                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync();

                    // Clear cache for combobox users since list has changed
                    await CacheHelper.RemoveByPatternAsync(CacheKeyConstant.System.ComboboxUsersPattern);

                    var responseData = new UpdateUserCommand.Response { Uuid = user.Uuid };
                    return ResponseHelper.Success(responseData, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_user));
                }
                catch (Exception)
                {
                    await dbContext.RollbackAsync();
                    throw;
                }
            }
        }
    }

    #endregion
}









