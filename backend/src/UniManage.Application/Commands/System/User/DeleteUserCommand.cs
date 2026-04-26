using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.System.User
{
    #region Command

    /// <summary>
    /// Command to delete (soft delete) users
    /// </summary>
    public sealed class DeleteUserCommand : BaseCommand, IRequest<ApiResponse<DeleteUserCommand.Response>>
    {
        public List<long> Ids { get; init; } = new();

        public sealed class Response
        {
            public List<long> DeletedIds { get; init; } = new();
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
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userIdentity))
                .Must(ids => ids != null && ids.All(id => id > 0)).WithMessage(CoreResource.validation_invalidId);
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for DeleteUserCommand
    /// </summary>
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<DeleteUserCommand.Response>>
    {
        public async Task<ApiResponse<DeleteUserCommand.Response>> Handle(DeleteUserCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.Ids), string.Join(",", request.Ids))
                }
            };

            try
            {
                using (var dbContext = new DbContext(openTransaction: true))
                {
                    try
                    {
                        // Find users by Ids using EF Core
                        var users = await dbContext.Set<sy_users>()
                            .Where(u => request.Ids.Contains(u.Id))
                            .ToListAsync(ct);

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

                        await dbContext.SaveChangesAsync(ct);
                        await dbContext.CommitAsync();

                        var responseData = new DeleteUserCommand.Response 
                        { 
                            DeletedIds = users.Select(u => u.Id).ToList(),
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
