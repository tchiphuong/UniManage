using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Sales.Orders
{
    public sealed class GetOrderListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetOrderListQuery.Result>>>
    {
        public string? CustomerCode { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string OrderCode { get; set; } = default!;
            public string CustomerCode { get; set; } = default!;
            public string CustomerName { get; set; } = default!;
            public string EmployeeCode { get; set; } = default!;
            public string EmployeeName { get; set; } = default!;
            public DateTime? OrderDate { get; set; }
            public decimal? TotalAmount { get; set; }
            public string Status { get; set; } = default!;
            public DateTime? CreatedAt { get; set; }
        }
    }

    public sealed class GetOrderListQueryValidator : AbstractValidator<GetOrderListQuery>
    {
        public GetOrderListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, ApiResponse<PagedResult<GetOrderListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetOrderListQuery.Result>>> Handle(GetOrderListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.CustomerCode), request.CustomerCode),
                    new CoreParamModel(nameof(request.Status), request.Status)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            o.Id,
                            o.OrderCode,
                            o.CustomerCode,
                            c.FullName AS CustomerName,
                            o.EmployeeCode,
                            e.FullName AS EmployeeName,
                            o.OrderDate,
                            o.TotalAmount,
                            o.Status,
                            o.CreatedAt
                        FROM sa_orders o
                        LEFT JOIN sa_customers c ON o.CustomerCode = c.CustomerCode
                        LEFT JOIN hr_employees e ON o.EmployeeCode = e.EmployeeCode
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (o.OrderCode LIKE @Keyword OR c.FullName LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    if (!string.IsNullOrEmpty(request.CustomerCode))
                    {
                        sql.AppendLine("AND o.CustomerCode = @CustomerCode");
                        parameters.Add("CustomerCode", request.CustomerCode);
                    }

                    if (!string.IsNullOrEmpty(request.Status))
                    {
                        sql.AppendLine("AND o.Status = @Status");
                        parameters.Add("Status", request.Status);
                    }

                    if (request.FromDate.HasValue)
                    {
                        sql.AppendLine("AND o.OrderDate >= @FromDate");
                        parameters.Add("FromDate", request.FromDate.Value);
                    }

                    if (request.ToDate.HasValue)
                    {
                        sql.AppendLine("AND o.OrderDate <= @ToDate");
                        parameters.Add("ToDate", request.ToDate.Value);
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "o.OrderDate DESC" },
                        { "orderCode", "o.OrderCode" },
                        { "orderDate", "o.OrderDate" },
                        { "totalAmount", "o.TotalAmount" },
                        { "status", "o.Status" }
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
                        FROM sa_orders o
                        LEFT JOIN sa_customers c ON o.CustomerCode = c.CustomerCode
                        WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (o.OrderCode LIKE @Keyword OR c.FullName LIKE @Keyword)");
                    }

                    if (!string.IsNullOrEmpty(request.CustomerCode))
                    {
                        countSql.AppendLine("AND o.CustomerCode = @CustomerCode");
                    }

                    if (!string.IsNullOrEmpty(request.Status))
                    {
                        countSql.AppendLine("AND o.Status = @Status");
                    }

                    if (request.FromDate.HasValue)
                    {
                        countSql.AppendLine("AND o.OrderDate >= @FromDate");
                    }

                    if (request.ToDate.HasValue)
                    {
                        countSql.AppendLine("AND o.OrderDate <= @ToDate");
                    }

                    var items = await dbContext.QueryAsync<GetOrderListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetOrderListQuery.Result>
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
                    UniLogger.Error($"Error retrieving orders: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetOrderListQuery.Result>>("Error occurred while retrieving orders");

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
