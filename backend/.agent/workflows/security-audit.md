# Security Audit

Perform comprehensive security audit for the UniManage system.

## Steps

### 1. Define Audit Scope

**What to audit:**

-   [ ] Authentication & Authorization
-   [ ] Input Validation & Injection Prevention
-   [ ] Data Protection & Encryption
-   [ ] API Security
-   [ ] Configuration & Secrets Management
-   [ ] Session Management
-   [ ] Logging & Monitoring
-   [ ] Third-party Dependencies

**Priority levels:**

-   🔴 Critical: Immediate fix required
-   🟠 High: Fix within 1 week
-   🟡 Medium: Fix within 1 month
-   🟢 Low: Fix when convenient

### 2. Authentication & Authorization Audit

**Check IdentityServer Configuration:**

```csharp
// ✅ VERIFY: Token lifetimes are appropriate
new Client
{
    AccessTokenLifetime = 3600, // 1 hour (not too long)
    RefreshTokenLifetime = 2592000, // 30 days
    AbsoluteRefreshTokenLifetime = 2592000,
    RefreshTokenExpiration = TokenExpiration.Sliding,
    SlidingRefreshTokenLifetime = 1296000, // 15 days
};

// ✅ VERIFY: PKCE required for public clients
new Client
{
    ClientId = "angular-client",
    RequirePkce = true, // MUST be true
    RequireClientSecret = false, // Public client
};

// ✅ VERIFY: Allowed scopes are minimal
new Client
{
    AllowedScopes = { "openid", "profile", "api" }, // Only what's needed
};
```

**Check Authorization Policies:**

```csharp
// ✅ VERIFY: Policies are defined and used
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("RequireSystemModule", policy =>
        policy.RequireClaim("module", "System"));
});

// ❌ SECURITY ISSUE: Missing [Authorize] attribute
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id) // Anyone can delete!
{
    // ...
}

// ✅ FIXED: Require authorization
[Authorize(Policy = "RequireAdminRole")]
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    // ...
}
```

**Check for Authorization Bypass:**

```csharp
// ❌ SECURITY ISSUE: User can access other users' data
[HttpGet("{id}")]
public async Task<IActionResult> GetUser(int id)
{
    var user = await _mediator.Send(new GetUserQuery { Id = id });
    return Ok(user);
}

// ✅ FIXED: Verify user has permission
[HttpGet("{id}")]
public async Task<IActionResult> GetUser(int id)
{
    var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    // Users can only access their own data (or admin can access all)
    if (!User.IsInRole("Admin") && id.ToString() != currentUserId)
    {
        return Forbid();
    }

    var user = await _mediator.Send(new GetUserQuery { Id = id });
    return Ok(user);
}
```

### 3. Input Validation & Injection Prevention

**SQL Injection Check:**

```csharp
// ❌ CRITICAL: SQL Injection vulnerability
var username = request.Username; // User input
var sql = $"SELECT * FROM sy_users WHERE Username = '{username}'"; // DANGEROUS!
var users = await dbContext.QueryAsync<User>(sql);

// ✅ FIXED: Use parameterized queries
var sql = "SELECT * FROM sy_users WHERE Username = @Username";
var users = await dbContext.QueryAsync<User>(sql, new { Username = request.Username });

// ❌ CRITICAL: Dynamic ORDER BY vulnerability
var orderBy = request.SortBy; // User input
var sql = $"SELECT * FROM sy_users ORDER BY {orderBy}"; // DANGEROUS!

// ✅ FIXED: Use QueryHelper with whitelist
var columnMappings = new Dictionary<string, string>
{
    { "username", "Username" },
    { "email", "Email" },
};
var (orderBy, _) = QueryHelper.BuildOrderByClause(
    request.SortBy,
    request.SortDirection,
    columnMappings); // Only allows whitelisted columns
```

**XSS Prevention:**

```tsx
// ❌ HIGH: XSS vulnerability
<div dangerouslySetInnerHTML={{ __html: userInput }} />

// ✅ FIXED: Sanitize HTML or use text content
import DOMPurify from 'dompurify';

<div dangerouslySetInnerHTML={{ __html: DOMPurify.sanitize(userInput) }} />

// OR better: Use text content
<div>{userInput}</div> // React escapes by default
```

**Path Traversal Check:**

```csharp
// ❌ HIGH: Path traversal vulnerability
[HttpGet("download/{filename}")]
public IActionResult Download(string filename)
{
    var path = Path.Combine("uploads", filename); // User controls path!
    return PhysicalFile(path, "application/octet-stream");
}

// ✅ FIXED: Validate and sanitize filename
[HttpGet("download/{filename}")]
public IActionResult Download(string filename)
{
    // Validate filename
    if (string.IsNullOrEmpty(filename) ||
        filename.Contains("..") ||
        Path.IsPathRooted(filename))
    {
        return BadRequest("Invalid filename");
    }

    // Use whitelist of allowed characters
    if (!Regex.IsMatch(filename, @"^[a-zA-Z0-9_\-\.]+$"))
    {
        return BadRequest("Invalid filename format");
    }

    var path = Path.Combine("uploads", filename);

    // Verify path is still within uploads directory
    var fullPath = Path.GetFullPath(path);
    var uploadsPath = Path.GetFullPath("uploads");
    if (!fullPath.StartsWith(uploadsPath))
    {
        return BadRequest("Invalid path");
    }

    if (!System.IO.File.Exists(fullPath))
    {
        return NotFound();
    }

    return PhysicalFile(fullPath, "application/octet-stream");
}
```

