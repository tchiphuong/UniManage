using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Master.Units;

#region Command

public sealed class UpdateUnitCommand : BaseCommand, IRequest<ApiResponse<UpdateUnitCommand.Response>>
{
    public string Code { get; set; } = default!;
    public string NameVi { get; init; } = default!;
    public string NameEn { get; init; } = default!;
    public byte[] DataRowVersion { get; init; } = default!;

    public sealed class Response
    {
        public bool Success { get; init; }
    }
}

#endregion

#region Validator

public sealed class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");

        RuleFor(x => x.NameVi).NotEmpty().WithMessage("Vietnamese name is required").Length(1, 100).WithMessage("Vietnamese name must be between 1 and 100 characters");

        RuleFor(x => x.NameEn).NotEmpty().WithMessage("English name is required").Length(1, 100).WithMessage("English name must be between 1 and 100 characters");

        RuleFor(x => x.DataRowVersion).NotEmpty().WithMessage("DataRowVersion is required");
    }
}

#endregion

#region Handler

public sealed class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, ApiResponse<UpdateUnitCommand.Response>>
{
    public async Task<ApiResponse<UpdateUnitCommand.Response>> Handle(UpdateUnitCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Code), request.Code),
                new CoreParamModel(nameof(request.NameVi), request.NameVi),
                new CoreParamModel(nameof(request.NameEn), request.NameEn)
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var rows = await dbContext.ExecuteAsync(@"UPDATE ms_units
                        SET NameVi = @NameVi,
                            NameEn = @NameEn,
                            UpdatedBy = @UpdatedBy,
                            UpdatedAt = GETDATE()
                        WHERE Code = @Code
                        AND DataRowVersion = @DataRowVersion", new
                {
                    request.Code,
                    request.NameVi,
                    request.NameEn,
                    UpdatedBy = request.HeaderInfo!.Username,
                    request.DataRowVersion
                }, ct);

                if (rows == 0)
                {
                    await dbContext.transaction.RollbackAsync(ct);

                    var notFoundResponse = ResponseHelper.NotFound<UpdateUnitCommand.Response>(CoreResource.common_notFound);
                    log.Result = notFoundResponse;
                    log.ReturnCode = notFoundResponse.ReturnCode;
                    UniLogManager.WriteApiLog(log);
                    return notFoundResponse;
                }

                await dbContext.transaction.CommitAsync(ct);

                var responseData = new UpdateUnitCommand.Response { Success = true };
                var response = ResponseHelper.Success(responseData, CoreResource.crud_updateSuccess);

                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.transaction.RollbackAsync(ct);

                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                return ResponseHelper.Error<UpdateUnitCommand.Response>(CoreResource.common_exceptionOccurred);
            }
        }
    }
}

#endregion
