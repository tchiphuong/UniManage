---
name: unimanage-backend
description: Builds .NET backend APIs using CQRS, MediatR, Dapper, and SQL Server for UniManage system. Use when creating or modifying backend APIs, handlers, validators, or database operations.
---

# UniManage Backend Development

This skill guides you through building backend APIs for the UniManage system using CQRS pattern, MediatR, Dapper, and SQL Server.

## When to use this skill

- Creating new API endpoints (Controllers)
- Building Commands or Queries with MediatR
- Writing Handlers with Dapper database operations
- Creating FluentValidation validators
- Implementing CQRS patterns
- Working with SQL Server stored procedures or raw SQL
- Building API responses following UniManage standards
- Troubleshooting backend issues

## Tech Stack Requirements

- **.NET**: 8+
- **Pattern**: CQRS + MediatR
- **Database**: SQL Server
- **ORM**: Dapper (NO Entity Framework Core)
- **Validation**: FluentValidation
- **Logging**: log4net (per-day and per-API files)
- **Auth**: Duende IdentityServer with Dapper stores
- **Jobs**: Hangfire with SQL Server storage

## 🔥 CRITICAL: Utilities-First Approach

**ALWAYS check UniManage.Core/Utilities/ BEFORE writing any logic:**

### Available Utilities

1. **PasswordHelper**: `HashPassword()`, `VerifyPassword()`, `GenerateRandomPassword()`, `IsValidPassword()`
2. **ValidationHelper**: `IsValidEmail()`, `IsValidPhoneNumber()`, `IsValidCode()`, `ToFieldErrorModels()`
3. **DatabaseHelper**: `UserCodeExistsAsync()`, `ExecuteWithTransactionAsync()`, `QueryPagingAsync()`, `CheckTableExistsAsync()`
4. **ResponseHelper**: `Success()`, `Error()`, `NotFound()`, `Forbidden()`, `PagedSuccess()`, `PagedError()`
5. **StringHelper**: `ToSlug()`, `ToCamelCase()`, `RemoveDiacritics()`, `MaskSensitiveData()`, `GenerateCode()`
6. **DateTimeHelper**: `ToVietnamTime()`, `CalculateAge()`, `GetRelativeTime()`, `AddBusinessDays()`
7. **FileHelper**: `IsValidImageFile()`, `ValidateFileUpload()`, `GetMimeType()`, `GetFileSizeText()`
8. **QueryHelper**: `BuildOrderByClause()`, `BuildWhereClause()`, `EscapeSqlIdentifier()`
9. **TranslateHelper**: `TranslateAsync()`, `RemoveDiacritics()`, `TranslateCommonTerms()`

### ❌ NEVER DO:

- Write logic that already exists in utilities
- Create response objects manually (use ResponseHelper)
- Hash passwords outside of PasswordHelper
- Validate email/phone manually (use ValidationHelper)
- Write transaction logic manually (use DatabaseHelper)

### ✅ ALWAYS DO:

- Check utilities before coding any feature
- Extend utilities if a needed method is missing
- Use ResponseHelper for ALL API responses
- Use DatabaseHelper for ALL database operations with transactions

## RESTful API Standards (REQUIRED)

### URL Patterns (MUST use nouns, NOT verbs)

```
✅ CORRECT:
GET    /api/v1/users          - List users
GET    /api/v1/users/{id}     - Get single user
POST   /api/v1/users          - Create user
PUT    /api/v1/users/{id}     - Update user
DELETE /api/v1/users/{id}     - Delete user
GET    /api/v1/users/combobox - Helper endpoint (exception)

❌ WRONG:
GET    /api/v1/users/getList  - Redundant verb
POST   /api/v1/users/create   - Redundant verb
GET    /api/v1/getUsers       - Missing resource path
```

### HTTP Method Mapping

- **GET**: Retrieve resource(s) - NO data modification
- **POST**: Create new resource
- **PUT**: Update existing resource (full update)
- **PATCH**: Partial update (optional, rarely used)
- **DELETE**: Remove resource(s)

## API Response Standard (REQUIRED)

### Single Resource Response

```json
{
    "returnCode": 0,
    "errors": [],
    "message": "Operation successful",
    "data": {
        "id": 1,
        "name": "John Doe"
    }
}
```

### Paginated List Response

