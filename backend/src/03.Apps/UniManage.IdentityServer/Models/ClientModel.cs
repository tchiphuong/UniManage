namespace UniManage.IdentityServer.Models
{
    /// <summary>
    /// Thông tin Client cấu hình cho hệ thống IdentityServer.
    /// </summary>
    public class ClientModel
    {
        /// <summary>
        /// Mã định danh duy nhất của Client.
        /// </summary>
        public string ClientId { get; set; } = default!;

        /// <summary>
        /// Tên hiển thị của Client.
        /// </summary>
        public string? ClientName { get; set; }

        /// <summary>
        /// Chuỗi bí mật dùng để xác thực Client.
        /// </summary>
        public string? ClientSecret { get; set; }

        /// <summary>
        /// Danh sách các Grant Type mà Client được phép sử dụng.
        /// </summary>
        public List<string> AllowedGrantTypes { get; set; } = new();

        /// <summary>
        /// Danh sách các Scope mà Client được phép truy cập.
        /// </summary>
        public List<string> AllowedScopes { get; set; } = new();

        /// <summary>
        /// Thời gian sống của Access Token (tính bằng giây).
        /// </summary>
        public int AccessTokenLifetime { get; set; } = 3600;

        /// <summary>
        /// Cờ cho phép Client nhận Refresh Token.
        /// </summary>
        public bool AllowOfflineAccess { get; set; }

        /// <summary>
        /// Thời gian sống của Refresh Token (tính bằng giây).
        /// </summary>
        public int RefreshTokenLifetime { get; set; } = 2592000;

        /// <summary>
        /// Cờ yêu cầu phải có Client Secret khi lấy token.
        /// </summary>
        public bool RequireClientSecret { get; set; }

        /// <summary>
        /// Mô tả chi tiết về Client.
        /// </summary>
        public string? Description { get; set; }
    }
}
