# MediatR Pipeline Behaviors - UniManage

## Mục đích
Document này mô tả chi tiết về các MediatR Pipeline Behaviors trong dự án UniManage, giúp AI Agent hiểu và implement đúng chuẩn CQRS.

## Thứ tự thực thi Pipeline (bắt buộc)
```
Request → Logging → Validation → Authorization → Transaction/Caching → Handler → Response
```

**Thứ tự chi tiết:**
1. **LoggingBehavior** - Log tất cả requests
2. **ValidationBehavior** - Validate input bằng FluentValidation
3. **AuthorizationBehavior** (tuỳ chọn) - Kiểm tra quyền
4. **TransactionBehavior** (chỉ Command) - Quản lý database transaction
5. **CachingBehavior** (chỉ Query) - Cache kết quả

---

## 1. LoggingBehavior (bắt buộc)

### Mục đích
- Log mọi request/response qua MediatR
- Đo thời gian xử lý
- Log handler name và parameters

### Implementation
```csharp
public class LoggingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "Handling {RequestName} {@Request}",
            requestName,
            request);

        try
        {
            var response = await next();
            
            stopwatch.Stop();
            
            _logger.LogInformation(
                "Handled {RequestName} in {ElapsedMs}ms",
                requestName,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            
            _logger.LogError(
                ex,
                "Error handling {RequestName} after {ElapsedMs}ms",
                requestName,
                stopwatch.ElapsedMilliseconds);
            
            throw;
        }
    }
}
```

### Đăng ký
```csharp
// Program.cs hoặc Startup.cs
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>), 
    typeof(LoggingBehavior<,>));
```

---

## 2. ValidationBehavior (bắt buộc)

### Mục đích
- Validate request bằng FluentValidation
- Gom tất cả lỗi validation
- Trả về ApiResponse.Fail() với danh sách lỗi

### Implementation
```csharp
public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            return CreateValidationErrorResponse(failures);
        }

        return await next();
    }

    private static TResponse CreateValidationErrorResponse(
        List<ValidationFailure> failures)
    {
        var errors = failures
            .Select(f => $"{f.PropertyName}: {f.ErrorMessage}")
            .ToList();

        // Tạo ApiResponse.Fail() với errors
        var responseType = typeof(TResponse);
        
        if (responseType.IsGenericType)
        {
            var genericType = responseType.GetGenericTypeDefinition();
            
            // ApiResponse<T>
            if (genericType == typeof(ApiResponse<>))
            {
                var dataType = responseType.GetGenericArguments()[0];
                var failMethod = typeof(ApiResponse<>)
                    .MakeGenericType(dataType)
                    .GetMethod("Fail", new[] { typeof(string), typeof(List<string>) });
                
                return (TResponse)failMethod!.Invoke(
                    null, 
                    new object[] { "Validation failed", errors });
            }
            
            // PagedResponse<T>
            if (genericType == typeof(PagedResponse<>))
            {
                var dataType = responseType.GetGenericArguments()[0];
                var failMethod = typeof(PagedResponse<>)
                    .MakeGenericType(dataType)
                    .GetMethod("Fail", new[] { typeof(string), typeof(List<string>) });
                
                return (TResponse)failMethod!.Invoke(
                    null, 
                    new object[] { "Validation failed", errors });
            }
        }

        throw new ValidationException(failures);
    }
}
```

### Đăng ký
```csharp
// Program.cs
builder.Services.AddValidatorsFromAssembly(
    typeof(CreateUserValidator).Assembly);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>), 
    typeof(ValidationBehavior<,>));
```

### Validator Example
```csharp
public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserCode)
            .NotEmpty()
            .WithMessage("User code is required")
            .MaximumLength(50)
            .WithMessage("User code must not exceed 50 characters");

        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .WithMessage("Display name is required")
            .MaximumLength(200)
            .WithMessage("Display name must not exceed 200 characters");

        RuleFor(x => x.Email)
            .Must(ValidationHelper.IsValidEmail)
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format");
    }
}
```

---

## 3. TransactionBehavior (chỉ Command)

### Mục đích
- Tự động mở transaction cho mọi Command
- Commit nếu thành công, Rollback nếu có exception
- Truyền IDbTransaction xuống repository

### Implementation
```csharp
public class TransactionBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        // Chỉ áp dụng cho Command (tên kết thúc bằng "Command")
        if (!requestName.EndsWith("Command"))
        {
            return await next();
        }

        _logger.LogInformation(
            "Begin transaction for {RequestName}",
            requestName);

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                var response = await next();

                await dbContext.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Committed transaction for {RequestName}",
                    requestName);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync(cancellationToken);

                _logger.LogError(
                    ex,
                    "Rolled back transaction for {RequestName}",
                    requestName);

                throw;
            }
        }
    }
}
```

### Đăng ký
```csharp
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>), 
    typeof(TransactionBehavior<,>));
```

