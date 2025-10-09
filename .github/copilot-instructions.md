Mục tiêu
Sinh code cho dự án UniManage theo chuẩn CQRS, .NET 8, SQL Server, Dapper (không EF Core), IdentityServer (Duende) với store tự viết, và log4net (log theo ngày & theo API). Ưu tiên code truyền thống, rõ ràng, dễ bảo trì.

Tech stack (bắt buộc)
Backend: ASP.NET Core .NET 8

Auth: Duende IdentityServer (tự viết IClientStore, IResourceStore, IPersistedGrantStore, IDeviceFlowStore bằng Dapper)

DB: SQL Server

Data access: Dapper (❌ không EF Core)

Pattern: CQRS + MediatR

Jobs: Hangfire (SQL Server storage, dashboard port riêng)

Logging: log4net (tách file theo ngày và API)

Frontend: Angular 17+ hoặc React + Tailwind (không AngularJS)

CI/CD: GitHub Actions + Docker

Chuẩn API response (bắt buộc)
JSON camelCase, errors luôn là array.

API thường:

json
Sao chép
Chỉnh sửa
{
"returnCode": 0,
"errors": [],
"message": "Thao tác thành công",
"data": {}
}
API danh sách (phân trang):

json
Sao chép
Chỉnh sửa
{
"returnCode": 0,
"errors": [],
"message": "Lấy danh sách thành công",
"data": {
"items": [],
"paging": {
"pageIndex": 1,
"pageSize": 20,
"totalItems": 100,
"totalPages": 5
}
}
}
Dùng các class:

ApiResponse<T>

PagedResult<T> (Items, Paging)

PagedResponse<T> : ApiResponse<PagedResult<T>>

PagingInfo (PageIndex, PageSize, TotalItems, TotalPages)

Kiến trúc & CQRS (bắt buộc)
Solution layout:

swift
Sao chép
Chỉnh sửa
UniManage.sln
├─ UniManage.API // Controllers mỏng (HTTP → Mediator)
├─ UniManage.Application // Commands, Queries, DTOs, Validators, Behaviors
│ ├─ Commands/{Module}/{UseCase}
│ ├─ Queries/{Module}/{UseCase}
│ ├─ Pipelines (Logging, Validation, Transaction, Caching)
│ └─ Results (ApiResponse, PagedResponse, …)
├─ UniManage.Infrastructure // Dapper Repos (Read/Write), Migrations, log4net
└─ UniManage.IdentityServer // Duende + Dapper stores (no EF)
Nguyên tắc CQRS:

Command = thay đổi dữ liệu → không trả entity đầy đủ, chỉ Id/Result.

Query = chỉ đọc → không mutate, trả DTO đã shape cho UI.

Controller mỏng: chỉ gọi \_mediator.Send(...).

Transaction: chỉ cho Command (qua TransactionBehavior).

Validation: FluentValidation → ValidationBehavior.

Caching (tuỳ): chỉ cho Query.

Không dùng SELECT \*.

Đặt tên:

Command: VerbNounCommand (CreateUserCommand).

Query: Get/List/FindNounQuery (ListUsersQuery).

1 Handler ↔ 1 Command/Query.

Database conventions (SQL Server)
PK: Id (INT IDENTITY hoặc BIGINT).

Text TV: NVARCHAR; thời gian: DATETIME2(3).

Concurrency: ROWVERSION → cột DataRowVersion (map byte[]).

Cột audit (mọi bảng): CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, DataRowVersion.

Hậu tố cột: Amount/Number/Name/Code/Qty/Date/Rate.

Index: IX*{Table}*{Column}.

Phân trang SQL:

sql
Sao chép
Chỉnh sửa
... ORDER BY SortColumn, Id
OFFSET (@PageIndex - 1) \* @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;

Database access patterns (bắt buộc)

-   Luôn dùng using để đảm bảo dispose connection:

```csharp
using (var dbContext = new DbContext())
{
    // Thực thi query/command
}
```

-   Command: dùng transaction

```csharp
using (var dbContext = new DbContext(openTransaction: true))
{
    try
    {
        // Thực thi command
        await dbContext.CommitAsync();
    }
    catch
    {
        await dbContext.RollbackAsync();
        throw;
    }
}
```

-   Query: không cần transaction

```csharp
using (var dbContext = new DbContext())
{
    var result = await dbContext.QueryAsync(...);
    return result;
}
```

-   Repository pattern:
    -   Write repo: nhận IDbTransaction từ TransactionBehavior
    -   Read repo: tự quản lý connection lifecycle
    -   Đặt tên hàm: \*Async suffix cho async methods

IdentityServer (không EF)
Lưu Clients, IdentityResources, ApiResources, ApiScopes, PersistedGrants, DeviceCodes, Keys trong SQL Server (schema thủ công).

Implement stores bằng Dapper: IClientStore, IResourceStore, IPersistedGrantStore, IDeviceFlowStore (+ IServerSideSessionStore nếu bật).

Flow mặc định: Authorization Code + PKCE (không ROPC).

