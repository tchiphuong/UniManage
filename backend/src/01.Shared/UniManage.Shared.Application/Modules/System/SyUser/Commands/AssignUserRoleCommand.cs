using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyUser.Commands
{
    #region Command

    /// <summary>
    /// Lệnh gán vai trò (Role) cho người dùng
    /// </summary>
    public sealed class AssignUserRoleCommand : BaseCommand, IRequest<ApiResponse<AssignUserRoleCommand.Response>>, ILoggableCommand
    {
        /// <summary>
        /// Uuid của người dùng cần được gán vai trò
        /// </summary>
        public Guid UserUuid { get; set; }

        /// <summary>
        /// Mã vai trò cần gán
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
    /// Bộ xử lý lệnh gán vai trò người dùng
    /// </summary>
    public sealed class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, ApiResponse<AssignUserRoleCommand.Response>>
    {
        public async Task<ApiResponse<AssignUserRoleCommand.Response>> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var user = await dbContext.Set<SyUsers>()
                        .FirstOrDefaultAsync(u => u.Uuid == request.UserUuid, cancellationToken);

                    if (user == null)
                    {
                        return ResponseHelper.NotFound<AssignUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                    }

                    // Kiểm tra nếu vai trò đã được gán trước đó để tránh trùng lặp
                    var isAlreadyAssigned = await dbContext.Set<SyUserRoles>()
                        .AnyAsync(ur => ur.Username == user.Username && ur.RoleCode == request.RoleCode, cancellationToken);

                    if (isAlreadyAssigned)
                    {
                        return ResponseHelper.Error<AssignUserRoleCommand.Response>(CoreResource.validation_alreadyExists);
                    }

                    // 1. Thêm bản ghi vào bảng mapping SyUserRoles
                    dbContext.Set<SyUserRoles>().Add(new SyUserRoles
                    {
                        Username = user.Username,
                        RoleCode = request.RoleCode,
                        CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                        CreatedAt = DateTimeHelper.Now
                    });

                    // 2. Nếu người dùng chưa có vai trò mặc định, gán vai trò này làm mặc định
                    if (string.IsNullOrEmpty(user.RoleCode))
                    {
                        user.RoleCode = request.RoleCode;
                        user.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                        user.UpdatedAt = DateTimeHelper.Now;
                    }

                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync();

                    var responseData = new AssignUserRoleCommand.Response { UserUuid = request.UserUuid, RoleCode = request.RoleCode };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_role));

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










