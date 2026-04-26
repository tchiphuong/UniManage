using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Attendance
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
        public async Task<ApiResponse<DeleteAttendanceCommand.Response>> Handle(DeleteAttendanceCommand request, CancellationToken ct)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var deletedCount = await dbContext.ExecuteAsync(
                        "DELETE FROM hr_attendance WHERE Id IN @Ids",
                        new { Ids = request.Ids },
                        ct);

                    

                    var responseData = new DeleteAttendanceCommand.Response { Success = true, DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, deletedCount));
return response;
        }
    }

    #endregion
}


