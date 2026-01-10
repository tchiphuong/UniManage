using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.WorkShifts
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
            RuleFor(x => x.Code).NotEmpty().WithMessage(CoreResource.Validation_msg_Required);
        }
    }

    #endregion

    #region Handler

    public sealed class GetWorkShiftByCodeQueryHandler : IRequestHandler<GetWorkShiftByCodeQuery, ApiResponse<GetWorkShiftByCodeQuery.Response>>
    {
        public async Task<ApiResponse<GetWorkShiftByCodeQuery.Response>> Handle(GetWorkShiftByCodeQuery request, CancellationToken ct)
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
                    var workShift = await dbContext.QueryFirstOrDefaultAsync<GetWorkShiftByCodeQuery.Response>(
                        @"SELECT Id, Code, Name, StartTime, EndTime, Description, CreatedAt, UpdatedAt
                          FROM hr_work_shifts
                          WHERE Code = @Code",
                        new { request.Code },
                        ct);

                    if (workShift == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetWorkShiftByCodeQuery.Response>(CoreResource.Common_msg_NotFound);
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(workShift, CoreResource.Common_msg_GetSuccess);
                    log.Result = workShift;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error getting work shift: {ex.Message}", ex);
                    var response = ResponseHelper.Error<GetWorkShiftByCodeQuery.Response>(CoreResource.Common_msg_ExceptionOccurred);

                    log.Message = ex.ToString();
                    log.IsException = 1;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }

    #endregion
}
