using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.System.Application.Queries.User
{
    #region Query

    /// <summary>
    /// Truy váº¥n láº¥y danh sÃ¡ch vai trÃ² cá»§a má»™t ngÆ°á»i dÃ¹ng cá»¥ thá»ƒ
    /// </summary>
    public sealed class GetUserRolesQuery : BaseQuery, IRequest<ApiResponse<List<UserRoleDto>>>
    {
        /// <summary>
        /// Uuid cá»§a ngÆ°á»i dÃ¹ng cáº§n láº¥y danh sÃ¡ch vai trÃ²
        /// </summary>
        public Guid Uuid { get; init; }
    }

    /// <summary>
    /// DTO chá»©a thÃ´ng tin vai trÃ² cá»§a ngÆ°á»i dÃ¹ng
    /// </summary>
    public sealed record UserRoleDto(
        string RoleCode,
        string RoleName,
        DateTime AssignedAt);

    #endregion

    #region Validator

    /// <summary>
    /// Bá»™ kiá»ƒm tra dá»¯ liá»‡u cho truy váº¥n láº¥y vai trÃ² ngÆ°á»i dÃ¹ng
    /// </summary>
    public sealed class GetUserRolesQueryValidator : AbstractValidator<GetUserRolesQuery>
    {
        public GetUserRolesQueryValidator()
        {
            RuleFor(x => x.Uuid)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "Uuid"));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bá»™ xá»­ lÃ½ truy váº¥n láº¥y danh sÃ¡ch vai trÃ² ngÆ°á»i dÃ¹ng
    /// </summary>
    public sealed class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, ApiResponse<List<UserRoleDto>>>
    {
        public async Task<ApiResponse<List<UserRoleDto>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            // Khá»Ÿi táº¡o log nghiá»‡p vá»¥
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Uuid), request.Uuid.ToString())
                }
            };

            try
            {
                using (var db = new DbContext())
                {
                    // BÆ°á»›c 1: Kiá»ƒm tra ngÆ°á»i dÃ¹ng cÃ³ tá»“n táº¡i trong há»‡ thá»‘ng hay khÃ´ng
                    var userExists = await db.ExecuteScalarAsync<bool>(
                        "SELECT CASE WHEN EXISTS(SELECT 1 FROM SyUsers WHERE Uuid = @Uuid) THEN 1 ELSE 0 END",
                        new { request.Uuid },
                        cancellationToken: cancellationToken);

                    if (!userExists)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<List<UserRoleDto>>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                        log.Message = notFoundResponse.Message;
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        return notFoundResponse;
                    }

                    // XÃ¡c Ä‘á»‹nh cá»™t tÃªn vai trÃ² theo ngÃ´n ngá»¯
                    var suffix = TranslateHelper.GetLanguageSuffix(request.HeaderInfo?.Language);
                    var roleNameColumn = $"r.Name{suffix}";

                    // BÆ°á»›c 2: Truy váº¥n danh sÃ¡ch vai trÃ² sá»­ dá»¥ng Dapper Ä‘á»ƒ tá»‘i Æ°u hiá»‡u nÄƒng
                    var roles = await db.QueryAsync<UserRoleDto>(
                        $"""
                        SELECT ur.RoleCode, {roleNameColumn} AS RoleName, ur.CreatedAt AS AssignedAt
                        FROM SyUserRoles ur
                        INNER JOIN SyRoles r ON ur.RoleCode = r.Code
                        INNER JOIN SyUsers u ON ur.Username = u.Username
                        WHERE u.Uuid = @Uuid
                        ORDER BY ur.CreatedAt
                        """,
                        new { request.Uuid },
                        cancellationToken: cancellationToken);

                    var result = roles.ToList();
                    var response = ResponseHelper.Success(result, string.Format(CoreResource.common_listSuccess, CoreResource.entity_role));
                    
                    log.Result = new { RoleCount = result.Count };
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;

                    return response;
                }
            }
            catch (Exception ex)
            {
                // Ghi nháº­n lá»—i ngoáº¡i lá»‡ vÃ o há»‡ thá»‘ng log
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<List<UserRoleDto>>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}












