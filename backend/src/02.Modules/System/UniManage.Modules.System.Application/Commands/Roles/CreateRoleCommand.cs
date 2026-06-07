using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Commands.Roles
{
    #region Command

    /// <summary>
    /// Lệnh tạo mới vai trò người dùng (Role)
    /// </summary>
    public sealed class CreateRoleCommand : BaseCommand, IRequest<ApiResponse<CreateRoleCommand.Response>>
    {
        public string RoleCode { get; init; } = default!;
        public string RoleName { get; init; } = default!;
        public string? Description { get; init; }
        public byte IsActive { get; init; } = 1;

        public sealed class Response
        {
            public long Id { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh tạo vai trò
    /// </summary>
    public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleCode)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode))
                .DependentRules(() =>
                {
                    RuleFor(x => x.RoleCode)
                        .Length(2, 50)
                        .WithMessage(string.Format(CoreResource.validation_length, CoreResource.lbl_roleCode, 2, 50))
                        .Must(ValidationHelper.IsValidUserCode)
                        .WithMessage(CoreResource.validation_alphanumericOnly)
                        .MustAsync(async (code, cancel) => !await IsRoleCodeExistsAsync(code))
                        .WithMessage(CoreResource.validation_alreadyExists);
                });

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleName))
                .DependentRules(() =>
                {
                    RuleFor(x => x.RoleName)
                        .Length(2, 200)
                        .WithMessage(string.Format(CoreResource.validation_length, CoreResource.lbl_roleName, 2, 200));
                });

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.common_description, 500));

            RuleFor(x => x.IsActive)
                .InclusiveBetween((byte)0, (byte)1)
                .WithMessage(CoreResource.validation_invalidStatus);
        }

        private static async Task<bool> IsRoleCodeExistsAsync(string roleCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<SyRoles>().AnyAsync(x => x.Code == roleCode);
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh tạo mới vai trò
    /// </summary>
    public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApiResponse<CreateRoleCommand.Response>>
    {
        public async Task<ApiResponse<CreateRoleCommand.Response>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.RoleCode), request.RoleCode),
                    new(nameof(request.RoleName), request.RoleName),
                    new(nameof(request.IsActive), request.IsActive.ToString())
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var role = new SyRoles
                {
                    Code = request.RoleCode,
                    NameVi = request.RoleName,
                    NameEn = request.RoleName,
                    Status = request.IsActive == 1 ? CoreCommon.Value.Commonstatus.Active : CoreCommon.Value.Commonstatus.Inactive,
                    CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                    CreatedAt = DateTimeHelper.Now,
                    UpdatedAt = DateTimeHelper.Now,
                    UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser
                };

                dbContext.Set<SyRoles>().Add(role);
                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                var responseData = new CreateRoleCommand.Response { Id = role.Id };
                var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_role));

                log.Result = responseData;
                log.Message = "Create role success";
                log.ReturnCode = response.ReturnCode;

                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<CreateRoleCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




