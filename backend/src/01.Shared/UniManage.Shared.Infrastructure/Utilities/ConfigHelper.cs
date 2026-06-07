using Microsoft.Extensions.Configuration;

namespace UniManage.Shared.Infrastructure.Utilities
{
    /// <summary>
    /// Static helper providing centralized access to appsettings.json configuration.
    /// Used for core logic where IConfiguration cannot be easily injected.
    /// </summary>
    public static class ConfigHelper
    {
        private static IConfiguration? _configuration;

        /// <summary>
        /// Gets or sets the application configuration.
        /// Automatically loads from appsettings.json if not explicitly initialized.
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();
                }
                return _configuration;
            }
            set => _configuration = value;
        }

        /// <summary>
        /// Retrieves a configuration value by key.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <returns>The string value or null if not found.</returns>
        public static string? GetValue(string key) => Configuration[key];

        /// <summary>
        /// Retrieves a database connection string by name.
        /// </summary>
        /// <param name="name">The connection string name.</param>
        /// <returns>The connection string value or null if not found.</returns>
        public static string? GetConnectionString(string name) => Configuration.GetConnectionString(name);
    }
}

