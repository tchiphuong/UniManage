using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.MsCountry.Queries
{
    #region Query

    public sealed class GetCountryByCodeQuery : BaseQuery, IRequest<ApiResponse<GetCountryByCodeQuery.Response>>
    {
        public string Code { get; set; } = default!;

        public sealed record Response
        {
            public string Code { get; set; } = default!;
            public string NameVi { get; set; } = default!;
            public string NameEn { get; set; } = default!;
            public string? FullNameVi { get; set; }
            public string? FullNameEn { get; set; }
            public string? CodeName { get; set; }
            public string? PhoneCode { get; set; }
            public int SortOrder { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetCountryByCodeQueryValidator : AbstractValidator<GetCountryByCodeQuery>
    {
        public GetCountryByCodeQueryValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(20);
        }
    }

    #endregion

    #region Handler

    public sealed class GetCountryByCodeQueryHandler : IRequestHandler<GetCountryByCodeQuery, ApiResponse<GetCountryByCodeQuery.Response>>
    {
        public async Task<ApiResponse<GetCountryByCodeQuery.Response>> Handle(GetCountryByCodeQuery request, CancellationToken cancellationToken)
        {
            ApiLogModel logData = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
            {
                new LogParamModel(nameof(request.Code), request.Code)
            }
            };

            using (DbContext dbContext = new DbContext())
            {
                try
                {
                    var country = await dbContext.QueryFirstOrDefaultAsync<GetCountryByCodeQuery.Response>(
                        $@"SELECT 
                        Code         AS {nameof(GetCountryByCodeQuery.Response.Code)},
                        NameVi       AS {nameof(GetCountryByCodeQuery.Response.NameVi)},
                        NameEn       AS {nameof(GetCountryByCodeQuery.Response.NameEn)},
                        FullNameVi   AS {nameof(GetCountryByCodeQuery.Response.FullNameVi)},
                        FullNameEn   AS {nameof(GetCountryByCodeQuery.Response.FullNameEn)},
                        CodeName     AS {nameof(GetCountryByCodeQuery.Response.CodeName)},
                        PhoneCode    AS {nameof(GetCountryByCodeQuery.Response.PhoneCode)},
                        SortOrder    AS {nameof(GetCountryByCodeQuery.Response.SortOrder)},
                        IsActive     AS {nameof(GetCountryByCodeQuery.Response.IsActive)},
                        CreatedAt    AS {nameof(GetCountryByCodeQuery.Response.CreatedAt)},
                        UpdatedAt    AS {nameof(GetCountryByCodeQuery.Response.UpdatedAt)}
                    FROM AdCountries
                    WHERE Code = @Code",
                        new { request.Code },
                        cancellationToken);

                    if (country == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetCountryByCodeQuery.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_country));
                        logData.Message = "Country not found";
                        logData.ReturnCode = notFoundResponse.ReturnCode;
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(country, string.Format(CoreResource.common_getSuccess, CoreResource.entity_country));
                    logData.Result = country;
                    logData.ReturnCode = response.ReturnCode;
                    logData.Message = "Get country success";
                    return response;
                }
                catch (Exception ex)
                {
                    logData.Message = ex.Message;
                    logData.IsException = true;
                    logData.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                    return ResponseHelper.Error<GetCountryByCodeQuery.Response>(CoreResource.common_error);
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



