namespace UniManage.Core.Models;

/// <summary>
/// Generic paged API response container
/// </summary>
public class PagedResponse<T> : ApiResponse<PagedResult<T>>
{
    public PagedResponse() { }

    public PagedResponse(PagedResult<T> data) : base(data)
    {
    }
}
