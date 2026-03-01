using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.WorkShifts
{
    #region Query

    public sealed class GetWorkShiftListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetWorkShiftListQuery.Response>>>
    {
        public sealed record Response
        {
            public int Id { get; set; }
            public string Code { get; set; } = default!;
            public string Name { get; set; } = default!;
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string? Description { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetWorkShiftListQueryValidator : AbstractValidator<GetWorkShiftListQuery>
    {
        public GetWorkShiftListQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }

    #endregion

    #region Handler

    public sealed class GetWorkShiftListQueryHandler : IRequestHandler<GetWorkShiftListQuery, ApiResponse<PagedResult<GetWorkShiftListQuery.Response>>>
    {
        public async Task<ApiResponse<PagedResult<GetWorkShiftListQuery.Response>>> Handle(GetWorkShiftListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var query = new StringBuilder();
                    query.AppendLine($@"SELECT 
                                        Id          AS {nameof(GetWorkShiftListQuery.Response.Id)},
                                        Code        AS {nameof(GetWorkShiftListQuery.Response.Code)},
                                        Name        AS {nameof(GetWorkShiftListQuery.Response.Name)},
                                        StartTime   AS {nameof(GetWorkShiftListQuery.Response.StartTime)},
                                        EndTime     AS {nameof(GetWorkShiftListQuery.Response.EndTime)},
                                        Description AS {nameof(GetWorkShiftListQuery.Response.Description)},
                                        CreatedAt   AS {nameof(GetWorkShiftListQuery.Response.CreatedAt)},
                                        UpdatedAt   AS {nameof(GetWorkShiftListQuery.Response.UpdatedAt)}
                                    FROM hr_work_shifts
                                    WHERE 1 = 1");

                    var result = await dbContext.QueryPagingAsync<GetWorkShiftListQuery.Response>(query, request);

                    var response = ResponseHelper.Success(result, CoreResource.crud_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving work shifts: {ex.Message}", ex);
                    var response = ResponseHelper.Error<PagedResult<GetWorkShiftListQuery.Response>>(CoreResource.common_exceptionOccurred);

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