**Command Injection Check:**

```csharp
// ❌ CRITICAL: Command injection
var filename = request.Filename; // User input
var process = Process.Start("convert", $"{filename} output.pdf"); // DANGEROUS!

// ✅ FIXED: Validate input and use argument array
if (!Regex.IsMatch(filename, @"^[a-zA-Z0-9_\-\.]+$"))
{
    throw new ValidationException("Invalid filename");
}

var startInfo = new ProcessStartInfo
{
    FileName = "convert",
    Arguments = $"\"{filename}\" \"output.pdf\"", // Quoted
    UseShellExecute = false, // Important!
};
var process = Process.Start(startInfo);
```

### 4. Data Protection & Encryption

**Password Storage:**

```csharp
// ❌ CRITICAL: Plain text password
INSERT INTO sy_users (Password) VALUES (@Password)

// ❌ CRITICAL: Weak hashing (MD5, SHA1)
var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));

// ✅ CORRECT: Use PasswordHelper (BCrypt)
var hash = PasswordHelper.HashPassword(password);
INSERT INTO sy_users (PasswordHash) VALUES (@PasswordHash)
```

**Sensitive Data in Logs:**

```csharp
// ❌ HIGH: Logging sensitive data
_logger.Info($"User login: {username}, Password: {password}");

// ✅ FIXED: Use CoreLogModel with masking
var log = new CoreLogModel(request.HeaderInfo)
{
    Parameter = new List<CoreParamModel>
    {
        new CoreParamModel(nameof(request.Username), request.Username),
        new CoreParamModel(nameof(request.Password), "***MASKED***"),
    }
};
UniLogManager.WriteApiLog(log);
```

**Connection Strings:**

```csharp
// ❌ HIGH: Hardcoded connection string
var connectionString = "Server=localhost;Database=UniManage;User Id=sa;Password=Admin123!;";

// ✅ FIXED: Use User Secrets / Environment Variables
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// appsettings.json (encrypted in production)
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=UniManage;Integrated Security=true;"
  }
}
```

**HTTPS Enforcement:**

```csharp
// ✅ VERIFY: HTTPS redirection enabled
app.UseHttpsRedirection();

// ✅ VERIFY: HSTS enabled in production
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

// ✅ VERIFY: Cookies are secure
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.HttpOnly = true; // Prevent XSS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS only
    options.Cookie.SameSite = SameSiteMode.Strict; // CSRF protection
});
```

### 5. API Security

**CORS Configuration:**

```csharp
// ❌ HIGH: Allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ✅ FIXED: Whitelist specific origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("https://app.unimanage.com")
              .WithMethods("GET", "POST", "PUT", "DELETE")
              .WithHeaders("Content-Type", "Authorization")
              .AllowCredentials());
});
```

**Rate Limiting:**

```csharp
// ✅ ADD: Rate limiting to prevent abuse
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("api", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 10;
    });
});

app.UseRateLimiter();

// Apply to controllers
[EnableRateLimiting("api")]
[ApiController]
public class UsersController : BaseController
{
    // ...
}
```

**API Key Validation:**

```csharp
// ✅ ADD: API key validation for external integrations
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Only for /api/external paths
        if (!context.Request.Path.StartsWithSegments("/api/external"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key missing");
            return;
        }

        var apiKey = _configuration.GetValue<string>("ApiKey");
        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        await _next(context);
    }
}
```

### 6. Session Management

**Check Session Configuration:**

```csharp
// ✅ VERIFY: Session timeout is appropriate
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Not too long
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
});

// ✅ VERIFY: Logout clears session
[HttpPost("logout")]
public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync();
    HttpContext.Session.Clear();
    return Ok();
}
```

### 7. Dependency Security Audit

**Check for vulnerable packages:**

```bash
# Backend - .NET
cd backend/src
dotnet list package --vulnerable --include-transitive

# Update vulnerable packages
dotnet add package PackageName --version x.y.z

# Frontend - npm
cd frontend/uni-manage
npm audit

# Fix automatically (if possible)
npm audit fix

# Manual fix for breaking changes
npm audit fix --force
```

**Pin dependency versions:**

```json
// package.json
{
    "dependencies": {
        "react": "19.0.0", // Exact version (not ^19.0.0)
        "@heroui/react": "beta" // Or specific beta version
    }
}
```

### 8. Configuration Security

**Secrets Management:**

```bash
# Backend - Use User Secrets in development
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=..."

# Frontend - Use environment variables
# .env.local (NOT committed)
NEXT_PUBLIC_API_URL=http://localhost:5000
API_SECRET_KEY=your-secret-key
```

