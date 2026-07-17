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
    public sealed class AssignUserRoleCommand : BaseCommand, IRequest<ApiResponse<AssignUserRoleCommand.Response>>
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

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh gán vai trò người dùng
    /// </summary>
    public sealed class AssignUserRoleCommandValidator : AbstractValidator<AssignUserRoleCommand>
    {
        public AssignUserRoleCommandValidator()
        {
            RuleFor(x => x.UserUuid)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userId))
                .MustAsync(async (uuid, cancel) => await IsUserExistsAsync(uuid))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_user));

            RuleFor(x => x.RoleCode)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode))
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_roleCode, 50))
                .MustAsync(async (code, cancel) => await IsRoleExistsAsync(code))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
        }

        private static async Task<bool> IsUserExistsAsync(Guid uuid)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<SyUsers>().AnyAsync(x => x.Uuid == uuid);
            }
        }

        private static async Task<bool> IsRoleExistsAsync(string roleCode)
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
    /// Bộ xử lý lệnh gán vai trò người dùng
    /// </summary>
    public sealed class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, ApiResponse<AssignUserRoleCommand.Response>>
    {
        public async Task<ApiResponse<AssignUserRoleCommand.Response>> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
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
                        var user = await dbContext.Set<SyUsers>()
                            .FirstOrDefaultAsync(u => u.Uuid == request.UserUuid, cancellationToken);

                        if (user == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<AssignUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                            log.Message = notFoundResponse.Message;
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // Kiểm tra nếu vai trò đã được gán trước đó để tránh trùng lặp
                        var isAlreadyAssigned = await dbContext.Set<SyUserRoles>()
                            .AnyAsync(ur => ur.Username == user.Username && ur.RoleCode == request.RoleCode, cancellationToken);

                        if (isAlreadyAssigned)
                        {
                            var errorResponse = ResponseHelper.Error<AssignUserRoleCommand.Response>(CoreResource.validation_alreadyExists);
                            log.Message = errorResponse.Message;
                            log.ReturnCode = errorResponse.ReturnCode;
                            return errorResponse;
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

                        log.Result = responseData;
                        log.Message = response.Message;
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
                
                return ResponseHelper.Error<AssignUserRoleCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}









