using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;
using UniManage.Core.Logging;
using UniManage.Core.Database;
using UniManage.Worker.Filters;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using UniManage.Worker.Infrastructure.AutofacModules;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuration
builder.Configuration.SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
      .AddEnvironmentVariables();

// 2. Logging
UniLogManager.Initialize(builder.Configuration["AppSettings:LogPath"] ?? "logs");

// 3. Database Connection
var dbSettings = builder.Configuration.GetSection("Database");
var connectionString = $"Server={dbSettings["Server"]};Database={dbSettings["DefaultDatabase"]};User Id={dbSettings["UserId"]};Password={dbSettings["Password"]};TrustServerCertificate={dbSettings["TrustServerCertificate"]};MultipleActiveResultSets={dbSettings["MultipleActiveResultSets"]};Connection Timeout={dbSettings["ConnectionTimeout"]}";
// Note: connectionString is used later for Hangfire storage

// 4. Configure Hangfire
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString));

// 5. Add Hangfire Worker Server (processing jobs)
builder.Services.AddHangfireServer();

// 5.1. Add Controllers for API endpoints
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// 6. Autofac Configuration
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new WorkerModule());
});

var app = builder.Build();

// 7. Configure Recurring Jobs
using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    // Default cron if DB fetch fails
    string cronSchedule = "*/15 * * * *";

    try
    {
        // Use UniManage.Core.Database.DbContext for consistency
        using (var db = new UniManage.Core.Database.DbContext())
        {
            var dbCron = await db.QueryFirstOrDefaultAsync<string>(
                "SELECT ConfigValue FROM sy_configs WHERE ConfigCode = @Code",
                new { Code = "JOB_TOKEN_CLEANUP_CRON" },
                CancellationToken.None
            );

            if (!string.IsNullOrEmpty(dbCron))
            {
                cronSchedule = dbCron;
                Console.WriteLine($"[JobConfig] Loaded TokenCleanupJob schedule from DB: {cronSchedule}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[JobConfig] Failed to load config from DB, using default. Error: {ex.Message}");
    }

    recurringJobManager.AddOrUpdate<UniManage.Worker.Jobs.TokenCleanupJob>(
        "token-cleanup",
        job => job.ExecuteAsync(),
        cronSchedule
    );
}

// 8. Configure Dashboard (UI)
app.UseRouting();

// Map Controllers
app.MapControllers();

// Redirect root to /hangfire for clarity
app.MapGet("/", () => Results.Redirect("/hangfire"));

// Standard mapping on /hangfire
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "UniManage Job Dashboard",
    // Allow remote access for development
    Authorization = new[] { new AllowAllAuthorizationFilter() }
});

Console.WriteLine("UniManage Worker & Dashboard is starting...");
Console.WriteLine("Dashboard available at: http://localhost:5002/hangfire");

app.Run();
