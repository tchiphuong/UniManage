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

namespace UniManage.Modules.System.Application.Commands.SystemConfigs
{
    #region Command

    public sealed class UpdateSystemConfigCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public Guid Uuid { get; set; }
        public string? ConfigValue { get; init; }
    }

    #endregion

    #region Validator

    public sealed class UpdateSystemConfigValidator : AbstractValidator<UpdateSystemConfigCommand>
    {
        public UpdateSystemConfigValidator()
        {
            RuleFor(x => x.Uuid)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, "Uuid"));
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateSystemConfigCommandHandler : IRequestHandler<UpdateSystemConfigCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateSystemConfigCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameters = new List<LogParamModel>
                {
                    new(nameof(request.Uuid), request.Uuid.ToString()),
                    new(nameof(request.ConfigValue), request.ConfigValue)
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var config = await dbContext.Set<SyConfigs>()
                    .FirstOrDefaultAsync(x => x.Uuid == request.Uuid, cancellationToken);

                if (config == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<bool>(string.Format(CoreResource.common_notFound, CoreResource.lbl_config));
                    log.Message = notFoundResponse.Message;
                    log.ReturnCode = notFoundResponse.ReturnCode;
                    return notFoundResponse;
                }

                // Validate Data Type
                if (config.DataType == "BOOL" && request.ConfigValue != "true" && request.ConfigValue != "false")
                {
                    var errorResponse = ResponseHelper.Error<bool>(string.Format(CoreResource.validation_invalidFormat, CoreResource.lbl_configValue));
                    log.Message = errorResponse.Message;
                    log.ReturnCode = errorResponse.ReturnCode;
                    return errorResponse;
                }

                if (config.DataType == "INT" && !int.TryParse(request.ConfigValue, out _))
                {
                    var errorResponse = ResponseHelper.Error<bool>(string.Format(CoreResource.validation_invalidFormat, CoreResource.lbl_configValue));
                    log.Message = errorResponse.Message;
                    log.ReturnCode = errorResponse.ReturnCode;
                    return errorResponse;
                }

                config.ConfigValue = request.ConfigValue;
                config.UpdatedBy = request.HeaderInfo?.Username;
                config.UpdatedAt = DateTimeHelper.Now;

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                var response = ResponseHelper.Success(true, string.Format(CoreResource.common_updateSuccess, CoreResource.lbl_config));

                log.Result = response.Data;
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








