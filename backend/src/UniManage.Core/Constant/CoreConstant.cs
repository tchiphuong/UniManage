namespace UniManage.Core.Constant
{
    /// <summary>
    /// Các hằng số dùng chung toàn hệ thống
    /// </summary>
    public static class CoreConstant
    {
        #region System & Defaults

        /// <summary>
        /// Tên người dùng đại diện cho hệ thống
        /// </summary>
        public const string SystemUser = "system";

        /// <summary>
        /// Tên người dùng chưa xác thực
        /// </summary>
        public const string AnonymousUser = "anonymous";

        /// <summary>
        /// Vai trò mặc định của người dùng
        /// </summary>
        public const string DefaultRole = "user";

        /// <summary>
        /// Vai trò quản trị viên
        /// </summary>
        public const string AdminRole = "admin";

        /// <summary>
        /// Mật khẩu mặc định khi khởi tạo
        /// </summary>
        public const string DefaultPassword = "123456";

        #endregion

        #region File Categories

        /// <summary>
        /// Các loại file được phân loại trong hệ thống
        /// </summary>
        public static class FileCategory
        {
            public const string Image = "images";
            public const string Document = "documents";
            public const string Video = "videos";
            public const string Audio = "audio";
            public const string Others = "others";
        }

        #endregion

        #region JWT & Claims

        /// <summary>
        /// Cấu hình mặc định cho Token xác thực
        /// </summary>
        public static class Jwt
        {
            public const string DefaultIssuer = "unimanage.api";
            public const string DefaultAudience = "unimanage.client";
            public const string DefaultSecretKey = "unimanage_secret_key_at_least_32_characters_long";
        }

        /// <summary>
        /// Định nghĩa các loại Claim trong JWT
        /// </summary>
        public static class Claim
        {
            public const string Subject = "sub";
            public const string Name = "name";
            public const string Email = "email";
            public const string Username = "username";
            public const string EmployeeCode = "employeecode";
            public const string Role = "role";
        }

        #endregion

        #region Scopes

        /// <summary>
        /// Định nghĩa các Scopes cho IdentityServer/OAuth2
        /// </summary>
        public static class Scope
        {
            public const string OpenId = "openid";
            public const string Profile = "profile";
            public const string OfflineAccess = "offline_access";
            public const string UniManageApi = "unimanage.api";
        }

        #endregion
        #region Database Columns

        /// <summary>
        /// Các tên cột/thuộc tính phổ biến trong hệ thống
        /// </summary>
        public static class Column
        {
            public const string Name = "Name";
            public const string Description = "Description";
            public const string Title = "Title";
            public const string FullName = "FullName";
        }

        #endregion

        #region Config Codes

        /// <summary>
        /// Định nghĩa các mã cấu hình hệ thống
        /// </summary>
        public static class ConfigCode
        {
            public const string CompanyName = "COMPANY_NAME";
            public const string CompanyAddress = "COMPANY_ADDRESS";
            public const string CompanyPhone = "COMPANY_PHONE";
            public const string CompanyEmail = "COMPANY_EMAIL";
            public const string CompanyTaxCode = "COMPANY_TAX_CODE";
            public const string CompanyLogo = "COMPANY_LOGO";
        }

        #endregion
    }
}
