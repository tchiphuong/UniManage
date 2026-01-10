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
                .GreaterThan(0).WithMessage(CoreResource.Validation_msg_Required);

            RuleFor(x => x.AttendanceDate)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required);

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required);

            RuleFor(x => x.DataRowVersion)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required);

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
        public async Task<ApiResponse<UpdateAttendanceCommand.Response>> Handle(UpdateAttendanceCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id),
                    new CoreParamModel(nameof(request.AttendanceDate), request.AttendanceDate),
                    new CoreParamModel(nameof(request.Status), request.Status)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var rowsAffected = await dbContext.ExecuteAsync(
                        @"UPDATE hr_attendance
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
                        ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.Error<UpdateAttendanceCommand.Response>("Attendance has been modified by another user");
                    }

                    await dbContext.CommitAsync();

                    var responseData = new UpdateAttendanceCommand.Response { Success = true, Id = request.Id };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_UpdateSuccess);

                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);
                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error updating attendance: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateAttendanceCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
                    log.Message = ex.ToString();
                    log.IsException = 1;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }

    #endregion
}
