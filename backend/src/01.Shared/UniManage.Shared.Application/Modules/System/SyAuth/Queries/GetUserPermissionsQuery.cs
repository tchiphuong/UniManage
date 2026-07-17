using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Queries
{
    #region Query

    /// <summary>
    /// Truy v?n l?y danh s�ch quy?n h?n (Permissions) v� vai tr� (Roles) c?a ngu?i d�ng hi?n t?i
    /// </summary>
    public sealed class GetUserPermissionsQuery : BaseQuery, IRequest<ApiResponse<GetUserPermissionsQuery.Result>>
    {
        /// <summary>
        /// K?t qu? tr? v? ch?a danh s�ch m� quy?n h?n v� m� vai tr�
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
    /// B? ki?m tra d? li?u cho truy v?n l?y quy?n h?n ngu?i d�ng
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
    /// B? x? l� truy v?n l?y danh s�ch quy?n h?n ngu?i d�ng
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
                    // 1. L?y danh s�ch c�c vai tr� (Roles) s? d?ng EF Core LINQ
                    var roles = await dbContext.Set<SyUserRoles>()
                        .Where(ur => ur.Username == username)
                        .Select(ur => ur.RoleCode)
                        .Distinct()
                        .ToListAsync(cancellationToken);

                    // 2. L?y danh s�ch c�c quy?n h?n chi ti?t (FunctionCode.ActionCode) s? d?ng EF Core LINQ
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





