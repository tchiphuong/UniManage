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
    public sealed class RemoveUserRoleCommand : BaseCommand, IRequest<ApiResponse<RemoveUserRoleCommand.Response>>
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

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh gỡ bỏ vai trò người dùng
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

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh gỡ bỏ vai trò người dùng
    /// </summary>
    public sealed class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, ApiResponse<RemoveUserRoleCommand.Response>>
    {
        public async Task<ApiResponse<RemoveUserRoleCommand.Response>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.UserUuid), request.UserUuid.ToString()),
                    new(nameof(request.RoleCode), request.RoleCode)
                }
            };

            try
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
                            var notFoundResponse = ResponseHelper.NotFound<RemoveUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                            log.Message = "User not found";
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // 2. Kiểm tra vai trò có tồn tại không
                        var role = await dbContext.Set<SyRoles>()
                            .FirstOrDefaultAsync(r => r.Code == request.RoleCode, cancellationToken);

                        if (role == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<RemoveUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
                            log.Message = "Role not found";
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
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

                        log.Result = responseData;
                        log.Message = "Remove user role success";
                        log.ReturnCode = response.ReturnCode;

                        return response;
                    }
                    catch (Exception)
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
                
                return ResponseHelper.Error<RemoveUserRoleCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}








