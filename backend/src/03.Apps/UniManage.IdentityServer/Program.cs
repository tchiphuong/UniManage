using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UniManage.IdentityServer.Interfaces;
using UniManage.IdentityServer.Services;
using UniManage.Shared.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from UniManage.Core (copied to output directory)
builder.Configuration.SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Configure Logging (Serilog / log4net)
UniLogManager.Initialize(builder.Configuration["AppSettings:LogPath"] ?? "logs");

// Configure services
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Register Custom Identity Server Services
builder.Services.AddSingleton<IKeyManagementService, KeyManagementService>();
builder.Services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();
builder.Services.AddScoped<IClientStore, DapperClientStore>();
builder.Services.AddScoped<IResourceStore, DapperResourceStore>();
builder.Services.AddScoped<IPersistedGrantStore, DapperPersistedGrantStore>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Add CORS for API communication
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Configure JWT Authentication (For /connect/userinfo and potentially other secured endpoints)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var keyManagementService = builder.Services.BuildServiceProvider().GetRequiredService<IKeyManagementService>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["IdentityServer:Authority"] ?? "https://identity.unimanage.com",
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = keyManagementService.GetSigningKey(),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure pipeline
app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.MapGet("/", () => "Custom IdentityServer is running!");

app.Run();

