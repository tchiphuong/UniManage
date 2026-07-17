using System.Text;

namespace UniManage.Shared.Application.Modules.HrEmployee.Queries
{



    /// <summary>
    /// Query to get paginated list of employees
    /// </summary>
    #region Query

    /// <summary>
    /// Query to get paginated list of employees
    /// </summary>
    public sealed class GetEmployeeListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetEmployeeListQuery.Response>>>
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
    /// <summary>
    /// Validator for ListEmployeesQuery
    /// </summary>
    public sealed class GetEmployeeListQueryValidator : AbstractValidator<GetEmployeeListQuery>
    {
        public GetEmployeeListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_invalidFormat, CoreResource.lbl_pageIndex));

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_pageSize, 100));

            RuleFor(x => x.Status)
                .InclusiveBetween((byte)0, (byte)1).When(x => x.Status.HasValue)
                .WithMessage(string.Format(CoreResource.validation_invalidStatus, CoreResource.lbl_status));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for ListEmployeesQuery
    /// </summary>
    public sealed class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, ApiResponse<PagedResult<GetEmployeeListQuery.Response>>>
    {
        public async Task<ApiResponse<PagedResult<GetEmployeeListQuery.Response>>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            // X?c d?nh c?t da ng?n ng? cho t?n ph?ng ban v? ch?c v?
            var suffix = TranslateHelper.GetLanguageSuffix(request.HeaderInfo?.Language);
            var deptNameCol = $"d.Name{suffix}";
            var posNameCol = $"p.Name{suffix}";

            // Initialize log data
            ApiLogModel logData = new ApiLogModel(request.HeaderInfo);
            logData.Parameter = new List<LogParamModel>
        {
            new(nameof(request.Keyword), request.Keyword),
            new(nameof(request.DepartmentCode), request.DepartmentCode),
            new(nameof(request.PositionCode), request.PositionCode)
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
                                        {deptNameCol}      AS {nameof(GetEmployeeListQuery.Response.DepartmentName)},
                                        e.PositionCode     AS {nameof(GetEmployeeListQuery.Response.PositionCode)},
                                        {posNameCol}      AS {nameof(GetEmployeeListQuery.Response.PositionName)},
                                        e.Status           AS {nameof(GetEmployeeListQuery.Response.Status)},
                                        e.CreatedAt        AS {nameof(GetEmployeeListQuery.Response.CreatedAt)}
                                    FROM HrEmployees e
                                    LEFT JOIN HrDepartments d ON e.DepartmentCode = d.Code
                                    LEFT JOIN HrPositions p ON e.PositionCode = p.Code
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

                    var response = ResponseHelper.Success(result, string.Format(CoreResource.common_listSuccess, CoreResource.entity_employee));

                    logData.Result = result;
                    logData.ReturnCode = response.ReturnCode;
                    logData.Message = response.Message;

                    return response;
                }
                catch (Exception ex)
                {
                    logData.Message = ex.Message;
                    logData.IsException = true;
                    logData.ReturnCode = CoreApiReturnCode.ExceptionOccurred;

                    return ResponseHelper.Error<PagedResult<GetEmployeeListQuery.Response>>(CoreResource.common_error);
                }
                finally
                {
                    UniLogManager.WriteApiLog(logData);
                }
            }
        }
    }

    #endregion


}