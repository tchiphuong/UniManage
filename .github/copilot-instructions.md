Mục tiêu
Sinh code cho dự án UniManage theo chuẩn CQRS, .NET 9, SQL Server, Dapper (không EF Core), IdentityServer (Duende) với store tự viết, và log4net (log theo ngày & theo API). Ưu tiên code truyền thống, rõ ràng, dễ bảo trì.

**🔥 QUY TẮC QUAN TRỌNG: LUÔN SỬ DỤNG UTILITIES TRƯỚC**
Trước khi viết bất kỳ logic nào, PHẢI kiểm tra UniManage.Core/Utilities/ trước để sử dụng lại:

-   ✅ **PasswordHelper**: HashPassword(), VerifyPassword(), GenerateRandomPassword(), IsValidPassword()
-   ✅ **ValidationHelper**: IsValidEmail(), IsValidPhoneNumber(), IsValidCode(), ToFieldErrorModels() (FluentValidation)
-   ✅ **DatabaseHelper**: UserCodeExistsAsync(), ExecuteWithTransactionAsync(), QueryPagingAsync(), CheckTableExistsAsync()
-   ✅ **ResponseHelper**: Success(), Error(), NotFound(), Forbidden(), PagedSuccess(), PagedError() - API responses chuẩn
-   ✅ **StringHelper**: ToSlug(), ToCamelCase(), RemoveDiacritics(), MaskSensitiveData(), GenerateCode()
-   ✅ **DateTimeHelper**: ToVietnamTime(), CalculateAge(), GetRelativeTime(), AddBusinessDays()
-   ✅ **FileHelper**: IsValidImageFile(), ValidateFileUpload(), GetMimeType(), GetFileSizeText()
-   ✅ **QueryHelper**: BuildOrderByClause(), BuildWhereClause(), EscapeSqlIdentifier() - SQL injection prevention
-   ✅ **TranslateHelper**: TranslateAsync(), RemoveDiacritics(), TranslateCommonTerms() - Google Translate + Vietnamese

**🚫 KHÔNG ĐƯỢC:**

-   Viết lại logic đã có trong utilities
-   Tạo response object thủ công thay vì dùng ResponseHelper
-   Hash password bằng cách khác ngoài PasswordHelper.HashPassword()
-   Validate email/phone thủ công thay vì dùng ValidationHelper
-   Viết transaction logic thay vì dùng DatabaseHelper.ExecuteWithTransactionAsync()

**✅ LUÔN LÀMM:**

-   Check utilities trước khi code bất kỳ tính năng nào
-   Extend utilities nếu thiếu method cần thiết
-   Sử dụng ResponseHelper cho tất cả API responses
-   Dùng DatabaseHelper cho tất cả database operations có transaction

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

**🔥 RESTful API Standards (BẮT BUỘC)**

**URLs PHẢI dùng nouns (danh từ), KHÔNG dùng verbs (động từ):**

✅ **ĐÚNG:**

-   GET /api/v1/users - Get list
-   GET /api/v1/users/{id} - Get single
-   POST /api/v1/users - Create
-   PUT /api/v1/users/{id} - Update
-   DELETE /api/v1/users - Delete (có thể batch với body)
-   GET /api/v1/menus - Get menu tree (không cần /tree)
-   GET /api/v1/users/combobox - Exception: combobox cho UI helper

❌ **SAI:**

-   GET /api/v1/users/getList (động từ redundant)
-   GET /api/v1/menus/tree (noun redundant, menu đã là resource)
-   POST /api/v1/users/create (động từ redundant)
-   GET /api/v1/getUsers (động từ, không có resource path)

**HTTP Method Mapping:**

-   GET: Retrieve resource(s) - KHÔNG thay đổi data
-   POST: Create new resource
-   PUT: Update existing resource (full update)
-   PATCH: Partial update (optional, ít dùng)
-   DELETE: Remove resource(s)

**URL Structure Rules:**

