using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.IvItem.Queries
{
    public sealed class GetItemByCodeQuery : BaseQuery, IRequest<ApiResponse<GetItemByCodeQuery.Result>>
    {
        public string Code { get; set; } = default!;

        public sealed class Result
        {
            public int Id { get; set; }
            public string Code { get; set; } = default!;
            public string Name { get; set; } = default!;
            public string? Description { get; set; }
            public string? BrandCode { get; set; }
            public string? BrandName { get; set; }
            public string? CategoryCode { get; set; }
            public string? CategoryName { get; set; }
            public string? ColorCode { get; set; }
            public string? ColorName { get; set; }
            public string? SizeCode { get; set; }
            public string? SizeName { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetItemByCodeQueryValidator : AbstractValidator<GetItemByCodeQuery>
    {
        public GetItemByCodeQueryValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Item code is required");
        }
    }

    public sealed class GetItemByCodeQueryHandler : IRequestHandler<GetItemByCodeQuery, ApiResponse<GetItemByCodeQuery.Result>>
    {
        public async Task<ApiResponse<GetItemByCodeQuery.Result>> Handle(GetItemByCodeQuery request, CancellationToken cancellationToken)
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
                    var sql = @"
                        SELECT 
                            i.Id,
                            i.Code,
                            i.Name,
                            i.Description,
                            i.BrandCode,
                            b.Name AS BrandName,
                            i.CategoryCode,
                            c.Name AS CategoryName,
                            i.ColorCode,
                            cl.Name AS ColorName,
                            i.SizeCode,
                            s.Name AS SizeName,
                            i.CreatedAt
                        FROM it_items i
                        LEFT JOIN it_item_brand b ON i.BrandCode = b.Code
                        LEFT JOIN it_item_category c ON i.CategoryCode = c.Code
                        LEFT JOIN it_item_color cl ON i.ColorCode = cl.Code
                        LEFT JOIN it_item_size s ON i.SizeCode = s.Code
                        WHERE i.Code = @Code";

                    var result = await dbContext.QueryFirstOrDefaultAsync<GetItemByCodeQuery.Result>(sql, new { request.Code }, cancellationToken);

                    if (result == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetItemByCodeQuery.Result>("Item not found");
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
                    UniLogger.Error($"Error retrieving item by code: {ex.Message}", ex);

                    var response = ResponseHelper.Error<GetItemByCodeQuery.Result>("Error occurred while retrieving item");

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


