using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.Salaries
{
    #region Query

    public sealed class GetSalaryByIdQuery : BaseQuery, IRequest<ApiResponse<GetSalaryByIdQuery.Response>>
    {
        public int Id { get; init; }

        public sealed record Response
        {
            public int Id { get; set; }
            public string EmployeeCode { get; set; } = default!;
            public string EmployeeName { get; set; } = default!;
            public decimal SalaryAmount { get; set; }
            public string? CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
            public string? UpdatedBy { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public byte[] DataRowVersion { get; set; } = default!;
        }
    }

    #endregion

    #region Validator

    public sealed class GetSalaryByIdQueryValidator : AbstractValidator<GetSalaryByIdQuery>
    {
        public GetSalaryByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(CoreResource.validation_required);
        }
    }

    #endregion

    #region Handler

    public sealed class GetSalaryByIdQueryHandler : IRequestHandler<GetSalaryByIdQuery, ApiResponse<GetSalaryByIdQuery.Response>>
    {
        public async Task<ApiResponse<GetSalaryByIdQuery.Response>> Handle(GetSalaryByIdQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Id), request.Id)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var salary = await dbContext.QueryFirstOrDefaultAsync<GetSalaryByIdQuery.Response>(
                        @"SELECT s.Id, s.EmployeeCode, e.FullName AS EmployeeName, s.SalaryAmount,
                                 s.CreatedBy, s.CreatedAt, s.UpdatedBy, s.UpdatedAt, s.DataRowVersion
                          FROM hr_salaries s
                          INNER JOIN hr_employees e ON s.EmployeeCode = e.EmployeeCode
                          WHERE s.Id = @Id",
                        new { request.Id },
                        ct);

                    if (salary == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetSalaryByIdQuery.Response>(CoreResource.common_notFound);
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(salary, CoreResource.crud_getSuccess);
                    log.Result = salary;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error getting salary: {ex.Message}", ex);
                    var response = ResponseHelper.Error<GetSalaryByIdQuery.Response>(CoreResource.common_exceptionOccurred);

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
