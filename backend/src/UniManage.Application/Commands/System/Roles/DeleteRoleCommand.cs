using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Roles
{
    public sealed class DeleteRoleCommand : BaseCommand, IRequest<ApiResponse<DeleteRoleCommand.Response>>
    {
        public List<string> RoleCodes { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

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
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE RoleCode IN @RoleCodes) THEN 1 ELSE 0 END",
                    new { RoleCodes = roleCodes });
            }
        }
    }

    public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApiResponse<DeleteRoleCommand.Response>>
    {
        public async Task<ApiResponse<DeleteRoleCommand.Response>> Handle(DeleteRoleCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.RoleCodes), string.Join(",", request.RoleCodes))
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        DELETE FROM sy_roles
                        WHERE RoleCode IN @RoleCodes";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { RoleCodes = request.RoleCodes }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new DeleteRoleCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_DeleteSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error deleting roles: {ex.Message}", ex);

                    var response = ResponseHelper.Error<DeleteRoleCommand.Response>("Error occurred while deleting roles");

                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}
