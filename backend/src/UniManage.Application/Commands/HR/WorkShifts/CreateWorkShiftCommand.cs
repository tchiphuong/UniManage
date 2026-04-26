using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using DbContext = UniManage.Core.Database.DbContext;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using UniManage.Core.Constant;

namespace UniManage.Application.Commands.HR.WorkShifts;

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
            .NotEmpty().WithMessage(CoreResource.validation_required)
            .MaximumLength(20).WithMessage(string.Format(CoreResource.validation_maxLength, 20))
            .MustAsync(async (code, cancel) => !await IsCodeExistsAsync(code))
            .WithMessage("Work shift code already exists");

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

    private static async Task<bool> IsCodeExistsAsync(string code)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.Set<hr_work_shifts>().AnyAsync(x => x.Code == code);
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
            Parameters = new List<CoreParamModel>
            {
                new(nameof(request.Code), request.Code),
                new(nameof(request.Name), request.Name),
                new(nameof(request.StartTime), request.StartTime.ToString()),
                new(nameof(request.EndTime), request.EndTime.ToString())
            }
        };

        try
        {
            using var dbContext = new DbContext(openTransaction: true);

            var workShift = new hr_work_shifts
            {
                Code = request.Code,
                Name = request.Name,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Description = request.Description ?? string.Empty,
                CreatedAt = DateTimeHelper.Now
            };

            dbContext.Set<hr_work_shifts>().Add(workShift);
            await dbContext.SaveChangesAsync(ct);
            await dbContext.CommitAsync();

            var responseData = new CreateWorkShiftCommand.Response { Id = workShift.Id, Code = workShift.Code };
            var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);

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


