using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Commands.Salaries
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
        public async Task<ApiResponse<UpdateSalaryCommand.Response>> Handle(UpdateSalaryCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var currentSalary = await dbContext.QueryFirstOrDefaultAsync<dynamic>(
                        "SELECT SalaryAmount FROM HrSalaries WHERE Id = @Id",
                        new { request.Id },
                        cancellationToken);

                    if (currentSalary == null)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.NotFound<UpdateSalaryCommand.Response>(CoreResource.common_notFound);
                    }

                    var rowsAffected = await dbContext.ExecuteAsync(
                        @"UPDATE HrSalaries
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
                        cancellationToken);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync();
                        return ResponseHelper.Error<UpdateSalaryCommand.Response>("Salary has been modified by another user");
                    }

                    if (currentSalary.SalaryAmount != request.SalaryAmount)
                    {
                        await dbContext.ExecuteAsync(
                            @"UPDATE HrSalaryHistory SET ToDate = GETDATE() WHERE SalaryId = @SalaryId AND ToDate IS NULL;
                              INSERT INTO HrSalaryHistory (SalaryId, SalaryAmount, FromDate, CreatedBy, CreatedAt, DataRowVersion)
                              VALUES (@SalaryId, @SalaryAmount, GETDATE(), @CreatedBy, GETDATE(), 0x00000000000000000001)",
                            new
                            {
                                SalaryId = request.Id,
                                request.SalaryAmount,
                                CreatedBy = request.HeaderInfo!.Username
                            },
                            cancellationToken);
                    }

                    

                    var responseData = new UpdateSalaryCommand.Response { Success = true, Id = request.Id };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }

    #endregion
}





