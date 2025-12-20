using System.Collections.Generic;

namespace UniManage.Model.Common;

/// <summary>
/// Represents a page of data with paging information
/// </summary>
public class PagedResult
{
    /// <summary>
    /// Gets or sets the collection of items contained in the list.
    /// </summary>
    public List<object> Items { get; set; } = new List<object>();

    /// <summary>
    /// Paging information
    /// </summary>
    public PagingInfo Paging { get; set; } = new PagingInfo();
}