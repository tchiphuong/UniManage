using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Queries.WorkShifts
{
    #region Query

    public sealed class GetWorkShiftByCodeQuery : BaseQuery, IRequest<ApiResponse<GetWorkShiftByCodeQuery.Response>>
    {
        public string Code { get; init; } = default!;

        public sealed record Response
        {
            public int Id { get; set; }
            public string Code { get; set; } = default!;
            public string Name { get; set; } = default!;
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string? Description { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetWorkShiftByCodeQueryValidator : AbstractValidator<GetWorkShiftByCodeQuery>
    {
        public GetWorkShiftByCodeQueryValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(CoreResource.validation_required);
        }
    }

    #endregion

    #region Handler

    public sealed class GetWorkShiftByCodeQueryHandler : IRequestHandler<GetWorkShiftByCodeQuery, ApiResponse<GetWorkShiftByCodeQuery.Response>>
    {
        public async Task<ApiResponse<GetWorkShiftByCodeQuery.Response>> Handle(GetWorkShiftByCodeQuery request, CancellationToken cancellationToken)
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
                    var workShift = await dbContext.QueryFirstOrDefaultAsync<GetWorkShiftByCodeQuery.Response>(
                        @"SELECT Id, Code, Name, StartTime, EndTime, Description, CreatedAt, UpdatedAt
                          FROM HrWorkShifts
                          WHERE Code = @Code",
                        new { request.Code },
                        cancellationToken);

                    if (workShift == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetWorkShiftByCodeQuery.Response>(CoreResource.common_notFound);
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(workShift, CoreResource.common_getSuccess);
                    log.Result = workShift;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error getting work shift: {ex.Message}", ex);
                    var response = ResponseHelper.Error<GetWorkShiftByCodeQuery.Response>(CoreResource.common_exceptionOccurred);

                    log.Message = ex.ToString();
                    log.IsException = true;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }

    #endregion
}



