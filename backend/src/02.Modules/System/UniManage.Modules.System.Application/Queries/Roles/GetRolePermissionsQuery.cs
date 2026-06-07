using FluentValidation;
using MediatR;
using System.Globalization;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Services;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.System.Application.Queries.Roles
{
    #region Query

    /// <summary>
    /// Truy vấn lấy ma trận quyền hạn (Role-Permission Matrix) của một vai trò
    /// </summary>
    public sealed class GetRolePermissionsQuery : BaseQuery, IRequest<ApiResponse<List<GetRolePermissionsQuery.Result>>>
    {
        /// <summary>
        /// Mã vai trò cần lấy phân quyền
        /// </summary>
        public string RoleCode { get; set; } = default!;

        /// <summary>
        /// Kết quả ma trận phân quyền trả về cho client
        /// </summary>
        public sealed class Result
        {
            /// <summary>
            /// Mã chức năng (ví dụ: SY_USER)
            /// </summary>
            public string FunctionCode { get; set; } = default!;

            /// <summary>
            /// Tên hiển thị chức năng (đã dịch đa ngôn ngữ)
            /// </summary>
            public string FunctionName { get; set; } = default!;

            /// <summary>
            /// Nhóm chức năng (ví dụ: SYSTEM)
            /// </summary>
            public string GroupCode { get; set; } = default!;

            /// <summary>
            /// Danh sách các hành động có thể thực hiện trên chức năng này và trạng thái gán quyền
            /// </summary>
            public List<ActionPermissionDto> Actions { get; set; } = new();
        }

        /// <summary>
        /// Chi tiết hành động và trạng thái gán quyền
        /// </summary>
        public sealed class ActionPermissionDto
        {
            /// <summary>
            /// Mã hành động (ví dụ: VIEW, CREATE, UPDATE, DELETE)
            /// </summary>
            public string ActionCode { get; set; } = default!;

            /// <summary>
            /// Tên hành động hiển thị (đã dịch đa ngôn ngữ)
            /// </summary>
            public string ActionName { get; set; } = default!;

            /// <summary>
            /// Trạng thái đã được gán cho vai trò này hay chưa
            /// </summary>
            public bool IsAssigned { get; set; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn lấy ma trận quyền hạn
    /// </summary>
    public sealed class GetRolePermissionsQueryValidator : AbstractValidator<GetRolePermissionsQuery>
    {
        /// <summary>
        /// Khởi tạo validator kiểm tra RoleCode không được rỗng
        /// </summary>
        public GetRolePermissionsQueryValidator()
        {
            RuleFor(x => x.RoleCode)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy ma trận quyền hạn vai trò
    /// </summary>
    public sealed class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, ApiResponse<List<GetRolePermissionsQuery.Result>>>
    {
        /// <summary>
        /// Xử lý lấy ma trận quyền hạn từ database
        /// </summary>
        public async Task<ApiResponse<List<GetRolePermissionsQuery.Result>>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.RoleCode), request.RoleCode)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    // 1. Kiểm tra RoleCode có tồn tại trong hệ thống hay không
                    var roleExists = await dbContext.ExecuteScalarAsync<int>(
                        "SELECT COUNT(1) FROM sy_roles WHERE Code = @RoleCode",
                        new { request.RoleCode },
                        cancellationToken);

                    if (roleExists == 0)
                    {
                        var notFoundRes = ResponseHelper.NotFound<List<GetRolePermissionsQuery.Result>>(string.Format(CoreResource.common_notFound, CoreResource.entity_role));
                        log.ReturnCode = notFoundRes.ReturnCode;
                        log.Message = notFoundRes.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundRes;
                    }

                    // 2. Lấy danh sách functions đang hoạt động
                    var sqlFunctions = "SELECT Code, ResourceKey, GroupCode FROM sy_functions WHERE IsActive = 1";
                    var functions = (await dbContext.QueryAsync<FunctionRawDto>(sqlFunctions, null, cancellationToken)).ToList();

                    // 3. Lấy danh sách actions đang hoạt động, sắp xếp theo thứ tự hiển thị
                    var sqlActions = "SELECT Code, ResourceKey FROM sy_actions WHERE IsActive = 1 ORDER BY Sort";
                    var actions = (await dbContext.QueryAsync<ActionRawDto>(sqlActions, null, cancellationToken)).ToList();

                    // 4. Lấy cấu hình mapping Function x Action trong hệ thống
                    var sqlMappings = "SELECT FunctionCode, ActionCode FROM sy_function_mapping";
                    var mappings = (await dbContext.QueryAsync<MappingRawDto>(sqlMappings, null, cancellationToken)).ToList();

                    // 5. Lấy danh sách quyền hạn đã được gán thực tế cho vai trò này
                    var sqlRolePermissions = "SELECT FunctionCode, ActionCode FROM sy_role_permissions WHERE RoleCode = @RoleCode";
                    var assignedPermissions = (await dbContext.QueryAsync<PermissionRawDto>(sqlRolePermissions, new { request.RoleCode }, cancellationToken)).ToList();

                    // Tạo đối tượng dịch thuật đa ngôn ngữ dựa trên Language của request Header
                    var resourceManager = new ResourceManager();
                    var culture = !string.IsNullOrEmpty(request.HeaderInfo?.Language)
                        ? new CultureInfo(request.HeaderInfo.Language)
                        : new CultureInfo("en-US");

                    var matrix = new List<GetRolePermissionsQuery.Result>();

                    // 6. Tổ hợp ma trận quyền hạn
                    foreach (var func in functions)
                    {
                        // Dịch tên Function
                        var funcName = resourceManager.GetString(func.ResourceKey, culture);
                        if (string.IsNullOrEmpty(funcName))
                        {
                            funcName = func.Code;
                        }

                        var row = new GetRolePermissionsQuery.Result
                        {
                            FunctionCode = func.Code,
                            FunctionName = funcName,
                            GroupCode = func.GroupCode
                        };

                        // Lọc ra danh sách Action được hỗ trợ bởi Function này theo mapping
                        var supportedActions = mappings
                            .Where(m => m.FunctionCode == func.Code)
                            .Select(m => m.ActionCode)
                            .ToList();

                        foreach (var act in actions)
                        {
                            // Chỉ hiển thị action nếu function này có mapping hỗ trợ hành động đó
                            if (supportedActions.Contains(act.Code))
                            {
                                // Dịch tên Action
                                var actName = resourceManager.GetString(act.ResourceKey, culture);
                                if (string.IsNullOrEmpty(actName))
                                {
                                    actName = act.Code;
                                }

                                // Kiểm tra xem quyền (Function, Action) này đã được gán cho Role chưa
                                var isAssigned = assignedPermissions.Any(ap => ap.FunctionCode == func.Code && ap.ActionCode == act.Code);

                                row.Actions.Add(new GetRolePermissionsQuery.ActionPermissionDto
                                {
                                    ActionCode = act.Code,
                                    ActionName = actName,
                                    IsAssigned = isAssigned
                                });
                            }
                        }

                        matrix.Add(row);
                    }

                    var response = ResponseHelper.Success(matrix, CoreResource.common_getSuccess);

                    log.Result = matrix;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Lỗi khi lấy ma trận phân quyền cho role {request.RoleCode}: {ex.Message}", ex);

                    var response = ResponseHelper.Error<List<GetRolePermissionsQuery.Result>>(CoreResource.common_error);

                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }

        #region Raw DTOs

        private sealed class FunctionRawDto
        {
            public string Code { get; set; } = default!;
            public string ResourceKey { get; set; } = default!;
            public string GroupCode { get; set; } = default!;
        }

        private sealed class ActionRawDto
        {
            public string Code { get; set; } = default!;
            public string ResourceKey { get; set; } = default!;
        }

        private sealed class MappingRawDto
        {
            public string FunctionCode { get; set; } = default!;
            public string ActionCode { get; set; } = default!;
        }

        private sealed class PermissionRawDto
        {
            public string FunctionCode { get; set; } = default!;
            public string ActionCode { get; set; } = default!;
        }

        #endregion
    }

    #endregion
}