-   Plural nouns: /users, /orders, /items (không /user, /order)
-   Hierarchical: /departments/{id}/employees
-   Query params cho filters: /users?status=active&keyword=john
-   Route params cho IDs: /users/{id}, /orders/{code}
-   Avoid redundant paths: /menus thay vì /menus/tree
-   Exception: Helper endpoints như /combobox, /export, /import

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

**🔥 QUY TẮC KẾ THỪA (BẮT BUỘC):**

**Command PHẢI kế thừa BaseCommand:**

-   BaseCommand cung cấp HeaderInfo cho logging context
-   HeaderInfo chứa Username, IP, Timestamp, TraceId từ request
-   Tự động tích hợp với CoreLogModel để log chi tiết
-   Mask sensitive data (password, token, secret) trong logs

```csharp
// ✅ ĐÚNG - Command kế thừa BaseCommand
public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<Response>>
{
    // HeaderInfo inherited from BaseCommand
    public string UserCode { get; init; } = default!;
    public string DisplayName { get; init; } = default!;

    public sealed class Response
    {
        public int Id { get; init; }
    }
}

// ❌ SAI - Thiếu BaseCommand
public sealed class CreateUserCommand : IRequest<ApiResponse<Response>>
{
    // Missing HeaderInfo - không log được context
    public string UserCode { get; init; } = default!;
}
```

**Query PHẢI kế thừa BaseQuery:**

-   BaseQuery cung cấp HeaderInfo + Keyword + PageIndex + PageSize + SortBy + SortDirection
-   Không cần khai báo lại pagination properties
-   PageIndex mặc định = 1, PageSize mặc định = 20

```csharp
// ✅ ĐÚNG - Query kế thừa BaseQuery
public sealed class ListUsersQuery : BaseQuery, IRequest<PagedResponse<UserListItemDto>>
{
    // HeaderInfo, Keyword, PageIndex, PageSize, SortBy, SortDirection inherited from BaseQuery
    public byte? Status { get; init; }
    public string? DepartmentCode { get; init; }
}

// ❌ SAI - Thiếu BaseQuery và duplicate pagination logic
public sealed class ListUsersQuery : IRequest<PagedResponse<UserListItemDto>>
{
    public string? Keyword { get; init; }
    public int PageIndex { get; init; } = 1; // Duplicate!
    public int PageSize { get; init; } = 20; // Duplicate!
}
```

**BaseCommand/BaseQuery location:**

-   Namespace: `UniManage.Model.Common`
-   File: `UniManage.Model/Common/BaseModel.cs`
-   BaseCommand extends BaseModel (has HeaderInfo with [JsonIgnore])
-   BaseQuery has HeaderInfo + pagination properties

**HeaderInfo usage in Handlers:**

```csharp
public async Task<ApiResponse<Response>> Handle(
    CreateUserCommand request,
    CancellationToken ct)
{
    var log = new CoreLogModel
    {
        Username = request.HeaderInfo?.Username ?? "Anonymous",
        IpAddress = request.HeaderInfo?.IpAddress,
        TraceId = request.HeaderInfo?.TraceId,
        Parameters = JsonConvert.SerializeObject(request, _maskSettings)
    };

    try
    {
        // Handler logic...
        log.Result = JsonConvert.SerializeObject(response);
        return response;
    }
    catch (Exception ex)
    {
        log.ErrorMessage = ex.Message;
        throw;
    }
    finally
    {
        _logger.Info(JsonConvert.SerializeObject(log));
    }
}
```

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

**Package Dependencies (Core project)**

```xml
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
<PackageReference Include="Dapper" Version="2.1.66" />
<PackageReference Include="FluentValidation" Version="12.0.0" />
<PackageReference Include="log4net" Version="3.1.0" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
<PackageReference Include="System.Data.SqlClient" Version="4.8.6" />
```

**Model Architecture (chuẩn hóa)**

