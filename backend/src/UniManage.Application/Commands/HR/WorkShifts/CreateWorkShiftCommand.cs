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

    public sealed class CreateWorkShiftCommand : BaseCommand, IRequest<ApiResponse<CreateWorkShiftCommand.Response>>
    {
        public string Code { get; init; } = default!;
        public string Name { get; init; } = default!;
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public string? Description { get; init; }

        public sealed class Response
        {
            public bool Success => Id > 0;
            public int Id { get; init; }
            public string Code { get; init; } = default!;
        }
    }

    #endregion

    #region Validator

    public sealed class CreateWorkShiftCommandValidator : AbstractValidator<CreateWorkShiftCommand>
    {
        public CreateWorkShiftCommandValidator()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
                .MaximumLength(20).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 20))
                .MustAsync(async (code, cancel) => !await IsCodeExistsAsync(code))
                .WithMessage("Work shift code already exists");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
                .MaximumLength(100).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 100));

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required);

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
                .Must((cmd, endTime) => endTime > cmd.StartTime)
                .WithMessage("End time must be after start time");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 250))
                .When(x => !string.IsNullOrEmpty(x.Description));
        }

        private static async Task<bool> IsCodeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_work_shifts WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }
    }

    #endregion

    #region Handler

    public sealed class CreateWorkShiftCommandHandler : IRequestHandler<CreateWorkShiftCommand, ApiResponse<CreateWorkShiftCommand.Response>>
    {
        public async Task<ApiResponse<CreateWorkShiftCommand.Response>> Handle(CreateWorkShiftCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Code), request.Code),
                    new CoreParamModel(nameof(request.Name), request.Name)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var id = await dbContext.ExecuteScalarAsync<int>(
                        @"INSERT INTO hr_work_shifts (Code, Name, StartTime, EndTime, Description, CreatedAt)
                          VALUES (@Code, @Name, @StartTime, @EndTime, @Description, GETDATE());
                          SELECT SCOPE_IDENTITY();",
                        new
                        {
                            request.Code,
                            request.Name,
                            request.StartTime,
                            request.EndTime,
                            request.Description
                        },
                        ct);

                    await dbContext.CommitAsync();

                    var response = ResponseHelper.Success(new CreateWorkShiftCommand.Response { Id = id, Code = request.Code }, CoreResource.Common_msg_CreateSuccess);

                    log.Result = response.Data;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error creating work shift: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateWorkShiftCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
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
