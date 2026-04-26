using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.User
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách vai trò của một người dùng cụ thể
    /// </summary>
    public sealed class GetUserRolesQuery : BaseQuery, IRequest<ApiResponse<List<UserRoleDto>>>
    {
        /// <summary>
        /// ID của người dùng cần lấy danh sách vai trò
        /// </summary>
        public long Id { get; init; }
    }

    /// <summary>
    /// DTO chứa thông tin vai trò của người dùng
    /// </summary>
    public sealed record UserRoleDto(
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
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_required, "Id"));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách vai trò người dùng
    /// </summary>
    public sealed class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, ApiResponse<List<UserRoleDto>>>
    {
        public async Task<ApiResponse<List<UserRoleDto>>> Handle(GetUserRolesQuery request, CancellationToken ct)
        {
            // Khởi tạo log nghiệp vụ
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.Id), request.Id.ToString())
                }
            };

            try
            {
                using (var db = new DbContext())
                {
                    // Bước 1: Kiểm tra người dùng có tồn tại trong hệ thống hay không
                    var userExists = await db.ExecuteScalarAsync<bool>(
                        "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Id = @Id) THEN 1 ELSE 0 END",
                        new { request.Id },
                        cancellationToken: ct);

                    if (!userExists)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<List<UserRoleDto>>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                        log.Message = notFoundResponse.Message;
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        return notFoundResponse;
                    }

                    // Xác định cột tên vai trò theo ngôn ngữ
                    var suffix = TranslateHelper.GetLanguageSuffix(request.HeaderInfo?.Language);
                    var roleNameColumn = $"r.Name{suffix}";

                    // Bước 2: Truy vấn danh sách vai trò sử dụng Dapper để tối ưu hiệu năng
                    var roles = await db.QueryAsync<UserRoleDto>(
                        $"""
                        SELECT ur.RoleCode, {roleNameColumn} AS RoleName, ur.CreatedAt AS AssignedAt
                        FROM sy_user_roles ur
                        INNER JOIN sy_roles r ON ur.RoleCode = r.Code
                        INNER JOIN sy_users u ON ur.Username = u.Username
                        WHERE u.Id = @Id
                        ORDER BY ur.CreatedAt
                        """,
                        new { request.Id },
                        cancellationToken: ct);

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
