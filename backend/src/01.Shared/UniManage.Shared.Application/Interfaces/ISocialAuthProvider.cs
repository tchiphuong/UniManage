using UniManage.Shared.Domain.Interfaces;

namespace UniManage.Shared.Application.Interfaces
{
    /// <summary>
    /// Interface chung cho các Social Auth Provider (Google, Facebook, Apple...)
    /// Sử dụng SocialUserProfile từ Domain layer
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
