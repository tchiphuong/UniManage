using UniManage.IdentityServer.Models;

namespace UniManage.IdentityServer.Interfaces
{
    /// <summary>
    /// Giao diện quản lý lưu trữ và tra cứu thông tin Client.
    /// </summary>
    public interface IClientStore
    {
        /// <summary>
        /// Tìm kiếm thông tin Client dựa trên ClientId.
        /// </summary>
        /// <param name="clientId">Mã định danh của Client.</param>
        /// <returns>Thông tin ClientModel tương ứng hoặc null nếu không tồn tại.</returns>
        Task<ClientModel?> FindClientByIdAsync(string clientId);
    }
}
