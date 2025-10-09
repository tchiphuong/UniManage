using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Domains.Command.Master.Unit
{
    #region Command
    public class UpdateUnitCommand : CoreBaseCommand, IRequest<CoreResponse>
	{
		public int Id { get; set; }
    }
    #endregion

    #region Validator
    public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
	{
		public UpdateUnitCommandValidator()
		{
		}
	}
    #endregion

    #region Handler
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, CoreResponse>
	{
		public async Task<CoreResponse> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
		{
			CoreResponse response = null;

			// khai báo log & các tham số đầu vào
			CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
			logData.Parameter = new List<CoreParamModel>
			{
			};

			using (DbContext dbContext = new DbContext(openTransaction: true))
			{
				try
				{
					response = new CoreResponse(returnCode: CoreApiReturnCode.Succeed);
					await dbContext.transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					await dbContext.transaction.RollbackAsync();
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
