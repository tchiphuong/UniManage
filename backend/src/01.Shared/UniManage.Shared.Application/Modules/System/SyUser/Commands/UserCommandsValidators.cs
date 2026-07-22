using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyUser.Commands
{
    /// <summary>
    /// Base helper class for user validators to share common database check logic
    /// </summary>
    public static class UserValidatorHelper
    {
        public static async Task<bool> IsUsernameExistsAsync(string username)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<SyUsers>().AnyAsync(x => x.Username == username);
            }
        }

        public static async Task<bool> IsUserExistsAsync(Guid uuid)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<SyUsers>().AnyAsync(x => x.Uuid == uuid);
            }
        }

        public static async Task<bool> IsEmployeeExistsAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrEmployees>().AnyAsync(x => x.EmployeeCode == employeeCode);
            }
        }

        public static async Task<bool> AreRolesValidAsync(List<string> roleCodes)
        {
            if (roleCodes == null || !roleCodes.Any()) return false;
            using (var dbContext = new DbContext())
            {
                var count = await dbContext.Set<SyRoles>().CountAsync(x => roleCodes.Contains(x.Code));
                return count == roleCodes.Distinct().Count();
            }
        }

        public static async Task<bool> IsRoleExistsAsync(string roleCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<SyRoles>().AnyAsync(x => x.Code == roleCode);
            }
        }
    }

    /// <summary>
    /// Validator for CreateUserCommand
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
                        .MustAsync(async (username, cancel) => !await UserValidatorHelper.IsUsernameExistsAsync(username))
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
                .MustAsync(async (roles, cancel) => await UserValidatorHelper.AreRolesValidAsync(roles))
                .WithMessage(CoreResource.validation_invalidRole);

            When(x => !string.IsNullOrEmpty(x.EmployeeCode), () =>
            {
                RuleFor(x => x.EmployeeCode)
                    .MustAsync(async (code, cancel) => await UserValidatorHelper.IsEmployeeExistsAsync(code!))
                    .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_employee));
            });
        }
    }

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
                .MustAsync(async (uuid, cancel) => await UserValidatorHelper.IsUserExistsAsync(uuid))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_user));

            RuleFor(x => x.EmployeeCode)
                .MaximumLength(50)
                .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.user_employeeCode, 50));

            When(x => !string.IsNullOrEmpty(x.EmployeeCode), () =>
            {
                RuleFor(x => x.EmployeeCode)
                    .MustAsync(async (code, cancel) => await UserValidatorHelper.IsEmployeeExistsAsync(code!))
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
    }

    /// <summary>
    /// Validator for DeleteUserCommand
    /// </summary>
    public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Uuids)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userIdentity))
                .Must(uuids => uuids != null && uuids.All(uuid => uuid != Guid.Empty)).WithMessage(CoreResource.validation_invalidId);
        }
    }

    /// <summary>
    /// Validator for AssignUserRoleCommand
    /// </summary>
    public sealed class AssignUserRoleCommandValidator : AbstractValidator<AssignUserRoleCommand>
    {
        public AssignUserRoleCommandValidator()
        {
            RuleFor(x => x.UserUuid)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userId))
                .MustAsync(async (uuid, cancel) => await UserValidatorHelper.IsUserExistsAsync(uuid))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_user));

            RuleFor(x => x.RoleCode)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode))
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_roleCode, 50))
                .MustAsync(async (code, cancel) => await UserValidatorHelper.IsRoleExistsAsync(code))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
        }
    }

    /// <summary>
    /// Validator for RemoveUserRoleCommand
    /// </summary>
    public sealed class RemoveUserRoleCommandValidator : AbstractValidator<RemoveUserRoleCommand>
    {
        public RemoveUserRoleCommandValidator()
        {
            RuleFor(x => x.UserUuid)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username));

            RuleFor(x => x.RoleCode)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode));
        }
    }

    /// <summary>
    /// Validator for ChangePasswordCommand
    /// </summary>
    public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.auth_oldPassword));

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.auth_newPassword))
                .DependentRules(() =>
                {
                    RuleFor(x => x.NewPassword)
                        .MinimumLength(PasswordHelper.MinPasswordLength).WithMessage(string.Format(CoreResource.validation_minLength, CoreResource.auth_newPassword, PasswordHelper.MinPasswordLength))
                        .MaximumLength(PasswordHelper.MaxPasswordLength).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.auth_newPassword, PasswordHelper.MaxPasswordLength))
                        .Must(PasswordHelper.IsValidPassword).WithMessage(CoreResource.validation_passwordComplexity)
                        .NotEqual(x => x.OldPassword).WithMessage(CoreResource.validation_newPasswordDifferent);
                });

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.auth_confirmPassword))
                .Equal(x => x.NewPassword).WithMessage(CoreResource.validation_confirmPasswordMismatch);
        }
    }
}
