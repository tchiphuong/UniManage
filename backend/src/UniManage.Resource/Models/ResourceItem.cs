namespace UniManage.Resource.Models;

/// <summary>
/// Represents a localized resource item
/// </summary>
public class ResourceItem
{
    /// <summary>
    /// Key of the resource
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Value in default culture (en)
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Comment/description for the resource
    /// </summary> 
    public string? Comment { get; set; }

    /// <summary>
    /// Category/group of the resource
    /// </summary>
    public string Category { get; set; } = "Common";
}
