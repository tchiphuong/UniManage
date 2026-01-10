using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.User;

#region Query

public sealed class GetUserListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetUserListQuery.Response>>>
{
    public string? Status { get; set; }

    public sealed record Response
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string EmployeeCode { get; set; } = default!;
        public string RoleCode { get; set; } = default!;
        public string? Email { get; set; }
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}

#endregion

#region Validator

public sealed class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
{
    public GetUserListQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("Page index must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
    }
}

#endregion

#region Handler

public sealed class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ApiResponse<PagedResult<GetUserListQuery.Response>>>
{
    public async Task<ApiResponse<PagedResult<GetUserListQuery.Response>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        ApiResponse<PagedResult<GetUserListQuery.Response>> response;

        // Initialize log data
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
        logData.Parameter = new List<CoreParamModel>
        {
            new CoreParamModel(nameof(request.Keyword), request.Keyword),
            new CoreParamModel(nameof(request.SearchFields), request.SearchFields),
            new CoreParamModel(nameof(request.Status), request.Status)
        };

        using (DbContext dbContext = new DbContext())
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine($@"SELECT 
                                        Id           AS {nameof(GetUserListQuery.Response.Id)},
                                        Username     AS {nameof(GetUserListQuery.Response.Username)},
                                        EmployeeCode AS {nameof(GetUserListQuery.Response.EmployeeCode)},
                                        RoleCode     AS {nameof(GetUserListQuery.Response.RoleCode)},
                                        Email        AS {nameof(GetUserListQuery.Response.Email)},
                                        Status       AS {nameof(GetUserListQuery.Response.Status)},
                                        CreatedAt    AS {nameof(GetUserListQuery.Response.CreatedAt)}
                                    FROM sy_users
                                    WHERE 1 = 1");

                if (!string.IsNullOrEmpty(request.Status))
                {
                    query.AppendLine("AND Status = @Status");
                }

                var result = await dbContext.QueryPagingAsync<GetUserListQuery.Response>(query, request);

                response = ResponseHelper.Success(result, CoreResource.User_msg_ListSuccess);

                logData.Result = result;
                logData.ReturnCode = response.ReturnCode;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving users list: {ex.Message}", ex);
                response = ResponseHelper.Error<PagedResult<GetUserListQuery.Response>>(CoreResource.Common_msg_ExceptionOccurred);

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