namespace UniManage.Core.Models;

/// <summary>
/// Base class for all queries providing common properties
/// </summary>
public abstract class BaseQuery
{
    /// <summary>
    /// Request header information for logging
    /// </summary>
    public HeaderInfo HeaderInfo { get; set; } = new();

    /// <summary>
    /// Optional keyword for searching
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// Page number, starting from 1
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// Items per page
    /// </summary>
    public int PageSize { get; set; } = 20;
}
