namespace UniManage.Core.Constant
{
    /// <summary>
    /// JWT Claims and OAuth/OpenID Connect constants
    /// </summary>
    public static class ClaimConstants
    {
        /// <summary>
        /// Standard OpenID Connect claims
        /// </summary>
        public static class StandardClaims
        {
            public const string Subject = "sub";
            public const string Name = "name";
            public const string Email = "email";
        }

        /// <summary>
        /// Custom application claims
        /// </summary>
        public static class CustomClaims
        {
            public const string Username = "username";
            public const string EmployeeCode = "employeeCode";
            public const string Role = "role";
        }

        /// <summary>
        /// OAuth2/OpenID Connect scopes
        /// </summary>
        public static class Scopes
        {
            public const string OpenId = "openid";
            public const string Profile = "profile";
            public const string OfflineAccess = "offline_access";

            // Application specific scopes
            public const string UniManageApi = "unimanage.api";
        }
    }
}