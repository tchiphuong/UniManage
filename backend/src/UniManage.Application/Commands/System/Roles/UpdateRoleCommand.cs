using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Roles
{
    public sealed class UpdateRoleCommand : BaseCommand, IRequest<ApiResponse<UpdateRoleCommand.Response>>
    {
        public string RoleCode { get; init; } = default!;
        public string RoleName { get; init; } = default!;
        public string? Description { get; init; }
        public byte IsActive { get; init; }
        public byte[] DataRowVersion { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.RoleCode)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleCode));

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_roleName))
                .DependentRules(() =>
                {
                    RuleFor(x => x.RoleName)
                        .Length(2, 200)
                        .WithMessage(string.Format(CoreResource.validation_length, CoreResource.lbl_roleName, 2, 200));
                });

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_description, 500));

            RuleFor(x => x.IsActive)
                .InclusiveBetween((byte)0, (byte)1)
                .WithMessage(CoreResource.validation_invalidStatus);

            RuleFor(x => x.DataRowVersion)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, "DataRowVersion"));
        }
    }

    public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ApiResponse<UpdateRoleCommand.Response>>
    {
        public async Task<ApiResponse<UpdateRoleCommand.Response>> Handle(UpdateRoleCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.RoleCode), request.RoleCode),
                    new CoreParamModel(nameof(request.RoleName), request.RoleName),
                    new CoreParamModel(nameof(request.IsActive), request.IsActive)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        UPDATE sy_roles
                        SET RoleName = @RoleName,
                            Description = @Description,
                            IsActive = @IsActive,
                            UpdatedBy = @UpdatedBy,
                            UpdatedAt = GETDATE()
                        WHERE RoleCode = @RoleCode AND DataRowVersion = @DataRowVersion";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.RoleCode,
                        request.RoleName,
                        request.Description,
                        request.IsActive,
                        UpdatedBy = request.HeaderInfo!.Username,
                        request.DataRowVersion
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateRoleCommand.Response>(CoreResource.common_concurrencyError);
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateRoleCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_updateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error updating role: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateRoleCommand.Response>(CoreResource.common_exceptionOccurred);

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