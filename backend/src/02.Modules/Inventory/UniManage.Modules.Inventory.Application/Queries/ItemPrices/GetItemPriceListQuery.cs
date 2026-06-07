using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Inventory.Application.Queries.ItemPrices
{
    public sealed class GetItemPriceListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetItemPriceListQuery.Result>>>
    {
        public string? ItemCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string ItemCode { get; set; } = default!;
            public string ItemName { get; set; } = default!;
            public decimal Price { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetItemPriceListQueryValidator : AbstractValidator<GetItemPriceListQuery>
    {
        public GetItemPriceListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetItemPriceListQueryHandler : IRequestHandler<GetItemPriceListQuery, ApiResponse<PagedResult<GetItemPriceListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetItemPriceListQuery.Result>>> Handle(GetItemPriceListQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Keyword), request.Keyword),
                    new LogParamModel(nameof(request.ItemCode), request.ItemCode)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            p.Id,
                            p.ItemCode,
                            i.Name AS ItemName,
                            p.Price,
                            p.StartDate,
                            p.EndDate,
                            CASE 
                                WHEN p.EndDate IS NULL OR p.EndDate >= GETDATE() THEN 1 
                                ELSE 0 
                            END AS IsActive,
                            p.CreatedAt
                        FROM it_item_price p
                        LEFT JOIN it_items i ON p.ItemCode = i.Code
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (p.ItemCode LIKE @Keyword OR i.Name LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    if (!string.IsNullOrEmpty(request.ItemCode))
                    {
                        sql.AppendLine("AND p.ItemCode = @ItemCode");
                        parameters.Add("ItemCode", request.ItemCode);
                    }

                    if (request.FromDate.HasValue)
                    {
                        sql.AppendLine("AND p.StartDate >= @FromDate");
                        parameters.Add("FromDate", request.FromDate.Value);
                    }

                    if (request.ToDate.HasValue)
                    {
                        sql.AppendLine("AND p.StartDate <= @ToDate");
                        parameters.Add("ToDate", request.ToDate.Value);
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "p.StartDate DESC" },
                        { "itemCode", "p.ItemCode" },
                        { "price", "p.Price" },
                        { "startDate", "p.StartDate" }
                    };

                    var (orderBy, _) = QueryHelper.BuildOrderByClause(
                        request.SortBy,
                        request.SortDirection ?? "DESC",
                        columnMappings);

                    sql.AppendLine($"ORDER BY {orderBy}");
                    sql.AppendLine("OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

                    parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);
                    parameters.Add("PageSize", request.PageSize);

                    var countSql = new StringBuilder();
                    countSql.AppendLine(@"
                        SELECT COUNT(*) 
                        FROM it_item_price p
                        LEFT JOIN it_items i ON p.ItemCode = i.Code
                        WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (p.ItemCode LIKE @Keyword OR i.Name LIKE @Keyword)");
                    }

                    if (!string.IsNullOrEmpty(request.ItemCode))
                    {
                        countSql.AppendLine("AND p.ItemCode = @ItemCode");
                    }

                    if (request.FromDate.HasValue)
                    {
                        countSql.AppendLine("AND p.StartDate >= @FromDate");
                    }

                    if (request.ToDate.HasValue)
                    {
                        countSql.AppendLine("AND p.StartDate <= @ToDate");
                    }

                    var items = await dbContext.QueryAsync<GetItemPriceListQuery.Result>(sql.ToString(), parameters, cancellationToken);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, cancellationToken);

                    var result = new PagedResult<GetItemPriceListQuery.Result>
                    {
                        Items = items.ToList(),
                        Paging = new PagingInfo
                        {
                            PageIndex = request.PageIndex,
                            PageSize = request.PageSize,
                            TotalItems = totalItems
                        }
                    };

                    var response = ResponseHelper.Success(result, CoreResource.common_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving item prices: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetItemPriceListQuery.Result>>("Error occurred while retrieving item prices");

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


