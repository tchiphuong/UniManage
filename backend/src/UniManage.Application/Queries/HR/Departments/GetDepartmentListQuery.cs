using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.Departments;

#region Query

public sealed class GetDepartmentListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetDepartmentListQuery.Response>>>
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

public sealed class GetDepartmentListQueryValidator : AbstractValidator<GetDepartmentListQuery>
{
    public GetDepartmentListQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}

#endregion

#region Handler

public sealed class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, ApiResponse<PagedResult<GetDepartmentListQuery.Response>>>
{
    public async Task<ApiResponse<PagedResult<GetDepartmentListQuery.Response>>> Handle(GetDepartmentListQuery request, CancellationToken ct)
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
                                        Id           AS {nameof(GetDepartmentListQuery.Response.Id)},
                                        Code         AS {nameof(GetDepartmentListQuery.Response.Code)},
                                        NameVi       AS {nameof(GetDepartmentListQuery.Response.NameVi)},
                                        NameEn       AS {nameof(GetDepartmentListQuery.Response.NameEn)},
                                        Description  AS {nameof(GetDepartmentListQuery.Response.Description)},
                                        1            AS {nameof(GetDepartmentListQuery.Response.Status)},
                                        CreatedAt    AS {nameof(GetDepartmentListQuery.Response.CreatedAt)}
                                    FROM hr_departments
                                    WHERE 1 = 1");

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query.AppendLine("AND (Code LIKE @Keyword OR NameVi LIKE @Keyword OR NameEn LIKE @Keyword)");
                }

                var result = await dbContext.QueryPagingAsync<GetDepartmentListQuery.Response>(query, request);

                var response = ResponseHelper.Success(result, CoreResource.Common_msg_Success);

                logData.Result = result;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving departments: {ex.Message}", ex);
                var response = ResponseHelper.Error<PagedResult<GetDepartmentListQuery.Response>>(CoreResource.Common_msg_ExceptionOccurred);
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
