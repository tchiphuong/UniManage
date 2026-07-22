using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyUser.Commands
{
    #region Command

    /// <summary>
    /// Lệnh gỡ bỏ một vai trò (Role) khỏi người dùng
    /// </summary>
    public sealed class RemoveUserRoleCommand : BaseCommand, IRequest<ApiResponse<RemoveUserRoleCommand.Response>>, ILoggableCommand
    {
        /// <summary>
        /// Uuid của người dùng cần gỡ bỏ vai trò
        /// </summary>
        public Guid UserUuid { get; init; }

        /// <summary>
        /// Mã vai trò cần gỡ bỏ
        /// </summary>
        public string RoleCode { get; init; } = default!;

        public sealed class Response
        {
            public Guid UserUuid { get; init; }
            public string RoleCode { get; init; } = default!;
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh gỡ bỏ vai trò người dùng
    /// </summary>
    public sealed class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, ApiResponse<RemoveUserRoleCommand.Response>>
    {
        public async Task<ApiResponse<RemoveUserRoleCommand.Response>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    // 1. Tìm thông tin người dùng bằng EF Core qua Uuid
                    var user = await dbContext.Set<SyUsers>()
                        .FirstOrDefaultAsync(u => u.Uuid == request.UserUuid, cancellationToken);

                    if (user == null)
                    {
                        return ResponseHelper.NotFound<RemoveUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                    }

                    // 2. Kiểm tra vai trò có tồn tại không
                    var role = await dbContext.Set<SyRoles>()
                        .FirstOrDefaultAsync(r => r.Code == request.RoleCode, cancellationToken);

                    if (role == null)
                    {
                        return ResponseHelper.NotFound<RemoveUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
                    }

                    // 3. Xóa bản ghi trong SyUserRoles
                    var userRole = await dbContext.Set<SyUserRoles>()
                        .FirstOrDefaultAsync(ur => ur.Username == user.Username && ur.RoleCode == request.RoleCode, cancellationToken);

                    if (userRole != null)
                    {
                        dbContext.Set<SyUserRoles>().Remove(userRole);
                    }

                    // 4. Nếu vai trò bị xóa trùng với vai trò mặc định của user, xóa vai trò mặc định đó
                    if (user.RoleCode == request.RoleCode)
                    {
                        user.RoleCode = null;
                        user.UpdatedAt = DateTimeHelper.Now;
                        user.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                    }

                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync();

                    var responseData = new RemoveUserRoleCommand.Response { UserUuid = request.UserUuid, RoleCode = request.RoleCode };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, CoreResource.entity_role));

                    return response;
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









