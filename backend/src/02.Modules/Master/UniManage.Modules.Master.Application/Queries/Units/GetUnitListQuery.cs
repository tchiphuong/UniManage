using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Master.Application.Queries.Units;

#region Query

public sealed class GetUnitListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetUnitListQuery.Response>>>
{
    public sealed class Response
    {
        public long Id { get; init; }
        public string Code { get; init; } = default!;
        public string NameVi { get; init; } = default!;
        public string NameEn { get; init; } = default!;
        public string CreatedBy { get; init; } = default!;
        public DateTime CreatedAt { get; init; }
        public string? UpdatedBy { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

#endregion

#region Validator

public sealed class GetUnitListQueryValidator : FluentValidation.AbstractValidator<GetUnitListQuery>
{
    public GetUnitListQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("Page index must be greater than 0");
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
    }
}

#endregion

#region Handler

public sealed class GetUnitListQueryHandler : IRequestHandler<GetUnitListQuery, ApiResponse<PagedResult<GetUnitListQuery.Response>>>
{
    public async Task<ApiResponse<PagedResult<GetUnitListQuery.Response>>> Handle(GetUnitListQuery request, CancellationToken cancellationToken)
    {
        var log = new ApiLogModel(request.HeaderInfo)
        {
            Parameter = new List<LogParamModel>
            {
                new LogParamModel(nameof(request.Keyword), request.Keyword),
                new LogParamModel(nameof(request.PageIndex), request.PageIndex.ToString()),
                new LogParamModel(nameof(request.PageSize), request.PageSize.ToString())
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                var baseQuery = new StringBuilder("SELECT Id, Code, NameVi, NameEn, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt FROM MsUnits");

                var filters = new Dictionary<string, object?>();
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    filters.Add("Code", QueryHelper.SanitizeSearchTerm(request.Keyword));
                    filters.Add("NameVi", QueryHelper.SanitizeSearchTerm(request.Keyword));
                    filters.Add("NameEn", QueryHelper.SanitizeSearchTerm(request.Keyword));
                }

                var result = await dbContext.QueryPagingAsync<GetUnitListQuery.Response>(baseQuery, request, filters);

                var response = ResponseHelper.Success(result, CoreResource.common_getSuccess);
                log.Result = result;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving units: {ex.Message}", ex);

                var response = ResponseHelper.Error<PagedResult<GetUnitListQuery.Response>>(CoreResource.common_exceptionOccurred);
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
        }
    }
}

#endregion



