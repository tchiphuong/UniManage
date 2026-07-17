namespace UniManage.Shared.Application.Modules.HrDepartment.Queries;

#region Query

/// <summary>
/// Query to get department by id
/// </summary>
public sealed class GetDepartmentByIdQuery : BaseQuery, IRequest<ApiResponse<GetDepartmentByIdQuery.Response>>
{
    public int Id { get; init; }

    public sealed record Response
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string NameVi { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for GetDepartmentByIdQuery
/// </summary>
public sealed class GetDepartmentByIdQueryValidator : AbstractValidator<GetDepartmentByIdQuery>
{
    public GetDepartmentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for GetDepartmentByIdQuery
/// </summary>
public sealed class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, ApiResponse<GetDepartmentByIdQuery.Response>>
{
    public async Task<ApiResponse<GetDepartmentByIdQuery.Response>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        ApiLogModel logData = new ApiLogModel(request.HeaderInfo)
        {
            Parameter = new List<LogParamModel>
            {
                new LogParamModel(nameof(request.Id), request.Id)
            }
        };

        using (DbContext dbContext = new DbContext())
        {
            try
            {
                var department = await dbContext.QueryFirstOrDefaultAsync<GetDepartmentByIdQuery.Response>(
                    @"SELECT 
                        Id,
                        Code,
                        NameVi,
                        NameEn,
                        Description,
                        1 AS Status,
                        CreatedAt,
                        UpdatedAt
                    FROM HrDepartments
                    WHERE Id = @Id",
                    new { request.Id },
                    cancellationToken: cancellationToken);

                if (department == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<GetDepartmentByIdQuery.Response>(CoreResource.common_notFound);
                    logData.ReturnCode = notFoundResponse.ReturnCode;
                    logData.Message = notFoundResponse.Message;
                    UniLogManager.WriteApiLog(logData);
                    return notFoundResponse;
                }

                var response = ResponseHelper.Success(department, CoreResource.common_success);
                logData.Result = department;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving department: {ex.Message}", ex);
                var response = ResponseHelper.Error<GetDepartmentByIdQuery.Response>(CoreResource.common_exceptionOccurred);
                logData.Message = ex.ToString();
                logData.IsException = true;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);
                return response;
            }
        }
    }
}

#endregion


