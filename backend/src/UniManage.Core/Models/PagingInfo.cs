namespace UniManage.Core.Models;

/// <summary>
/// Common paging information
/// </summary>
public class PagingInfo
{
    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (TotalItems + PageSize - 1) / PageSize;

    public PagingInfo() { }

    public PagingInfo(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}
