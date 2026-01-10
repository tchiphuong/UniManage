using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using Dapper;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Inventory.Items
{
    public sealed class GetItemListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetItemListQuery.Result>>>
    {
        public string? BrandCode { get; set; }
        public string? CategoryCode { get; set; }
        public string? ColorCode { get; set; }
        public string? SizeCode { get; set; }

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

    public sealed class GetItemListQueryValidator : AbstractValidator<GetItemListQuery>
    {
        public GetItemListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetItemListQueryHandler : IRequestHandler<GetItemListQuery, ApiResponse<PagedResult<GetItemListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetItemListQuery.Result>>> Handle(GetItemListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.BrandCode), request.BrandCode),
                    new CoreParamModel(nameof(request.CategoryCode), request.CategoryCode),
                    new CoreParamModel(nameof(request.ColorCode), request.ColorCode),
                    new CoreParamModel(nameof(request.SizeCode), request.SizeCode)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
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
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.BrandCode))
                    {
                        sql.AppendLine("AND i.BrandCode = @BrandCode");
                        parameters.Add("BrandCode", request.BrandCode);
                    }

                    if (!string.IsNullOrEmpty(request.CategoryCode))
                    {
                        sql.AppendLine("AND i.CategoryCode = @CategoryCode");
                        parameters.Add("CategoryCode", request.CategoryCode);
                    }

                    if (!string.IsNullOrEmpty(request.ColorCode))
                    {
                        sql.AppendLine("AND i.ColorCode = @ColorCode");
                        parameters.Add("ColorCode", request.ColorCode);
                    }

                    if (!string.IsNullOrEmpty(request.SizeCode))
                    {
                        sql.AppendLine("AND i.SizeCode = @SizeCode");
                        parameters.Add("SizeCode", request.SizeCode);
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (i.Code LIKE @Keyword OR i.Name LIKE @Keyword OR i.Description LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "i.CreatedAt DESC" },
                        { "code", "i.Code" },
                        { "name", "i.Name" },
                        { "createdAt", "i.CreatedAt" }
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
                    countSql.AppendLine("SELECT COUNT(*) FROM it_items i WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.BrandCode))
                    {
                        countSql.AppendLine("AND i.BrandCode = @BrandCode");
                    }

                    if (!string.IsNullOrEmpty(request.CategoryCode))
                    {
                        countSql.AppendLine("AND i.CategoryCode = @CategoryCode");
                    }

                    if (!string.IsNullOrEmpty(request.ColorCode))
                    {
                        countSql.AppendLine("AND i.ColorCode = @ColorCode");
                    }

                    if (!string.IsNullOrEmpty(request.SizeCode))
                    {
                        countSql.AppendLine("AND i.SizeCode = @SizeCode");
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (i.Code LIKE @Keyword OR i.Name LIKE @Keyword OR i.Description LIKE @Keyword)");
                    }

                    var items = await dbContext.QueryAsync<GetItemListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetItemListQuery.Result>
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
                    UniLogger.Error($"Error retrieving items: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetItemListQuery.Result>>("Error occurred while retrieving items");

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
