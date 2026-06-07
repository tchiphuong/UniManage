using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.HumanResource.Application.Queries.Positions;

#region Query

/// <summary>
/// Query to get position by id
/// </summary>
public sealed class GetPositionByIdQuery : BaseQuery, IRequest<ApiResponse<GetPositionByIdQuery.Response>>
{
    public int Id { get; init; }

    public sealed record Response
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string NameVi { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for GetPositionByIdQuery
/// </summary>
public sealed class GetPositionByIdQueryValidator : AbstractValidator<GetPositionByIdQuery>
{
    public GetPositionByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for GetPositionByIdQuery
/// </summary>
public sealed class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, ApiResponse<GetPositionByIdQuery.Response>>
{
    public async Task<ApiResponse<GetPositionByIdQuery.Response>> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        ApiLogModel logData = new ApiLogModel(request.HeaderInfo)
        {
            Parameter = new List<LogParamModel>
            {
                new LogParamModel(nameof(request.Id), request.Id)
            }
        };

        using (DbContext dbContext = new DbContext())
        {
            try
            {
                var position = await dbContext.QueryFirstOrDefaultAsync<GetPositionByIdQuery.Response>(
                    @"SELECT 
                        Id,
                        Code,
                        NameVi,
                        NameEn,
                        Description,
                        1 AS Status,
                        CreatedAt,
                        UpdatedAt
                    FROM HrPositions
                    WHERE Id = @Id",
                    new { request.Id },
                    cancellationToken: cancellationToken);

                if (position == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<GetPositionByIdQuery.Response>(CoreResource.common_notFound);
                    logData.ReturnCode = notFoundResponse.ReturnCode;
                    logData.Message = notFoundResponse.Message;
                    UniLogManager.WriteApiLog(logData);
                    return notFoundResponse;
                }

                var response = ResponseHelper.Success(position, CoreResource.common_success);
                logData.Result = position;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving position: {ex.Message}", ex);
                var response = ResponseHelper.Error<GetPositionByIdQuery.Response>(CoreResource.common_exceptionOccurred);
                logData.Message = ex.ToString();
                logData.IsException = true;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);
                return response;
            }
        }
    }
}

#endregion



