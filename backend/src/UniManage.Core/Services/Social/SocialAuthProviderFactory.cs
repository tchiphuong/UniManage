using Microsoft.Extensions.DependencyInjection;

namespace UniManage.Core.Services.Social
{
    /// <summary>
    /// Factory quản lý các Social Providers (Hỗ trợ mở rộng)
    /// </summary>
    public class SocialAuthProviderFactory
    {
        private readonly IEnumerable<ISocialAuthProvider> _providers;

        public SocialAuthProviderFactory(IEnumerable<ISocialAuthProvider> providers)
        {
            _providers = providers;
        }

        /// <summary>
        /// Lấy provider tương ứng với tên (google, facebook...)
        /// </summary>
        public ISocialAuthProvider? GetProvider(string providerName)
        {
            return _providers.FirstOrDefault(p => p.ProviderName.Equals(providerName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
