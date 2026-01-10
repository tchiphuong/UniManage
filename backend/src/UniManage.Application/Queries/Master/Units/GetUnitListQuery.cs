using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Units;

#region Query

public sealed class GetUnitListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetUnitListQuery.Response>>>
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
    public async Task<ApiResponse<PagedResult<GetUnitListQuery.Response>>> Handle(GetUnitListQuery request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Keyword), request.Keyword),
                new CoreParamModel(nameof(request.PageIndex), request.PageIndex.ToString()),
                new CoreParamModel(nameof(request.PageSize), request.PageSize.ToString())
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                var baseQuery = new StringBuilder("SELECT Id, Code, NameVi, NameEn, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt FROM ms_units");

                var filters = new Dictionary<string, object?>();
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    filters.Add("Code", QueryHelper.SanitizeSearchTerm(request.Keyword));
                    filters.Add("NameVi", QueryHelper.SanitizeSearchTerm(request.Keyword));
                    filters.Add("NameEn", QueryHelper.SanitizeSearchTerm(request.Keyword));
                }

                var result = await dbContext.QueryPagingAsync<GetUnitListQuery.Response>(baseQuery, request, filters);

                var response = ResponseHelper.Success(result, CoreResource.Common_msg_GetSuccess);
                log.Result = result;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving units: {ex.Message}", ex);

                var response = ResponseHelper.Error<PagedResult<GetUnitListQuery.Response>>(CoreResource.Common_msg_ExceptionOccurred);
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
        }
    }
}

#endregion
