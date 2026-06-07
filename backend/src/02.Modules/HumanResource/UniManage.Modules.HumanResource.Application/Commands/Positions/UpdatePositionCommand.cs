using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.HumanResource.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.HumanResource.Application.Commands.Positions;

#region Command

public sealed class UpdatePositionCommand : BaseCommand, IRequest<ApiResponse<UpdatePositionCommand.Response>>
{
    public int Id { get; set; }
    public string PositionCode { get; init; } = default!;
    public string NameVi { get; init; } = default!;
    public string NameEn { get; init; } = default!;
    public string? Description { get; init; }
    public byte[] DataRowVersion { get; init; } = default!;

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
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_required, "Id"))
            .MustAsync(async (id, cancel) => await IsPositionExistsByIdAsync(id))
            .WithMessage(string.Format(CoreResource.validation_notFound, CoreResource.entity_position));

        RuleFor(x => x.PositionCode)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_positionCode))
            .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_positionCode, 50));

        RuleFor(x => x.NameVi)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name_vi))
            .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_name_vi, 200));

        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name_en))
            .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_name_en, 200));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.common_description, 500));

        RuleFor(x => x.DataRowVersion)
            .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "DataRowVersion"));
    }

    private static async Task<bool> IsPositionExistsByIdAsync(int id)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.Set<HrPositions>().AnyAsync(x => x.Id == id);
        }
    }
}

#endregion

#region Handler

public sealed class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, ApiResponse<UpdatePositionCommand.Response>>
{
    public async Task<ApiResponse<UpdatePositionCommand.Response>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var log = new ApiLogModel(request.HeaderInfo)
        {
            Parameters = new List<LogParamModel>
            {
                new(nameof(request.Id), request.Id),
                new(nameof(request.PositionCode), request.PositionCode),
                new(nameof(request.NameVi), request.NameVi),
                new(nameof(request.NameEn), request.NameEn),
                new(nameof(request.DataRowVersion), request.DataRowVersion != null ? Convert.ToBase64String(request.DataRowVersion) : string.Empty)
            }
        };

        try
        {
            using var dbContext = new DbContext(openTransaction: true);

            var position = await dbContext.Set<HrPositions>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (position == null)
            {
                var notFoundResponse = ResponseHelper.NotFound<UpdatePositionCommand.Response>(CoreResource.common_notFound);
                log.Message = notFoundResponse.Message;
                log.ReturnCode = notFoundResponse.ReturnCode;
                return notFoundResponse;
            }

            // Concurrency check
            if (request.DataRowVersion != null && !position.DataRowVersion.SequenceEqual(request.DataRowVersion))
            {
                var concurrencyResponse = ResponseHelper.Error<UpdatePositionCommand.Response>(CoreResource.common_concurrencyError);
                log.Message = concurrencyResponse.Message;
                log.ReturnCode = concurrencyResponse.ReturnCode;
                return concurrencyResponse;
            }

            position.Code = request.PositionCode;
            position.NameVi = request.NameVi;
            position.NameEn = request.NameEn;
            position.Description = request.Description ?? string.Empty;
            position.UpdatedBy = request.HeaderInfo!.Username;
            position.UpdatedAt = DateTimeHelper.Now;

            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.CommitAsync();

            var responseData = new UpdatePositionCommand.Response { Success = true, Id = request.Id };
            var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_position));
            
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





