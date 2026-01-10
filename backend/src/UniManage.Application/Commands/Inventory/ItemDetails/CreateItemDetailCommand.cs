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
    public sealed class CreateItemDetailCommand : BaseCommand, IRequest<ApiResponse<CreateItemDetailCommand.Response>>
    {
        public string ItemCode { get; init; } = default!;
        public int? Type { get; init; }
        public string Key { get; init; } = default!;
        public string ValueVi { get; init; } = default!;
        public string ValueEn { get; init; } = default!;

        public sealed class Response
        {
            public long Id { get; init; }
        }
    }

    public sealed class CreateItemDetailCommandValidator : AbstractValidator<CreateItemDetailCommand>
    {
        public CreateItemDetailCommandValidator()
        {
            RuleFor(x => x.ItemCode)
                .NotEmpty().WithMessage("Item code is required")
                .MustAsync(async (code, cancel) => await IsItemExistsAsync(code))
                .WithMessage("Item does not exist");

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

        private static async Task<bool> IsItemExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM it_items WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }
    }

    public sealed class CreateItemDetailCommandHandler : IRequestHandler<CreateItemDetailCommand, ApiResponse<CreateItemDetailCommand.Response>>
    {
        public async Task<ApiResponse<CreateItemDetailCommand.Response>> Handle(CreateItemDetailCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.ItemCode), request.ItemCode),
                    new CoreParamModel(nameof(request.Key), request.Key),
                    new CoreParamModel(nameof(request.Type), request.Type?.ToString())
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        INSERT INTO it_item_details (ItemCode, Type, [Key], ValueVi, ValueEn, InsertBy, InsertOn)
                        VALUES (@ItemCode, @Type, @Key, @ValueVi, @ValueEn, @InsertBy, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<long>(sql, new
                    {
                        request.ItemCode,
                        request.Type,
                        request.Key,
                        request.ValueVi,
                        request.ValueEn,
                        InsertBy = request.HeaderInfo!.Username
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateItemDetailCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_CreateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error creating item detail: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateItemDetailCommand.Response>("Error occurred while creating item detail");

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
