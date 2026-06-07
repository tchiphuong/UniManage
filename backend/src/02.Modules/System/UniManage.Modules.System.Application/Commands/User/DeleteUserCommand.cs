using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Commands.User
{
    #region Command

    /// <summary>
    /// Command to delete (soft delete) users
    /// </summary>
    public sealed class DeleteUserCommand : BaseCommand, IRequest<ApiResponse<DeleteUserCommand.Response>>
    {
        public List<Guid> Uuids { get; init; } = new();

        public sealed class Response
        {
            public List<Guid> DeletedUuids { get; init; } = new();
            public int Count { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for DeleteUserCommand
    /// </summary>
    public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Uuids)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userIdentity))
                .Must(uuids => uuids != null && uuids.All(uuid => uuid != Guid.Empty)).WithMessage(CoreResource.validation_invalidId);
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
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Uuids), string.Join(",", request.Uuids))
                }
            };

            try
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
                            var notFoundResponse = ResponseHelper.NotFound<DeleteUserCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                            log.Message = notFoundResponse.Message;
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
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

                        log.Result = responseData;
                        log.Message = response.Message;
                        log.ReturnCode = response.ReturnCode;

                        return response;
                    }
                    catch (Exception ex)
                    {
                        await dbContext.RollbackAsync();
                        log.IsException = true;
                        log.Message = ex.Message;
                        log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                        return ResponseHelper.Error<DeleteUserCommand.Response>(CoreResource.common_error);
                    }
                }
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}









