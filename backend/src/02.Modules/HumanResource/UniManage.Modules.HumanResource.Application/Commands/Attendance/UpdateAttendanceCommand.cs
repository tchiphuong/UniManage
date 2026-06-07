using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Commands.Attendance
{
    #region Command

    public sealed class UpdateAttendanceCommand : BaseCommand, IRequest<ApiResponse<UpdateAttendanceCommand.Response>>
    {
        public int Id { get; init; }
        public DateTime AttendanceDate { get; init; }
        public TimeSpan? CheckInTime { get; init; }
        public TimeSpan? CheckOutTime { get; init; }
        public string Status { get; init; } = default!;
        public byte[] DataRowVersion { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
            public int Id { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class UpdateAttendanceCommandValidator : AbstractValidator<UpdateAttendanceCommand>
    {
        public UpdateAttendanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(CoreResource.validation_required);

            RuleFor(x => x.AttendanceDate)
                .NotEmpty().WithMessage(CoreResource.validation_required);

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage(CoreResource.validation_required);

            RuleFor(x => x.DataRowVersion)
                .NotEmpty().WithMessage(CoreResource.validation_required);

            RuleFor(x => x.CheckOutTime)
                .GreaterThan(x => x.CheckInTime)
                .When(x => x.CheckInTime.HasValue && x.CheckOutTime.HasValue)
                .WithMessage("CheckOutTime must be after CheckInTime");
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, ApiResponse<UpdateAttendanceCommand.Response>>
    {
        public async Task<ApiResponse<UpdateAttendanceCommand.Response>> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var rowsAffected = await dbContext.ExecuteAsync(
                        @"UPDATE HrAttendance
                          SET AttendanceDate = @AttendanceDate,
                              CheckInTime = @CheckInTime,
                              CheckOutTime = @CheckOutTime,
                              Status = @Status,
                              UpdatedBy = @UpdatedBy,
                              UpdatedAt = GETDATE()
                          WHERE Id = @Id AND DataRowVersion = @DataRowVersion",
                        new
                        {
                            request.Id,
                            request.AttendanceDate,
                            request.CheckInTime,
                            request.CheckOutTime,
                            request.Status,
                            UpdatedBy = request.HeaderInfo!.Username,
                            request.DataRowVersion
                        },
                        cancellationToken);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.Error<UpdateAttendanceCommand.Response>("Attendance has been modified by another user");
                    }

                    

                    var responseData = new UpdateAttendanceCommand.Response { Success = true, Id = request.Id };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }

    #endregion
}