```csharp
// Base API Response
public class ApiResponse<T>
{
    public int ReturnCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
}

// Paged Response Structure
public class PagedResponse<T> : ApiResponse<PagedResult<T>> { }
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public PagingInfo Paging { get; set; } = new();
}

public class PagingInfo
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}

// Validation Error Model
public class FieldErrorModel
{
    public string Field { get; set; } = string.Empty;
    public List<string> Messages { get; set; } = new();
}
```

**Core Project Structure (sau cleanup)**

```
UniManage.Core/
├── Database/           # DbContext, IDbContext, UniManageDbContext, Generator/
├── Logging/           # UniLogger, UniLogManager
├── Models/            # ApiResponse, PagedResponse, PagingInfo, FieldErrorModel, Entities/
├── Security/          # ConfigEncryption
└── Utilities/         # 9 comprehensive utility classes
```

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

**Logging Pattern trong Handler:**

```csharp
// Khởi tạo log với HeaderInfo từ BaseCommand/BaseQuery
var log = new CoreLogModel(request.HeaderInfo)
{
    Parameter = new List<CoreParamModel>
    {
        new CoreParamModel(nameof(request.Username), request.Username),
        new CoreParamModel(nameof(request.Email), request.Email)
    }
};

try
{
    // Handler logic...
    var response = ResponseHelper.Success(data, "Operation successful");
    log.Result = response;
    log.ReturnCode = response.ReturnCode;
    log.Message = response.Message;
    return response;
}
catch (Exception ex)
{
    log.IsException = 1;
    log.Message = ex.Message;
    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
    return ResponseHelper.Error<T>("Error message");
}
finally
{
    UniLogManager.WriteApiLog(log);
}
```

Yêu cầu Copilot khi sinh code:

Tạo ApiLogContextMiddleware gắn api theo {controller}-{action}.

Tạo CorrelationIdMiddleware (header X-Correlation-Id).

Tạo HttpLoggingMiddleware để log request/response + st, ms.

Cấu hình log4net bằng SiftingAppender trỏ tới logs/%date{yyyy-MM-dd}/%property{api}.log.

Controller & Handler (mẫu tối thiểu)

**🔥 CONTROLLER FORMATTING RULES (BẮT BUỘC):**

1. **Kế thừa BaseController** (KHÔNG dùng ControllerBase)
2. **Parameters PHẢI viết cùng dòng với method signature** - KHÔNG xuống dòng
3. **Body method dùng explicit blocks** - KHÔNG dùng expression-bodied members
4. **Luôn gán HeaderInfo** trước khi Send

**🔥 RESPONSEHELPER FORMATTING RULES (BẮT BUỘC):**

1. **Parameters PHẢI viết cùng dòng** - KHÔNG xuống dòng
2. **Áp dụng cho TẤT CẢ methods**: Success, Error, NotFound, Forbidden, etc.
3. **Nếu dòng quá dài (>120 chars)**: Tách object creation ra biến riêng trước

```csharp
// ✅ ĐÚNG - Parameters cùng dòng
var response = ResponseHelper.Success(new CreateUserCommand.Response
{
    Id = id
},
CoreResource.User_msg_CreateSuccess);

// ✅ ĐÚNG - Object phức tạp tách ra biến
var responseData = new CreateUserCommand.Response { Id = id, UserCode = userCode };
var response = ResponseHelper.Success(responseData, CoreResource.User_msg_CreateSuccess);

// ❌ SAI - Xuống dòng parameters
var response = ResponseHelper.Success(
    new CreateUserCommand.Response { Id = id },
    CoreResource.User_msg_CreateSuccess);
```

Controller (mỏng):

```csharp
[ApiController]
[Route("api/v1/users")]
public class UsersController : BaseController  // PHẢI kế thừa BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateUserCommand.Response>>> Create([FromBody] CreateUserCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;  // PHẢI gán HeaderInfo
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<GetUserListQuery.Response>>>> List([FromQuery] GetUserListQuery query, CancellationToken ct)
    {
        query ??= new GetUserListQuery();
        query.HeaderInfo = HeaderInfo;  // PHẢI gán HeaderInfo
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }
}
```

