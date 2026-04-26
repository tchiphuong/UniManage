namespace UniManage.Core.Services.Social
{
    /// <summary>
    /// Model chứa thông tin người dùng lấy từ Social Provider
    /// </summary>
    public class SocialUserProfile
    {
        public string Provider { get; set; } = string.Empty;
        public string ProviderUserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Picture { get; set; }
    }

    /// <summary>
    /// Interface chung cho các Social Auth Provider (Google, Facebook, Apple...)
    /// </summary>
    public interface ISocialAuthProvider
    {
        /// <summary>
        /// Tên định danh của provider (google, facebook...)
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Xác thực token và lấy thông tin profile người dùng
        /// </summary>
        /// <param name="token">Token từ client SDK</param>
        /// <returns>Profile người dùng nếu token hợp lệ</returns>
        Task<SocialUserProfile?> VerifyTokenAsync(string token, CancellationToken ct = default);
    }
}
