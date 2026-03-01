using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Countries
{
    #region Query

    public sealed class GetCountryListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetCountryListQuery.Response>>>
    {
        public bool? IsActive { get; set; }

        public sealed record Response
        {
            public string Code { get; set; } = default!;
            public string NameVi { get; set; } = default!;
            public string NameEn { get; set; } = default!;
            public string? FullNameVi { get; set; }
            public string? FullNameEn { get; set; }
            public string? PhoneCode { get; set; }
            public int SortOrder { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetCountryListQueryValidator : AbstractValidator<GetCountryListQuery>
    {
        public GetCountryListQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }

    #endregion

    #region Handler

    public sealed class GetCountryListQueryHandler : IRequestHandler<GetCountryListQuery, ApiResponse<PagedResult<GetCountryListQuery.Response>>>
    {
        public async Task<ApiResponse<PagedResult<GetCountryListQuery.Response>>> Handle(GetCountryListQuery request, CancellationToken ct)
        {
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Keyword), request.Keyword),
                new CoreParamModel(nameof(request.IsActive), request.IsActive)
            }
            };

            using (DbContext dbContext = new DbContext())
            {
                try
                {
                    var query = new StringBuilder();
                    query.AppendLine($@"SELECT 
                                        Code         AS {nameof(GetCountryListQuery.Response.Code)},
                                        NameVi       AS {nameof(GetCountryListQuery.Response.NameVi)},
                                        NameEn       AS {nameof(GetCountryListQuery.Response.NameEn)},
                                        FullNameVi   AS {nameof(GetCountryListQuery.Response.FullNameVi)},
                                        FullNameEn   AS {nameof(GetCountryListQuery.Response.FullNameEn)},
                                        PhoneCode    AS {nameof(GetCountryListQuery.Response.PhoneCode)},
                                        SortOrder    AS {nameof(GetCountryListQuery.Response.SortOrder)},
                                        IsActive     AS {nameof(GetCountryListQuery.Response.IsActive)},
                                        CreatedAt    AS {nameof(GetCountryListQuery.Response.CreatedAt)}
                                    FROM ad_countries
                                    WHERE 1 = 1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine("AND (Code LIKE @Keyword OR NameVi LIKE @Keyword OR NameEn LIKE @Keyword)");
                    }

                    if (request.IsActive.HasValue)
                    {
                        query.AppendLine("AND IsActive = @IsActive");
                    }

                    var result = await dbContext.QueryPagingAsync<GetCountryListQuery.Response>(query, request);

                    var response = ResponseHelper.Success(result, string.Format(CoreResource.crud_listSuccess, CoreResource.entity_country));

                    logData.Result = result;
                    logData.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(logData);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving countries: {ex.Message}", ex);
                    var response = ResponseHelper.Error<PagedResult<GetCountryListQuery.Response>>(CoreResource.common_exceptionOccurred);
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
}
