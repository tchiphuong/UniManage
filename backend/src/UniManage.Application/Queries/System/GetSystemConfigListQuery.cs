using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;

namespace UniManage.Application.Queries.System
{
    #region Query

    public sealed class GetSystemConfigListQuery : BaseQuery, IRequest<ApiResponse<IEnumerable<SystemConfig>>>
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
        public async Task<ApiResponse<IEnumerable<SystemConfig>>> Handle(GetSystemConfigListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Method = nameof(GetSystemConfigListQueryHandler),
                Path = "SystemConfig"
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = "SELECT * FROM sy_configs ORDER BY IsSystem DESC, ConfigCode";
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
                    log.IsException = 1;
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
