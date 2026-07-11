namespace UniManage.IdentityServer.Models
{
    /// <summary>
    /// Thông tin tài nguyên API.
    /// </summary>
    public class ApiResourceModel
    {
        /// <summary>
        /// Tên định danh của API Resource.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Tên hiển thị thân thiện.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Danh sách các scope được phép.
        /// </summary>
        public List<string> Scopes { get; set; } = new();

        /// <summary>
        /// Danh sách các claims yêu cầu.
        /// </summary>
        public List<string> UserClaims { get; set; } = new();
    }

    /// <summary>
    /// Thông tin tài nguyên xác thực Identity.
    /// </summary>
    public class IdentityResourceModel
    {
        /// <summary>
        /// Tên định danh (ví dụ: openid, profile).
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Tên hiển thị thân thiện.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Danh sách các claims yêu cầu.
        /// </summary>
        public List<string> UserClaims { get; set; } = new();
    }

    /// <summary>
    /// Thông tin chi tiết một phạm vi truy cập (Scope).
    /// </summary>
    public class ApiScopeModel
    {
        /// <summary>
        /// Tên định danh của Scope.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Tên hiển thị thân thiện.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Danh sách các claims yêu cầu.
        /// </summary>
        public List<string> UserClaims { get; set; } = new();
    }
}
