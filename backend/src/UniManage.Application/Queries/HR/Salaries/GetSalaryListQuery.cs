using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.Salaries
{
    #region Query

    public sealed class GetSalaryListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetSalaryListQuery.Response>>>
    {
        public string? EmployeeCode { get; set; }

        public sealed record Response
        {
            public int Id { get; set; }
            public string EmployeeCode { get; set; } = default!;
            public string EmployeeName { get; set; } = default!;
            public string? DepartmentName { get; set; }
            public string? PositionName { get; set; }
            public decimal SalaryAmount { get; set; }
            public string? CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetSalaryListQueryValidator : AbstractValidator<GetSalaryListQuery>
    {
        public GetSalaryListQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }

    #endregion

    #region Handler

    public sealed class GetSalaryListQueryHandler : IRequestHandler<GetSalaryListQuery, ApiResponse<PagedResult<GetSalaryListQuery.Response>>>
    {
        public async Task<ApiResponse<PagedResult<GetSalaryListQuery.Response>>> Handle(GetSalaryListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var isEnglish = request.HeaderInfo?.Language?.StartsWith("en", StringComparison.OrdinalIgnoreCase) == true;

                    var query = new StringBuilder();
                    query.AppendLine($@"SELECT 
                                        s.Id                   AS {nameof(GetSalaryListQuery.Response.Id)},
                                        s.EmployeeCode         AS {nameof(GetSalaryListQuery.Response.EmployeeCode)},
                                        e.FullName             AS {nameof(GetSalaryListQuery.Response.EmployeeName)},
                                        {(isEnglish ? "d.NameEn" : "d.NameVi")} AS {nameof(GetSalaryListQuery.Response.DepartmentName)},
                                        {(isEnglish ? "p.NameEn" : "p.NameVi")} AS {nameof(GetSalaryListQuery.Response.PositionName)},
                                        s.SalaryAmount         AS {nameof(GetSalaryListQuery.Response.SalaryAmount)},
                                        s.CreatedBy            AS {nameof(GetSalaryListQuery.Response.CreatedBy)},
                                        s.CreatedAt            AS {nameof(GetSalaryListQuery.Response.CreatedAt)},
                                        s.UpdatedAt            AS {nameof(GetSalaryListQuery.Response.UpdatedAt)}
                                    FROM hr_salaries s
                                    INNER JOIN hr_employees e ON s.EmployeeCode = e.EmployeeCode
                                    LEFT JOIN hr_departments d ON e.DepartmentCode = d.Code
                                    LEFT JOIN hr_positions p ON e.PositionCode = p.Code
                                    WHERE 1 = 1");

                    if (!string.IsNullOrEmpty(request.EmployeeCode))
                    {
                        query.AppendLine("AND s.EmployeeCode = @EmployeeCode");
                    }

                    var result = await dbContext.QueryPagingAsync<GetSalaryListQuery.Response>(query, request);

                    var response = ResponseHelper.Success(result, CoreResource.crud_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving salaries: {ex.Message}", ex);
                    var response = ResponseHelper.Error<PagedResult<GetSalaryListQuery.Response>>(CoreResource.common_exceptionOccurred);

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