**🔥 REGION FORMATTING (BẮT BUỘC):**

**1. Command/Query files PHẢI có 3 regions**: Command/Query, Validator, Handler

```csharp
#region Command
// Command definition
#endregion

#region Validator
// Validator definition
#endregion

#region Handler
// Handler definition
#endregion
```

**2. Controller files PHẢI có regions cho Properties và mỗi endpoint**:

```csharp
[ApiController]
[Route("api/v1/users")]
public class UsersController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/users

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<Response>>>> GetList([FromQuery] GetUserListQuery query, CancellationToken ct)
    {
        query ??= new GetUserListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/users

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateUserCommand.Response>>> Create([FromBody] CreateUserCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/users/{id}

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<Response>>> Update([FromRoute] long id, [FromBody] UpdateUserCommand command, CancellationToken ct)
    {
        command ??= new UpdateUserCommand();
        command.Id = id;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/users/{id}

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(long id, CancellationToken ct)
    {
        var command = new DeleteUserCommand { Id = id, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}
```

Command mẫu (kèm Validator và Handler):

```csharp
#region Command

// Command (PHẢI kế thừa BaseCommand)
public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<CreateUserCommand.Response>>
{
    public string Username { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;

    public sealed class Response
    {
        public bool Success => Id > 0;
        public int Id { get; init; }
    }
}

#endregion

#region Validator

// Validator (có thể có static async methods để check DB)
public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .Cascade(CascadeMode.Stop) // Stop on first error
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters")
            .Must(ValidationHelper.IsValidUserCode).WithMessage("Username allows only alphanumeric")
            .MustAsync(async (username, cancel) => !await IsUsernameExistsAsync(username))
            .WithMessage("Username is already taken");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MustAsync(async (email, cancel) => !await IsEmailExistsAsync(email))
            .WithMessage("Email is already registered");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Z]").WithMessage("Must contain uppercase")
            .Matches(@"[a-z]").WithMessage("Must contain lowercase")
            .Matches(@"[0-9]").WithMessage("Must contain number");
    }

    private static Task<bool> IsUsernameExistsAsync(string username)
    {
        using (var dbContext = new DbContext())
        {
            return dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Username = @Username) THEN 1 ELSE 0 END",
                new { Username = username });
        }
    }

    private static Task<bool> IsEmailExistsAsync(string email)
    {
        using (var dbContext = new DbContext())
        {
            return dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Email = @Email) THEN 1 ELSE 0 END",
                new { Email = email });
        }
    }
}

#endregion

#region Handler

// Handler (KHÔNG inject ILogger, dùng UniLogManager)
// **Handler method parameters PHẢI cùng dòng với signature**
public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
{
    public async Task<ApiResponse<CreateUserCommand.Response>> Handle(CreateUserCommand request, CancellationToken ct)
    {
        // Initialize log với HeaderInfo từ BaseCommand
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Username), request.Username),
                new CoreParamModel(nameof(request.Email), request.Email)
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                // Hash password bằng PasswordHelper
                var passwordHash = PasswordHelper.HashPassword(request.Password);

                var id = await dbContext.ExecuteScalarAsync<int>(
                    @"INSERT INTO sy_users (Username, DisplayName, Email, PasswordHash, CreatedBy, CreatedAt)
                      VALUES (@Username, @DisplayName, @Email, @PasswordHash, @CreatedBy, GETDATE());
                      SELECT SCOPE_IDENTITY();",
                    new
                    {
                        request.Username,
                        request.DisplayName,
                        request.Email,
                        PasswordHash = passwordHash,
                        CreatedBy = request.HeaderInfo!.Username
                    },
                    ct);

                await dbContext.transaction.CommitAsync(ct);

                var response = ResponseHelper.Success(
                    new CreateUserCommand.Response { Id = id },
                    "User created successfully");

                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.transaction.RollbackAsync(ct);

                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                return ResponseHelper.Error<CreateUserCommand.Response>("Error occurred");
            }
        }
    }
}

#endregion
```

