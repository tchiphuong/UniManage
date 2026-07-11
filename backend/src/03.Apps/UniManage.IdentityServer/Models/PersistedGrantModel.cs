namespace UniManage.IdentityServer.Models
{
    /// <summary>
    /// Thông tin lưu trữ Refresh Token hoặc Authorization Code.
    /// </summary>
    public class PersistedGrantModel
    {
        /// <summary>
        /// Mã định danh duy nhất của grant (thường là token string đã hash).
        /// </summary>
        public string Key { get; set; } = default!;

        /// <summary>
        /// Loại grant (ví dụ: refresh_token, authorization_code).
        /// </summary>
        public string Type { get; set; } = default!;

        /// <summary>
        /// Mã người dùng sở hữu grant.
        /// </summary>
        public string? SubjectId { get; set; }

        /// <summary>
        /// Mã phiên làm việc (Session ID).
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// Mã định danh của Client.
        /// </summary>
        public string ClientId { get; set; } = default!;

        /// <summary>
        /// Thời gian tạo grant.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Thời gian hết hạn của grant.
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// Thời gian grant đã được sử dụng.
        /// </summary>
        public DateTime? ConsumedTime { get; set; }

        /// <summary>
        /// Dữ liệu bổ sung (như danh sách scopes, claims).
        /// </summary>
        public string Data { get; set; } = default!;
    }
}