**Verify no secrets in code:**

```bash
# Search for potential secrets
git grep -i "password\s*=\s*['\"]"
git grep -i "api[_-]?key\s*=\s*['\"]"
git grep -i "secret\s*=\s*['\"]"
git grep -i "token\s*=\s*['\"]"

# Check git history for leaked secrets
git log -p | grep -i "password"
```

### 9. Security Headers

**Add security headers:**

```csharp
// Add SecurityHeadersMiddleware
app.Use(async (context, next) =>
{
    // Prevent clickjacking
    context.Response.Headers.Add("X-Frame-Options", "DENY");

    // Prevent MIME type sniffing
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

    // Enable XSS protection
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

    // Referrer policy
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");

    // Content Security Policy
    context.Response.Headers.Add("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval'; " +
        "style-src 'self' 'unsafe-inline'; " +
        "img-src 'self' data: https:; " +
        "font-src 'self' data:;");

    // Permissions Policy
    context.Response.Headers.Add("Permissions-Policy",
        "geolocation=(), microphone=(), camera=()");

    await next();
});
```

**Frontend security headers (next.config.ts):**

```typescript
const nextConfig = {
    async headers() {
        return [
            {
                source: "/:path*",
                headers: [
                    { key: "X-Frame-Options", value: "DENY" },
                    { key: "X-Content-Type-Options", value: "nosniff" },
                    { key: "X-XSS-Protection", value: "1; mode=block" },
                    { key: "Referrer-Policy", value: "strict-origin-when-cross-origin" },
                    {
                        key: "Content-Security-Policy",
                        value: "default-src 'self'; script-src 'self' 'unsafe-eval' 'unsafe-inline'; style-src 'self' 'unsafe-inline';",
                    },
                ],
            },
        ];
    },
};
```

### 10. Create Security Report

**Document findings:**

```markdown
## Security Audit Report - [Date]

### Executive Summary

-   Total issues found: 12
-   Critical: 2
-   High: 4
-   Medium: 5
-   Low: 1

### Critical Issues

#### 1. SQL Injection in User Search (🔴 CRITICAL)

**File:** `UniManage.Application/Queries/Users/GetUserListQuery.cs`
**Line:** 45
**Issue:** Dynamic SQL construction with user input
**Risk:** Attackers can execute arbitrary SQL commands
**Fix:** Use parameterized queries with DatabaseHelper.BuildWhereClause()
**Status:** ✅ Fixed

#### 2. Passwords Stored in Plain Text (🔴 CRITICAL)

**File:** `UniManage.Infrastructure/Repositories/UserWriteRepository.cs`
**Line:** 23
**Issue:** Password stored without hashing
**Risk:** Database breach exposes all user passwords
**Fix:** Use PasswordHelper.HashPassword() before storing
**Status:** ✅ Fixed

### High Priority Issues

#### 3. Missing Authorization Check (🟠 HIGH)

**File:** `UniManage.Api/Controllers/UsersController.cs`
**Line:** 67
**Issue:** Delete endpoint lacks [Authorize] attribute
**Risk:** Any authenticated user can delete any user
**Fix:** Add [Authorize(Policy = "RequireAdminRole")]
**Status:** ✅ Fixed

[... continue for all issues ...]

### Recommendations

1. Implement automated security scanning in CI/CD
2. Regular dependency updates (monthly)
3. Security training for development team
4. Penetration testing before production release

### Next Audit

Scheduled: [Date + 3 months]
```

### 11. Verify Checklist

✅ Authentication properly configured
✅ Authorization checks on all endpoints
✅ SQL injection prevention verified
✅ XSS prevention verified
✅ Path traversal prevention verified
✅ Passwords properly hashed
✅ Sensitive data not logged
✅ Secrets not in source code
✅ HTTPS enforced
✅ CORS properly configured
✅ Rate limiting implemented
✅ Security headers added
✅ Dependencies up to date
✅ No known vulnerabilities
✅ Security report documented

## Security Best Practices

### Backend

-   Always use parameterized queries
-   Hash passwords with BCrypt (PasswordHelper)
-   Require [Authorize] on sensitive endpoints
-   Validate and sanitize all user input
-   Log security events (login failures, access denied)
-   Use HTTPS only in production

### Frontend

-   Never store secrets in client code
-   Sanitize HTML before rendering (DOMPurify)
-   Use HttpOnly cookies for tokens
-   Implement CSRF protection
-   Validate data before sending to API

### Database

-   Use least privilege principle for database users
-   Enable SQL Server auditing
-   Regular backups with encryption
-   Keep SQL Server patched

### Configuration

-   Use User Secrets in development
-   Use Azure Key Vault / AWS Secrets Manager in production
-   Never commit secrets to git
-   Rotate secrets regularly

## Summary

This workflow provides comprehensive security audit with:

-   Authentication & Authorization review
-   Injection attack prevention
-   Data protection verification
-   API security hardening
-   Dependency vulnerability scanning
-   Configuration security check
-   Security headers implementation
-   Detailed finding documentation
