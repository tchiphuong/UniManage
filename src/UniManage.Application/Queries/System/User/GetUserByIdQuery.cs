using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Domains.Query.System.User
{
    #region Query
    public class GetUserByIdQuery : CoreBaseQuery, IRequest<CoreResponse>
    {
        public int Id { get; set; }
        public class Result
        {
        }
    }
    #endregion

    #region Validator
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
        }
    }
    #endregion

    #region Handler
    public class GetUserByIdQueryHandler : CoreBaseQuery, IRequestHandler<GetUserByIdQuery, CoreResponse>
    {
        public async Task<CoreResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            CoreResponse response;

            // Initialize log data
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
            logData.Parameter = new List<CoreParamModel>
            {
            };
            List<GetUserByIdQuery.Result> result = new List<GetUserByIdQuery.Result>();

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