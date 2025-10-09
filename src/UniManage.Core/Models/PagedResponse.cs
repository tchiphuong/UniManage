namespace UniManage.Core.Models;

/// <summary>
/// Generic paged result container
/// </summary>
public class PagedResponse<T>
{
    /// <summary>
    /// List of items for the current page
    /// </summary>
    public IReadOnlyList<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Paging information
    /// </summary>
    public PagingInfo Paging { get; set; } = new();

    public PagedResponse() { }

    public PagedResponse(IReadOnlyList<T> items, PagingInfo paging)
    {
        Items = items;
        Paging = paging;
    }
}
