using System.Globalization;
using System.Resources;

namespace UniManage.Resource
{
    public static class ResourceProvider
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("UniManage.Resource.UniManageResources", typeof(ResourceProvider).Assembly);

        public static string GetString(string key, CultureInfo? culture = null)
        {
            return _resourceManager.GetString(key, culture ?? CultureInfo.CurrentUICulture) ?? key;
        }
    }
}
