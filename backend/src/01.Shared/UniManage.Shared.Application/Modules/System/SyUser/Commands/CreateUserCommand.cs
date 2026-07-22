using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Database.Repositories;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyUser.Commands
{
    #region Command

    /// <summary>
    /// L?nh t?o m?i ngu?i d�ng trong h? th?ng
    /// </summary>
    public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<CreateUserCommand.Response>>
    {
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string? EmployeeCode { get; init; }
        public string Password { get; init; } = default!;
        public string Status { get; init; } = default!;
        public List<string> RoleCodes { get; init; } = default!;

        public sealed class Response
        {
            public bool Success => Id > 0;
            public long Id { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// B? ki?m tra d? li?u cho l?nh t?o m?i ngu?i d�ng
    /// </summary>
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Username)
                        .Length(3, 50)
                        .WithMessage(string.Format(CoreResource.validation_length, CoreResource.lbl_username, 3, 50))
                        .Must(ValidationHelper.IsValidUserCode)
                        .WithMessage(CoreResource.validation_alphanumericOnly)
                        .MustAsync(async (username, cancel) => !await IsUsernameExistsAsync(username))
                        .WithMessage(CoreResource.validation_alreadyExists);
                });

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_password))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Password)
                        .MinimumLength(PasswordHelper.MinPasswordLength).WithMessage(string.Format(CoreResource.validation_minLength, CoreResource.lbl_password, PasswordHelper.MinPasswordLength))
                        .Must(PasswordHelper.IsValidPassword).WithMessage(CoreResource.validation_passwordComplexity);
                });

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_status))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Status)
                        .Must(status => CoreCommon.Value.Commonstatus.All.Contains(status))
                        .WithMessage(CoreResource.validation_invalidStatus);
                });

            RuleFor(x => x.RoleCodes)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode))
                .MustAsync(async (roles, cancel) => await AreRolesValidAsync(roles))
                .WithMessage(CoreResource.validation_invalidRole);

            When(x => !string.IsNullOrEmpty(x.EmployeeCode), () =>
            {
                RuleFor(x => x.EmployeeCode)
                    .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code!))
                    .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_employee));
            });
        }

        private static async Task<bool> IsUsernameExistsAsync(string username)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<SyUsers>().AnyAsync(x => x.Username == username);
            }
        }

        private static async Task<bool> IsEmployeeExistsAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrEmployees>().AnyAsync(x => x.EmployeeCode == employeeCode);
            }
        }

        private static async Task<bool> AreRolesValidAsync(List<string> roleCodes)
        {
            if (roleCodes == null || !roleCodes.Any()) return false;
            using (var dbContext = new DbContext())
            {
                var count = await dbContext.Set<SyRoles>().CountAsync(x => roleCodes.Contains(x.Code));
                return count == roleCodes.Distinct().Count();
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for CreateUserCommand
    /// </summary>
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
    {
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ApiResponse<CreateUserCommand.Response>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var passwordHash = PasswordHelper.HashPassword(request.Password);

                    var newUser = SyUsers.Create(
                        username: request.Username,
                        email: request.Email,
                        passwordHash: passwordHash,
                        status: request.Status,
                        createdBy: request.HeaderInfo?.Username ?? CoreConstant.SystemUser
                    );
                    newUser.EmployeeCode = request.EmployeeCode;
                    newUser.RoleCode = request.RoleCodes?.FirstOrDefault();

                    dbContext.Set<SyUsers>().Add(newUser);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    // Assign roles to user
                    if (request.RoleCodes != null)
                    {
                        foreach (var role in request.RoleCodes)
                        {
                            dbContext.Set<SyUserRoles>().Add(new SyUserRoles
                            {
                                Username = newUser.Username,
                                RoleCode = role,
                                CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                                CreatedAt = DateTimeHelper.Now
                            });
                        }
                        await dbContext.SaveChangesAsync(cancellationToken);
                    }

                    await dbContext.CommitAsync();

                    // Clear cache for combobox users since list has changed
                    await CacheHelper.RemoveByPatternAsync(CacheKeyConstant.System.ComboboxUsersPattern);

                    var responseData = new CreateUserCommand.Response { Id = newUser.Id };
                    return ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_user));
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

