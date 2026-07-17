namespace UniManage.Shared.Application.Modules.HrWorkShift.Commands
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
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_required, "Id"))
                .MustAsync(async (id, cancel) => await IsWorkShiftExistsByIdAsync(id))
                .WithMessage(string.Format(CoreResource.validation_notFound, CoreResource.entity_workShift));

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name))
                .MaximumLength(100).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_name, 100));

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_startTime));

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_endTime))
                .Must((cmd, endTime) => endTime > cmd.StartTime)
                .WithMessage(CoreResource.validation_invalidWorkShiftTime);

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.common_description, 500));
        }

        private static async Task<bool> IsWorkShiftExistsByIdAsync(int id)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<HrWorkShifts>().AnyAsync(x => x.Id == id);
            }
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateWorkShiftCommandHandler : IRequestHandler<UpdateWorkShiftCommand, ApiResponse<UpdateWorkShiftCommand.Response>>
    {
        public async Task<ApiResponse<UpdateWorkShiftCommand.Response>> Handle(UpdateWorkShiftCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameters = new List<LogParamModel>
                {
                    new(nameof(request.Id), request.Id),
                    new(nameof(request.Name), request.Name),
                    new(nameof(request.StartTime), request.StartTime.ToString()),
                    new(nameof(request.EndTime), request.EndTime.ToString())
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var workShift = await dbContext.Set<HrWorkShifts>()
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (workShift == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<UpdateWorkShiftCommand.Response>(CoreResource.common_notFound);
                    log.Message = notFoundResponse.Message;
                    log.ReturnCode = notFoundResponse.ReturnCode;
                    return notFoundResponse;
                }

                workShift.Name = request.Name;
                workShift.StartTime = request.StartTime;
                workShift.EndTime = request.EndTime;
                workShift.Description = request.Description ?? string.Empty;
                workShift.UpdatedAt = DateTimeHelper.Now;

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                var responseData = new UpdateWorkShiftCommand.Response { Success = true, Id = request.Id };
                var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);

                log.Result = responseData;
                log.Message = response.Message;
                log.ReturnCode = response.ReturnCode;

                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                throw;
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




