using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Master.Application.Queries.Units;

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
    public async Task<ApiResponse<GetUnitByCodeQuery.Response?>> Handle(GetUnitByCodeQuery request, CancellationToken cancellationToken)
    {
        var log = new ApiLogModel(request.HeaderInfo)
        {
            Parameter = new List<LogParamModel>
            {
                new LogParamModel(nameof(request.Code), request.Code)
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                var sql = @"SELECT Id, Code, NameVi, NameEn, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, DataRowVersion 
                           FROM MsUnits 
                           WHERE Code = @Code";

                var unit = await dbContext.QueryFirstOrDefaultAsync<GetUnitByCodeQuery.Response>(sql, new { Code = request.Code }, cancellationToken);

                if (unit == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<GetUnitByCodeQuery.Response?>(CoreResource.common_notFound);
                    log.Result = notFoundResponse;
                    log.ReturnCode = notFoundResponse.ReturnCode;
                    UniLogManager.WriteApiLog(log);
                    return notFoundResponse;
                }

                var response = ResponseHelper.Success(unit, CoreResource.common_getSuccess);
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



