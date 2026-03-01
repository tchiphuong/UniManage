using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Salaries
{
    #region Command

    public sealed class UpdateSalaryCommand : BaseCommand, IRequest<ApiResponse<UpdateSalaryCommand.Response>>
    {
        public int Id { get; init; }
        public decimal SalaryAmount { get; init; }
        public byte[] DataRowVersion { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
            public int Id { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class UpdateSalaryCommandValidator : AbstractValidator<UpdateSalaryCommand>
    {
        public UpdateSalaryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(CoreResource.validation_required);

            RuleFor(x => x.SalaryAmount)
                .GreaterThan(0).WithMessage("Salary amount must be greater than 0");

            RuleFor(x => x.DataRowVersion)
                .NotEmpty().WithMessage(CoreResource.validation_required);
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateSalaryCommandHandler : IRequestHandler<UpdateSalaryCommand, ApiResponse<UpdateSalaryCommand.Response>>
    {
        public async Task<ApiResponse<UpdateSalaryCommand.Response>> Handle(UpdateSalaryCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id),
                    new CoreParamModel(nameof(request.SalaryAmount), request.SalaryAmount)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var currentSalary = await dbContext.QueryFirstOrDefaultAsync<dynamic>(
                        "SELECT SalaryAmount FROM hr_salaries WHERE Id = @Id",
                        new { request.Id },
                        ct);

                    if (currentSalary == null)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.NotFound<UpdateSalaryCommand.Response>(CoreResource.common_notFound);
                    }

                    var rowsAffected = await dbContext.ExecuteAsync(
                        @"UPDATE hr_salaries
                          SET SalaryAmount = @SalaryAmount,
                              UpdatedBy = @UpdatedBy,
                              UpdatedAt = GETDATE()
                          WHERE Id = @Id AND DataRowVersion = @DataRowVersion",
                        new
                        {
                            request.Id,
                            request.SalaryAmount,
                            UpdatedBy = request.HeaderInfo!.Username,
                            request.DataRowVersion
                        },
                        ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.Error<UpdateSalaryCommand.Response>("Salary has been modified by another user");
                    }

                    if (currentSalary.SalaryAmount != request.SalaryAmount)
                    {
                        await dbContext.ExecuteAsync(
                            @"UPDATE hr_salary_history SET ToDate = GETDATE() WHERE SalaryId = @SalaryId AND ToDate IS NULL;
                              INSERT INTO hr_salary_history (SalaryId, SalaryAmount, FromDate, CreatedBy, CreatedAt, DataRowVersion)
                              VALUES (@SalaryId, @SalaryAmount, GETDATE(), @CreatedBy, GETDATE(), 0x00000000000000000001)",
                            new
                            {
                                SalaryId = request.Id,
                                request.SalaryAmount,
                                CreatedBy = request.HeaderInfo!.Username
                            },
                            ct);
                    }

                    await dbContext.CommitAsync();

                    var responseData = new UpdateSalaryCommand.Response { Success = true, Id = request.Id };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_updateSuccess);

                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);
                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error updating salary: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateSalaryCommand.Response>(CoreResource.common_exceptionOccurred);
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
