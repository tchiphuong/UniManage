---
name: unimanage-backend
description: Architecture reference and code templates for UniManage backend development using .NET 9, CQRS, MediatR, EF Core + Dapper hybrid. Use when creating new backend modules, Commands, Queries, Controllers, or Validators.
---

# UniManage Backend Developer Skill

## When to use this skill

- Creating new backend modules (Commands, Queries, Handlers, Controllers)
- Writing FluentValidation validators
- Choosing between EF Core and Dapper for data access
- Understanding the project's CQRS architecture
- Referencing CoreCommon constants, utilities, or response patterns

## Tech Stack

- **Runtime**: ASP.NET Core .NET 9
- **Auth**: Duende IdentityServer (custom Dapper stores)
- **Database**: SQL Server
- **Data Access**: EF Core 9.0 (CRUD) + Dapper (complex queries)
- **Pattern**: CQRS + MediatR + FluentValidation
- **Jobs**: Hangfire (SQL Server storage)
- **Logging**: log4net (per-day, per-API files) + `ILoggableCommand` pipeline

## Solution Architecture

```
UniManage.sln
├─ UniManage.WebApi           // Thin Controllers (HTTP → MediatR)
├─ UniManage.Shared.Application // Commands, Queries, Validators, Pipelines
│  └─ Modules/{System|HR|...}/{Entity}/
│     ├─ Commands/            // CreateXxxCommand.cs, UpdateXxxCommand.cs, ...
│     └─ Queries/             // GetXxxListQuery.cs, GetXxxByIdQuery.cs, ...
├─ UniManage.Shared.Domain    // Entities, Base classes, Models, Interfaces
├─ UniManage.Shared.Infrastructure // DbContext, Pipelines, Utilities
└─ UniManage.IdentityServer   // Duende + Dapper stores
```

## Base Class Hierarchy

```
BaseQuery              → HeaderInfo only (for get-by-id, combobox, etc.)
  └── BaseListQuery    → + Keyword, SearchFields, PageIndex, PageSize, Offset, SortBy, SortDirection

BaseCommand            → HeaderInfo (for create, update, delete)
```

## Response Model Architecture

```csharp
ApiResponse<T>         // { ReturnCode, Message, Data, Errors }
PagedResponse<T>       // : ApiResponse<PagedResult<T>>
PagedResult<T>         // { Items, Paging }
PagingInfo             // { PageIndex, PageSize, TotalItems, TotalPages }
```

## Data Access Decision Tree

```
Need to access DB?
├── Simple CRUD (1 entity)? → EF Core (.Set<T>(), LINQ)
├── Complex joins / aggregations? → Dapper (raw SQL via dbContext.QueryAsync)
├── Existence check in Validator? → EF Core (AnyAsync)
└── Performance-critical read? → Dapper
```

## CoreCommon Constants (Auto-generated from sy_commons via T4)

```csharp
// Type keys
CoreCommon.Type.Commonstatus       // "CommonStatus"
CoreCommon.Type.Invoicestatus      // "InvoiceStatus"
CoreCommon.Type.Language           // "Language"

// Value keys
CoreCommon.Value.Commonstatus.Active      // "active"
CoreCommon.Value.Commonstatus.Inactive    // "inactive"
CoreCommon.Value.Language.English         // "english"
CoreCommon.Value.Language.Vietnamese      // "vietnamese"
```

Regenerate: Open `CoreCommon.tt` → Right-click → "Run Custom Tool".

## Available Utilities (ALWAYS check before writing new logic)

| Utility            | Key Methods                                                              |
| ------------------ | ------------------------------------------------------------------------ |
| `ResponseHelper`   | `Success()`, `Error()`, `NotFound()`, `PagedSuccess()`, `PagedError()`   |
| `PasswordHelper`   | `HashPassword()`, `VerifyPassword()`, `IsValidPassword()`                |
| `ValidationHelper` | `IsValidEmail()`, `IsValidPhoneNumber()`, `IsValidCode()`                |
| `DatabaseHelper`   | `QueryPagingAsync()`, `ExecuteWithTransactionAsync()`                    |
| `QueryHelper`      | `BuildOrderByClause()`, `SanitizeSearchTerm()`, `EscapeSqlIdentifier()`  |
| `StringHelper`     | `ToSlug()`, `ToCamelCase()`, `RemoveDiacritics()`, `MaskSensitiveData()` |
| `DateTimeHelper`   | `ToVietnamTime()`, `CalculateAge()`, `AddBusinessDays()`                 |
| `TranslateHelper`  | `GetLanguageSuffix()`, `TranslateAsync()`                                |
| `CacheHelper`      | `BuildKey()`, `RemoveByPatternAsync()`                                   |

## MediatR Pipeline Order

`Logging → Validation → Authorization → Transaction (Command only) → Caching (Query only)`

## Standard CRUD Code Templates

For complete, ready-to-copy code templates, read the files in the `examples/` directory:

- **@examples/standard-query.cs** — Paginated list query + get-by-id query
- **@examples/standard-command.cs** — Create command with transaction
- **@examples/standard-controller.cs** — Thin controller with region formatting
- **@examples/standard-validators.cs** — Consolidated validators pattern

### Key Patterns in Templates

1. **ILoggableCommand (CRITICAL RULE)**: All Commands/Queries inherit `BaseCommand`/`BaseQuery` which implements `ILoggableCommand`. Logging is automated by `LoggingBehavior` pipeline.
   - 🚫 **FORBIDDEN**: Do NOT instantiate `ApiLogModel`.
   - 🚫 **FORBIDDEN**: Do NOT call `UniLogManager.WriteApiLog(log)`.
   - 🚫 **FORBIDDEN**: Do NOT wrap Handler logic in a `try-catch-finally` just to write logs. Throw exceptions normally so the pipeline can catch and log them.
2. **Nested Response**: `public sealed record Response` nested inside the Command/Query class.
3. **DbContext scope**: `using (var dbContext = new DbContext(openTransaction: true))` wraps OUTSIDE a single `try-catch`.
   - When an exception occurs, call `await dbContext.RollbackAsync()` then `throw;` to let the pipeline log the 500 error.
4. **Validators grouped**: One file per feature group (e.g., `UserQueriesValidators.cs`).

## Controller Formatting

```csharp
[ApiController]
[Route("api/v1/users")]
public class UsersController : BaseController
{
    #region Properties
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator) { _mediator = mediator; }
    #endregion

    #region GET: /api/v1/users
    [HttpGet]
    public async Task<ActionResult<PagedResponse<GetUserListQuery.Response>>> GetList([FromQuery] GetUserListQuery query, CancellationToken ct)
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
}
```

## IdentityServer (Dapper-only, no EF Core)

- Custom stores: `IClientStore`, `IResourceStore`, `IPersistedGrantStore`, `IDeviceFlowStore`
- Flow: Authorization Code + PKCE (no ROPC)
- Token cleanup: Hangfire job every 15 min

## Social Auth

- Strategy Pattern via `ISocialAuthProvider` interface
- Register providers in `ApplicationModule` (Autofac)
- Use `SocialAuthProviderFactory` in Handlers
