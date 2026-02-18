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
-   **ORM**: Entity Framework Core 9.0 + Dapper (Hybrid approach)
    - **EF Core**: CRUD operations (Create, Read, Update, Delete)
    - **Dapper**: Complex queries, joins, aggregations, high-performance scenarios
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

We use a **hybrid approach** with Entity Framework Core and Dapper:

#### EF Core (for CRUD operations)

```csharp
// In Command Handler - Create
using (var db = new DbContext(openTransaction: true)) {
    var entity = new sy_users { Username = ..., Email = ... };
    db.Set<sy_users>().Add(entity);
    await db.SaveChangesAsync(ct);
    await db.CommitAsync(ct);
    return entity.Id; // Auto-populated
}

// In Command Handler - Update with Optimistic Concurrency
using (var db = new DbContext(openTransaction: true)) {
    var entity = await db.Set<sy_users>()
        .FirstOrDefaultAsync(u => u.Id == id, ct);
    
    if (entity == null) return NotFound();
    
    // Modify properties
    entity.Email = newEmail;
    entity.UpdatedBy = currentUser;
    entity.UpdatedAt = DateTime.UtcNow;
    
    await db.SaveChangesAsync(ct); // Auto-checks RowVersion
    await db.CommitAsync(ct);
}

// In Query Handler - Read with projection
using (var db = new DbContext()) {
    var users = await db.Set<sy_users>()
        .Where(u => u.Status == "active")
        .OrderBy(u => u.Username)
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .Select(u => new UserDto { 
            Id = u.Id, 
            Username = u.Username 
        })
        .ToListAsync(ct);
    
    return users;
}
```

#### Dapper (for complex queries)

```csharp
// Complex joins and aggregations
using (var db = new DbContext()) {
    var sql = @"
        SELECT u.*, r.RoleName, d.DepartmentName
        FROM sy_users u
        LEFT JOIN sy_roles r ON u.RoleCode = r.RoleCode
        LEFT JOIN hr_departments d ON u.DepartmentCode = d.DepartmentCode
        WHERE u.Status = @Status";
    
    return await db.QueryAsync<UserDetailDto>(sql, new { Status = "active" });
}
```

**Rule of Thumb:**
- ✅ Use **EF Core** for: Single entity operations, CRUD, basic filters
- ✅ Use **Dapper** for: Complex joins, aggregations, performance-critical queries

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
