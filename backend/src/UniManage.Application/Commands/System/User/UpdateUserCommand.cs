using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.User
{
	#region Command
	public class UpdateUserCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? RoleCode { get; set; }
        public string? Status { get; set; }
    }
	#endregion

	#region Validator
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator()
		{
		}
	}
	#endregion

	#region Handler
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<bool>>
	{
		public async Task<ApiResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			ApiResponse<bool> response;

			// khai b�o log & c�c tham s? d?u v�o
			CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
			logData.Parameter = new List<CoreParamModel>
			{
			};

			using (DbContext dbContext = new DbContext(openTransaction: true))
			{
				try
				{
					response = ResponseHelper.Success<bool>(false);
					await dbContext.transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					await dbContext.transaction.RollbackAsync();
					response = ResponseHelper.Error<bool>(CoreResource.Common_msg_ExceptionOccurred);
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