```json
{
  "returnCode": 0,
  "errors": [],
  "message": "List retrieved successfully",
  "data": {
    "items": [...],
    "paging": {
      "pageIndex": 1,
      "pageSize": 20,
      "totalItems": 100,
      "totalPages": 5
    }
  }
}
```

### Response Classes

- `ApiResponse<T>` - Single resource
- `PagedResult<T>` - Contains Items + Paging
- `PagedResponse<T>` - Extends ApiResponse<PagedResult<T>>
- `PagingInfo` - PageIndex, PageSize, TotalItems, TotalPages

## Project Structure

```
UniManage.sln
├── UniManage.API/              # Controllers (thin, HTTP → Mediator)
├── UniManage.Application/      # Commands, Queries, DTOs, Validators, Behaviors
│   ├── Commands/{Module}/{UseCase}/
│   ├── Queries/{Module}/{UseCase}/
│   └── Pipelines/              # Logging, Validation, Transaction, Caching
├── UniManage.Core/             # Database, Logging, Models, Utilities
├── UniManage.Model/            # DTOs, BaseCommand, BaseQuery
├── UniManage.Resource/         # Localization resources
└── UniManage.IdentityServer/   # Duende + Dapper stores (no EF)
```

## CQRS Principles

### Command Rules

- ✅ Changes data (INSERT, UPDATE, DELETE)
- ✅ Returns only Id or simple result (NOT full entity)
- ✅ MUST inherit `BaseCommand`
- ✅ Uses transaction via TransactionBehavior
- ✅ Naming: `VerbNounCommand` (e.g., `CreateUserCommand`)

### Query Rules

- ✅ Read-only (SELECT)
- ✅ Returns DTO shaped for UI
- ✅ MUST inherit the correct base class:
    - **`BaseListQuery`** → Query trả `PagedResult` (có Keyword, PageIndex, PageSize, Offset, SortBy, SortDirection)
    - **`BaseQuery`** → Query đơn (get by id/code, check exists, combobox...) — chỉ có HeaderInfo
- ✅ NO transaction needed
- ✅ Naming: `Get/List/FindNounQuery` (e.g., `ListUsersQuery`)

> **BaseQuery hierarchy (UniManage.Model/Common/BaseModel.cs):**
>
> ```
> BaseQuery (HeaderInfo only)
>   └── BaseListQuery (+ Keyword, SearchFields, PageIndex, PageSize, Offset, SortBy, SortDirection)
> ```

### Handler Rules

- ✅ One Handler per Command/Query
- ✅ NO ILogger injection (use UniLogManager)
- ✅ Initialize CoreLogModel with HeaderInfo
- ✅ Log with UniLogManager.WriteApiLog()

## Code Templates

### Controller (Thin Pattern)

```csharp
#region Properties

[ApiController]
[Route("api/v1/users")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region POST: /api/v1/users

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateUserCommand.Response>>> Create([FromBody] CreateUserCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;  // MUST assign HeaderInfo
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/users

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<Response>>>> GetList([FromQuery] GetUserListQuery query, CancellationToken ct)
    {
        query ??= new GetUserListQuery();
        query.HeaderInfo = HeaderInfo;  // MUST assign HeaderInfo
        var result = await _mediator.Send(query, ct);
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

### Command (MUST inherit BaseCommand)

```csharp
#region Command

