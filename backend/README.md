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

- **Framework**: .NET 9
- **Database**: SQL Server
- **ORM**: Entity Framework Core 9.0 + Dapper (Hybrid approach)
    - **EF Core**: CRUD operations (Create, Read, Update, Delete)
    - **Dapper**: Complex queries, joins, aggregations, high-performance scenarios
- **Auth**: Duende IdentityServer (Custom Stores)
- **CQRS**: MediatR
- **Validation**: FluentValidation
- **Logging**: Serilog (Sifting by API name + date)
- **Jobs**: Hangfire

## 🔒 Security Architecture

This API follows **Zero Trust Architecture** principles. Every request is treated as potentially hostile. Key security measures:

### Authentication & Authorization

- **Global `[Authorize]`** — All endpoints require authentication by default
- Use `[AllowAnonymous]` to explicitly opt-out (login, health check, etc.)
- **JWT Bearer** tokens validated against IdentityServer (signature, expiry, issuer, audience)
- **IDOR Prevention** — User identity always read from JWT claims, never from client input

### CORS (Cross-Origin Resource Security)

- **Strict whitelist** — Only explicitly allowed origins can call the API
- Configured via `appsettings.json`:

```json
{
    "Cors": {
        "AllowedOrigins": ["http://localhost:3000", "https://app.unimanage.com"]
    }
}
```

- Restricted HTTP methods: `GET`, `POST`, `PUT`, `DELETE`, `OPTIONS`
- Restricted headers: Only known headers are allowed

### HTTP Security Headers

Every response includes:
| Header | Value | Purpose |
|--------|-------|---------|
| `X-Content-Type-Options` | `nosniff` | MIME sniffing prevention |
| `X-Frame-Options` | `DENY` | Clickjacking prevention |
| `Content-Security-Policy` | `default-src 'self'` | XSS prevention |
| `Referrer-Policy` | `strict-origin-when-cross-origin` | Referrer leakage |
| `Permissions-Policy` | `camera=(), microphone=()...` | Feature restriction |
| `Strict-Transport-Security` | `max-age=31536000` | HTTPS enforcement |

### Rate Limiting (Multi-Layer)

| Layer     | Target | Limit       | Purpose                     |
| --------- | ------ | ----------- | --------------------------- |
| Global    | Per IP | 100 req/min | General DoS protection      |
| Login     | Per IP | 5 req/min   | Brute-force prevention      |
| Sensitive | Per IP | 10 req/min  | User enumeration prevention |

Sensitive endpoints: `check-username`, `check-email`, `forgot-password`, `reset-password`, `refresh-token`

### Password Security

- **BCrypt** hashing with work factor 12
- **CSPRNG** (cryptographically secure) for password generation
- **Unified policy**: min 8 chars, upper + lower + digit + special character
- **Max length 128** chars to prevent BCrypt DoS

### Data Protection

- **Sensitive data logging** disabled in production (EF Core)
- **PII masking** in API logs (`password`, `token`, `secret`, `authorization`)
- **Request body truncation** at 64KB in logs
- **Request body size** limited to 10MB

## 🔐 Security & Encryption

Sensitive configuration values (e.g., DB passwords) are encrypted at rest using AES-256.

### Required Environment Variables

> **⚠️ CRITICAL**: These MUST be set before running the application.

| Variable                    | Purpose                        | Example                      |
| --------------------------- | ------------------------------ | ---------------------------- |
| `UNIMANAGE_ENCRYPTION_SEED` | Encryption key derivation seed | `MyS3cur3Se3d!2024`          |
| `ASPNETCORE_ENVIRONMENT`    | Runtime environment            | `Development` / `Production` |

### How to Encrypt/Decrypt

```bash
# Set encryption seed first
setx UNIMANAGE_ENCRYPTION_SEED "your-secret-seed-value"

# Encrypt a password
dotnet run --project src/UniManage.Core -- encrypt "your_password_here"

# Decrypt
dotnet run --project src/UniManage.Core -- decrypt "ENC:your_encrypted_string"
```

Encrypted values start with `ENC:` in `appsettings.json`.

### Swagger Configuration

Swagger is controlled via config, **not** by environment:

```json
{
    "Swagger": {
        "Enabled": true // Set to false in production!
    }
}
```

## 📝 Coding Conventions

### CQRS Pattern

We strictly separate **Write** (Commands) and **Read** (Queries) operations.

#### Commands (Write)

- **Purpose**: Mutate state (Create, Update, Delete).
- **Base Class**: Must inherit `BaseCommand`.
- **Transaction**: Handled via `DbContext(openTransaction: true)`.
- **Return**: `ApiResponse<T>`.

```csharp
public class CreateUserCommand : BaseCommand, IRequest<ApiResponse<Response>> { ... }
```

#### Queries (Read)

- **Purpose**: Retrieve data (Select).
- **Base Class**: `BaseQuery` (single item) or `BaseListQuery` (paginated list).
- **Transaction**: **NO** transaction used.
- **Return**: `ApiResponse<T>` or `ApiResponse<PagedResult<T>>`.

```csharp
// Single item query
public class GetUserByIdQuery : BaseQuery, IRequest<ApiResponse<Response>> { ... }

// Paginated list query
public class ListUsersQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<UserDto>>> { ... }
```

### Database Access

Hybrid approach: **EF Core** for CRUD, **Dapper** for complex queries.

```csharp
// EF Core CRUD with transaction
using (var db = new DbContext(openTransaction: true)) {
    var entity = new sy_users { Username = ..., CreatedAt = DateTime.UtcNow };
    db.Set<sy_users>().Add(entity);
    await db.SaveChangesAsync(ct);
    await db.CommitAsync(ct);
}

// Dapper complex query (no transaction needed)
using (var db = new DbContext()) {
    var sql = @"SELECT u.*, r.RoleName FROM sy_users u
                LEFT JOIN sy_roles r ON u.RoleCode = r.RoleCode
                WHERE u.Status = @Status";
    return await db.QueryAsync<UserDetailDto>(sql, new { Status = "active" });
}
```

## 🚀 Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server
- `UNIMANAGE_ENCRYPTION_SEED` environment variable set

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

## 🚢 Production Deployment Checklist

- [ ] Set `UNIMANAGE_ENCRYPTION_SEED` environment variable
- [ ] Set `ASPNETCORE_ENVIRONMENT=Production`
- [ ] Encrypt database passwords in `appsettings.json` (use `ENC:` prefix)
- [ ] Configure `Cors:AllowedOrigins` with production domains
- [ ] Set `Swagger:Enabled` to `false`
- [ ] Set `Database:TrustServerCertificate` to `false`
- [ ] Trust HTTPS certificates on all servers (`dotnet dev-certs https --trust` for dev)
- [ ] Verify all security headers present in responses
- [ ] Verify rate limiting is active on login endpoints
- [ ] Run application with non-root/non-admin user
