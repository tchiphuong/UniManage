using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using UniManage.Application.Utilities;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.System.User
{
    #region Command

    /// <summary>
    /// Lệnh tạo mới người dùng trong hệ thống
    /// </summary>
    public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<CreateUserCommand.Response>>
    {
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string? EmployeeCode { get; init; }
        public string Password { get; init; } = default!;
        public string Status { get; init; } = default!;
        public List<string> RoleCode { get; init; } = default!;

        public sealed class Response
        {
            public bool Success => Id > 0;
            public long Id { get; init; } = default!;
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh tạo mới người dùng
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

            RuleFor(x => x.RoleCode)
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
                return await dbContext.Set<sy_users>().AnyAsync(x => x.Username == username);
            }
        }

        private static async Task<bool> IsEmployeeExistsAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<hr_employees>().AnyAsync(x => x.EmployeeCode == employeeCode);
            }
        }

        private static async Task<bool> AreRolesValidAsync(List<string> roleCodes)
        {
            if (roleCodes == null || !roleCodes.Any()) return false;
            using (var dbContext = new DbContext())
            {
                var count = await dbContext.Set<sy_roles>().CountAsync(x => roleCodes.Contains(x.Code));
                return count == roleCodes.Distinct().Count();
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh tạo mới người dùng
    /// </summary>
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
    {
        public async Task<ApiResponse<CreateUserCommand.Response>> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.Username), request.Username),
                    new(nameof(request.Email), request.Email),
                    new(nameof(request.EmployeeCode), request.EmployeeCode),
                    new(nameof(request.Status), request.Status),
                    new(nameof(request.RoleCode), string.Join(",", request.RoleCode))
                }
            };

            try
            {
                using (var dbContext = new DbContext(openTransaction: true))
                {
                    try
                    {
                        var passwordHash = PasswordHelper.HashPassword(request.Password);

                        var newUser = new sy_users
                        {
                            Username = request.Username,
                            Password = passwordHash,
                            Email = request.Email,
                            EmployeeCode = request.EmployeeCode,
                            RoleCode = request.RoleCode?.FirstOrDefault(),
                            Status = request.Status,
                            CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                            CreatedAt = DateTimeHelper.Now
                        };

                        dbContext.Set<sy_users>().Add(newUser);
                        await dbContext.SaveChangesAsync(ct);

                        // Gán danh sách vai trò cho người dùng
                        if (request.RoleCode != null)
                        {
                            foreach (var role in request.RoleCode)
                            {
                                dbContext.Set<sy_user_roles>().Add(new sy_user_roles
                                {
                                    Username = newUser.Username,
                                    RoleCode = role,
                                    CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                                    CreatedAt = DateTimeHelper.Now
                                });
                            }
                            await dbContext.SaveChangesAsync(ct);
                        }

                        await dbContext.CommitAsync();

                        var responseData = new CreateUserCommand.Response { Id = newUser.Id };
                        var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_user));

                        log.Result = responseData;
                        log.Message = response.Message;
                        log.ReturnCode = response.ReturnCode;

                        return response;
                    }
                    catch (Exception ex)
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
                
                return ResponseHelper.Error<CreateUserCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}