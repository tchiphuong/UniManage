using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;

namespace UniManage.Application.Commands.System.SystemConfigs
{
    #region Command

    public sealed class UpdateSystemConfigCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public int Id { get; init; }
        public string? ConfigValue { get; init; }
    }

    #endregion

    #region Validator

    public sealed class UpdateSystemConfigValidator : AbstractValidator<UpdateSystemConfigCommand>
    {
        public UpdateSystemConfigValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateSystemConfigCommandHandler : IRequestHandler<UpdateSystemConfigCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateSystemConfigCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Method = nameof(UpdateSystemConfigCommandHandler),
                Path = "SystemConfig",
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id),
                    new CoreParamModel(nameof(request.ConfigValue), request.ConfigValue)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    // 1. Get current
                    var sqlGet = "SELECT Id, DataType FROM sy_configs WHERE Id = @Id";
                    var current = await dbContext.QueryFirstOrDefaultAsync<SystemConfig>(sqlGet, new { request.Id });

                    if (current == null)
                        return ResponseHelper.NotFound<bool>("Config not found");

                    // 2. Validate Data Type
                    if (current.DataType == "BOOL" && request.ConfigValue != "true" && request.ConfigValue != "false")
                        return ResponseHelper.Error<bool>("Value must be 'true' or 'false' for BOOL type");

                    if (current.DataType == "INT" && !int.TryParse(request.ConfigValue, out _))
                        return ResponseHelper.Error<bool>("Value must be an integer for INT type");

                    // 3. Update
                    var sqlUpdate = @"
                        UPDATE sy_configs 
                        SET ConfigValue = @ConfigValue, 
                            UpdatedBy = @UpdatedBy, 
                            UpdatedAt = SYSUTCDATETIME() 
                        WHERE Id = @Id";

                    await dbContext.ExecuteAsync(sqlUpdate, new
                    {
                        request.ConfigValue,
                        request.Id,
                        UpdatedBy = request.HeaderInfo?.Username
                    });

                    await dbContext.CommitAsync(ct);

                    var response = ResponseHelper.Success(true, "Config updated successfully");

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                    UniLogManager.WriteApiLog(log);

                    return ResponseHelper.Error<bool>("Error occurred");
                }
            }
        }
    }

    #endregion
}
