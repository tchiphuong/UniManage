using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Inventory.ItemDetails
{
    public sealed class GetItemDetailListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetItemDetailListQuery.Result>>>
    {
        public string? ItemCode { get; set; }
        public int? Type { get; set; }

        public sealed class Result
        {
            public long Id { get; set; }
            public string ItemCode { get; set; } = default!;
            public string ItemName { get; set; } = default!;
            public int? Type { get; set; }
            public string Key { get; set; } = default!;
            public string ValueVi { get; set; } = default!;
            public string ValueEn { get; set; } = default!;
            public DateTime? InsertOn { get; set; }
        }
    }

    public sealed class GetItemDetailListQueryValidator : AbstractValidator<GetItemDetailListQuery>
    {
        public GetItemDetailListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetItemDetailListQueryHandler : IRequestHandler<GetItemDetailListQuery, ApiResponse<PagedResult<GetItemDetailListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetItemDetailListQuery.Result>>> Handle(GetItemDetailListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.ItemCode), request.ItemCode),
                    new CoreParamModel(nameof(request.Type), request.Type?.ToString())
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            d.Id,
                            d.ItemCode,
                            i.Name AS ItemName,
                            d.Type,
                            d.[Key],
                            d.ValueVi,
                            d.ValueEn,
                            d.InsertOn
                        FROM it_item_details d
                        LEFT JOIN it_items i ON d.ItemCode = i.Code
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (d.[Key] LIKE @Keyword OR d.ValueVi LIKE @Keyword OR d.ValueEn LIKE @Keyword OR i.Name LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    if (!string.IsNullOrEmpty(request.ItemCode))
                    {
                        sql.AppendLine("AND d.ItemCode = @ItemCode");
                        parameters.Add("ItemCode", request.ItemCode);
                    }

                    if (request.Type.HasValue)
                    {
                        sql.AppendLine("AND d.Type = @Type");
                        parameters.Add("Type", request.Type.Value);
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "d.InsertOn DESC" },
                        { "itemCode", "d.ItemCode" },
                        { "type", "d.Type" },
                        { "key", "d.[Key]" }
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
                        FROM it_item_details d
                        LEFT JOIN it_items i ON d.ItemCode = i.Code
                        WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (d.[Key] LIKE @Keyword OR d.ValueVi LIKE @Keyword OR d.ValueEn LIKE @Keyword OR i.Name LIKE @Keyword)");
                    }

                    if (!string.IsNullOrEmpty(request.ItemCode))
                    {
                        countSql.AppendLine("AND d.ItemCode = @ItemCode");
                    }

                    if (request.Type.HasValue)
                    {
                        countSql.AppendLine("AND d.Type = @Type");
                    }

                    var items = await dbContext.QueryAsync<GetItemDetailListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetItemDetailListQuery.Result>
                    {
                        Items = items.ToList(),
                        Paging = new PagingInfo
                        {
                            PageIndex = request.PageIndex,
                            PageSize = request.PageSize,
                            TotalItems = totalItems
                        }
                    };

                    var response = ResponseHelper.Success(result, CoreResource.crud_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving item details: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetItemDetailListQuery.Result>>("Error occurred while retrieving item details");

                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}
