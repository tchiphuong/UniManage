using UniManage.Shared.Application.Interfaces;
namespace UniManage.Shared.Infrastructure.Services
{
    /// <summary>
    /// Factory quß║ún l├╜ c├íc Social Providers (Hß╗ù trß╗ú mß╗ƒ rß╗Öng)
    /// </summary>
    public class SocialAuthProviderFactory
    {
        private readonly IEnumerable<ISocialAuthProvider> _providers;

        public SocialAuthProviderFactory(IEnumerable<ISocialAuthProvider> providers)
        {
            _providers = providers;
        }

        /// <summary>
        /// Lß║Ñy provider t╞░╞íng ß╗⌐ng vß╗¢i t├¬n (google, facebook...)
        /// </summary>
        public ISocialAuthProvider? GetProvider(string providerName)
        {
            return _providers.FirstOrDefault(p => p.ProviderName.Equals(providerName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