Query mẫu (paging với DatabaseHelper và QueryHelper):

```csharp
#region Query

// Query (PHẢI kế thừa BaseQuery - có sẵn Keyword, PageIndex, PageSize, SortBy, SortDirection)
public sealed class GetUserListQuery : BaseQuery, IRequest<ApiResponse<PagedResult>>
{
    public string? Status { get; set; }

    public class Result
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string EmployeeCode { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}

#endregion

#region Validator
// Validator (validate PageIndex/PageSize từ BaseQuery)
public sealed class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
{
    public GetUserListQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("Page index must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
    }
}

#endregion

#region Handler

// Handler (KHÔNG inject ILogger, dùng DatabaseHelper và QueryHelper)
public sealed class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ApiResponse<PagedResult>>
{
    public async Task<ApiResponse<PagedResult>> Handle(
        GetUserListQuery request,
        CancellationToken ct)
    {
        // Initialize log với HeaderInfo từ BaseQuery
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Keyword), request.Keyword),
                new CoreParamModel(nameof(request.Status), request.Status)
            }
        };

        using (var dbContext = new DbContext()) // Query KHÔNG cần transaction
        {
            try
            {
                // Build filters dictionary cho DatabaseHelper.BuildWhereClause
                var filters = new Dictionary<string, object?>();

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    filters.Add("Username", QueryHelper.SanitizeSearchTerm(request.Keyword));
                }

                if (!string.IsNullOrEmpty(request.Status))
                {
                    filters.Add("Status", request.Status);
                }

                // Build WHERE clause và parameters bằng DatabaseHelper
                var (whereClause, parameters) = DatabaseHelper.BuildWhereClause(filters);

                // Build ORDER BY bằng QueryHelper cho dynamic sorting
                var columnMappings = new Dictionary<string, string>
                {
                    { "default", "CreatedAt" },
                    { "createdAt", "CreatedAt" },
                    { "username", "Username" },
                    { "email", "Email" },
                    { "status", "Status" }
                };
                var (orderBy, _) = QueryHelper.BuildOrderByClause(
                    request.SortBy,
                    request.SortDirection ?? "DESC",
                    columnMappings);

                // Base query
                var baseQuery = "SELECT Id, Username, EmployeeCode, Email, Status, CreatedAt FROM sy_users";

                // Dùng DatabaseHelper.QueryPagingAsync cho automatic pagination + count
                var result = await DatabaseHelper.QueryPagingAsync(
                    dbContext,
                    baseQuery,
                    whereClause,
                    orderBy,
                    parameters,
                    request.PageIndex,
                    request.PageSize);

                var response = ResponseHelper.Success(result, "Users retrieved successfully");

                log.Result = result;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving users: {ex.Message}", ex);

                var response = ResponseHelper.Error<PagedResult>("Error occurred");
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
        }
    }
}

#endregion
```

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
```

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

**Query & Filter Utilities (bắt buộc cho Query handlers)**

```csharp
// 1. Build dynamic WHERE clause
var filters = new Dictionary<string, object?>
{
    { "Username", QueryHelper.SanitizeSearchTerm(keyword) },
    { "Status", status }
};
var (whereClause, parameters) = DatabaseHelper.BuildWhereClause(filters);

// 2. Build dynamic ORDER BY
var columnMappings = new Dictionary<string, string>
{
    { "default", "CreatedAt" },
    { "username", "Username" }
};
var (orderBy, _) = QueryHelper.BuildOrderByClause(
    sortBy,
    sortDirection ?? "DESC",
    columnMappings);

// 3. Execute paginated query
var result = await DatabaseHelper.QueryPagingAsync(
    dbContext,
    baseQuery,
    whereClause,
    orderBy,
    parameters,
    pageIndex,
    pageSize);
