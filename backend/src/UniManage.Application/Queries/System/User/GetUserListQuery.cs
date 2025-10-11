using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Domains.Query.System.User
{
    #region Query
    public class GetUserListQuery : BaseQuery, IRequest<ApiResponse<object>>
    {
        public class Result
        {
        }
    }
    #endregion

    #region Validator
    public class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
    {
        public GetUserListQueryValidator()
        {
        }
    }
    #endregion

    #region Handler
    public class GetUserListQueryHandler : BaseQuery, IRequestHandler<GetUserListQuery, ApiResponse<object>>
    {
        public async Task<CoreResponse> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            CoreResponse response;

            // Initialize log data
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
            logData.Parameter = new List<CoreParamModel>
            {
            };
            List<GetUserListQuery.Result> result = new List<GetUserListQuery.Result>();

            // Use a using statement to manage the DbContext lifecycle
            using (DbContext dbContext = new DbContext())
            {
                try
                {
                    response = new CoreResponse(returnCode: CoreApiReturnCode.Succeed, result: result);
                }
                catch (Exception ex)
                {
                    response = new CoreResponse(CoreApiReturnCode.ExceptionOccurred, CoreResource.Common_msg_ExceptionOccurred);
                    #region write log
                    logData.Result = response.Data;
                    logData.Message = ex.ToString();
                    logData.IsException = 1;
                    logData.ReturnCode = response.ReturnCode;
                    #endregion
                }
            }

            UniLogManager.WriteApiLog(logData);

            return await Task.FromResult(response);
        }
    }
    #endregion
}
