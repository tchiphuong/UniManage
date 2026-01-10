using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;
using UniManage.Core.Logging;
using UniManage.Worker.Filters;

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

// 4. Configure Hangfire
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString));

// 5. Add Hangfire Worker Server (processing jobs)
builder.Services.AddHangfireServer();

// 5.1 Register Jobs
// builder.Services.AddTransient<IRealJob, RealJob>();

var app = builder.Build();

// 6. Configure Dashboard (UI)
app.UseRouting();

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
