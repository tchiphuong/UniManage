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

// Register Identity User Repository
builder.Services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();
// builder.Services.AddScoped<UniManage.Core.Services.IAuthService, UniManage.Core.Services.AuthService>(); // Xóa do không tồn tại

// Add IdentityServer with Dapper stores
builder.Services.AddIdentityServer(options =>
    {
        options.EmitStaticAudienceClaim = true;
    })
    .AddClientStore<DapperClientStore>()
    .AddResourceStore<DapperResourceStore>()
    .AddPersistedGrantStore<DapperPersistedGrantStore>()
    .AddDeviceFlowStore<DapperDeviceFlowStore>()
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

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.SignInScheme = Duende.IdentityServer.IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "dummy-google-id";
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "dummy-google-secret";
    })
    .AddFacebook(options =>
    {
        options.SignInScheme = Duende.IdentityServer.IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.AppId = builder.Configuration["Authentication:Facebook:AppId"] ?? "dummy-fb-id";
        options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"] ?? "dummy-fb-secret";
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
