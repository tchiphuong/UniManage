using FluentValidation;
using MediatR;
using System.Text;
using Dapper;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Inventory.ItemImages
{
    public sealed class GetItemImageListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetItemImageListQuery.Result>>>
    {
        public string? ItemCode { get; set; }
        public bool? IsThumbnail { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string ItemCode { get; set; } = default!;
            public string ItemName { get; set; } = default!;
            public string ImageUrl { get; set; } = default!;
            public bool IsThumbnail { get; set; }
            public int SortOrder { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetItemImageListQueryValidator : AbstractValidator<GetItemImageListQuery>
    {
        public GetItemImageListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetItemImageListQueryHandler : IRequestHandler<GetItemImageListQuery, ApiResponse<PagedResult<GetItemImageListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetItemImageListQuery.Result>>> Handle(GetItemImageListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.ItemCode), request.ItemCode),
                    new CoreParamModel(nameof(request.IsThumbnail), request.IsThumbnail?.ToString())
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            img.Id,
                            img.ItemCode,
                            i.Name AS ItemName,
                            img.ImageUrl,
                            img.IsThumbnail,
                            img.SortOrder,
                            img.CreatedAt
                        FROM it_item_image img
                        LEFT JOIN it_items i ON img.ItemCode = i.Code
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (img.ItemCode LIKE @Keyword OR i.Name LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    if (!string.IsNullOrEmpty(request.ItemCode))
                    {
                        sql.AppendLine("AND img.ItemCode = @ItemCode");
                        parameters.Add("ItemCode", request.ItemCode);
                    }

                    if (request.IsThumbnail.HasValue)
                    {
                        sql.AppendLine("AND img.IsThumbnail = @IsThumbnail");
                        parameters.Add("IsThumbnail", request.IsThumbnail.Value);
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "img.SortOrder ASC" },
                        { "itemCode", "img.ItemCode" },
                        { "sortOrder", "img.SortOrder" },
                        { "createdAt", "img.CreatedAt" }
                    };

                    var (orderBy, _) = QueryHelper.BuildOrderByClause(
                        request.SortBy,
                        request.SortDirection ?? "ASC",
                        columnMappings);

                    sql.AppendLine($"ORDER BY {orderBy}");
                    sql.AppendLine("OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

                    parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);
                    parameters.Add("PageSize", request.PageSize);

                    var countSql = new StringBuilder();
                    countSql.AppendLine(@"
                        SELECT COUNT(*) 
                        FROM it_item_image img
                        LEFT JOIN it_items i ON img.ItemCode = i.Code
                        WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (img.ItemCode LIKE @Keyword OR i.Name LIKE @Keyword)");
                    }

                    if (!string.IsNullOrEmpty(request.ItemCode))
                    {
                        countSql.AppendLine("AND img.ItemCode = @ItemCode");
                    }

                    if (request.IsThumbnail.HasValue)
                    {
                        countSql.AppendLine("AND img.IsThumbnail = @IsThumbnail");
                    }

                    var items = await dbContext.QueryAsync<GetItemImageListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetItemImageListQuery.Result>
                    {
                        Items = items.ToList(),
                        Paging = new PagingInfo
                        {
                            PageIndex = request.PageIndex,
                            PageSize = request.PageSize,
                            TotalItems = totalItems
                        }
                    };

                    var response = ResponseHelper.Success(result, CoreResource.Common_msg_GetSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving item images: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetItemImageListQuery.Result>>("Error occurred while retrieving item images");

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
