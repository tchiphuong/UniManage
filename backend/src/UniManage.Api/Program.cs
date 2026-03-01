using Asp.Versioning;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.RateLimiting;
using UniManage.Api.Filters;
using UniManage.Api.Infrastructure.AutofacModules;
using UniManage.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from UniManage.Core (copied to output directory)
builder.Configuration.SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Build connection string from configuration
var dbSettings = builder.Configuration.GetSection("Database");
var connectionString = $"Server={dbSettings["Server"]};Database={dbSettings["DefaultDatabase"]};User Id={dbSettings["UserId"]};Password={dbSettings["Password"]};TrustServerCertificate={dbSettings["TrustServerCertificate"]};MultipleActiveResultSets={dbSettings["MultipleActiveResultSets"]};Connection Timeout={dbSettings["ConnectionTimeout"]}";

// Use Autofac as the service provider factory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule());
});

// ============================================================================
// SERVICES CONFIGURATION
// ============================================================================

// ===========================================
// [SECURITY] Request body size limit (M4)
// Prevents DoS via oversized payloads
// ===========================================
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB max
});

// Controllers with JSON camelCase
builder.Services.AddControllers(options =>
{
    // Add ApiResponseFilter globally to all controllers
    options.Filters.Add<ApiResponseFilter>();

    // ===========================================
    // [SECURITY] Global Authorization filter (C8)
    // All endpoints require authentication by default.
    // Use [AllowAnonymous] to explicitly opt-out.
    // ===========================================
    var authPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter(authPolicy));
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// MediatR for CQRS (handlers, validators, pipeline behaviors registered via Autofac)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

// ===========================================
// [SECURITY] Database-Driven Authorization
// PermissionAuthorizationHandler queries sy_role_permissions table
// Cached per user for 5 minutes via IMemoryCache
// ===========================================
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IAuthorizationHandler, UniManage.Api.Authorization.PermissionAuthorizationHandler>();

// ===========================================
// [SECURITY] H1 — Register IHttpClientFactory
// Named client "IdentityServer" used by auth handlers.
// Prevents socket exhaustion from new HttpClient() per request.
// ===========================================
builder.Services.AddHttpClient("IdentityServer", client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Health Checks
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString, name: "Database", failureStatus: HealthStatus.Unhealthy);

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version")
    );
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Serilog configuration via UniLogManager
UniManage.Core.Logging.UniLogManager.Initialize(builder.Configuration["AppSettings:LogPath"] ?? "logs");

// Test log to verify configuration
Serilog.Log.Information($"Application Starting Up.");

// Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerIgnoreFilter>();

    // Custom SchemaId to handle generic types and nested types properly
    options.CustomSchemaIds(type =>
    {
        if (type.IsGenericType)
        {
            // For generic types like ApiResponse<T>, build schema id with type args
            var genericName = type.Name.Substring(0, type.Name.IndexOf('`'));
            var genericArgs = string.Join("_", type.GetGenericArguments().Select(arg =>
            {
                var argName = arg.FullName ?? arg.Name;
                // Replace special characters in nested types
                return argName
                    .Replace("+", "_")
                    .Replace(".", "_")
                    .Replace("[", "")
                    .Replace("]", "")
                    .Replace(",", "")
                    .Replace(" ", "")
                    .Replace("`", "");
            }));
            return $"{genericName}Of{genericArgs}";
        }

        var fullName = type.FullName ?? type.Name;
        // Handle nested types (e.g., CreateUserCommand+Response)
        return fullName
            .Replace("+", "_")
            .Replace(".", "_")
            .Replace("[", "")
            .Replace("]", "")
            .Replace(",", "")
            .Replace(" ", "")
            .Replace("`", "");
    });

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "UniManage API",
        Version = "v1",
        Description = "API for UniManage system - CQRS + Dapper + .NET 9",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Phuong Tran",
            Email = "chiphuong9299@hotmail.com",
            Url = new Uri("https://www.facebook.com/tchiphuong")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Include XML documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    // JWT Bearer security
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ===========================================
// [SECURITY] CORS — Strict whitelist (C1)
// Only explicitly allowed origins can call API.
// NEVER use AllowAnyOrigin() in production.
// ===========================================
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? new[] { "http://localhost:3000" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("StrictCors", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
              .WithHeaders(
                  "Content-Type",
                  "Authorization",
                  "X-Correlation-Id",
                  "Accept-Language",
                  "X-Timezone-Offset",
                  "App-Version",
                  "Device-Id",
                  "Device-Type",
                  "Session-Id")
              .AllowCredentials()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });
});

