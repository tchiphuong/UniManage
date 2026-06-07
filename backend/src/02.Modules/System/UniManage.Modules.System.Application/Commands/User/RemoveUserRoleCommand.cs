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

namespace UniManage.Modules.System.Application.Commands.User
{
    #region Command

    /// <summary>
    /// Lá»‡nh gá»¡ bá» má»™t vai trÃ² (Role) khá»i ngÆ°á»i dÃ¹ng
    /// </summary>
    public sealed class RemoveUserRoleCommand : BaseCommand, IRequest<ApiResponse<RemoveUserRoleCommand.Response>>
    {
        /// <summary>
        /// Uuid cá»§a ngÆ°á»i dÃ¹ng cáº§n gá»¡ bá» vai trÃ²
        /// </summary>
        public Guid UserUuid { get; init; }

        /// <summary>
        /// MÃ£ vai trÃ² cáº§n gá»¡ bá»
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
    /// Bá»™ kiá»ƒm tra dá»¯ liá»‡u cho lá»‡nh gá»¡ bá» vai trÃ² ngÆ°á»i dÃ¹ng
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
    /// Bá»™ xá»­ lÃ½ lá»‡nh gá»¡ bá» vai trÃ² ngÆ°á»i dÃ¹ng
    /// </summary>
    public sealed class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, ApiResponse<RemoveUserRoleCommand.Response>>
    {
        public async Task<ApiResponse<RemoveUserRoleCommand.Response>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            // Khá»Ÿi táº¡o log nghiá»‡p vá»¥
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
                        // 1. TÃ¬m thÃ´ng tin ngÆ°á»i dÃ¹ng báº±ng EF Core qua Uuid
                        var user = await dbContext.Set<SyUsers>()
                            .FirstOrDefaultAsync(u => u.Uuid == request.UserUuid, cancellationToken);

                        if (user == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<RemoveUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                            log.Message = "User not found";
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // 2. Kiá»ƒm tra vai trÃ² cÃ³ tá»“n táº¡i khÃ´ng
                        var role = await dbContext.Set<SyRoles>()
                            .FirstOrDefaultAsync(r => r.Code == request.RoleCode, cancellationToken);

                        if (role == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<RemoveUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
                            log.Message = "Role not found";
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // 3. XÃ³a báº£n ghi trong SyUserRoles
                        var userRole = await dbContext.Set<SyUserRoles>()
                            .FirstOrDefaultAsync(ur => ur.Username == user.Username && ur.RoleCode == request.RoleCode, cancellationToken);

                        if (userRole != null)
                        {
                            dbContext.Set<SyUserRoles>().Remove(userRole);
                        }

                        // 4. Náº¿u vai trÃ² bá»‹ xÃ³a trÃ¹ng vá»›i vai trÃ² máº·c Ä‘á»‹nh cá»§a user, xÃ³a vai trÃ² máº·c Ä‘á»‹nh Ä‘Ã³
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








