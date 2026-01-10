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

public sealed class CreateUnitCommand : BaseCommand, IRequest<ApiResponse<CreateUnitCommand.Response>>
{
    public string Code { get; init; } = default!;
    public string NameVi { get; init; } = default!;
    public string NameEn { get; init; } = default!;

    public sealed class Response
    {
        public long Id { get; init; }
        public string Code { get; init; } = default!;
    }
}

#endregion

#region Validator

public sealed class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitCommandValidator()
    {
        RuleFor(x => x.Code).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Code is required").Length(1, 50).WithMessage("Code must be between 1 and 50 characters").Must(ValidationHelper.IsValidUserCode).WithMessage("Code allows only alphanumeric and underscore").MustAsync(async (code, cancel) => !await IsCodeExistsAsync(code)).WithMessage("Code already exists");

        RuleFor(x => x.NameVi).NotEmpty().WithMessage("Vietnamese name is required").Length(1, 100).WithMessage("Vietnamese name must be between 1 and 100 characters");

        RuleFor(x => x.NameEn).NotEmpty().WithMessage("English name is required").Length(1, 100).WithMessage("English name must be between 1 and 100 characters");
    }

    private static Task<bool> IsCodeExistsAsync(string code)
    {
        using (var dbContext = new DbContext())
        {
            return dbContext.ExecuteScalarAsync<bool>("SELECT CASE WHEN EXISTS(SELECT 1 FROM ms_units WHERE Code = @Code) THEN 1 ELSE 0 END", new { Code = code });
        }
    }
}

#endregion

#region Handler

public sealed class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, ApiResponse<CreateUnitCommand.Response>>
{
    public async Task<ApiResponse<CreateUnitCommand.Response>> Handle(CreateUnitCommand request, CancellationToken ct)
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
                var id = await dbContext.ExecuteScalarAsync<long>(@"INSERT INTO ms_units (Code, NameVi, NameEn, CreatedBy, CreatedAt)
                      VALUES (@Code, @NameVi, @NameEn, @CreatedBy, GETDATE());
                      SELECT SCOPE_IDENTITY();", new
                {
                    request.Code,
                    request.NameVi,
                    request.NameEn,
                    CreatedBy = request.HeaderInfo!.Username
                }, ct);

                await dbContext.transaction.CommitAsync(ct);

                var responseData = new CreateUnitCommand.Response { Id = id, Code = request.Code };
                var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_CreateSuccess);

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

                return ResponseHelper.Error<CreateUnitCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }
}

#endregion