```

**✅ Luôn sử dụng:**

-   `QueryHelper.SanitizeSearchTerm()` cho search keywords
-   `DatabaseHelper.BuildWhereClause()` cho dynamic filters
-   `QueryHelper.BuildOrderByClause()` cho dynamic sorting
-   `DatabaseHelper.QueryPagingAsync()` cho pagination

**🚫 Không được:**

-   Viết raw WHERE clauses thủ công
-   Concatenate SQL strings với user input
-   Implement pagination logic thủ công

Few-shot gợi ý cho Copilot
"Sinh CreateUserCommand kế thừa BaseCommand + Handler + Validator, repo Dapper (Insert), trả ApiResponse theo chuẩn, có TransactionBehavior cho Command, log với CoreLogModel(HeaderInfo)."

"Sinh GetUserListQuery kế thừa BaseQuery + Handler + Validator, sử dụng DatabaseHelper.QueryPagingAsync, QueryHelper.BuildOrderByClause, trả ApiResponse<PagedResult>, log với CoreLogModel(HeaderInfo), sử dụng properties từ BaseQuery (Keyword, PageIndex, PageSize, SortBy, SortDirection)."

“Sinh Dapper IClientStore cho IdentityServer lấy client theo ClientId, có RedirectUris, PostLogoutUris, Scopes từ các bảng riêng.”

“Tạo log4net SiftingAppender để log theo logs/%date{yyyy-MM-dd}/%property{api}.log và error-%property{api}.log (ERROR+).”

Chất lượng & review checklist
Controller mỏng, mọi nghiệp vụ nằm trong Handler

Command/Query tách rõ, 1–1 Handler

**Command PHẢI kế thừa BaseCommand, Query PHẢI kế thừa BaseQuery**

Có Validator; lỗi hợp nhất theo ApiResponse

Command có TransactionBehavior; Query không

Dapper SQL param hóa, không SELECT \*

Update/Delete kiểm tra ROWVERSION

Query trả DTO đã shape + paging chuẩn

Log per-day/per-api, có cid/user/method/path/status/ms, mask secrets

**Handler phải sử dụng CoreLogModel(request.HeaderInfo) với Parameter list**

**Handler KHÔNG inject ILogger, dùng UniLogManager.WriteApiLog()**

**Validator có thể có static async methods để check DB**

**Query handler PHẢI dùng DatabaseHelper và QueryHelper utilities**

**Dùng ResponseHelper.Success/Error thay vì tạo response thủ công**

**Cleanup & Code Quality Rules (từ experience)**
✅ **Utilities-First Approach:**

-   Luôn check UniManage.Core/Utilities/ trước khi viết logic mới
-   Extend utilities thay vì duplicate code
-   Utilities phải có XML documentation đầy đủ

✅ **Package Dependencies:**

-   BCrypt.Net-Next cho password hashing (không tự implement)
-   FluentValidation cho validation rules
-   Dapper cho data access (không EF Core)
-   System.Data.SqlClient cho SQL Server connection

✅ **Model Structure:**

-   ApiResponse<T> cho tất cả API responses
-   PagedResponse<T> : ApiResponse<PagedResult<T>> cho pagination
-   FieldErrorModel cho validation errors từ FluentValidation

✅ **Database Patterns:**

-   using statements để đảm bảo disposal
-   Write operations có transaction (openTransaction: true)
-   Read operations không cần transaction
-   Async methods với CancellationToken

✅ **File Organization:**

-   Không duplicate folders (tránh Helpers/ và Utilities/ cùng tồn tại)
-   Xóa files không sử dụng (Database/Test/, empty handlers)
-   Tổ chức theo responsibility: Database/, Models/, Utilities/, Security/

🚫 **Anti-patterns:**

-   Không tạo response objects thủ công
-   Không hash passwords bằng MD5/SHA
-   Không viết SQL injection vulnerable queries
-   Không để files duplicate giữa các folders
-   Không để external dependencies trong Core project

Nguyên tắc: ít phép màu, ưu tiên rõ ràng, đúng “truyền thống” để team onboard nhanh, debug khỏe.
