using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Queries.System.User
{
    public sealed class GetUserListQuery : BaseQuery, IRequest<ApiResponse<PagedResult>>
    {
        public int? Status { get; set; }

        public class Result
        {
            public int Id { get; set; }
            public string Username { get; set; } = default!;
            public string EmployeeCode { get; set; } = default!;
            public string RoleCode { get; set; } = default!;
            public string? Email { get; set; }
            public int Status { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class ListUsersQueryValidator : AbstractValidator<GetUserListQuery>
    {
        public ListUsersQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("Page index must be greater than 0");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }

    public sealed class ListUsersQueryHandler : IRequestHandler<GetUserListQuery, ApiResponse<PagedResult>>
    {
        public async Task<ApiResponse<PagedResult>> Handle(GetUserListQuery request, CancellationToken ct)
        {
            try
            {
                using (var dbContext = new DbContext())
                {
                    var sql = """
                        SELECT Id, Username, EmployeeCode, RoleCode, Email, Status, CreatedAt
                        FROM Users
                        WHERE (@Keyword IS NULL 
                            OR Username LIKE @Keyword + '%' 
                            OR EmployeeCode LIKE @Keyword + '%'
                            OR Email LIKE '%' + @Keyword + '%')
                        AND (@Status IS NULL OR Status = @Status)
                        ORDER BY CreatedAt DESC
                        OFFSET (@PageIndex - 1) * @PageSize ROWS
                        FETCH NEXT @PageSize ROWS ONLY;

                        SELECT COUNT(*)
                        FROM Users
                        WHERE (@Keyword IS NULL 
                            OR Username LIKE @Keyword + '%' 
                            OR EmployeeCode LIKE @Keyword + '%'
                            OR Email LIKE '%' + @Keyword + '%')
                        AND (@Status IS NULL OR Status = @Status);
                        """;

                    var cmd = new CommandDefinition(
                        sql,
                        new
                        {
                            request.Keyword,
                            request.Status,
                            request.PageIndex,
                            request.PageSize
                        },
                        dbContext.transaction,
                        cancellationToken: ct);

                    using (var multi = await dbContext.connection.QueryMultipleAsync(cmd))
                    {
                        var items = await multi.ReadAsync<GetUserListQuery.Result>();
                        var total = await multi.ReadSingleAsync<int>();

                        var result = new PagedResult
                        {
                            Items = items.Cast<object>().ToList(),
                            Paging = new PagingInfo
                            {
                                PageIndex = request.PageIndex,
                                PageSize = request.PageSize,
                                TotalItems = total
                            }
                        };

                        return ResponseHelper.Success(result, "Users retrieved successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving users list: {ex.Message}", ex);
                return ResponseHelper.Error<PagedResult>("Failed to retrieve users");
            }
        }
    }
}
