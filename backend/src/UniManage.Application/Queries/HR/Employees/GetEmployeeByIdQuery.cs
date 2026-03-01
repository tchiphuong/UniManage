using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.Employees;

#region Query

/// <summary>
/// Query to get employee by id
/// </summary>
public sealed class GetEmployeeByIdQuery : BaseQuery, IRequest<ApiResponse<GetEmployeeByIdQuery.Response>>
{
    public long Id { get; init; }

    public sealed record Response
    {
        public string EmployeeCode { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? DepartmentCode { get; set; }
        public string? DepartmentName { get; set; }
        public string? PositionCode { get; set; }
        public string? PositionName { get; set; }
        public DateTime? JoinDate { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for GetEmployeeByIdQuery
/// </summary>
public sealed class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for GetEmployeeByIdQuery
/// </summary>
public sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, ApiResponse<GetEmployeeByIdQuery.Response>>
{
    public async Task<ApiResponse<GetEmployeeByIdQuery.Response>> Handle(GetEmployeeByIdQuery request, CancellationToken ct)
    {
        ApiResponse<GetEmployeeByIdQuery.Response> response;

        // Initialize log data
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
        logData.Parameter = new List<CoreParamModel>
        {
            new CoreParamModel(nameof(request.Id), request.Id.ToString())
        };

        using (DbContext dbContext = new DbContext())
        {
            try
            {
                var query = $@"
                    SELECT 
                        e.EmployeeCode   AS {nameof(GetEmployeeByIdQuery.Response.EmployeeCode)},
                        e.FullName       AS {nameof(GetEmployeeByIdQuery.Response.FullName)},
                        e.Email          AS {nameof(GetEmployeeByIdQuery.Response.Email)},
                        e.PhoneNumber    AS {nameof(GetEmployeeByIdQuery.Response.PhoneNumber)},
                        e.BirthDate      AS {nameof(GetEmployeeByIdQuery.Response.BirthDate)},
                        e.Gender         AS {nameof(GetEmployeeByIdQuery.Response.Gender)},
                        e.Address        AS {nameof(GetEmployeeByIdQuery.Response.Address)},
                        e.DepartmentCode AS {nameof(GetEmployeeByIdQuery.Response.DepartmentCode)},
                        d.NameVi           AS {nameof(GetEmployeeByIdQuery.Response.DepartmentName)},
                        e.PositionCode   AS {nameof(GetEmployeeByIdQuery.Response.PositionCode)},
                        p.NameVi           AS {nameof(GetEmployeeByIdQuery.Response.PositionName)},
                        e.JoinDate       AS {nameof(GetEmployeeByIdQuery.Response.JoinDate)},
                        e.CreatedAt      AS {nameof(GetEmployeeByIdQuery.Response.CreatedAt)},
                        e.UpdatedAt      AS {nameof(GetEmployeeByIdQuery.Response.UpdatedAt)}
                    FROM hr_employees e
                    LEFT JOIN hr_departments d ON e.DepartmentCode = d.Code
                    LEFT JOIN hr_positions p ON e.PositionCode = p.PositionCode
                    WHERE e.Id = @Id";

                var employee = await dbContext.QueryFirstOrDefaultAsync<GetEmployeeByIdQuery.Response>(
                    query,
                    new { request.Id },
                    ct);

                if (employee == null)
                {
                    response = ResponseHelper.NotFound<GetEmployeeByIdQuery.Response>("Employee not found");
                    logData.ReturnCode = response.ReturnCode;
                }
                else
                {
                    response = ResponseHelper.Success(employee, string.Format(CoreResource.crud_getSuccess, CoreResource.entity_employee));
                    logData.Result = new { employee.EmployeeCode };
                    logData.ReturnCode = response.ReturnCode;
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving employee by id {request.Id}: {ex.Message}", ex);
                response = ResponseHelper.Error<GetEmployeeByIdQuery.Response>(CoreResource.common_exceptionOccurred);

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
