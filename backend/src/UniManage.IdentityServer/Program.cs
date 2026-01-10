using System.Reflection;
using UniManage.IdentityServer;
using UniManage.IdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from UniManage.Core (copied to output directory)
builder.Configuration.SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Configure Logging (Serilog)
UniManage.Core.Logging.UniLogManager.Initialize(builder.Configuration["AppSettings:LogPath"] ?? "logs");

// Configure services
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Add IdentityServer with In-Memory stores (Simplified)
builder.Services.AddIdentityServer(options =>
    {
        options.EmitStaticAudienceClaim = true;
    })
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddProfileService<CustomProfileService>()
    .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
    .AddDeveloperSigningCredential();

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

var app = builder.Build();

// Configure pipeline
app.UseCors();
app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.MapGet("/", () => "IdentityServer is running!");

app.Run();
