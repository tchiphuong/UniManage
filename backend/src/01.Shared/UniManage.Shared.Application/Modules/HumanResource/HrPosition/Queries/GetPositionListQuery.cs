using System.Text;

namespace UniManage.Shared.Application.Modules.HrPosition.Queries;

#region Query

public sealed class GetPositionListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetPositionListQuery.Response>>>
{
    public sealed record Response
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string NameVi { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

#endregion

#region Validator

public sealed class GetPositionListQueryValidator : AbstractValidator<GetPositionListQuery>
{
    public GetPositionListQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}

#endregion

#region Handler

public sealed class GetPositionListQueryHandler : IRequestHandler<GetPositionListQuery, ApiResponse<PagedResult<GetPositionListQuery.Response>>>
{
    public async Task<ApiResponse<PagedResult<GetPositionListQuery.Response>>> Handle(GetPositionListQuery request, CancellationToken cancellationToken)
    {
        ApiLogModel logData = new ApiLogModel(request.HeaderInfo)
        {
            Parameter = new List<LogParamModel>
            {
                new LogParamModel(nameof(request.Keyword), request.Keyword)
            }
        };

        using (DbContext dbContext = new DbContext())
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine($@"SELECT 
                                        Id           AS {nameof(GetPositionListQuery.Response.Id)},
                                        PositionCode AS {nameof(GetPositionListQuery.Response.Code)},
                                        NameVi       AS {nameof(GetPositionListQuery.Response.NameVi)},
                                        NameEn       AS {nameof(GetPositionListQuery.Response.NameEn)},
                                        Description  AS {nameof(GetPositionListQuery.Response.Description)},
                                        1            AS {nameof(GetPositionListQuery.Response.Status)},
                                        CreatedAt    AS {nameof(GetPositionListQuery.Response.CreatedAt)}
                                    FROM HrPositions
                                    WHERE 1 = 1");

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query.AppendLine("AND (PositionCode LIKE @Keyword OR NameVi LIKE @Keyword OR NameEn LIKE @Keyword)");
                }

                var result = await dbContext.QueryPagingAsync<GetPositionListQuery.Response>(query, request);

                var response = ResponseHelper.Success(result, CoreResource.common_success);

                logData.Result = result;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving positions: {ex.Message}", ex);
                var response = ResponseHelper.Error<PagedResult<GetPositionListQuery.Response>>(CoreResource.common_exceptionOccurred);
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



