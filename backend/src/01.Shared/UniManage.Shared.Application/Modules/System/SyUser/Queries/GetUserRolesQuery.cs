
namespace UniManage.Shared.Application.Modules.SyUser.Queries
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách vai trò của một người dùng cụ thể
    /// </summary>
    public sealed class GetUserRolesQuery : BaseQuery, IRequest<ApiResponse<List<SyUserRoleModel>>>
    {
        /// <summary>
        /// Uuid của người dùng cần lấy danh sách vai trò
        /// </summary>
        public Guid Uuid { get; init; }
    }

    /// <summary>
    /// DTO chứa thông tin vai trò của người dùng
    /// </summary>
    public sealed record SyUserRoleModel(
        string RoleCode,
        string RoleName,
        DateTime AssignedAt);

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn lấy vai trò người dùng
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
    /// Bộ xử lý truy vấn lấy danh sách vai trò người dùng
    /// </summary>
    public sealed class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, ApiResponse<List<SyUserRoleModel>>>
    {
        public async Task<ApiResponse<List<SyUserRoleModel>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ
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
                    // Bước 1: Kiểm tra người dùng có tồn tại trong hệ thống hay không
                    var userExists = await db.ExecuteScalarAsync<bool>(
                        "SELECT CASE WHEN EXISTS(SELECT 1 FROM SyUsers WHERE Uuid = @Uuid) THEN 1 ELSE 0 END",
                        new { request.Uuid },
                        cancellationToken: cancellationToken);

                    if (!userExists)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<List<SyUserRoleModel>>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                        log.Message = notFoundResponse.Message;
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        return notFoundResponse;
                    }

                    // Xác định cột tên vai trò theo ngôn ngữ
                    var suffix = TranslateHelper.GetLanguageSuffix(request.HeaderInfo?.Language);
                    var roleNameColumn = $"r.Name{suffix}";

                    // Bước 2: Truy vấn danh sách vai trò sử dụng Dapper để tối ưu hiệu năng
                    var roles = await db.QueryAsync<SyUserRoleModel>(
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
                // Ghi nhận lỗi ngoại lệ vào hệ thống log
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<List<SyUserRoleModel>>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}












