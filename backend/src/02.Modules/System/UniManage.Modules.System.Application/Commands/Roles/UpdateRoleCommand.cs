using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Commands.Roles
{
    #region Command

    /// <summary>
    /// Command to update an existing role
    /// </summary>
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

    #endregion

    #region Validator

    /// <summary>
    /// Validator for UpdateRoleCommand
    /// </summary>
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
                .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.common_description, 500));

            RuleFor(x => x.IsActive)
                .InclusiveBetween((byte)0, (byte)1)
                .WithMessage(CoreResource.validation_invalidStatus);

            RuleFor(x => x.DataRowVersion)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, "DataRowVersion"));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for UpdateRoleCommand
    /// </summary>
    public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ApiResponse<UpdateRoleCommand.Response>>
    {
        public async Task<ApiResponse<UpdateRoleCommand.Response>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameters = new List<LogParamModel>
                {
                    new(nameof(request.RoleCode), request.RoleCode),
                    new(nameof(request.RoleName), request.RoleName),
                    new(nameof(request.IsActive), request.IsActive.ToString()),
                    new(nameof(request.DataRowVersion), request.DataRowVersion != null ? Convert.ToBase64String(request.DataRowVersion) : string.Empty)
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var role = await dbContext.Set<SyRoles>()
                    .FirstOrDefaultAsync(x => x.Code == request.RoleCode, cancellationToken);

                if (role == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<UpdateRoleCommand.Response>(CoreResource.common_notFound);
                    log.Message = notFoundResponse.Message;
                    log.ReturnCode = notFoundResponse.ReturnCode;
                    return notFoundResponse;
                }

                // Optimistic concurrency check
                if (request.DataRowVersion != null && !role.DataRowVersion.SequenceEqual(request.DataRowVersion))
                {
                    var concurrencyResponse = ResponseHelper.Error<UpdateRoleCommand.Response>(CoreResource.common_concurrencyError);
                    log.Message = concurrencyResponse.Message;
                    log.ReturnCode = concurrencyResponse.ReturnCode;
                    return concurrencyResponse;
                }

                role.NameVi = request.RoleName;
                role.NameEn = request.RoleName;
                role.Status = request.IsActive == 1 ? "Active" : "Inactive";
                role.UpdatedBy = request.HeaderInfo!.Username;
                role.UpdatedAt = DateTimeHelper.Now;

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                var responseData = new UpdateRoleCommand.Response { Success = true };
                var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_role));

                log.Result = responseData;
                log.Message = "Update role success";
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




