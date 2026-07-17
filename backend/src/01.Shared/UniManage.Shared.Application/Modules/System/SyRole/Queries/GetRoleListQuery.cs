using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Shared.Domain;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyRole.Queries
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách vai trò có phân trang
    /// </summary>
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

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn danh sách vai trò
    /// </summary>
    public sealed class GetRoleListQueryValidator : AbstractValidator<GetRoleListQuery>
    {
        public GetRoleListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_invalidFormat, CoreResource.lbl_pageIndex));

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_pageSize, 100));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách vai trò
    /// </summary>
    public sealed class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, ApiResponse<PagedResult<GetRoleListQuery.Result>>>
    {
        public async Task<ApiResponse<PagedResult<GetRoleListQuery.Result>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            // Xác định các cột ngôn ngữ động (Zero Hardcode)
            var nameCol = TranslateHelper.GetLocalizedColumnName(CoreConstant.Column.Name, request.HeaderInfo?.Language);
            var descCol = TranslateHelper.GetLocalizedColumnName(CoreConstant.Column.Description, request.HeaderInfo?.Language);

            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Keyword), request.Keyword),
                    new(nameof(request.IsActive), request.IsActive)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine($@"
                        SELECT 
                            {nameof(SyRoles.Id)},
                            {nameof(SyRoles.Code)} AS RoleCode,
                            {nameCol} AS RoleName,
                            {descCol} AS Description,
                            CASE WHEN {nameof(SyRoles.Status)} = 'Active' THEN 1 ELSE 0 END AS IsActive,
                            {nameof(SyRoles.CreatedAt)}
                        FROM {nameof(SyRoles)}
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine($@"AND ({nameof(SyRoles.Code)} LIKE @Keyword OR {nameCol} LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    if (request.IsActive.HasValue)
                    {
                        sql.AppendLine($@"AND {nameof(SyRoles.Status)} = @Status");
                        parameters.Add("Status", request.IsActive.Value == 1 ? "Active" : "Inactive");
                    }

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "default", nameof(SyRoles.Code) },
                        { "roleCode", nameof(SyRoles.Code) },
                        { "roleName", nameCol },
                        { "createdAt", nameof(SyRoles.CreatedAt) }
                    };

                    var (orderBy, _) = QueryHelper.BuildOrderByClause(
                        request.SortBy,
                        request.SortDirection ?? SortDirection.Asc,
                        columnMappings);

                    sql.AppendLine($"ORDER BY {orderBy}");
                    sql.AppendLine("OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

                    parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);
                    parameters.Add("PageSize", request.PageSize);

                    var countSql = new StringBuilder();
                    countSql.AppendLine($@"SELECT COUNT(*) FROM {nameof(SyRoles)} WHERE 1=1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        countSql.AppendLine($@"AND ({nameof(SyRoles.Code)} LIKE @Keyword OR {nameCol} LIKE @Keyword)");
                    }

                    if (request.IsActive.HasValue)
                    {
                        countSql.AppendLine($@"AND {nameof(SyRoles.Status)} = @Status");
                    }

                    var items = await dbContext.QueryAsync<GetRoleListQuery.Result>(sql.ToString(), parameters, cancellationToken);
                    var totalItems = await dbContext.ExecuteScalarAsync<int>(countSql.ToString(), parameters, cancellationToken);

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

                    var response = ResponseHelper.Success(result, string.Format(CoreResource.common_getSuccess, CoreResource.entity_role));

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;

                    return response;
                }
                catch (Exception ex)
                {
                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;

                    return ResponseHelper.Error<PagedResult<GetRoleListQuery.Result>>(CoreResource.common_error);
                }
                finally
                {
                    UniLogManager.WriteApiLog(log);
                }
            }
        }
    }

    #endregion
}




