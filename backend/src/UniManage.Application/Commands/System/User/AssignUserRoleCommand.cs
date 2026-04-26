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
    /// Lệnh gán vai trò (Role) cho người dùng
    /// </summary>
    public sealed class AssignUserRoleCommand : BaseCommand, IRequest<ApiResponse<AssignUserRoleCommand.Response>>
    {
        /// <summary>
        /// ID của người dùng cần được gán vai trò
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Mã vai trò cần gán
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
    /// Bộ kiểm tra dữ liệu cho lệnh gán vai trò người dùng
    /// </summary>
    public sealed class AssignUserRoleCommandValidator : AbstractValidator<AssignUserRoleCommand>
    {
        public AssignUserRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userId))
                .MustAsync(async (id, cancel) => await IsUserExistsAsync(id))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_user));

            RuleFor(x => x.RoleCode)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode))
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_roleCode, 50))
                .MustAsync(async (code, cancel) => await IsRoleExistsAsync(code))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
        }

        private static async Task<bool> IsUserExistsAsync(long userId)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<sy_users>().AnyAsync(x => x.Id == userId);
            }
        }

        private static async Task<bool> IsRoleExistsAsync(string roleCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<sy_roles>().AnyAsync(x => x.Code == roleCode);
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
        public async Task<ApiResponse<AssignUserRoleCommand.Response>> Handle(AssignUserRoleCommand request, CancellationToken ct)
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
                        var user = await dbContext.Set<sy_users>()
                            .FirstOrDefaultAsync(u => u.Id == request.UserId, ct);

                        if (user == null)
                        {
                            var notFoundResponse = ResponseHelper.NotFound<AssignUserRoleCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                            log.Message = notFoundResponse.Message;
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }

                        // Kiểm tra nếu vai trò đã được gán trước đó để tránh trùng lặp
                        var isAlreadyAssigned = await dbContext.Set<sy_user_roles>()
                            .AnyAsync(ur => ur.Username == user.Username && ur.RoleCode == request.RoleCode, ct);

                        if (isAlreadyAssigned)
                        {
                            var errorResponse = ResponseHelper.Error<AssignUserRoleCommand.Response>(CoreResource.validation_alreadyExists);
                            log.Message = errorResponse.Message;
                            log.ReturnCode = errorResponse.ReturnCode;
                            return errorResponse;
                        }

                        // 1. Thêm bản ghi vào bảng mapping sy_user_roles
                        dbContext.Set<sy_user_roles>().Add(new sy_user_roles
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

                        await dbContext.SaveChangesAsync(ct);
                        await dbContext.CommitAsync();

                        var responseData = new AssignUserRoleCommand.Response { UserId = request.UserId, RoleCode = request.RoleCode };
                        var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_role));

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
