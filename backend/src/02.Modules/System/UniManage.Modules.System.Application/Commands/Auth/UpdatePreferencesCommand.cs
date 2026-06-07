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

namespace UniManage.Modules.System.Application.Commands.Auth
{
    #region Command

    public sealed class UpdatePreferencesCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public string Preferences { get; init; } = default!;
    }

    #endregion

    #region Validator

    public sealed class UpdatePreferencesCommandValidator : AbstractValidator<UpdatePreferencesCommand>
    {
        public UpdatePreferencesCommandValidator()
        {
            RuleFor(x => x.Preferences)
                .NotEmpty().WithMessage(CoreResource.validation_required);
        }
    }

    #endregion

    #region Handler

    public sealed class UpdatePreferencesCommandHandler : IRequestHandler<UpdatePreferencesCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdatePreferencesCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameters = new List<LogParamModel>
                {
                    new(nameof(request.Preferences), request.Preferences)
                }
            };

            var username = request.HeaderInfo?.Username;
            if (string.IsNullOrEmpty(username))
            {
                return ResponseHelper.Error<bool>(CoreResource.common_unauthorized);
            }

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var user = await dbContext.Set<SyUsers>()
                    .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

                if (user == null)
                {
                    return ResponseHelper.NotFound<bool>(CoreResource.entity_user);
                }

                user.Preferences = request.Preferences;
                user.UpdatedAt = DateTimeHelper.Now;
                user.UpdatedBy = username;

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                var response = ResponseHelper.Success(true, CoreResource.common_updateSuccess);
                log.ReturnCode = response.ReturnCode;
                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




