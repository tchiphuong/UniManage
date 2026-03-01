using Microsoft.AspNetCore.Authorization;

namespace UniManage.Api.Authorization
{
    /// <summary>
    /// Custom authorization attribute to enforce database-driven permissions.
    /// 
    /// Usage:
    ///   [PermissionAuthorize("users", "view")]     // Requires users.view permission
    ///   [PermissionAuthorize("users", "create")]   // Requires users.create permission
    ///   [PermissionAuthorize("roles", "delete")]   // Requires roles.delete permission
    /// 
    /// Permissions are checked against sy_role_permissions table via PermissionAuthorizationHandler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationRequirementData
    {
        /// <summary>
        /// Mã chức năng (FunctionCode)
        /// </summary>
        public string FunctionCode { get; }

        /// <summary>
        /// Mã hành động (ActionCode)
        /// </summary>
        public string ActionCode { get; }

        public PermissionAuthorizeAttribute(string functionCode, string actionCode)
            : base(policy: $"Permission:{functionCode}.{actionCode}")
        {
            FunctionCode = functionCode;
            ActionCode = actionCode;
        }

        /// <summary>
        /// Cung cấp PermissionRequirement cho ASP.NET Core Authorization system.
        /// </summary>
        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            yield return new PermissionRequirement(FunctionCode, ActionCode);
        }
    }
}
