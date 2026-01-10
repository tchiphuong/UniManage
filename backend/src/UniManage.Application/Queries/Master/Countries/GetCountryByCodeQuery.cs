using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Countries
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
        public async Task<ApiResponse<GetCountryByCodeQuery.Response>> Handle(GetCountryByCodeQuery request, CancellationToken ct)
        {
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Code), request.Code)
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
                    FROM ad_countries
                    WHERE Code = @Code",
                        new { request.Code },
                        ct);

                    if (country == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetCountryByCodeQuery.Response>(CoreResource.Country_msg_NotFound);
                        logData.ReturnCode = notFoundResponse.ReturnCode;
                        logData.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(logData);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(country, CoreResource.Country_msg_GetSuccess);
                    logData.Result = country;
                    logData.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(logData);
                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving country: {ex.Message}", ex);
                    var response = ResponseHelper.Error<GetCountryByCodeQuery.Response>(CoreResource.Common_msg_ExceptionOccurred);
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
