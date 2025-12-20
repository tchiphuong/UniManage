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
	public class DeleteUserCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public List<int> Ids { get; set; } = new List<int>();
    }
	#endregion

	#region Validator
	public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
	{
		public DeleteUserCommandValidator()
		{
			RuleFor(x => x.Ids)
				.NotEmpty().WithMessage("Ids are required");
		}
	}
	#endregion

	#region Handler
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<bool>>
	{
		public async Task<ApiResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			ApiResponse<bool> response;

			// khai bao log & cac tham so dau vao
			CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
			logData.Parameter = new List<CoreParamModel>
			{
			};

			using (DbContext dbContext = new DbContext(openTransaction: true))
			{
				try
				{
					response = ResponseHelper.Success<bool>(true);
					await dbContext.transaction.CommitAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					await dbContext.transaction.RollbackAsync(cancellationToken);
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
