using Duende.IdentityServer;
using UniManage.IdentityServer.Stores;
using UniManage.IdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Add IdentityServer with Dapper stores
builder.Services.AddIdentityServer(options =>
    {
        options.EmitStaticAudienceClaim = true;
    })
    .AddResourceStore<DapperResourceStore>()
    .AddClientStore<DapperClientStore>()
    .AddPersistedGrantStore<DapperPersistedGrantStore>()
    .AddProfileService<CustomProfileService>()
    .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

// Add CORS for API communication
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:5297")
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

app.MapGet("/", () => "IdentityServer is running on port 5001!");

app.Run();
