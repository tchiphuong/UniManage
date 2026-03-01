using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Departments;

#region Command

public sealed class UpdateDepartmentCommand : BaseCommand, IRequest<ApiResponse<UpdateDepartmentCommand.Response>>
{
    public int Id { get; set; }
    public string Code { get; init; } = default!;
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

public sealed class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.NameVi).NotEmpty().MaximumLength(200);
        RuleFor(x => x.NameEn).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}

#endregion

#region Handler

public sealed class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ApiResponse<UpdateDepartmentCommand.Response>>
{
    public async Task<ApiResponse<UpdateDepartmentCommand.Response>> Handle(UpdateDepartmentCommand request, CancellationToken ct)
    {
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Id), request.Id),
                new CoreParamModel(nameof(request.Code), request.Code)
            }
        };

        using (DbContext dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var rowsAffected = await dbContext.ExecuteAsync(
                    @"UPDATE hr_departments 
                      SET Code = @Code, NameVi = @NameVi, NameEn = @NameEn, Description = @Description,
                          UpdatedBy = @UpdatedBy, UpdatedAt = GETDATE()
                      WHERE Id = @Id",
                    new
                    {
                        request.Id,
                        request.Code,
                        request.NameVi,
                        request.NameEn,
                        request.Description,
                        UpdatedBy = request.HeaderInfo!.Username
                    },
                    ct);

                await dbContext.CommitAsync();

                if (rowsAffected == 0)
                {
                    return ResponseHelper.NotFound<UpdateDepartmentCommand.Response>(CoreResource.common_notFound);
                }

                var responseData = new UpdateDepartmentCommand.Response { Success = true, Id = request.Id };
                var response = ResponseHelper.Success(responseData, string.Format(CoreResource.crud_updateSuccess, CoreResource.entity_department));

                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);
                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync();
                UniLogger.Error($"Error updating department: {ex.Message}", ex);
                var response = ResponseHelper.Error<UpdateDepartmentCommand.Response>(CoreResource.common_exceptionOccurred);
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
