using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyUser.Commands
{
    #region Command

    /// <summary>
    /// Command to delete (soft delete) users
    /// </summary>
    public sealed class DeleteUserCommand : BaseCommand, IRequest<ApiResponse<DeleteUserCommand.Response>>, ILoggableCommand
    {
        public List<Guid> Uuids { get; init; } = new();

        public sealed class Response
        {
            public List<Guid> DeletedUuids { get; init; } = new();
            public int Count { get; init; }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for DeleteUserCommand
    /// </summary>
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<DeleteUserCommand.Response>>
    {
        public async Task<ApiResponse<DeleteUserCommand.Response>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    // Find users by Uuids using EF Core
                    var users = await dbContext.Set<SyUsers>()
                        .Where(u => request.Uuids.Contains(u.Uuid))
                        .ToListAsync(cancellationToken);

                    if (users.Count == 0)
                    {
                        return ResponseHelper.NotFound<DeleteUserCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                    }

                    // Soft delete: Set Status to "Inactive"
                    foreach (var user in users)
                    {
                        user.Status = CoreCommon.Value.Commonstatus.Inactive;
                        user.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                        user.UpdatedAt = DateTimeHelper.Now;
                    }

                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync();

                    // Xóa cache combobox user
                    await CacheHelper.RemoveByPatternAsync(CacheKeyConstant.System.ComboboxUsersPattern);

                    var responseData = new DeleteUserCommand.Response 
                    { 
                        DeletedUuids = users.Select(u => u.Uuid).ToList(),
                        Count = users.Count 
                    };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, CoreResource.entity_user));

                    return response;
                }
                catch (Exception)
                {
                    await dbContext.RollbackAsync();
                    throw;
                }
            }
        }
    }

    #endregion
}










