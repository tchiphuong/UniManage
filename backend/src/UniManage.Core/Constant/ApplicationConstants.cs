namespace UniManage.Core.Constant
{
    /// <summary>
    /// Application-wide constants and configuration values
    /// For dynamic configuration, consider using sy_commons table
    /// </summary>
    public static class ApplicationConstants
    {
        /// <summary>
        /// Default values - Consider moving to sy_commons for configurability
        /// </summary>
        public static class Defaults
        {
            public const string SystemUser = "system";
            public const string DefaultRole = "User";
            public const string AdminRole = "Admin";
            public const string DefaultPassword = "123456";
        }

        /// <summary>
        /// JWT settings and defaults
        /// </summary>
        public static class JwtSettings
        {
            public const string DefaultIssuer = "UniManage.Api";
            public const string DefaultAudience = "UniManage.Client";
            public const string DefaultSecretKey = "UniManage_Secret_Key_2025_At_Least_32_Characters_Long";
        }
    }
}