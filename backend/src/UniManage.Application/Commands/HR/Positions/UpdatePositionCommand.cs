using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Positions;

#region Command

public sealed class UpdatePositionCommand : BaseCommand, IRequest<ApiResponse<UpdatePositionCommand.Response>>
{
    public int Id { get; set; }
    public string PositionCode { get; init; } = default!;
    public string NameVi { get; init; } = default!;
    public string NameEn { get; init; } = default!;
    public string? Description { get; init; }

    public sealed class Response
    {
        public bool Success { get; init; }
        public int Id { get; init; }
    }
}

#endregion

#region Validator

public sealed class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
{
    public UpdatePositionCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.PositionCode).NotEmpty().MaximumLength(50);
        RuleFor(x => x.NameVi).NotEmpty().MaximumLength(200);
        RuleFor(x => x.NameEn).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}

#endregion

#region Handler

public sealed class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, ApiResponse<UpdatePositionCommand.Response>>
{
    public async Task<ApiResponse<UpdatePositionCommand.Response>> Handle(UpdatePositionCommand request, CancellationToken ct)
    {
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Id), request.Id),
                new CoreParamModel(nameof(request.PositionCode), request.PositionCode)
            }
        };

        using (DbContext dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var rowsAffected = await dbContext.ExecuteAsync(
                    @"UPDATE hr_positions 
                      SET PositionCode = @PositionCode, NameVi = @NameVi, NameEn = @NameEn, Description = @Description,
                          UpdatedBy = @UpdatedBy, UpdatedAt = GETDATE()
                      WHERE Id = @Id",
                    new
                    {
                        request.Id,
                        request.PositionCode,
                        request.NameVi,
                        request.NameEn,
                        request.Description,
                        UpdatedBy = request.HeaderInfo!.Username
                    },
                    ct);

                await dbContext.CommitAsync();

                if (rowsAffected == 0)
                {
                    return ResponseHelper.NotFound<UpdatePositionCommand.Response>(CoreResource.common_notFound);
                }

                var responseData = new UpdatePositionCommand.Response { Success = true, Id = request.Id };
                var response = ResponseHelper.Success(responseData, string.Format(CoreResource.crud_updateSuccess, CoreResource.entity_position));

                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);
                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync();
                UniLogger.Error($"Error updating position: {ex.Message}", ex);
                var response = ResponseHelper.Error<UpdatePositionCommand.Response>(CoreResource.common_exceptionOccurred);
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
