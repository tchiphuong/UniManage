using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using DbContext = UniManage.Core.Database.DbContext;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Core.Constant;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Roles
{
    #region Command

    /// <summary>
    /// Command to delete roles
    /// </summary>
    public sealed class DeleteRoleCommand : BaseCommand, IRequest<ApiResponse<DeleteRoleCommand.Response>>
    {
        public List<string> RoleCodes { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for DeleteRoleCommand
    /// </summary>
    public sealed class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.RoleCodes)
                .NotEmpty().WithMessage("Role codes are required")
                .Must(codes => codes.All(code => !string.IsNullOrEmpty(code)))
                .WithMessage("All role codes must be valid");

            RuleFor(x => x.RoleCodes)
                .MustAsync(async (codes, cancel) => !await IsAnyRoleInUseAsync(codes))
                .WithMessage("One or more roles are assigned to users and cannot be deleted");
        }

        private static async Task<bool> IsAnyRoleInUseAsync(List<string> roleCodes)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<sy_users>().AnyAsync(x => roleCodes.Contains(x.RoleCode));
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for DeleteRoleCommand
    /// </summary>
    public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApiResponse<DeleteRoleCommand.Response>>
    {
        public async Task<ApiResponse<DeleteRoleCommand.Response>> Handle(DeleteRoleCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameters = new List<CoreParamModel>
                {
                    new(nameof(request.RoleCodes), string.Join(",", request.RoleCodes))
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var rolesToDelete = await dbContext.Set<sy_roles>()
                    .Where(x => request.RoleCodes.Contains(x.Code))
                    .ToListAsync(ct);

                var deletedCount = rolesToDelete.Count;
                if (deletedCount > 0)
                {
                    dbContext.Set<sy_roles>().RemoveRange(rolesToDelete);
                    await dbContext.SaveChangesAsync(ct);
                }

                await dbContext.CommitAsync();

                var responseData = new DeleteRoleCommand.Response { DeletedCount = deletedCount };
                var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);

                log.Result = responseData;
                log.Message = response.Message;
                log.ReturnCode = response.ReturnCode;

                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                throw;
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
