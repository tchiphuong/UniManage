using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UniManage.Resource;

/// <summary>
/// Extension methods for configuring resource system
/// </summary>
public static class ResourceExtensions
{
    /// <summary>
    /// Add resource system services
    /// </summary>
    public static IServiceCollection AddResourceManager(this IServiceCollection services)
    {
        // Register resource provider as singleton to maintain cache
        services.AddSingleton<IResourceProvider, ResourceManager>();

        return services;
    }

    /// <summary>
    /// Use culture middleware
    /// </summary>
    public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CultureMiddleware>();
    }
}
