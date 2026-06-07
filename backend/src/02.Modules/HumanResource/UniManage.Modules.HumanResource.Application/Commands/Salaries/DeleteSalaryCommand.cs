using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Commands.Salaries
{
    #region Command

    public sealed class DeleteSalaryCommand : BaseCommand, IRequest<ApiResponse<DeleteSalaryCommand.Response>>
    {
        public List<int> Ids { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
            public int DeletedCount { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class DeleteSalaryCommandValidator : AbstractValidator<DeleteSalaryCommand>
    {
        public DeleteSalaryCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All IDs must be greater than 0");
        }
    }

    #endregion

    #region Handler

    public sealed class DeleteSalaryCommandHandler : IRequestHandler<DeleteSalaryCommand, ApiResponse<DeleteSalaryCommand.Response>>
    {
        public async Task<ApiResponse<DeleteSalaryCommand.Response>> Handle(DeleteSalaryCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    await dbContext.ExecuteAsync(
                        "DELETE FROM HrSalaryHistory WHERE SalaryId IN @Ids",
                        new { Ids = request.Ids },
                        cancellationToken);

                    var deletedCount = await dbContext.ExecuteAsync(
                        "DELETE FROM HrSalaries WHERE Id IN @Ids",
                        new { Ids = request.Ids },
                        cancellationToken);

                    

                    var responseData = new DeleteSalaryCommand.Response { Success = true, DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, deletedCount));
return response;
        }
    }

    #endregion
}





