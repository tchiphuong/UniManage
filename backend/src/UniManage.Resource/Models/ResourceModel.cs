namespace UniManage.Resource.Models;

/// <summary>
/// Model for resource data from database
/// </summary>
public sealed class ResourceModel
{
    /// <summary>
    /// Language code (e.g. en, vi)
    /// </summary>
    public string LanguageCode { get; set; } = string.Empty;

    /// <summary>
    /// Resource key name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Resource value in specified language
    /// </summary>
    public string Value { get; set; } = string.Empty;
}
