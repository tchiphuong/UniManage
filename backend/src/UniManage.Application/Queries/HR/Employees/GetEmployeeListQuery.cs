using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.Employees;



/// <summary>
/// Query to get paginated list of employees
/// </summary>
#region Query

/// <summary>
/// Query to get paginated list of employees
/// </summary>
public sealed class GetEmployeeListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetEmployeeListQuery.Response>>>
{
    public string? DepartmentCode { get; init; }
    public string? PositionCode { get; init; }
    public byte? Status { get; init; }

    public sealed record Response
    {
        public string EmployeeCode { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DepartmentCode { get; set; }
        public string? DepartmentName { get; set; }
        public string? PositionCode { get; set; }
        public string? PositionName { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for ListEmployeesQuery
/// </summary>
public sealed class GetEmployeeListQueryValidator : AbstractValidator<GetEmployeeListQuery>
{
    public GetEmployeeListQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("Page index must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");

        RuleFor(x => x.Status)
            .InclusiveBetween((byte)0, (byte)1).When(x => x.Status.HasValue)
            .WithMessage("Status must be 0 or 1");
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for ListEmployeesQuery
/// </summary>
public sealed class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, ApiResponse<PagedResult<GetEmployeeListQuery.Response>>>
{
    public async Task<ApiResponse<PagedResult<GetEmployeeListQuery.Response>>> Handle(GetEmployeeListQuery request, CancellationToken ct)
    {
        ApiResponse<PagedResult<GetEmployeeListQuery.Response>> response;

        // Initialize log data
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
        logData.Parameter = new List<CoreParamModel>
        {
            new CoreParamModel(nameof(request.Keyword), request.Keyword),
            new CoreParamModel(nameof(request.SearchFields), request.SearchFields),
            new CoreParamModel(nameof(request.DepartmentCode), request.DepartmentCode),
            new CoreParamModel(nameof(request.PositionCode), request.PositionCode)
        };

        using (DbContext dbContext = new DbContext())
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine($@"SELECT 
                                        e.EmployeeCode     AS {nameof(GetEmployeeListQuery.Response.EmployeeCode)},
                                        e.FullName         AS {nameof(GetEmployeeListQuery.Response.FullName)},
                                        e.Email            AS {nameof(GetEmployeeListQuery.Response.Email)},
                                        e.PhoneNumber      AS {nameof(GetEmployeeListQuery.Response.PhoneNumber)},
                                        e.DepartmentCode   AS {nameof(GetEmployeeListQuery.Response.DepartmentCode)},
                                        d.NameVi           AS {nameof(GetEmployeeListQuery.Response.DepartmentName)},
                                        e.PositionCode     AS {nameof(GetEmployeeListQuery.Response.PositionCode)},
                                        p.NameVi           AS {nameof(GetEmployeeListQuery.Response.PositionName)},
                                        e.Status           AS {nameof(GetEmployeeListQuery.Response.Status)},
                                        e.CreatedAt        AS {nameof(GetEmployeeListQuery.Response.CreatedAt)}
                                    FROM hr_employees e
                                    LEFT JOIN hr_departments d ON e.DepartmentCode = d.Code
                                    LEFT JOIN hr_positions p ON e.PositionCode = p.PositionCode
                                    WHERE 1 = 1");

                if (!string.IsNullOrEmpty(request.DepartmentCode))
                {
                    query.AppendLine("AND e.DepartmentCode = @DepartmentCode");
                }

                if (!string.IsNullOrEmpty(request.PositionCode))
                {
                    query.AppendLine("AND e.PositionCode = @PositionCode");
                }

                var result = await dbContext.QueryPagingAsync<GetEmployeeListQuery.Response>(query, request);

                response = ResponseHelper.Success(result, CoreResource.Employee_msg_ListSuccess);

                logData.Result = result;
                logData.ReturnCode = response.ReturnCode;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving employees list: {ex.Message}", ex);
                response = ResponseHelper.Error<PagedResult<GetEmployeeListQuery.Response>>(CoreResource.Common_msg_ExceptionOccurred);

                logData.Message = ex.ToString();
                logData.IsException = 1;
                logData.ReturnCode = response.ReturnCode;
            }
        }

        UniLogManager.WriteApiLog(logData);

        return response;
    }
}

#endregion