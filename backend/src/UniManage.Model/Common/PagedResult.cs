namespace UniManage.Model.Common;

/// <summary>
/// Represents a page of data with paging information
/// </summary>
public class PagedResult<T>
{
    /// <summary>
    /// Gets or sets the collection of items contained in the list.
    /// </summary>
    public List<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Paging information
    /// </summary>
    public PagingInfo Paging { get; set; } = new PagingInfo();
}