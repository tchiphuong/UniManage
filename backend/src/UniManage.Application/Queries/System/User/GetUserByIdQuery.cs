using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.User
{
    #region Query
    public class GetUserByIdQuery : BaseQuery, IRequest<ApiResponse<GetUserByIdQuery.Result>>
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
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<GetUserByIdQuery.Result>>
    {
        public async Task<ApiResponse<GetUserByIdQuery.Result>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse<GetUserByIdQuery.Result> response;

            // Initialize log data
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
            logData.Parameter = new List<CoreParamModel>
            {
            };
            GetUserByIdQuery.Result result = new GetUserByIdQuery.Result();

            // Use a using statement to manage the DbContext lifecycle
            using (DbContext dbContext = new DbContext())
            {
                try
                {
                    response = ResponseHelper.Success(result);
                }
                catch (Exception ex)
                {
                    response = ResponseHelper.Error<GetUserByIdQuery.Result>(CoreResource.Common_msg_ExceptionOccurred);
                    #region write log
                    logData.Result = response.Data;
                    logData.Message = ex.ToString();
                    logData.IsException = 1;
                    logData.ReturnCode = response.ReturnCode;
                    #endregion
                }
            }

            UniLogManager.WriteApiLog(logData);

            return response;
        }
    }
    #endregion
}
