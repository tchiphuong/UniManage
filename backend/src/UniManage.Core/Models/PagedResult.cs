namespace UniManage.Core.Models;

/// <summary>
/// Represents a page of data with paging information
/// </summary>
/// <typeparam name="T">Type of items in the page</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// List of items in current page
    /// </summary>
    public IReadOnlyList<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Paging information
    /// </summary>
    public PagingInfo Paging { get; set; } = new PagingInfo();
}