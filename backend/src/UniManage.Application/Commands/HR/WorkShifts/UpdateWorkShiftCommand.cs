using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.WorkShifts
{
    #region Command

    public sealed class UpdateWorkShiftCommand : BaseCommand, IRequest<ApiResponse<UpdateWorkShiftCommand.Response>>
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public string? Description { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
            public int Id { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class UpdateWorkShiftCommandValidator : AbstractValidator<UpdateWorkShiftCommand>
    {
        public UpdateWorkShiftCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(CoreResource.validation_required);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MaximumLength(100).WithMessage(string.Format(CoreResource.validation_maxLength, 100));

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage(CoreResource.validation_required);

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .Must((cmd, endTime) => endTime > cmd.StartTime)
                .WithMessage("End time must be after start time");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage(string.Format(CoreResource.validation_maxLength, 250))
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateWorkShiftCommandHandler : IRequestHandler<UpdateWorkShiftCommand, ApiResponse<UpdateWorkShiftCommand.Response>>
    {
        public async Task<ApiResponse<UpdateWorkShiftCommand.Response>> Handle(UpdateWorkShiftCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id),
                    new CoreParamModel(nameof(request.Name), request.Name)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var rowsAffected = await dbContext.ExecuteAsync(
                        @"UPDATE hr_work_shifts
                          SET Name = @Name,
                              StartTime = @StartTime,
                              EndTime = @EndTime,
                              Description = @Description,
                              UpdatedAt = GETDATE()
                          WHERE Id = @Id",
                        new
                        {
                            request.Id,
                            request.Name,
                            request.StartTime,
                            request.EndTime,
                            request.Description
                        },
                        ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.NotFound<UpdateWorkShiftCommand.Response>(CoreResource.common_notFound);
                    }

                    await dbContext.CommitAsync();

                    var responseData = new UpdateWorkShiftCommand.Response { Success = true, Id = request.Id };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_updateSuccess);

                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);
                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error updating work shift: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateWorkShiftCommand.Response>(CoreResource.common_exceptionOccurred);
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
