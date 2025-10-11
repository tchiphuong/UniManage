using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Models;
using UniManage.Core.Logging;

namespace UniManage.Application.Queries.System.User
{
    public sealed class ListUsersQuery : IRequest<PagedResponse<UserListItemDto>>
    {
        public string? Keyword { get; init; }
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 20;
        public int? Status { get; init; } // null = all, 0 = inactive, 1 = active
    }

    public sealed record UserListItemDto(
        int Id,
        string Username,
        string EmployeeCode,
        string RoleCode,
        string? Email,
        int Status,
        DateTime CreatedAt);

    public sealed class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
    {
        public ListUsersQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("Page index must be greater than 0");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, PagedResponse<UserListItemDto>>
    {
        public async Task<PagedResponse<UserListItemDto>> Handle(
            ListUsersQuery request,
            CancellationToken ct)
        {
            try
            {
                using var dbContext = new DbContext();

                // Build WHERE conditions
                var conditions = new List<string>();
                var parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    conditions.Add("([UserName] LIKE @Keyword OR [EmployeeCode] LIKE @Keyword OR [Email] LIKE @Keyword)");
                    parameters.Add("Keyword", $"%{request.Keyword}%");
                }

                if (request.Status.HasValue)
                {
                    conditions.Add("[Status] = @Status");
                    parameters.Add("Status", request.Status.Value);
                }

                parameters.Add("PageIndex", request.PageIndex);
                parameters.Add("PageSize", request.PageSize);

                var whereClause = conditions.Any() ? "WHERE " + string.Join(" AND ", conditions) : "";

                var sql = $@"
					SELECT 
						[Id],
						[UserName] AS Username,
						[EmployeeCode],
						[RoleCode],
						[Email],
						[Status],
						[CreatedAt]
					FROM [dbo].[sy_users]
					{whereClause}
					ORDER BY [CreatedAt] DESC, [Id]
					OFFSET (@PageIndex - 1) * @PageSize ROWS
					FETCH NEXT @PageSize ROWS ONLY;

					SELECT COUNT(*)
					FROM [dbo].[sy_users]
					{whereClause};";

                using var multi = await dbContext.connection.QueryMultipleAsync(sql, parameters);

                var items = await multi.ReadAsync<UserListItemDto>();
                var total = await multi.ReadSingleAsync<int>();

                var paging = new PagingInfo
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalItems = total
                };

                UniLogger.Info($"Retrieved {items.Count()} users, total: {total}");

                return PagedResponse<UserListItemDto>.Success(
                    items.ToList(),
                    paging,
                    "Users retrieved successfully");
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving users list: {ex.Message}", ex);
                return PagedResponse<UserListItemDto>.Fail("Failed to retrieve users");
            }
        }
    }
}
