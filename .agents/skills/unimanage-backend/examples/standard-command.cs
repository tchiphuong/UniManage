// =============================================================================
// STANDARD COMMAND TEMPLATE — UniManage CQRS Pattern
// =============================================================================
// Usage: Copy this pattern when creating new Command handlers.
// Rules:
//   - Implement ILoggableCommand (logging is automated by pipeline)
//   - Nest Response record inside Command class
//   - Write operations: new DbContext(openTransaction: true)
//   - using(dbContext) wraps OUTSIDE try-catch
//   - CommitAsync on success, RollbackAsync on failure
// =============================================================================

using MediatR;
using UniManage.Shared.Domain;
using UniManage.Shared.Application.Interfaces;

namespace UniManage.Shared.Application.Modules.{Module}.Commands;

// =============================================================================
// EXAMPLE 1: Create Command
// =============================================================================

#region Command

/// <summary>
/// Command to create a new item
/// </summary>
public sealed class CreateItemCommand : BaseCommand, IRequest<ApiResponse<CreateItemCommand.Response>>, ILoggableCommand
{
    /// <summary>
    /// Item name
    /// </summary>
    public string Name { get; init; } = default!;

    /// <summary>
    /// Item status
    /// </summary>
    public string Status { get; init; } = default!;

    /// <summary>
    /// Response DTO with created item ID
    /// </summary>
    public sealed record Response
    {
        public long Id { get; init; }
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for creating a new item
/// </summary>
public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ApiResponse<CreateItemCommand.Response>>
{
    public async Task<ApiResponse<CreateItemCommand.Response>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        // Write operations MUST use openTransaction: true
        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var entity = new ItemEntity
                {
                    Name = request.Name,
                    Status = request.Status,
                    CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                    CreatedAt = DateTimeHelper.Now
                };

                dbContext.Set<ItemEntity>().Add(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                // Invalidate related caches
                await CacheHelper.RemoveByPatternAsync(CacheKeyConstant.Module.ItemPattern);

                var responseData = new CreateItemCommand.Response { Id = entity.Id };
                return ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, "Item"));
            }
            catch (Exception)
            {
                await dbContext.RollbackAsync();
                return ResponseHelper.Error<CreateItemCommand.Response>(CoreResource.common_error);
            }
        }
    }
}

#endregion


// =============================================================================
// EXAMPLE 2: Update Command (with Optimistic Concurrency)
// =============================================================================

#region Command

/// <summary>
/// Command to update an existing item
/// </summary>
public sealed class UpdateItemCommand : BaseCommand, IRequest<ApiResponse<bool>>, ILoggableCommand
{
    /// <summary>
    /// UUID of the item to update
    /// </summary>
    public Guid Uuid { get; init; }

    /// <summary>
    /// Updated name
    /// </summary>
    public string Name { get; init; } = default!;

    /// <summary>
    /// Row version for optimistic concurrency
    /// </summary>
    public byte[] RowVersion { get; init; } = default!;
}

#endregion

#region Handler

/// <summary>
/// Handler for updating an item
/// </summary>
public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var entity = await dbContext.Set<ItemEntity>()
                    .FirstOrDefaultAsync(x => x.Uuid == request.Uuid, cancellationToken);

                if (entity == null)
                {
                    return ResponseHelper.NotFound<bool>(string.Format(CoreResource.common_notFound, "Item"));
                }

                // Update properties
                entity.Name = request.Name;
                entity.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                entity.UpdatedAt = DateTimeHelper.Now;

                await dbContext.SaveChangesAsync(cancellationToken); // Auto-checks RowVersion
                await dbContext.CommitAsync();

                return ResponseHelper.Success(true, string.Format(CoreResource.common_updateSuccess, "Item"));
            }
            catch (DbUpdateConcurrencyException)
            {
                await dbContext.RollbackAsync();
                return ResponseHelper.Error<bool>("Data has been modified by another user. Please refresh and try again.");
            }
            catch (Exception)
            {
                await dbContext.RollbackAsync();
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
        }
    }
}

#endregion


// =============================================================================
// EXAMPLE 3: Delete Command (Batch)
// =============================================================================

#region Command

/// <summary>
/// Command to delete one or more items
/// </summary>
public sealed class DeleteItemCommand : BaseCommand, IRequest<ApiResponse<bool>>, ILoggableCommand
{
    /// <summary>
    /// List of UUIDs to delete
    /// </summary>
    public List<Guid> Uuids { get; init; } = new();
}

#endregion

#region Handler

/// <summary>
/// Handler for deleting items
/// </summary>
public sealed class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var entities = await dbContext.Set<ItemEntity>()
                    .Where(x => request.Uuids.Contains(x.Uuid))
                    .ToListAsync(cancellationToken);

                if (!entities.Any())
                {
                    return ResponseHelper.NotFound<bool>(string.Format(CoreResource.common_notFound, "Item"));
                }

                dbContext.Set<ItemEntity>().RemoveRange(entities);
                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                return ResponseHelper.Success(true, string.Format(CoreResource.common_deleteSuccess, "Item"));
            }
            catch (Exception)
            {
                await dbContext.RollbackAsync();
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
        }
    }
}

#endregion
