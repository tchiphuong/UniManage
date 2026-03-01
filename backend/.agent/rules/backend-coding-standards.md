# Backend Coding Standards

**Activation**: Always On

This rule applies to all backend development in the UniManage project.

## Tech Stack Requirements

-   **.NET 8+** with ASP.NET Core
-   **CQRS Pattern** with MediatR
-   **Dapper** for data access (NO Entity Framework Core)
-   **SQL Server** as database
-   **FluentValidation** for validation
-   **log4net** for logging
-   **Duende IdentityServer** for authentication

## Critical: Utilities-First Approach

**ALWAYS check `UniManage.Core/Utilities/` before writing any logic.**

Available utilities:

-   `PasswordHelper`: HashPassword(), VerifyPassword(), GenerateRandomPassword()
-   `ValidationHelper`: IsValidEmail(), IsValidPhoneNumber(), IsValidCode()
-   `DatabaseHelper`: QueryPagingAsync(), BuildWhereClause(), ExecuteWithTransactionAsync()
-   `ResponseHelper`: Success(), Error(), NotFound(), Forbidden(), PagedSuccess()
-   `StringHelper`: ToSlug(), ToCamelCase(), RemoveDiacritics(), MaskSensitiveData()
-   `DateTimeHelper`: ToVietnamTime(), CalculateAge(), GetRelativeTime()
-   `FileHelper`: IsValidImageFile(), ValidateFileUpload(), GetMimeType()
-   `QueryHelper`: BuildOrderByClause(), EscapeSqlIdentifier()
-   `TranslateHelper`: TranslateAsync(), RemoveDiacritics()

**Never:**

-   Write logic that exists in utilities
-   Create response objects manually (use ResponseHelper)
-   Hash passwords without PasswordHelper
-   Validate email/phone manually (use ValidationHelper)

## CQRS Pattern Rules

### Commands

-   **Must inherit** `BaseCommand` from `UniManage.Model.Common`
-   Changes data (INSERT, UPDATE, DELETE)
-   Returns only Id or simple result (NOT full entity)
-   Uses transaction automatically via TransactionBehavior
-   Naming: `VerbNounCommand` (e.g., `CreateUserCommand`)

### Queries

-   **Must inherit** `BaseQuery` from `UniManage.Model.Common`
-   Read-only operations (SELECT)
-   Returns DTO shaped for UI
-   NO transaction needed
-   Naming: `Get/List/FindNounQuery` (e.g., `ListUsersQuery`)

### Handlers

-   One Handler per Command/Query
-   NO `ILogger` injection (use `UniLogManager`)
-   Initialize `CoreLogModel` with `request.HeaderInfo`
-   Always call `UniLogManager.WriteApiLog(log)` in finally block

## API Standards (RESTful)

### URL Patterns (MUST use nouns, NOT verbs)

```
✅ CORRECT:
GET    /api/v1/users          - List users
GET    /api/v1/users/{id}     - Get single user
POST   /api/v1/users          - Create user
PUT    /api/v1/users/{id}     - Update user
DELETE /api/v1/users/{id}     - Delete user

❌ WRONG:
GET    /api/v1/users/getList  - Redundant verb
POST   /api/v1/users/create   - Redundant verb
GET    /api/v1/getUsers       - Verb in URL
```

### HTTP Methods

-   **GET**: Retrieve (no data modification)
-   **POST**: Create new resource
-   **PUT**: Update existing resource
-   **DELETE**: Remove resource

## Controller Standards

Controllers must:

-   Inherit from `BaseController`
-   Be thin (only HTTP → Mediator)
-   **Always assign** `HeaderInfo` to commands/queries
-   Use regions for organization

```csharp
#region Properties
private readonly IMediator _mediator;
#endregion

#region POST: /api/v1/users
[HttpPost]
public async Task<ActionResult<ApiResponse<Response>>> Create(
    [FromBody] CreateUserCommand command,
    CancellationToken ct)
{
    command.HeaderInfo = HeaderInfo;  // REQUIRED
    var result = await _mediator.Send(command, ct);
    return Ok(result);
}
#endregion
```

