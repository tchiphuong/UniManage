using Microsoft.AspNetCore.Authorization;

namespace UniManage.Api.Authorization
{
    /// <summary>
    /// Authorization requirement chứa FunctionCode + ActionCode.
    /// Được tạo bởi PermissionAuthorizeAttribute và được xử lý bởi PermissionAuthorizationHandler.
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Mã chức năng (e.g. "users", "roles", "menus")
        /// Tương ứng với cột FunctionCode trong bảng sy_role_permissions
        /// </summary>
        public string FunctionCode { get; }

        /// <summary>
        /// Mã hành động (e.g. "view", "create", "update", "delete")
        /// Tương ứng với cột ActionCode trong bảng sy_role_permissions
        /// </summary>
        public string ActionCode { get; }

        /// <summary>
        /// Permission dạng "FunctionCode.ActionCode" (e.g. "users.view")
        /// </summary>
        public string PermissionCode => $"{FunctionCode}.{ActionCode}";

        public PermissionRequirement(string functionCode, string actionCode)
        {
            FunctionCode = functionCode;
            ActionCode = actionCode;
        }
    }
}
