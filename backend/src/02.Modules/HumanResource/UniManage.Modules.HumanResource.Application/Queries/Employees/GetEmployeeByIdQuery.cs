using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Queries.Employees;

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
    public async Task<ApiResponse<GetEmployeeByIdQuery.Response>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        ApiResponse<GetEmployeeByIdQuery.Response> response;

        // Initialize log data
        ApiLogModel logData = new ApiLogModel(request.HeaderInfo);
        logData.Parameter = new List<LogParamModel>
        {
            new LogParamModel(nameof(request.Id), request.Id.ToString())
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
                    FROM HrEmployees e
                    LEFT JOIN HrDepartments d ON e.DepartmentCode = d.Code
                    LEFT JOIN HrPositions p ON e.PositionCode = p.PositionCode
                    WHERE e.Id = @Id";

                var employee = await dbContext.QueryFirstOrDefaultAsync<GetEmployeeByIdQuery.Response>(
                    query,
                    new { request.Id },
                    cancellationToken);

                if (employee == null)
                {
                    response = ResponseHelper.NotFound<GetEmployeeByIdQuery.Response>("Employee not found");
                    logData.ReturnCode = response.ReturnCode;
                }
                else
                {
                    response = ResponseHelper.Success(employee, string.Format(CoreResource.common_getSuccess, CoreResource.entity_employee));
                    
                    logData.Result = employee;
                    logData.ReturnCode = response.ReturnCode;
                    logData.Message = "Get employee by ID success";
                }
            }
            catch (Exception ex)
            {
                logData.IsException = true;
                logData.Message = ex.Message;
                logData.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                response = ResponseHelper.Error<GetEmployeeByIdQuery.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(logData);
            }

            return response;
        }
    }
}

#endregion