## Response Standards

All API responses must use:

-   `ApiResponse<T>` for single resources
-   `PagedResponse<T>` for paginated lists
-   `ResponseHelper` methods (never create manually)

Response format:

```json
{
    "returnCode": 0,
    "errors": [],
    "message": "Operation successful",
    "data": {}
}
```

## File Structure

### Commands/Queries Organization

Each Command/Query file must have 3 regions:

```csharp
#region Command
// Command definition inheriting BaseCommand
#endregion

#region Validator
// FluentValidation validator
#endregion

#region Handler
// Handler implementation
#endregion
```

### Project Structure

```
UniManage.Api/          - Controllers (thin)
UniManage.Application/  - Commands, Queries, Validators, Pipelines
UniManage.Core/         - Database, Logging, Utilities
UniManage.Model/        - DTOs, BaseCommand, BaseQuery
```

## Database Standards

### Connection Management

Always use `using` statements:

```csharp
// Query (no transaction)
using (var dbContext = new DbContext())
{
    var result = await dbContext.QueryAsync<T>(...);
    return result;
}

// Command (with transaction)
using (var dbContext = new DbContext(openTransaction: true))
{
    try
    {
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

### Table Conventions

-   Primary Key: `Id` (INT IDENTITY or BIGINT)
-   Text columns: `NVARCHAR` (for Vietnamese)
-   DateTime columns: `DATETIME2(3)`
-   Concurrency: `DataRowVersion` (ROWVERSION → byte[])

### Audit Columns (ALL tables)

```sql
CreatedBy NVARCHAR(50) NOT NULL,
CreatedAt DATETIME2(3) NOT NULL DEFAULT GETDATE(),
UpdatedBy NVARCHAR(50) NULL,
UpdatedAt DATETIME2(3) NULL,
DataRowVersion ROWVERSION NOT NULL
```

### Never use `SELECT *`

Always specify columns explicitly.

## Validation Standards

Use FluentValidation with these patterns:

```csharp
RuleFor(x => x.Email)
    .NotEmpty().WithMessage("Email is required")
    .Must(ValidationHelper.IsValidEmail).WithMessage("Invalid email");

RuleFor(x => x.Username)
    .Cascade(CascadeMode.Stop)
    .NotEmpty()
    .Length(3, 50)
    .Must(ValidationHelper.IsValidUserCode)
    .MustAsync(async (username, ct) => !await IsUsernameExistsAsync(username));
```

## Logging Standards

### Pattern for ALL Handlers

```csharp
var log = new CoreLogModel(request.HeaderInfo)
{
    Parameter = new List<CoreParamModel>
    {
        new CoreParamModel(nameof(request.Field), request.Field)
    }
};

try
{
    // Handler logic
    var response = ResponseHelper.Success(data, "Success");
    log.Result = response;
    log.ReturnCode = response.ReturnCode;
    return response;
}
catch (Exception ex)
{
    log.IsException = 1;
    log.Message = ex.Message;
    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
    return ResponseHelper.Error<T>("Error occurred");
}
finally
{
    UniLogManager.WriteApiLog(log);
}
```

## Security Standards

-   Use `PasswordHelper.HashPassword()` for password hashing
-   Use `StringHelper.MaskSensitiveData()` for logging
-   Always use parameterized queries (Dapper handles this)
-   Use `QueryHelper.EscapeSqlIdentifier()` for dynamic column names

## Code Quality Checklist

Before submitting code, verify:

✅ Command/Query inherits BaseCommand/BaseQuery
✅ HeaderInfo is assigned in Controller
✅ ResponseHelper used for all responses
✅ Utilities used instead of reimplementing logic
✅ Validator created with FluentValidation
✅ Logging with CoreLogModel + UniLogManager
✅ Using statements for DbContext
✅ Transactions for Commands, none for Queries
✅ Regions properly organized
✅ No SELECT \*
✅ RESTful URLs (nouns, not verbs)
