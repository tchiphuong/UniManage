using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Shared.Domain.Entities;
using UniManage.Shared.Application.Interfaces;

namespace UniManage.Shared.Application.Modules.SyConfig.Queries
{
    #region Query

    public sealed class GetSystemConfigByCodeQuery : BaseQuery, IRequest<ApiResponse<SystemConfig>>
    {
        public string Code { get; init; } = default!;
    }

    #endregion

    #region Validator

    public sealed class GetSystemConfigByCodeQueryValidator : AbstractValidator<GetSystemConfigByCodeQuery>
    {
        public GetSystemConfigByCodeQueryValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Config code is required");
        }
    }

    #endregion

    #region Handler

    public sealed class GetSystemConfigByCodeQueryHandler : IRequestHandler<GetSystemConfigByCodeQuery, ApiResponse<SystemConfig>>
    {
        public async Task<ApiResponse<SystemConfig>> Handle(GetSystemConfigByCodeQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Method = nameof(GetSystemConfigByCodeQueryHandler),
                Path = "SystemConfig",
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Code), request.Code)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = "SELECT * FROM SyConfigs WHERE ConfigCode = @Code";
                    var item = await dbContext.QueryFirstOrDefaultAsync<SystemConfig>(sql, new { request.Code });

                    if (item == null)
                    {
                        return ResponseHelper.NotFound<SystemConfig>("Config not found");
                    }

                    var response = ResponseHelper.Success(item);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                    UniLogManager.WriteApiLog(log);

                    return ResponseHelper.Error<SystemConfig>("Error occurred");
                }
            }
        }
    }

    #endregion
}