### Command Handler Pattern (sử dụng Transaction)
```csharp
public sealed class CreateUserCommandHandler 
    : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
{
    public async Task<ApiResponse<CreateUserCommand.Response>> Handle(
        CreateUserCommand request,
        CancellationToken ct)
    {
        try
        {
            // TransactionBehavior đã mở transaction
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
            return ApiResponse<CreateUserCommand.Response>.Fail(
                "Failed to create user");
        }
    }
}
```

---

## 4. CachingBehavior (chỉ Query - tuỳ chọn)

### Mục đích
- Cache kết quả Query để tăng performance
- Cache key = TypeName + hash(parameters)
- TTL tuỳ chỉnh theo từng Query

### Implementation
```csharp
public class CachingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(
        IMemoryCache cache,
        ILogger<CachingBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        // Chỉ áp dụng cho Query
        if (!requestName.EndsWith("Query"))
        {
            return await next();
        }

        // Check if request implements ICacheable
        if (request is not ICacheable cacheableRequest)
        {
            return await next();
        }

        var cacheKey = GetCacheKey(requestName, request);

        if (_cache.TryGetValue<TResponse>(cacheKey, out var cachedResponse))
        {
            _logger.LogInformation(
                "Cache hit for {RequestName} with key {CacheKey}",
                requestName,
                cacheKey);

            return cachedResponse!;
        }

        _logger.LogInformation(
            "Cache miss for {RequestName} with key {CacheKey}",
            requestName,
            cacheKey);

        var response = await next();

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(cacheableRequest.CacheDuration);

        _cache.Set(cacheKey, response, cacheOptions);

        return response;
    }

    private static string GetCacheKey(string requestName, TRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var hash = ComputeHash(json);
        return $"{requestName}:{hash}";
    }

    private static string ComputeHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}

// Interface để đánh dấu Query có cache
public interface ICacheable
{
    TimeSpan CacheDuration { get; }
}
```

### Đăng ký
```csharp
builder.Services.AddMemoryCache();

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>), 
    typeof(CachingBehavior<,>));
```

### Query Example với Cache
```csharp
public sealed class GetUserByIdQuery 
    : IRequest<ApiResponse<UserDetailDto>>, ICacheable
{
    public int Id { get; init; }
    
    // Cache trong 5 phút
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(5);
}
```

---

## 5. AuthorizationBehavior (tuỳ chọn)

### Mục đích
- Kiểm tra quyền truy cập cho Command/Query
- Dựa trên Claims/Roles/Policies
- Trả về Forbidden nếu không có quyền

### Implementation
```csharp
public class AuthorizationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthorizationBehavior<TRequest, TResponse>> _logger;

    public AuthorizationBehavior(
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthorizationBehavior<TRequest, TResponse>> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Check if request implements IAuthorizable
        if (request is not IAuthorizable authorizableRequest)
        {
            return await next();
        }

        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning(
                "Unauthorized access to {RequestName}",
                typeof(TRequest).Name);

            return CreateUnauthorizedResponse();
        }

        var requiredRoles = authorizableRequest.RequiredRoles;
        var requiredPermissions = authorizableRequest.RequiredPermissions;

        // Check roles
        if (requiredRoles.Any() && !requiredRoles.Any(role => user.IsInRole(role)))
        {
            _logger.LogWarning(
                "User {UserId} does not have required role for {RequestName}",
                user.Identity.Name,
                typeof(TRequest).Name);

            return CreateForbiddenResponse();
        }

        // Check permissions (claims)
        if (requiredPermissions.Any())
        {
            var userPermissions = user.Claims
                .Where(c => c.Type == "permission")
                .Select(c => c.Value)
                .ToList();

            if (!requiredPermissions.All(p => userPermissions.Contains(p)))
            {
                _logger.LogWarning(
                    "User {UserId} does not have required permissions for {RequestName}",
                    user.Identity.Name,
                    typeof(TRequest).Name);

                return CreateForbiddenResponse();
            }
        }

        return await next();
    }

    private static TResponse CreateUnauthorizedResponse()
    {
        var responseType = typeof(TResponse);
        
        if (responseType.IsGenericType)
        {
            var genericType = responseType.GetGenericTypeDefinition();
            
            if (genericType == typeof(ApiResponse<>))
            {
                var dataType = responseType.GetGenericArguments()[0];
                var failMethod = typeof(ApiResponse<>)
                    .MakeGenericType(dataType)
                    .GetMethod("Fail", new[] { typeof(string), typeof(int) });
                
                return (TResponse)failMethod!.Invoke(
                    null, 
                    new object[] { "Unauthorized", 401 });
            }
        }

        throw new UnauthorizedAccessException();
    }

    private static TResponse CreateForbiddenResponse()
    {
        var responseType = typeof(TResponse);
        
        if (responseType.IsGenericType)
        {
            var genericType = responseType.GetGenericTypeDefinition();
            
            if (genericType == typeof(ApiResponse<>))
            {
                var dataType = responseType.GetGenericArguments()[0];
                var failMethod = typeof(ApiResponse<>)
                    .MakeGenericType(dataType)
                    .GetMethod("Fail", new[] { typeof(string), typeof(int) });
                
                return (TResponse)failMethod!.Invoke(
                    null, 
                    new object[] { "Forbidden", 403 });
            }
        }

        throw new UnauthorizedAccessException();
    }
}

// Interface để đánh dấu yêu cầu authorization
public interface IAuthorizable
{
    List<string> RequiredRoles { get; }
    List<string> RequiredPermissions { get; }
}
```

