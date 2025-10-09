using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Domains.Query.Master.Employee
{
    #region Query
    public class GetEmployeeByIdQuery : CoreBaseQuery, IRequest<CoreResponse>
    {
        public int Id { get; set; }
        public class Result
        {
        }
    }
    #endregion

    #region Validator
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
        }
    }
    #endregion

    #region Handler
    public class GetEmployeeByIdQueryHandler : CoreBaseQuery, IRequestHandler<GetEmployeeByIdQuery, CoreResponse>
    {
        public async Task<CoreResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            CoreResponse response;

            // Initialize log data
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
            logData.Parameter = new List<CoreParamModel>
            {
            };
            List<GetEmployeeByIdQuery.Result> result = new List<GetEmployeeByIdQuery.Result>();

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