// Response Compression
builder.Services.AddResponseCompression();

// Authentication & Authorization (JWT Bearer)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var authority = builder.Configuration["IdentityServer:Authority"];

        options.Authority = authority;
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("IdentityServer:RequireHttpsMetadata");
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["IdentityServer:ApiName"],
            
            ValidateIssuer = true,
            ValidIssuer = authority,
            
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Authentication Failed: {context.Exception.Message}");
                Console.WriteLine($"[AUTH_FAIL] {context.Exception.Message}"); // Force console output
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                var claims = string.Join(", ", context.Principal?.Claims.Select(c => $"{c.Type}={c.Value}") ?? Array.Empty<string>());
                logger.LogInformation($"Token Validated. Claims: {claims}");
                Console.WriteLine($"[AUTH_OK] User: {context.Principal?.Identity?.Name}"); // Force console output
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError($"OnChallenge: {context.Error}, {context.ErrorDescription}");
                Console.WriteLine($"[AUTH_CHALLENGE] {context.Error} - {context.ErrorDescription}");
                return Task.CompletedTask;
            }
        }; 
    });

builder.Services.AddAuthorization();

// ===========================================
// [SECURITY] Rate Limiting (C6, M7)
// Multi-layer: Global per-IP + per-user + strict login limit
// ===========================================
builder.Services.AddRateLimiter(options =>
{
    // Layer 1: Global per-IP rate limit (100 req/min)
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }
        )
    );

    // Layer 2: Strict login rate limit (5 req/min per IP)
    // Prevents brute-force password attacks
    options.AddFixedWindowLimiter("LoginRateLimit", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 0;
    });

    // Layer 3: Sensitive endpoint rate limit (10 req/min per IP)
    // For check-username, check-email, forgot-password, etc.
    options.AddFixedWindowLimiter("SensitiveRateLimit", opt =>
    {
        opt.PermitLimit = 10;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 0;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Forwarded Headers (for reverse proxy/nginx)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// ============================================================================
// MIDDLEWARE PIPELINE (order matters!)
// ============================================================================

// 1. Forwarded Headers (must be first for proxy scenarios)
app.UseForwardedHeaders();

// 2. Response Compression
app.UseResponseCompression();

// 3. Rate Limiting
app.UseRateLimiter();

// 4. Global Exception Handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

// 5. Culture/Localization
app.UseMiddleware<CultureMiddleware>();

// 6. API Logging (request/response) - Moved after Routing to get Controller/Action
// app.UseMiddleware<ApiLoggingMiddleware>();

// ===========================================
// [SECURITY] Swagger controlled by config (M5)
// Use "Swagger:Enabled": true in appsettings.Development.json
// Never expose Swagger in production
// ===========================================
var swaggerEnabled = app.Configuration.GetValue<bool>("Swagger:Enabled", app.Environment.IsDevelopment());
if (swaggerEnabled)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "UniManage API v1");
        options.DocumentTitle = "UniManage API Documentation";
        options.RoutePrefix = "swagger"; // Swagger UI at /swagger
        options.DisplayRequestDuration();
        options.EnableDeepLinking();
        options.EnableFilter();
        options.ShowExtensions();
        options.EnableValidator();
    });
}

// 8. HTTPS Redirection
app.UseHttpsRedirection();

// 9. HSTS (Production only)
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// 10. Static Files (if needed)
app.UseStaticFiles();

// ===========================================
// [SECURITY] HTTP Security Headers (C7)
// Protects against clickjacking, MIME sniffing,
// XSS, and information leakage
// ===========================================
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    headers["X-Content-Type-Options"] = "nosniff";
    headers["X-Frame-Options"] = "DENY";
    headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
    headers["X-XSS-Protection"] = "1; mode=block";
    headers["Content-Security-Policy"] = "default-src 'self'; frame-ancestors 'none'";

    // HSTS: 1 year, include subdomains
    if (!context.Request.Host.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
    {
        headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains; preload";
    }

    await next();
});

// 11. Routing
app.UseRouting();

// 11.1 API Logging (request/response) - Must be after Routing to access RouteData
app.UseMiddleware<ApiLoggingMiddleware>();

// 12. CORS — using strict policy (C1)
app.UseCors("StrictCors");

// 13. Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 14. Map Controllers
app.MapControllers();

// 15. Health Check Endpoint
app.MapHealthChecks("/health");

app.Run();
