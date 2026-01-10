using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Positions;

#region Command

public sealed class CreatePositionCommand : BaseCommand, IRequest<ApiResponse<CreatePositionCommand.Response>>
{
    public string PositionCode { get; init; } = default!;
    public string NameVi { get; init; } = default!;
    public string NameEn { get; init; } = default!;
    public string? Description { get; init; }

    public sealed class Response
    {
        public bool Success => Id > 0;
        public int Id { get; init; }
        public string PositionCode { get; init; } = default!;
    }
}

#endregion

#region Validator

public sealed class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    public CreatePositionCommandValidator()
    {
        RuleFor(x => x.PositionCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
            .MaximumLength(50).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 50))
            .Matches("^[A-Z0-9]+$").WithMessage(CoreResource.Validation_msg_OnlyNumber)
            .MustAsync(async (code, cancel) => !await IsCodeExistsAsync(code))
            .WithMessage(CoreResource.Validation_msg_AlreadyExists);

        RuleFor(x => x.NameVi).NotEmpty().WithMessage(CoreResource.Validation_msg_Required).MaximumLength(200).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 200));
        RuleFor(x => x.NameEn).NotEmpty().WithMessage(CoreResource.Validation_msg_Required).MaximumLength(200).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 200));
        RuleFor(x => x.Description).MaximumLength(500).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 500));
    }

    private static async Task<bool> IsCodeExistsAsync(string code)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_positions WHERE PositionCode = @Code) THEN 1 ELSE 0 END",
                new { Code = code });
        }
    }
}

#endregion

#region Handler

public sealed class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, ApiResponse<CreatePositionCommand.Response>>
{
    public async Task<ApiResponse<CreatePositionCommand.Response>> Handle(CreatePositionCommand request, CancellationToken ct)
    {
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.PositionCode), request.PositionCode),
                new CoreParamModel(nameof(request.NameVi), request.NameVi)
            }
        };

        using (DbContext dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var id = await dbContext.ExecuteScalarAsync<int>(
                    @"INSERT INTO hr_positions (PositionCode, NameVi, NameEn, Description, CreatedBy, CreatedAt)
                      VALUES (@PositionCode, @NameVi, @NameEn, @Description, @CreatedBy, GETDATE());
                      SELECT SCOPE_IDENTITY();",
                    new
                    {
                        request.PositionCode,
                        request.NameVi,
                        request.NameEn,
                        request.Description,
                        CreatedBy = request.HeaderInfo!.Username
                    },
                    ct);

                await dbContext.CommitAsync();

                var response = ResponseHelper.Success(new CreatePositionCommand.Response { Id = id, PositionCode = request.PositionCode }, CoreResource.Position_msg_CreateSuccess);
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);
                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync();
                UniLogger.Error($"Error creating position: {ex.Message}", ex);
                var response = ResponseHelper.Error<CreatePositionCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
                logData.Message = ex.ToString();
                logData.IsException = 1;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);
                return response;
            }
        }
    }
}

#endregion
