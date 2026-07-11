using UniManage.IdentityServer.Models;

namespace UniManage.IdentityServer.Interfaces
{
    /// <summary>
    /// Giao diện quản lý các PersistedGrant (như Refresh Token, Authorization Code).
    /// </summary>
    public interface IPersistedGrantStore
    {
        /// <summary>
        /// Lưu trữ một PersistedGrant mới hoặc cập nhật nếu đã tồn tại.
        /// </summary>
        Task StoreAsync(PersistedGrantModel grant);

        /// <summary>
        /// Lấy thông tin PersistedGrant dựa trên Key.
        /// </summary>
        Task<PersistedGrantModel?> GetAsync(string key);

        /// <summary>
        /// Xóa một PersistedGrant dựa trên Key.
        /// </summary>
        Task RemoveAsync(string key);

        /// <summary>
        /// Xóa danh sách PersistedGrant dựa trên SubjectId, ClientId và Type.
        /// </summary>
        Task RemoveAllAsync(string? subjectId, string clientId, string? type = null);
    }
}
