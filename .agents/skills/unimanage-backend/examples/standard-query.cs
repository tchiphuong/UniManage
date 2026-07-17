// =============================================================================
// STANDARD QUERY TEMPLATE — UniManage CQRS Pattern
// =============================================================================
// Usage: Copy this pattern when creating new Query handlers.
// Rules:
//   - Implement ILoggableCommand (logging is automated by pipeline)
//   - Nest Response record inside Query class
//   - using(dbContext) wraps OUTSIDE try-catch
//   - NO manual ApiLogModel or UniLogManager calls
// =============================================================================

using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Application.Interfaces;

namespace UniManage.Shared.Application.Modules.{Module}.Queries;

// =============================================================================
// EXAMPLE 1: Paginated List Query
// =============================================================================

#region Query

/// <summary>
/// Query to get paginated item list with filters
/// </summary>
public sealed class GetItemListQuery : BaseListQuery, IRequest<PagedResponse<GetItemListQuery.Response>>, ILoggableCommand
{
    /// <summary>
    /// Filter by status
    /// </summary>
    public string? Status { get; init; }

    /// <summary>
    /// Response DTO for each item row
    /// </summary>
    public sealed record Response
    {
        public long Id { get; init; }
        public Guid Uuid { get; init; }
        public string Name { get; init; } = default!;
        public string Status { get; init; } = default!;
        public DateTime CreatedAt { get; init; }
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for getting paginated item list
/// </summary>
public sealed class GetItemListQueryHandler : IRequestHandler<GetItemListQuery, PagedResponse<GetItemListQuery.Response>>
{
    /// <summary>
    /// Handle getting paginated data from Database
    /// </summary>
    public async Task<PagedResponse<GetItemListQuery.Response>> Handle(GetItemListQuery request, CancellationToken cancellationToken)
    {
        // DO NOT manually instantiate ApiLogModel — handled by ILoggableCommand pipeline
        using (var dbContext = new DbContext())
        {
            try
            {
                // Initialize query
                var query = dbContext.Set<ItemEntity>().AsQueryable();

                // Apply keyword filter
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    var kw = request.Keyword.Trim();
                    query = query.Where(x => x.Name.Contains(kw));
                }

                // Apply status filter
                if (!string.IsNullOrEmpty(request.Status))
                {
                    query = query.Where(x => x.Status == request.Status);
                }

                // Get total count for pagination
                var totalCount = await query.CountAsync(cancellationToken);

                // Apply sorting and pagination with projection
                var items = await query
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new GetItemListQuery.Response
                    {
                        Id = x.Id,
                        Uuid = x.Uuid,
                        Name = x.Name,
                        Status = x.Status,
                        CreatedAt = x.CreatedAt
                    })
                    .ToListAsync(cancellationToken);

                return ResponseHelper.PagedSuccess(items, totalCount, request.PageIndex, request.PageSize);
            }
            catch (Exception)
            {
                return ResponseHelper.PagedError<GetItemListQuery.Response>(CoreResource.common_error);
            }
        }
    }
}

#endregion


// =============================================================================
// EXAMPLE 2: Get By Id Query
// =============================================================================

#region Query

/// <summary>
/// Query to get item details by UUID
/// </summary>
public sealed class GetItemByIdQuery : BaseQuery, IRequest<ApiResponse<GetItemByIdQuery.Response>>, ILoggableCommand
{
    /// <summary>
    /// UUID of the item to retrieve
    /// </summary>
    public Guid Uuid { get; init; }

    /// <summary>
    /// Detailed response DTO
    /// </summary>
    public sealed record Response
    {
        public string Name { get; init; } = default!;
        public string Status { get; init; } = default!;
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public byte[] RowVersion { get; init; } = default!;
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for getting item by UUID
/// </summary>
public sealed class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ApiResponse<GetItemByIdQuery.Response>>
{
    public async Task<ApiResponse<GetItemByIdQuery.Response>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        using (var dbContext = new DbContext())
        {
            try
            {
                var item = await dbContext.Set<ItemEntity>()
                    .Where(x => x.Uuid == request.Uuid)
                    .Select(x => new GetItemByIdQuery.Response
                    {
                        Name = x.Name,
                        Status = x.Status,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        RowVersion = x.RowVersion
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (item == null)
                {
                    return ResponseHelper.NotFound<GetItemByIdQuery.Response>("Item not found");
                }

                return ResponseHelper.Success(item, string.Format(CoreResource.common_getSuccess, "Item"));
            }
            catch (Exception)
            {
                return ResponseHelper.Error<GetItemByIdQuery.Response>(CoreResource.common_error);
            }
        }
    }
}

#endregion
