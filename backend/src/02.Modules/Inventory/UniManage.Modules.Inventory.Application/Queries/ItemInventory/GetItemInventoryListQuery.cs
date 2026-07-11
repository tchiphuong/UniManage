using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.Inventory.Application.Queries.ItemInventory
{
    public sealed class GetItemInventoryListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetItemInventoryListQuery.Result>>>
    {
        public string? ItemCode { get; set; }
        public string? WarehouseLocation { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string ItemCode { get; set; } = default!;
            public string ItemName { get; set; } = default!;
            public int Quantity { get; set; }
            public string WarehouseLocation { get; set; } = default!;
            public DateTime UpdatedAt { get; set; }
        }
    }

    public sealed class GetItemInventoryListQueryValidator : AbstractValidator<GetItemInventoryListQuery>
    {
        public GetItemInventoryListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetItemInventoryListQueryHandler : IRequestHandler<GetItemInventoryListQuery, ApiResponse<PagedResult<GetItemInventoryListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetItemInventoryListQuery.Result>>> Handle(GetItemInventoryListQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Keyword), request.Keyword),
                    new LogParamModel(nameof(request.ItemCode), request.ItemCode),
                    new LogParamModel(nameof(request.WarehouseLocation), request.WarehouseLocation)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            inv.Id,
                            inv.ItemCode,
                            i.Name AS ItemName,
                            inv.Quantity,
                            inv.WarehouseLocation,
                            inv.UpdatedAt
                        FROM it_item_inventory inv
                        LEFT JOIN it_items i ON inv.ItemCode = i.Code
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (inv.ItemCode LIKE @Keyword OR i.Name LIKE @Keyword OR inv.WarehouseLocation LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    if (!string.IsNullOrEmpty(request.ItemCode))
                    {
                        sql.AppendLine("AND inv.ItemCode = @ItemCode");
                        parameters.Add("ItemCode", request.ItemCode);
                    }

                    if (!string.IsNullOrEmpty(request.WarehouseLocation))
                    {
                        sql.AppendLine("AND inv.WarehouseLocation LIKE @WarehouseLocation");
                        parameters.Add("WarehouseLocation", $"%{request.WarehouseLocation}%");
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "inv.UpdatedAt DESC" },
                        { "itemCode", "inv.ItemCode" },
                        { "quantity", "inv.Quantity" },
                        { "warehouseLocation", "inv.WarehouseLocation" }
                    };

                    var (orderBy, _) = QueryHelper.BuildOrderByClause(
                        request.SortBy,
                        request.SortDirection,
                        columnMappings);

                    sql.AppendLine($"ORDER BY {orderBy}");
                    sql.AppendLine("OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

                    parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);
                    parameters.Add("PageSize", request.PageSize);

                    var countSql = new StringBuilder();
                    countSql.AppendLine(@"
                        SELECT COUNT(*) 
                        FROM it_item_inventory inv
                        LEFT JOIN it_items i ON inv.ItemCode = i.Code
                        WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (inv.ItemCode LIKE @Keyword OR i.Name LIKE @Keyword OR inv.WarehouseLocation LIKE @Keyword)");
                    }

                    if (!string.IsNullOrEmpty(request.ItemCode))
                    {
                        countSql.AppendLine("AND inv.ItemCode = @ItemCode");
                    }

                    if (!string.IsNullOrEmpty(request.WarehouseLocation))
                    {
                        countSql.AppendLine("AND inv.WarehouseLocation LIKE @WarehouseLocation");
                    }

                    var items = await dbContext.QueryAsync<GetItemInventoryListQuery.Result>(sql.ToString(), parameters, cancellationToken);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, cancellationToken);

                    var result = new PagedResult<GetItemInventoryListQuery.Result>
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
                    UniLogger.Error($"Error retrieving item inventory: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetItemInventoryListQuery.Result>>("Error occurred while retrieving item inventory");

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


