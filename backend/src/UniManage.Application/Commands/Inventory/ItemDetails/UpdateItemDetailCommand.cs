using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Inventory.ItemDetails
{
    public sealed class UpdateItemDetailCommand : BaseCommand, IRequest<ApiResponse<UpdateItemDetailCommand.Response>>
    {
        public long Id { get; init; }
        public int? Type { get; init; }
        public string Key { get; init; } = default!;
        public string ValueVi { get; init; } = default!;
        public string ValueEn { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateItemDetailCommandValidator : AbstractValidator<UpdateItemDetailCommand>
    {
        public UpdateItemDetailCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Key)
                .NotEmpty().WithMessage("Key is required")
                .Length(1, 50).WithMessage("Key must be between 1 and 50 characters");

            RuleFor(x => x.ValueVi)
                .NotEmpty().WithMessage("Vietnamese value is required")
                .Length(1, 50).WithMessage("Vietnamese value must be between 1 and 50 characters");

            RuleFor(x => x.ValueEn)
                .NotEmpty().WithMessage("English value is required")
                .Length(1, 50).WithMessage("English value must be between 1 and 50 characters");
        }
    }

    public sealed class UpdateItemDetailCommandHandler : IRequestHandler<UpdateItemDetailCommand, ApiResponse<UpdateItemDetailCommand.Response>>
    {
        public async Task<ApiResponse<UpdateItemDetailCommand.Response>> Handle(UpdateItemDetailCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id.ToString()),
                    new CoreParamModel(nameof(request.Key), request.Key),
                    new CoreParamModel(nameof(request.Type), request.Type?.ToString())
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        UPDATE it_item_details
                        SET Type = @Type,
                            [Key] = @Key,
                            ValueVi = @ValueVi,
                            ValueEn = @ValueEn,
                            UpdateBy = @UpdateBy,
                            UpdateOn = GETDATE()
                        WHERE Id = @Id";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Id,
                        request.Type,
                        request.Key,
                        request.ValueVi,
                        request.ValueEn,
                        UpdateBy = request.HeaderInfo!.Username
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateItemDetailCommand.Response>("Item detail not found");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateItemDetailCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_UpdateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error updating item detail: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateItemDetailCommand.Response>("Error occurred while updating item detail");

                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}
