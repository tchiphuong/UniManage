using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Units;

#region Query

public sealed class GetUnitByCodeQuery : BaseQuery, IRequest<ApiResponse<GetUnitByCodeQuery.Response?>>
{
    public string Code { get; init; } = default!;

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
        public byte[] DataRowVersion { get; init; } = default!;
    }
}

#endregion

#region Validator

public sealed class GetUnitByCodeQueryValidator : FluentValidation.AbstractValidator<GetUnitByCodeQuery>
{
    public GetUnitByCodeQueryValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
    }
}

#endregion

#region Handler

public sealed class GetUnitByCodeQueryHandler : IRequestHandler<GetUnitByCodeQuery, ApiResponse<GetUnitByCodeQuery.Response?>>
{
    public async Task<ApiResponse<GetUnitByCodeQuery.Response?>> Handle(GetUnitByCodeQuery request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Code), request.Code)
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                var sql = @"SELECT Id, Code, NameVi, NameEn, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, DataRowVersion 
                           FROM ms_units 
                           WHERE Code = @Code";

                var unit = await dbContext.QueryFirstOrDefaultAsync<GetUnitByCodeQuery.Response>(sql, new { Code = request.Code }, ct);

                if (unit == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<GetUnitByCodeQuery.Response?>(CoreResource.common_notFound);
                    log.Result = notFoundResponse;
                    log.ReturnCode = notFoundResponse.ReturnCode;
                    UniLogManager.WriteApiLog(log);
                    return notFoundResponse;
                }

                var response = ResponseHelper.Success(unit, CoreResource.crud_getSuccess);
                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving unit by code: {ex.Message}", ex);

                var response = ResponseHelper.Error<GetUnitByCodeQuery.Response?>(CoreResource.common_exceptionOccurred);
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
