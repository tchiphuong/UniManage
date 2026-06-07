using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Queries.Auth
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách quyền hạn (Permissions) và vai trò (Roles) của người dùng hiện tại
    /// </summary>
    public sealed class GetUserPermissionsQuery : BaseQuery, IRequest<ApiResponse<GetUserPermissionsQuery.Result>>
    {
        /// <summary>
        /// Kết quả trả về chứa danh sách mã quyền hạn và mã vai trò
        /// </summary>
        public class Result
        {
            public List<string> Permissions { get; set; } = new();
            public List<string> Roles { get; set; } = new();
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn lấy quyền hạn người dùng
    /// </summary>
    public sealed class GetUserPermissionsQueryValidator : AbstractValidator<GetUserPermissionsQuery>
    {
        public GetUserPermissionsQueryValidator()
        {
            RuleFor(x => x.HeaderInfo.Username)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách quyền hạn người dùng
    /// </summary>
    public sealed class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, ApiResponse<GetUserPermissionsQuery.Result>>
    {
        public async Task<ApiResponse<GetUserPermissionsQuery.Result>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var username = request.HeaderInfo.Username ?? string.Empty;
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.HeaderInfo.Username), username)
                }
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // 1. Lấy danh sách các vai trò (Roles) sử dụng EF Core LINQ
                    var roles = await dbContext.Set<SyUserRoles>()
                        .Where(ur => ur.Username == username)
                        .Select(ur => ur.RoleCode)
                        .Distinct()
                        .ToListAsync(cancellationToken);

                    // 2. Lấy danh sách các quyền hạn chi tiết (FunctionCode.ActionCode) sử dụng EF Core LINQ
                    var permissions = await dbContext.Set<SyUserRoles>()
                        .Where(ur => ur.Username == username)
                        .Join(dbContext.Set<SyRolePermissions>(),
                            ur => ur.RoleCode,
                            rp => rp.RoleCode,
                            (ur, rp) => new { rp.FunctionCode, rp.ActionCode })
                        .Where(p => p.FunctionCode != null && p.ActionCode != null)
                        .Select(p => p.FunctionCode + "." + p.ActionCode)
                        .Distinct()
                        .ToListAsync(cancellationToken);

                    var result = new GetUserPermissionsQuery.Result
                    {
                        Permissions = permissions,
                        Roles = roles
                    };

                    var response = ResponseHelper.Success(result, string.Format(CoreResource.common_listSuccess, CoreResource.entity_permission));
                    
                    log.Result = new { PermissionCount = permissions.Count, RoleCount = roles.Count };
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<GetUserPermissionsQuery.Result>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}





