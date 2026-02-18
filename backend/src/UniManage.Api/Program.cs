using Microsoft.AspNetCore.HttpOverrides;
using System.Threading.RateLimiting;
using Asp.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using UniManage.Api.Middleware;
using UniManage.Api.Filters;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using UniManage.Api.Infrastructure.AutofacModules;

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

// Controllers with JSON camelCase
builder.Services.AddControllers(options =>
{
    // Add ApiResponseFilter globally to all controllers
    options.Filters.Add<ApiResponseFilter>();
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// MediatR for CQRS (handlers, validators, pipeline behaviors registered via Autofac)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

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

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Response Compression
builder.Services.AddResponseCompression();

// Authentication & Authorization (JWT Bearer)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Authority"];
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("IdentityServer:RequireHttpsMetadata");
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["IdentityServer:ApiName"],
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
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

// 7. Swagger (Development only)
if (app.Environment.IsDevelopment())
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

// 11. Routing
app.UseRouting();

// 11.1 API Logging (request/response) - Must be after Routing to access RouteData
app.UseMiddleware<ApiLoggingMiddleware>();

// 12. CORS
app.UseCors("AllowAll");

// 13. Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 14. Map Controllers
app.MapControllers();

// 15. Health Check Endpoint
app.MapHealthChecks("/health");

app.Run();