### Command Example với Authorization
```csharp
public sealed class DeleteUserCommand 
    : IRequest<ApiResponse<bool>>, IAuthorizable
{
    public int Id { get; init; }
    
    // Chỉ Admin mới được xoá user
    public List<string> RequiredRoles => new() { "Admin" };
    
    // Hoặc có permission "users.delete"
    public List<string> RequiredPermissions => new() { "users.delete" };
}
```

---

## Đăng ký đầy đủ trong Program.cs

```csharp
// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
});

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserValidator).Assembly);

// Pipeline Behaviors (thứ tự quan trọng)
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

// Memory Cache (nếu dùng CachingBehavior)
builder.Services.AddMemoryCache();

// HttpContextAccessor (nếu dùng AuthorizationBehavior)
builder.Services.AddHttpContextAccessor();
```

---

## Checklist cho Agent

Khi sinh code Command/Query, AI Agent cần:

### ✅ Command
- [ ] Tên kết thúc bằng `Command`
- [ ] Implement `IRequest<ApiResponse<T>>`
- [ ] Có `Validator` kế thừa `AbstractValidator<TCommand>`
- [ ] Handler trả về `ApiResponse<T>`
- [ ] Sử dụng `using (var db = new DbContext(openTransaction: true))`
- [ ] Có try-catch với Commit/Rollback
- [ ] Không SELECT *, chỉ trả Id/Result cần thiết
- [ ] Log error với ILogger

### ✅ Query
- [ ] Tên kết thúc bằng `Query`
- [ ] Implement `IRequest<ApiResponse<T>>` hoặc `IRequest<PagedResponse<T>>`
- [ ] Không có Validator (tuỳ chọn)
- [ ] Handler trả về DTO đã shape
- [ ] Sử dụng `using (var db = new DbContext())` (không transaction)
- [ ] Phân trang dùng OFFSET/FETCH
- [ ] Không mutate data
- [ ] Có thể implement `ICacheable` nếu cần cache

### ✅ Pipeline
- [ ] LoggingBehavior: log tất cả
- [ ] ValidationBehavior: validate với FluentValidation
- [ ] TransactionBehavior: chỉ Command
- [ ] CachingBehavior: chỉ Query có ICacheable
- [ ] AuthorizationBehavior: tuỳ IAuthorizable

---

## Best Practices

1. **Logging**: Log đầy đủ request name, elapsed time, parameters (mask sensitive data)
2. **Validation**: Tất cả validation rules trong Validator, không validate trong Handler
3. **Transaction**: Command luôn có transaction, Query không cần
4. **Error Handling**: Trả về ApiResponse.Fail() với message rõ ràng
5. **Separation**: Command mutate data, Query chỉ đọc - tách biệt rõ ràng
6. **Performance**: Cache Query thường dùng, không cache Command
7. **Security**: Kiểm tra quyền ở AuthorizationBehavior, không ở Handler

---

## Tóm tắt Pipeline Flow

```
┌─────────────────────────────────────────────────────────┐
│ Request từ Controller → IMediator.Send()                │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│ 1. LoggingBehavior (start timer, log request)          │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│ 2. ValidationBehavior (FluentValidation)               │
│    ❌ Fail → return ApiResponse.Fail(errors)           │
│    ✅ Pass → continue                                   │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│ 3. AuthorizationBehavior (check roles/permissions)     │
│    ❌ Fail → return 401/403                            │
│    ✅ Pass → continue                                   │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│ 4a. TransactionBehavior (nếu Command)                  │
│     - Open connection + transaction                     │
│     - Execute handler                                   │
│     - Commit/Rollback                                   │
└─────────────────────────────────────────────────────────┘
                   hoặc ↓
┌─────────────────────────────────────────────────────────┐
│ 4b. CachingBehavior (nếu Query có ICacheable)          │
│     - Check cache → hit? return cached                  │
│     - Miss? execute handler → cache result              │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│ 5. Handler thực thi business logic                     │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│ LoggingBehavior (stop timer, log response)             │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│ Response trả về Controller → Client                    │
└─────────────────────────────────────────────────────────┘
```

---

**Lưu ý:** Document này dành cho AI Agent để tự động sinh code theo đúng chuẩn CQRS + MediatR Pipeline của UniManage.
