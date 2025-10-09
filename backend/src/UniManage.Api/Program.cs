using log4net;
using log4net.Config;
using Microsoft.AspNetCore.HttpOverrides;
using System.Threading.RateLimiting;
using UniManage.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// SERVICES CONFIGURATION
// ============================================================================

// Controllers with JSON camelCase
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// MediatR for CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

// log4net configuration
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

// Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "UniManage API",
        Version = "v1",
        Description = "API for UniManage system - CQRS + Dapper + .NET 9",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "UniManage Team",
            Email = "support@unimanage.local",
            Url = new Uri("https://unimanage.local")
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

// 6. API Logging (request/response)
app.UseMiddleware<ApiLoggingMiddleware>();

// 7. Swagger (Development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "UniManage API v1");
        options.DocumentTitle = "UniManage API Documentation";
        options.RoutePrefix = string.Empty; // Swagger UI at root
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

// 12. CORS
app.UseCors("AllowAll");

// 13. Authentication & Authorization (will be enabled with IdentityServer)
// app.UseAuthentication();
// app.UseAuthorization();

// 14. Map Controllers
app.MapControllers();

app.Run();
