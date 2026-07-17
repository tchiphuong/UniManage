using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.IvItemDetail.Queries
{
    public sealed class GetItemDetailByIdQuery : BaseQuery, IRequest<ApiResponse<GetItemDetailByIdQuery.Result>>
    {
        public long Id { get; set; }

        public sealed class Result
        {
            public long Id { get; set; }
            public string ItemCode { get; set; } = default!;
            public string ItemName { get; set; } = default!;
            public int? Type { get; set; }
            public string Key { get; set; } = default!;
            public string ValueVi { get; set; } = default!;
            public string ValueEn { get; set; } = default!;
            public string InsertBy { get; set; } = default!;
            public DateTime? InsertOn { get; set; }
            public string UpdateBy { get; set; } = default!;
            public DateTime? UpdateOn { get; set; }
        }
    }

    public sealed class GetItemDetailByIdQueryValidator : AbstractValidator<GetItemDetailByIdQuery>
    {
        public GetItemDetailByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }

    public sealed class GetItemDetailByIdQueryHandler : IRequestHandler<GetItemDetailByIdQuery, ApiResponse<GetItemDetailByIdQuery.Result>>
    {
        public async Task<ApiResponse<GetItemDetailByIdQuery.Result>> Handle(GetItemDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Id), request.Id.ToString())
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = @"
                        SELECT 
                            d.Id,
                            d.ItemCode,
                            i.Name AS ItemName,
                            d.Type,
                            d.[Key],
                            d.ValueVi,
                            d.ValueEn,
                            d.InsertBy,
                            d.InsertOn,
                            d.UpdateBy,
                            d.UpdateOn
                        FROM it_item_details d
                        LEFT JOIN it_items i ON d.ItemCode = i.Code
                        WHERE d.Id = @Id";

                    var result = await dbContext.QueryFirstOrDefaultAsync<GetItemDetailByIdQuery.Result>(sql, new { request.Id }, cancellationToken);

                    if (result == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetItemDetailByIdQuery.Result>("Item detail not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(result, CoreResource.common_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving item detail by id: {ex.Message}", ex);

                    var response = ResponseHelper.Error<GetItemDetailByIdQuery.Result>("Error occurred while retrieving item detail");

                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}


