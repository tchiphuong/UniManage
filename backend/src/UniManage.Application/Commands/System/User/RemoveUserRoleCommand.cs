using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.System.User
{
    #region Command

    /// <summary>
    /// Lệnh gỡ bỏ một vai trò (Role) khỏi người dùng
    /// </summary>
    public sealed class RemoveUserRoleCommand : BaseCommand, IRequest<ApiResponse<RemoveUserRoleCommand.Response>>
    {
        /// <summary>
        /// ID của người dùng cần gỡ bỏ vai trò
        /// </summary>
        public long UserId { get; init; }

        /// <summary>
        /// Mã vai trò cần gỡ bỏ
        /// </summary>
        public string RoleCode { get; init; } = default!;

        public sealed class Response
        {
            public long UserId { get; init; }
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
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username));

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
        public async Task<ApiResponse<RemoveUserRoleCommand.Response>> Handle(RemoveUserRoleCommand request, CancellationToken ct)
        {
            // Khởi tạo log nghiệp vụ
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.UserId), request.UserId),
                    new(nameof(request.RoleCode), request.RoleCode)
                }
            };

            try
            {
                using (var dbContext = new DbContext(openTransaction: true))
                {
                    try
                    {
                        // 1. Tìm thông tin người dùng bằng EF Core
                        var user = await dbContext.Set<sy_users>()
                            .FirstOrDefaultAsync(u => u.Id == request.UserId, ct);

                        if (user == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<RemoveUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                            log.Message = "User not found";
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // 2. Kiểm tra vai trò có tồn tại không
                        var role = await dbContext.Set<sy_roles>()
                            .FirstOrDefaultAsync(r => r.Code == request.RoleCode, ct);

                        if (role == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<RemoveUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
                            log.Message = "Role not found";
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // 3. Xóa bản ghi trong sy_user_roles
                        var userRole = await dbContext.Set<sy_user_roles>()
                            .FirstOrDefaultAsync(ur => ur.Username == user.Username && ur.RoleCode == request.RoleCode, ct);

                        if (userRole != null)
                        {
                            dbContext.Set<sy_user_roles>().Remove(userRole);
                        }

                        // 4. Nếu vai trò bị xóa trùng với vai trò mặc định của user, xóa vai trò mặc định đó
                        if (user.RoleCode == request.RoleCode)
                        {
                            user.RoleCode = null;
                            user.UpdatedAt = DateTimeHelper.Now;
                            user.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                        }

                        await dbContext.SaveChangesAsync(ct);
                        await dbContext.CommitAsync();

                        var responseData = new RemoveUserRoleCommand.Response { UserId = request.UserId, RoleCode = request.RoleCode };
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