public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<CreateUserCommand.Response>>
{
    // HeaderInfo inherited from BaseCommand
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

public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters")
            .Must(ValidationHelper.IsValidUserCode).WithMessage("Username allows only alphanumeric")
            .MustAsync(async (username, cancel) => !await IsUsernameExistsAsync(username))
            .WithMessage("Username is already taken");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .Must(ValidationHelper.IsValidEmail).WithMessage("Invalid email format")
            .MustAsync(async (email, cancel) => !await IsEmailExistsAsync(email))
            .WithMessage("Email is already registered");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Must(PasswordHelper.IsValidPassword)
            .WithMessage("Password must contain uppercase, lowercase, and number");
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

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
{
    public async Task<ApiResponse<CreateUserCommand.Response>> Handle(CreateUserCommand request, CancellationToken ct)
    {
        // Initialize log with HeaderInfo from BaseCommand
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
                // Hash password using PasswordHelper
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

                var response = ResponseHelper.Success(new CreateUserCommand.Response { Id = id }, "User created successfully");

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

### List Query (MUST inherit BaseListQuery)

Dùng `BaseListQuery` khi Query trả về `PagedResult` (danh sách có phân trang):

```csharp
#region Query

// ✅ List Query → kế thừa BaseListQuery
public sealed class GetUserListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetUserListQuery.Result>>>
{
    // HeaderInfo, Keyword, SearchFields, PageIndex, PageSize, Offset, SortBy, SortDirection inherited from BaseListQuery
    public string? Status { get; set; }
    public string? DepartmentCode { get; set; }

    public class Result
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}

#endregion

#region Validator

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

public sealed class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ApiResponse<PagedResult<GetUserListQuery.Result>>>
{
    public async Task<ApiResponse<PagedResult<GetUserListQuery.Result>>> Handle(GetUserListQuery request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Keyword), request.Keyword),
                new CoreParamModel(nameof(request.Status), request.Status)
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                // Build filters using DatabaseHelper
                var filters = new Dictionary<string, object?>();

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    filters.Add("Username", QueryHelper.SanitizeSearchTerm(request.Keyword));
                }

                if (!string.IsNullOrEmpty(request.Status))
                {
                    filters.Add("Status", request.Status);
                }

                var (whereClause, parameters) = DatabaseHelper.BuildWhereClause(filters);

                // Build ORDER BY using QueryHelper
                var columnMappings = new Dictionary<string, string>
                {
                    { "default", "CreatedAt" },
                    { "createdAt", "CreatedAt" },
                    { "username", "Username" },
                    { "email", "Email" }
                };
                var (orderBy, _) = QueryHelper.BuildOrderByClause(
                    request.SortBy,
                    request.SortDirection ?? "DESC",
                    columnMappings);

                // Base query
                var baseQuery = "SELECT Id, Username, Email, Status, CreatedAt FROM sy_users";

                // Use DatabaseHelper for pagination
                var result = await DatabaseHelper.QueryPagingAsync<GetUserListQuery.Result>(
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
                var response = ResponseHelper.Error<PagedResult<GetUserListQuery.Result>>("Error occurred");
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

## Database Conventions

### Table Design

- **Primary Key**: `Id` (INT IDENTITY or BIGINT)
- **Text columns**: NVARCHAR (for Vietnamese support)
- **DateTime columns**: DATETIME2(3)
- **Concurrency**: ROWVERSION → column `DataRowVersion` (map to byte[])

### Audit Columns (ALL tables)

```sql
CreatedBy NVARCHAR(50) NOT NULL,
CreatedAt DATETIME2(3) NOT NULL DEFAULT GETDATE(),
UpdatedBy NVARCHAR(50) NULL,
UpdatedAt DATETIME2(3) NULL,
DataRowVersion ROWVERSION NOT NULL
```

### Column Naming Suffixes

- Amount (money)
- Number (numeric)
- Name (text)
- Code (identifier)
- Qty (quantity)
- Date (date only)
- Rate (percentage)

### Index Naming

`IX_{TableName}_{ColumnName}`

### Pagination SQL Pattern

```sql
SELECT * FROM table
WHERE ...
ORDER BY SortColumn, Id
OFFSET (@PageIndex - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
```

## Database Access Patterns

### Always use `using` for disposal

```csharp
using (var dbContext = new DbContext())
{
    // Execute query/command
}
```

### Command Pattern (with transaction)

```csharp
using (var dbContext = new DbContext(openTransaction: true))
{
    try
    {
        // Execute command
        await dbContext.ExecuteAsync(...);
        await dbContext.CommitAsync();
    }
    catch
    {
        await dbContext.RollbackAsync();
        throw;
    }
}
```

### Query Pattern (no transaction)

```csharp
using (var dbContext = new DbContext())
{
    var result = await dbContext.QueryAsync<T>(...);
    return result;
}
```

## Region Formatting (REQUIRED)

### Command/Query Files

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

### Controller Files

```csharp
#region Properties
// Constructor and fields
#endregion

#region GET: /api/v1/resource
// GET endpoint
#endregion

#region POST: /api/v1/resource
// POST endpoint
#endregion

#region PUT: /api/v1/resource/{id}
// PUT endpoint
#endregion

#region DELETE: /api/v1/resource/{id}
// DELETE endpoint
#endregion
```

## Logging Pattern

```csharp
var log = new CoreLogModel(request.HeaderInfo)
{
    Parameter = new List<CoreParamModel>
    {
        new CoreParamModel("key", "value")
    }
};

try
{
    // Logic here
    var response = ResponseHelper.Success(data, "Success");
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

## 🔥 Quy Tắc Bắt Buộc: Đồng Bộ Instruction Khi Thay Đổi Quy Tắc

> **Khi có bất kỳ thay đổi nào ảnh hưởng đến quy tắc chung (class Common, naming convention, pattern mới, utility mới, cấu trúc project, ...) PHẢI cập nhật đủ cả 3 file:**
>
> 1. **SKILL.md** (`/.agent/skills/unimanage-backend/SKILL.md`) — cho Antigravity
> 2. **GEMINI.md** (`GEMINI.md`) — cho Gemini trong VS Code
> 3. **copilot-instructions.md** (`.github/copilot-instructions.md`) — cho GitHub Copilot
>
> **Lý do:** 3 AI tools cùng lúc, nếu không đồng bộ → mỗi tool sinh code theo pattern khác nhau → không nhất quán, gây lỗi.

## Best Practices

### ✅ DO:

1. Inherit `BaseCommand` for Commands
2. Inherit `BaseListQuery` for List Queries (PagedResult), `BaseQuery` for single-item queries
3. Use ResponseHelper for all responses
4. Use DatabaseHelper/QueryHelper for database operations
5. Use PasswordHelper for password hashing
6. Use ValidationHelper for validation
7. Log with CoreLogModel and UniLogManager
8. Use `using` statements for DbContext
9. Use transactions for Commands
10. Return DTOs from Queries (NOT entities)

### ❌ DON'T:

1. Use Entity Framework Core
2. Use `SELECT *`
3. Manually create response objects
4. Hash passwords without PasswordHelper
5. Write SQL without parameterization
6. Forget to assign HeaderInfo in Controllers
7. Inject ILogger in Handlers
8. Use verbs in API URLs
9. Return full entities from Commands
10. Use transactions for Queries

## Troubleshooting

### Issue: Transaction not working

**Solution:** Ensure `openTransaction: true` when creating DbContext:

```csharp
using (var dbContext = new DbContext(openTransaction: true))
```

### Issue: HeaderInfo is null

**Solution:** Always assign HeaderInfo in Controller:

```csharp
command.HeaderInfo = HeaderInfo;
```

### Issue: Validation not working

**Solution:** Ensure ValidationBehavior is registered in MediatR pipeline.

### Issue: Logs not appearing

**Solution:** Check log4net configuration and ensure UniLogManager.WriteApiLog() is called in finally block.

## Decision Tree

**When creating a new API endpoint:**

1. **Does it modify data?**
    - Yes → Create Command + Handler + Validator
    - No → Create Query + Handler + Validator

2. **Does it return a list (PagedResult)?**
    - Yes → Inherit **`BaseListQuery`**, return `ApiResponse<PagedResult<T>>`
    - No → Inherit **`BaseQuery`** (get by id/code/combobox), return `ApiResponse<T>`

3. **Does it need validation?**
    - Yes → Create Validator with FluentValidation
    - No → Skip validator (rare)

4. **Does it need transaction?**
    - Command → Yes (automatic via TransactionBehavior)
    - Query → No

5. **Does it use existing utility?**
    - Yes → Use from UniManage.Core/Utilities/
    - No → Consider adding to utilities if reusable

## Quick Reference

### Package Dependencies

```xml
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
<PackageReference Include="Dapper" Version="2.1.66" />
<PackageReference Include="FluentValidation" Version="12.0.0" />
<PackageReference Include="log4net" Version="3.1.0" />
<PackageReference Include="MediatR" Version="12.0.0" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
<PackageReference Include="System.Data.SqlClient" Version="4.8.6" />
```

### Common Return Codes

```csharp
public static class CoreApiReturnCode
{
    public const int Success = 0;
    public const int ValidationError = 1;
    public const int NotFound = 2;
    public const int Unauthorized = 3;
    public const int Forbidden = 4;
    public const int ExceptionOccurred = 99;
}
```

### ResponseHelper Methods

```csharp
ResponseHelper.Success(data, message)
ResponseHelper.Error<T>(message)
ResponseHelper.NotFound<T>(message)
ResponseHelper.Forbidden<T>(message)
ResponseHelper.PagedSuccess(pagedResult, message)
ResponseHelper.PagedError<T>(message)
```
