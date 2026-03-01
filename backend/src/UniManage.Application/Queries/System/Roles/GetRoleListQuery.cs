using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Roles
{
    public sealed class GetRoleListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetRoleListQuery.Result>>>
    {
        public byte? IsActive { get; set; }

        public sealed class Result
        {
            public int Id { get; set; }
            public string RoleCode { get; set; } = default!;
            public string RoleName { get; set; } = default!;
            public string? Description { get; set; }
            public byte IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetRoleListQueryValidator : AbstractValidator<GetRoleListQuery>
    {
        public GetRoleListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, ApiResponse<PagedResult<GetRoleListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetRoleListQuery.Result>>> Handle(GetRoleListQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.IsActive), request.IsActive)
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
                            RoleCode,
                            RoleName,
                            Description,
                            IsActive,
                            CreatedAt
                        FROM sy_roles
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (RoleCode LIKE @Keyword OR RoleName LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    if (request.IsActive.HasValue)
                    {
                        sql.AppendLine("AND IsActive = @IsActive");
                        parameters.Add("IsActive", request.IsActive.Value);
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", "RoleCode" },
                        { "roleCode", "RoleCode" },
                        { "roleName", "RoleName" },
                        { "createdAt", "CreatedAt" }
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
                    countSql.AppendLine("SELECT COUNT(*) FROM sy_roles WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine("AND (RoleCode LIKE @Keyword OR RoleName LIKE @Keyword)");
                    }

                    if (request.IsActive.HasValue)
                    {
                        countSql.AppendLine("AND IsActive = @IsActive");
                    }

                    var items = await dbContext.QueryAsync<GetRoleListQuery.Result>(sql.ToString(), parameters, ct);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, ct);

                    var result = new PagedResult<GetRoleListQuery.Result>
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
                    UniLogger.Error($"Error retrieving roles: {ex.Message}", ex);

                    var response = ResponseHelper.Error<PagedResult<GetRoleListQuery.Result>>("Error occurred while retrieving roles");

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
