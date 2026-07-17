using FluentValidation;
using MediatR;
using UniManage.Shared.Domain.Entities;
using UniManage.Shared.Application.Interfaces;

namespace UniManage.Shared.Application.Modules.SyConfig.Queries
{
    #region Query

    public sealed class GetSystemConfigListQuery : BaseListQuery, IRequest<ApiResponse<IEnumerable<SystemConfig>>>
    {
        // Inherits HeaderInfo from BaseQuery
    }

    #endregion

    #region Validator

    public sealed class GetSystemConfigListQueryValidator : AbstractValidator<GetSystemConfigListQuery>
    {
        public GetSystemConfigListQueryValidator()
        {
            // No specific validation needed for list all
        }
    }

    #endregion

    #region Handler

    public sealed class GetSystemConfigListQueryHandler : IRequestHandler<GetSystemConfigListQuery, ApiResponse<IEnumerable<SystemConfig>>>
    {
        public async Task<ApiResponse<IEnumerable<SystemConfig>>> Handle(GetSystemConfigListQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Method = nameof(GetSystemConfigListQueryHandler),
                Path = "SystemConfig"
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = "SELECT * FROM SyConfigs ORDER BY IsSystem DESC, ConfigCode";
                    var items = await dbContext.QueryAsync<SystemConfig>(sql);

                    var response = ResponseHelper.Success(items);

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

                    return ResponseHelper.Error<IEnumerable<SystemConfig>>("Error occurred while retrieving system configs");
                }
            }
        }
    }

    #endregion
}






