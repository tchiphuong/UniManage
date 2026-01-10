# UniManage Backend

This repository contains the backend source code for **UniManage**, built with **ASP.NET Core 9** and **Dapper**.

## 🏗 Architecture

The solution follows **Clean Architecture** principles with **CQRS** pattern:

```
src/
├── UniManage.Api/           # Entry point (Controllers, Middleware, DI)
├── UniManage.Application/   # Business Logic (Commands, Queries, Validators)
├── UniManage.Core/          # Domain, Database, Infrastructure, Utils
├── UniManage.Model/         # Shared Entities, DTOs, Enums
├── UniManage.IdentityServer/# Auth Server (Duende IdentityServer)
├── UniManage.Resource/      # Localization Resources (T4 Templates)
└── UniManage.Worker/        # Background Jobs (Hangfire)
```

## 🛠 Tech Stack

-   **Framework**: .NET 9
-   **Database**: SQL Server
-   **ORM**: Dapper (No EF Core)
-   **Auth**: Duende IdentityServer (Custom Stores)
-   **CQRS**: MediatR
-   **Validation**: FluentValidation
-   **Logging**: log4net (SiftingAppender)
-   **Jobs**: Hangfire

## 📝 Coding Conventions

### CQRS Pattern

We strictly separate **Write** (Commands) and **Read** (Queries) operations.

#### Commands (Write)
-   **Purpose**: Mutate state (Create, Update, Delete).
-   **Base Class**: Must inherit `BaseCommand`.
-   **Transaction**: Handled automatically via `TransactionBehavior`.
-   **Return**: `ApiResponse<T>`.

```csharp
// Example
public class CreateUserCommand : BaseCommand, IRequest<ApiResponse<Response>> { ... }
```

#### Queries (Read)
-   **Purpose**: Retrieve data (Select).
-   **Base Class**: Must inherit `BaseQuery` (provides Pagination, Sort, HeaderInfo).
-   **Transaction**: **NO** transaction used.
-   **Return**: `PagedResponse<T>` or `ApiResponse<T>`.

```csharp
// Example
public class ListUsersQuery : BaseQuery, IRequest<PagedResponse<UserDto>> { ... }
```

### Database Access

All database interaction goes through `UniManageDbContext` using Dapper.

```csharp
// In Command Handler (Transaction is managed by structure)
using (var db = new DbContext(openTransaction: true)) {
    await db.ExecuteAsync(...);
    await db.CommitAsync();
}

// In Query Handler (Read-only)
using (var db = new DbContext()) {
    return await db.QueryAsync(...);
}
```

## 🔐 Security & Encryption

Sensitive configuration values (e.g., DB Connection Strings) must be encrypted in Production.
-   **Algorithm**: AES-256
-   **Tool**: `UniManage.Core` Console Application.

### How to Encrypt/Decrypt
The encryption utility is built into the `UniManage.Core` project.

**Encrypt:**
```bash
dotnet run --project src/UniManage.Core -- encrypt "your_password_here"
```

**Decrypt:**
```bash
dotnet run --project src/UniManage.Core -- decrypt "ENC:your_encrypted_string"
```

**Config**: Encrypted values start with `ENC:` in `appsettings.json`.

## 🚀 Getting Started

### Prerequisites
-   .NET 9 SDK
-   SQL Server

### Setup Database
1.  Create `UniManage` and `UniManageLog` databases.
2.  Run migrations located in `../scripts/` (See `../scripts/README.md` for execution order).

### Run API
```bash
cd src/UniManage.Api
dotnet run
```
API URL: `http://localhost:5297`

### Run IdentityServer
```bash
cd src/UniManage.IdentityServer
dotnet run
```
Auth URL: `http://localhost:5000`