Dọn token: job Hangfire 15 phút xóa PersistedGrants/DeviceCodes hết hạn (SQL DELETE ... WHERE Expiration < SYSUTCDATETIME()).

Logging (log4net) — theo ngày & theo API
Mỗi request gắn api = {controller}-{action} → log vào file:

logs/yyyy-MM-dd/{api}.log (INFO+)

logs/yyyy-MM-dd/error-{api}.log (ERROR/FATAL)

Mọi log có: timestamp, CorrelationId (cid), user, method, path, status, ms.

Mask dữ liệu nhạy cảm: password|token|secret|authorization.

Không log raw body > 64KB.

Yêu cầu Copilot khi sinh code:

Tạo ApiLogContextMiddleware gắn api theo {controller}-{action}.

Tạo CorrelationIdMiddleware (header X-Correlation-Id).

Tạo HttpLoggingMiddleware để log request/response + st, ms.

Cấu hình log4net bằng SiftingAppender trỏ tới logs/%date{yyyy-MM-dd}/%property{api}.log.

Controller & Handler (mẫu tối thiểu)
Controller (mỏng):

csharp
Sao chép
Chỉnh sửa
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
private readonly IMediator \_mediator;
public UsersController(IMediator mediator) => \_mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateUserCommand.Response>>> Create(
        [FromBody] CreateUserCommand req, CancellationToken ct)
        => Ok(await _mediator.Send(req, ct));

    [HttpGet]
    public async Task<ActionResult<PagedResponse<UserListItemDto>>> List(
        [FromQuery] ListUsersQuery req, CancellationToken ct)
        => Ok(await _mediator.Send(req, ct));

}
Command mẫu (kèm Validator và Handler):

````csharp
// Command
public sealed class CreateUserCommand : IRequest<ApiResponse<CreateUserCommand.Response>>
{
    public string UserCode { get; init; } = default!;
    public string DisplayName { get; init; } = default!;

    public sealed class Response
    {
        public int Id { get; init; }
    }
}

// Validator
public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserCode)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .MaximumLength(200);
    }
}

// Handler
public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<CreateUserCommand.Response>> Handle(
        CreateUserCommand request,
        CancellationToken ct)
    {
        try
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var id = await dbContext.ExecuteScalarAsync<int>(
                        """
                        INSERT INTO Users (UserCode, DisplayName)
                        VALUES (@UserCode, @DisplayName);
                        SELECT SCOPE_IDENTITY();
                        """,
                        new { request.UserCode, request.DisplayName },
                        ct);

                    await dbContext.CommitAsync(ct);

                    return ApiResponse<CreateUserCommand.Response>.Success(
                        new() { Id = id });
                }
                catch
                {
                    await dbContext.RollbackAsync(ct);
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create user {UserCode}", request.UserCode);
            return ApiResponse<CreateUserCommand.Response>.Fail(
                "Failed to create user");
        }
    }
}
Query mẫu (paging):

