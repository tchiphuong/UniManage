using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace UniManage.Tests.Fixtures;

/// <summary>
/// Custom WebApplicationFactory for integration testing
/// </summary>
public class UniManageWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Use shared database (same as development/production)
            // Tests use transactions for isolation - no separate test database needed
            // Override specific services if needed (e.g., email service, external APIs)
        });

        builder.UseEnvironment("Test");
    }
}
