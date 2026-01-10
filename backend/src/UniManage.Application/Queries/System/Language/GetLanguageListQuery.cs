using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Language;

#region Query

public sealed class GetLanguageListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetLanguageListQuery.Response>>>
{
    public string? StatusCode { get; set; }

    public sealed record Response
    {
        public string? LanguageCode { get; set; }
        public string? LanguageName { get; set; }
        public string? Icon { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}

#endregion

#region Validator

public sealed class GetLanguageListQueryValidator : AbstractValidator<GetLanguageListQuery>
{
    public GetLanguageListQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("Page index must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
    }
}

#endregion

#region Handler

public sealed class GetLanguageListQueryHandler : IRequestHandler<GetLanguageListQuery, ApiResponse<PagedResult<GetLanguageListQuery.Response>>>
{
    public async Task<ApiResponse<PagedResult<GetLanguageListQuery.Response>>> Handle(GetLanguageListQuery request, CancellationToken cancellationToken)
    {
        ApiResponse<PagedResult<GetLanguageListQuery.Response>> response;

        // Initialize log data
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
        logData.Parameter = new List<CoreParamModel>
        {
            new CoreParamModel(nameof(request.Keyword), request.Keyword),
            new CoreParamModel(nameof(request.SearchFields), request.SearchFields),
            new CoreParamModel(nameof(request.StatusCode), request.StatusCode)
        };

        using (DbContext dbContext = new DbContext())
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine($@"SELECT 
                                        LanguageCode AS {nameof(GetLanguageListQuery.Response.LanguageCode)},
                                        LanguageName AS {nameof(GetLanguageListQuery.Response.LanguageName)},
                                        Icon         AS {nameof(GetLanguageListQuery.Response.Icon)},
                                        IsDefault    AS {nameof(GetLanguageListQuery.Response.IsDefault)},
                                        IsActive     AS {nameof(GetLanguageListQuery.Response.IsActive)}
                                    FROM sy_languages
                                    WHERE 1 = 1");

                if (!string.IsNullOrEmpty(request.StatusCode))
                {
                    query.AppendLine("AND StatusCode = @StatusCode");
                }

                var result = await dbContext.QueryPagingAsync<GetLanguageListQuery.Response>(query, request);

                response = ResponseHelper.Success(result, CoreResource.Language_msg_ListSuccess);

                logData.Result = result;
                logData.ReturnCode = response.ReturnCode;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving languages list: {ex.Message}", ex);
                response = ResponseHelper.Error<PagedResult<GetLanguageListQuery.Response>>(CoreResource.Common_msg_ExceptionOccurred);

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
