using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Commands.Attendance
{
    #region Command

    public sealed class DeleteAttendanceCommand : BaseCommand, IRequest<ApiResponse<DeleteAttendanceCommand.Response>>
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

    public sealed class DeleteAttendanceCommandValidator : AbstractValidator<DeleteAttendanceCommand>
    {
        public DeleteAttendanceCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All IDs must be greater than 0");
        }
    }

    #endregion

    #region Handler

    public sealed class DeleteAttendanceCommandHandler : IRequestHandler<DeleteAttendanceCommand, ApiResponse<DeleteAttendanceCommand.Response>>
    {
        public async Task<ApiResponse<DeleteAttendanceCommand.Response>> Handle(DeleteAttendanceCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var deletedCount = await dbContext.ExecuteAsync(
                        "DELETE FROM HrAttendance WHERE Id IN @Ids",
                        new { Ids = request.Ids },
                        cancellationToken);

                    

                    var responseData = new DeleteAttendanceCommand.Response { Success = true, DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, deletedCount));
return response;
        }
    }

    #endregion
}