csharp
Sao chép
Chỉnh sửa
// Query
public sealed class ListUsersQuery : IRequest<PagedResponse<UserListItemDto>>
{
    public string? Keyword { get; init; }
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

// DTO
public sealed record UserListItemDto(
    int Id,
    string UserCode,
    string DisplayName,
    byte Status);

// Handler
public sealed class ListUsersQueryHandler
    : IRequestHandler<ListUsersQuery, PagedResponse<UserListItemDto>>
{
    private readonly ILogger<ListUsersQueryHandler> _logger;

    public ListUsersQueryHandler(ILogger<ListUsersQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<PagedResponse<UserListItemDto>> Handle(
        ListUsersQuery request,
        CancellationToken ct)
    {
        try
        {
            using (var dbContext = new DbContext())
            {
                var sql = """
                    SELECT Id, UserCode, DisplayName, Status
                    FROM Users
                    WHERE (@Keyword IS NULL
                        OR UserCode LIKE @Keyword + '%'
                        OR DisplayName LIKE '%' + @Keyword + '%')
                    ORDER BY UserCode
                    OFFSET (@PageIndex - 1) * @PageSize ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM Users
                    WHERE (@Keyword IS NULL
                        OR UserCode LIKE @Keyword + '%'
                        OR DisplayName LIKE '%' + @Keyword + '%');
                    """;

                using (var multi = await dbContext.QueryMultipleAsync(
                    sql,
                    new {
                        request.Keyword,
                        request.PageIndex,
                        request.PageSize
                    },
                    ct))
                {
                    var items = await multi.ReadAsync<UserListItemDto>();
                    var total = await multi.ReadSingleAsync<int>();

                    var paging = new PagingInfo
                    {
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                        TotalItems = total
                    };

                    return PagedResponse<UserListItemDto>.Success(
                        items.ToList(),
                        paging);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get users list");
            return PagedResponse<UserListItemDto>.Fail(
                "Failed to retrieve users");
        }
    }
}
Repository classes mẫu:

```csharp
// Models
public class UserWriteModel
{
    public string UserCode { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
}

// Write repository
public class UserWriteRepository
{
    private static readonly string InsertSql = """
        INSERT INTO Users (UserCode, DisplayName)
        VALUES (@UserCode, @DisplayName);
        SELECT SCOPE_IDENTITY();
        """;

    private static readonly string UpdateSql = """
        UPDATE Users
        SET DisplayName = @DisplayName,
            UpdatedAt = SYSUTCDATETIME(),
            UpdatedBy = @UpdatedBy
        WHERE Id = @Id
        AND DataRowVersion = @RowVersion;
        """;

    private static readonly string DeleteSql = """
        DELETE FROM Users
        WHERE Id = @Id
        AND DataRowVersion = @RowVersion;
        """;

    public async Task<int> InsertAsync(
        UserWriteModel model,
        CancellationToken ct)
    {
        using (var db = new DbContext(openTransaction: true))
        {
            try
            {
                var id = await db.ExecuteScalarAsync<int>(
                    InsertSql,
                    model,
                    ct);

                await db.CommitAsync(ct);
                return id;
            }
            catch
            {
                await db.RollbackAsync(ct);
                throw;
            }
        }
    }

    public async Task<int> UpdateAsync(
        int id,
        UserWriteModel model,
        byte[] rowVersion,
        string updatedBy,
        CancellationToken ct)
    {
        using (var db = new DbContext(openTransaction: true))
        {
            try
            {
                var rows = await db.ExecuteAsync(
                    UpdateSql,
                    new {
                        Id = id,
                        model.DisplayName,
                        RowVersion = rowVersion,
                        UpdatedBy = updatedBy
                    },
                    ct);

                if (rows == 0)
                    throw new DbConcurrencyException();

                await db.CommitAsync(ct);
                return rows;
            }
            catch
            {
                await db.RollbackAsync(ct);
                throw;
            }
        }
    }
}

// Read repository
public class UserReadRepository
{
    private static readonly string SearchSql = """
        SELECT Id, UserCode, DisplayName, Status
        FROM Users
        WHERE (@Keyword IS NULL
            OR UserCode LIKE @Keyword + '%'
            OR DisplayName LIKE '%' + @Keyword + '%')
        ORDER BY UserCode
        OFFSET (@PageIndex - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*)
        FROM Users
        WHERE (@Keyword IS NULL
            OR UserCode LIKE @Keyword + '%'
            OR DisplayName LIKE '%' + @Keyword + '%');
        """;

    public async Task<(IReadOnlyList<UserListItemDto> Items, int Total)>
        SearchAsync(string? keyword, int pageIndex, int pageSize, CancellationToken ct)
    {
        using (var db = new DbContext())
        {
            using var multi = await db.QueryMultipleAsync(
                SearchSql,
                new { Keyword = keyword, PageIndex = pageIndex, PageSize = pageSize },
                ct);

            var items = await multi.ReadAsync<UserListItemDto>();
            var total = await multi.ReadSingleAsync<int>();

            return (items.ToList(), total);
        }
    }
}
````

Repository pattern đơn giản hóa:

-   Không dùng interface và DI
-   Static SQL queries
-   Write repo tự quản lý transaction
-   Read repo không dùng transaction

MediatR Pipelines (bắt buộc)
Thứ tự: Logging → Validation → Authorization (tuỳ) → Transaction (Command) → Caching (Query)

LoggingBehavior: log handler name + thời gian.

ValidationBehavior: gom lỗi FluentValidation → ApiResponse.Fail(errors).

TransactionBehavior (Command): mở SqlConnection + BeginTransaction, truyền IDbTransaction xuống repo, commit/rollback.

CachingBehavior (Query, tuỳ): key = TypeName + hash(params).

JSON camelCase & chuẩn hóa
csharp
Sao chép
Chỉnh sửa
builder.Services.AddControllers()
.AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
❌ Không SELECT \*.

✔️ Có <summary> cho mọi public type/method/prop.

Few-shot gợi ý cho Copilot
“Sinh CreateUserCommand + Handler + Validator, repo Dapper (Insert), trả ApiResponse theo chuẩn, có TransactionBehavior cho Command.”

“Sinh ListUsersQuery + Handler + repo Dapper (Search + COUNT, OFFSET/FETCH), trả PagedResponse<UserListItemDto>.”

“Sinh Dapper IClientStore cho IdentityServer lấy client theo ClientId, có RedirectUris, PostLogoutUris, Scopes từ các bảng riêng.”

“Tạo log4net SiftingAppender để log theo logs/%date{yyyy-MM-dd}/%property{api}.log và error-%property{api}.log (ERROR+).”

Chất lượng & review checklist
Controller mỏng, mọi nghiệp vụ nằm trong Handler

Command/Query tách rõ, 1–1 Handler

Có Validator; lỗi hợp nhất theo ApiResponse

Command có TransactionBehavior; Query không

Dapper SQL param hóa, không SELECT \*

Update/Delete kiểm tra ROWVERSION

Query trả DTO đã shape + paging chuẩn

Log per-day/per-api, có cid/user/method/path/status/ms, mask secrets

Nguyên tắc: ít phép màu, ưu tiên rõ ràng, đúng “truyền thống” để team onboard nhanh, debug khỏe.
