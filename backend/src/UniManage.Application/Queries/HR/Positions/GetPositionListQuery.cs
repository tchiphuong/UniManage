using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.Positions;

#region Query

public sealed class GetPositionListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetPositionListQuery.Response>>>
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
    public async Task<ApiResponse<PagedResult<GetPositionListQuery.Response>>> Handle(GetPositionListQuery request, CancellationToken ct)
    {
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Keyword), request.Keyword)
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
                                    FROM hr_positions
                                    WHERE 1 = 1");

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query.AppendLine("AND (PositionCode LIKE @Keyword OR NameVi LIKE @Keyword OR NameEn LIKE @Keyword)");
                }

                var result = await dbContext.QueryPagingAsync<GetPositionListQuery.Response>(query, request);

                var response = ResponseHelper.Success(result, CoreResource.Common_msg_Success);

                logData.Result = result;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving positions: {ex.Message}", ex);
                var response = ResponseHelper.Error<PagedResult<GetPositionListQuery.Response>>(CoreResource.Common_msg_ExceptionOccurred);
                logData.Message = ex.ToString();
                logData.IsException = 1;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);
                return response;
            }
        }
    }
}

#endregion
