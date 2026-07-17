using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyRole.Commands
{
    #region Command

    /// <summary>
    /// Lệnh cập nhật danh sách quyền hạn cho một vai trò
    /// </summary>
    public sealed class UpdateRolePermissionsCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Mã vai trò cần cập nhật quyền
        /// </summary>
        public string RoleCode { get; set; } = default!;

        /// <summary>
        /// Danh sách các quyền (chức năng x hành động) được gán
        /// </summary>
        public List<RolePermissionInputModel> Permissions { get; set; } = new();

        /// <summary>
        /// DTO đầu vào của từng quyền hạn được gán
        /// </summary>
        public sealed class RolePermissionInputModel
        {
            /// <summary>
            /// Mã chức năng (ví dụ: SY_USER)
            /// </summary>
            public string FunctionCode { get; set; } = default!;

            /// <summary>
            /// Mã hành động (ví dụ: VIEW, CREATE, UPDATE, DELETE)
            /// </summary>
            public string ActionCode { get; set; } = default!;
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh cập nhật quyền vai trò
    /// </summary>
    public sealed class UpdateRolePermissionsCommandValidator : AbstractValidator<UpdateRolePermissionsCommand>
    {
        /// <summary>
        /// Khởi tạo validator kiểm tra tính hợp lệ của tham số đầu vào
        /// </summary>
        public UpdateRolePermissionsCommandValidator()
        {
            RuleFor(x => x.RoleCode)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode));

            RuleFor(x => x.Permissions)
                .NotNull()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.entity_permission));

            RuleForEach(x => x.Permissions).ChildRules(p =>
            {
                p.RuleFor(x => x.FunctionCode)
                    .NotEmpty()
                    .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_code));

                p.RuleFor(x => x.ActionCode)
                    .NotEmpty()
                    .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_code));
            });
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh cập nhật quyền cho vai trò
    /// </summary>
    public sealed class UpdateRolePermissionsCommandHandler : IRequestHandler<UpdateRolePermissionsCommand, ApiResponse<bool>>
    {
        /// <summary>
        /// Xử lý cập nhật quyền hạn trong Database sử dụng EF Core và Transaction
        /// </summary>
        public async Task<ApiResponse<bool>> Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.RoleCode), request.RoleCode),
                    new("PermissionsCount", request.Permissions.Count.ToString())
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    // 1. Kiểm tra sự tồn tại của RoleCode trong database
                    var roleExists = await dbContext.Set<SyRoles>().AnyAsync(x => x.Code == request.RoleCode, cancellationToken);
                    if (!roleExists)
                    {
                        var notFoundRes = ResponseHelper.NotFound<bool>(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
                        log.ReturnCode = notFoundRes.ReturnCode;
                        log.Message = notFoundRes.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundRes;
                    }

                    // 2. Xóa các quyền cũ của vai trò này
                    var oldPermissions = await dbContext.Set<SyRolePermissions>()
                        .Where(x => x.RoleCode == request.RoleCode)
                        .ToListAsync(cancellationToken);

                    if (oldPermissions.Any())
                    {
                        dbContext.Set<SyRolePermissions>().RemoveRange(oldPermissions);
                    }

                    // 3. Thêm danh sách quyền mới được gán
                    var username = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                    var now = DateTimeHelper.Now;

                    var newPermissions = request.Permissions.Select(p => new SyRolePermissions
                    {
                        RoleCode = request.RoleCode,
                        FunctionCode = p.FunctionCode,
                        ActionCode = p.ActionCode,
                        CreatedBy = username,
                        CreatedAt = now,
                        UpdatedBy = username,
                        UpdatedAt = now
                    }).ToList();

                    if (newPermissions.Any())
                    {
                        await dbContext.Set<SyRolePermissions>().AddRangeAsync(newPermissions, cancellationToken);
                    }

                    // 4. Lưu thay đổi và Commit Transaction
                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync();

                    var response = ResponseHelper.Success(true, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_permission));

                    log.Result = true;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();

                    UniLogger.Error($"Lỗi khi cập nhật danh sách quyền cho vai trò {request.RoleCode}: {ex.Message}", ex);

                    var response = ResponseHelper.Error<bool>(CoreResource.common_error);

                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }

    #endregion
}
