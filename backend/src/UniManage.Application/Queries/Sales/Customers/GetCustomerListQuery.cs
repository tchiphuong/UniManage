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

namespace UniManage.Application.Queries.Sales.Customers
{
    public sealed class GetCustomerListQuery : BaseQuery, IRequest<ApiResponse<PagedResult<GetCustomerListQuery.Result>>>
    {
        public sealed class Result
        {
            public int Id { get; set; }
            public string Code { get; set; } = default!;
            public string Name { get; set; } = default!;
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetCustomerListQueryValidator : AbstractValidator<GetCustomerListQuery>
    {
        public GetCustomerListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, ApiResponse<PagedResult<GetCustomerListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetCustomerListQuery.Result>>> Handle(GetCustomerListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            Id,
                            Code,
                            Name,
                            Email,
                            Phone,
                            Address,
                            CreatedAt
                        FROM sa_customers
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (Code LIKE @Keyword OR Name LIKE @Keyword OR Email LIKE @Keyword OR Phone LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "CreatedAt DESC" },
                        { "code", "Code" },
                        { "name", "Name" },
                        { "createdAt", "CreatedAt" }
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
                    countSql.AppendLine("SELECT COUNT(*) FROM sa_customers WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (Code LIKE @Keyword OR Name LIKE @Keyword OR Email LIKE @Keyword OR Phone LIKE @Keyword)");
                    }

                    var items = await dbContext.QueryAsync<GetCustomerListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetCustomerListQuery.Result>
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
                    UniLogger.Error($"Error retrieving customers: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetCustomerListQuery.Result>>("Error occurred while retrieving customers");

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
