---
description: Mandatory coding constraints for UniManage backend (.NET 9, CQRS, EF Core + Dapper). Applied to ALL C# files.
alwaysOn: true
---

# UniManage Backend Rules

## 1. Language & Documentation
- ALL code comments, XML summaries, and variable names MUST be in **English**.
- Only i18n resource files (`.resx`, `locales/*.json`) may contain Vietnamese.
- Every `public class`, `interface`, `method`, `property`, and `field` MUST have `<summary>` XML comment.

## 2. Naming Conventions
- Models: `{Name}Model` suffix. **NEVER** use `Dto` suffix.
- Command: `VerbNounCommand` (e.g., `CreateUserCommand`).
- Query: `GetNounQuery` / `GetNounListQuery` (e.g., `GetUserListQuery`).
- Response DTO: Nested `public sealed record Response` inside Query/Command class.
- Validator files: Consolidated as `{Module}QueriesValidators.cs` or `{Module}CommandValidators.cs`.

## 3. Base Class Inheritance (MANDATORY)
- **Command** → `BaseCommand` (provides `HeaderInfo`).
- **Query (single item)** → `BaseQuery` (provides `HeaderInfo`).
- **Query (list/paged)** → `BaseListQuery` (provides `HeaderInfo` + `Keyword`, `PageIndex`, `PageSize`, `SortBy`, `SortDirection`).
- **Controller** → `BaseController`. Must assign `command.HeaderInfo = HeaderInfo` before `_mediator.Send()`.

## 4. Automated Logging (ILoggableCommand)
- ALL Commands and Queries MUST implement `ILoggableCommand` interface.
- **DO NOT** manually instantiate `ApiLogModel` or call `UniLogManager.WriteApiLog()` in Handlers.
- Logging is handled automatically by `LoggingBehavior` MediatR pipeline.

## 5. Database Context Scope
- **MUST**: `using (var dbContext = new DbContext())` wrapping OUTSIDE `try-catch`.
- **FORBIDDEN**: `using var dbContext = ...` inside `try-catch` (loses scope for rollback).
- Write operations: `new DbContext(openTransaction: true)` with `CommitAsync()`/`RollbackAsync()`.
- Read operations: `new DbContext()` without transaction.

## 6. RESTful API Standards
- URLs MUST be **plural nouns**: `/api/v1/users`, `/api/v1/orders`.
- **FORBIDDEN**: Verb URLs like `/getList`, `/create`, `/delete`.
- All responses wrapped in `ApiResponse<T>` or `PagedResponse<T>`.
- **PAGINATION MANDATORY**: ALL list/get-list APIs MUST return `PagedResponse<T>` with pagination. NEVER return unbounded result sets. Query MUST extend `BaseListQuery` (which provides `PageIndex`, `PageSize`).

## 7. Data Access (EF Core + Dapper Hybrid)
- **EF Core**: Single entity CRUD, basic filters, `.Set<T>()` pattern.
- **Dapper**: Complex joins, aggregations, performance-critical queries.
- **Status values**: Use `CoreCommon.Value.{Type}.{Value}` constants. NEVER hardcode strings.
- **Validators**: Use EF Core `AnyAsync()` for existence checks, NOT raw SQL.

## 8. Code Formatting
- File-scoped namespaces only (e.g., `namespace X.Y.Z;`).
- Raw SQL strings: Each field on its own line, aligned.
- Dynamic SQL: MUST use `StringBuilder`. FORBIDDEN: string concatenation.
- Controller parameters on same line as method signature.
- `ResponseHelper` parameters on same line (extract complex objects to variables first).

## 9. Anti-Patterns (FORBIDDEN)
- Manual response objects (use `ResponseHelper`).
- MD5/SHA password hashing (use `PasswordHelper` with BCrypt).
- SQL injection via string concatenation.
- `DbSet<T>` properties in DbContext (use auto-discovery `Set<T>()`).
- Dapper for simple CRUD when EF Core is simpler.
- Manual logging in Handlers (use `ILoggableCommand` pipeline).
- Hardcoded cache keys (declare in `CacheKeyConstant.cs`).
