using UniManage.IdentityServer.Models;

namespace UniManage.IdentityServer.Interfaces
{
    /// <summary>
    /// Giao diện quản lý các tài nguyên (Identity, API, Scopes) của IdentityServer.
    /// </summary>
    public interface IResourceStore
    {
        /// <summary>
        /// Tìm kiếm các tài nguyên Identity theo danh sách scope names.
        /// </summary>
        Task<IEnumerable<IdentityResourceModel>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames);

        /// <summary>
        /// Tìm kiếm các API Scopes theo danh sách scope names.
        /// </summary>
        Task<IEnumerable<ApiScopeModel>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames);

        /// <summary>
        /// Tìm kiếm các tài nguyên API theo danh sách scope names.
        /// </summary>
        Task<IEnumerable<ApiResourceModel>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames);

        /// <summary>
        /// Tìm kiếm các tài nguyên API theo danh sách API resource names.
        /// </summary>
        Task<IEnumerable<ApiResourceModel>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames);
    }
}
