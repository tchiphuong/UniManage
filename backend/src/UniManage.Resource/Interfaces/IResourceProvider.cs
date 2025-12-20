namespace UniManage.Resource.Interfaces;

/// <summary>
/// Interface for accessing localized resources
/// </summary>
public interface IResourceProvider
{
    /// <summary>
    /// Gets a localized string by key
    /// </summary>
    Task<string> GetStringAsync(string key, string cultureName = "en");

    /// <summary>
    /// Gets a formatted localized string
    /// </summary>
    Task<string> GetStringAsync(string key, string cultureName, params object[] args);

    /// <summary>
    /// Gets all resources for a culture
    /// </summary>
    Task<IDictionary<string, string>> GetAllStringsAsync(string cultureName = "en");
}
