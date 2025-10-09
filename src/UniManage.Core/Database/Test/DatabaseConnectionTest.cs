using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using UniManage.Core.Security;

namespace UniManage.Core.Database.Test
{
    public class DatabaseConnectionTest
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=".PadRight(60, '='));
            Console.WriteLine("UniManage Database Connection Test");
            Console.WriteLine("=".PadRight(60, '='));
            Console.WriteLine();

            try
            {
                // Test configuration loading
                Console.WriteLine("1. Testing configuration loading...");
                var config = LoadConfiguration();
                Console.WriteLine("✅ Configuration loaded successfully");
                Console.WriteLine();

                // Test connection string building
                Console.WriteLine("2. Testing connection string...");
                var connectionString = GetConnectionString(config);
                var maskedConnString = MaskPassword(connectionString);
                Console.WriteLine($"Connection String: {maskedConnString}");
                Console.WriteLine();

                // Test database connection
                Console.WriteLine("3. Testing database connection...");
                TestDatabaseConnection(connectionString);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Test failed: {ex.Message}");
                Console.WriteLine($"Details: {ex}");
                Environment.Exit(1);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static IConfiguration LoadConfiguration()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"Base path: {basePath}");

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // Display loaded configuration
            var dbSection = config.GetSection("Database");
            Console.WriteLine($"Server: {dbSection["Server"]}");
            Console.WriteLine($"Database: {dbSection["Database"]}");
            Console.WriteLine($"User: {dbSection["User"]}");
            Console.WriteLine($"Port: {dbSection["Port"]}");

            var connString = config.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(connString))
            {
                Console.WriteLine("✅ Found DefaultConnection in ConnectionStrings section");
            }
            else
            {
                Console.WriteLine("⚠️ DefaultConnection not found, will use Database section");
            }

            return config;
        }

        private static string GetConnectionString(IConfiguration config)
        {
            // Try Database section with individual keys (recommended for encryption)
            var server = config["Database:Server"];
            var database = config["Database:DefaultDatabase"];
            var userId = config["Database:UserId"];
            var password = config["Database:Password"];

            if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(database))
            {
                // Decrypt password if encrypted
                var decryptedPassword = DecryptIfNeeded(password);

                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = server,
                    InitialCatalog = database,
                    UserID = userId,
                    Password = decryptedPassword,
                    TrustServerCertificate = bool.Parse(config["Database:TrustServerCertificate"] ?? "true"),
                    MultipleActiveResultSets = bool.Parse(config["Database:MultipleActiveResultSets"] ?? "true"),
                    ConnectTimeout = int.Parse(config["Database:ConnectionTimeout"] ?? "30")
                };

                return builder.ConnectionString;
            }

            // Fallback: không có Database section
            throw new InvalidOperationException("Database configuration is missing in appsettings.json");
        }

        private static string? DecryptIfNeeded(string? encryptedValue)
        {
            return ConfigEncryption.Decrypt(encryptedValue);
        }

        private static void TestDatabaseConnection(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Attempting to open connection...");
                connection.Open();
                Console.WriteLine("✅ Connection opened successfully");

                // Test simple query
                using (var command = new SqlCommand("SELECT @@SERVERNAME, @@VERSION, GETDATE()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Server Name: {reader[0]}");
                            Console.WriteLine($"SQL Version: {reader[1]?.ToString()?.Substring(0, Math.Min(100, reader[1]?.ToString()?.Length ?? 0))}...");
                            Console.WriteLine($"Current Time: {reader[2]}");
                        }
                    }
                }

                // Test database existence
                using (var command = new SqlCommand("SELECT DB_NAME()", connection))
                {
                    var currentDb = command.ExecuteScalar()?.ToString();
                    Console.WriteLine($"Current Database: {currentDb}");
                }

                // Check if key tables exist
                CheckTableExistence(connection);

                Console.WriteLine("✅ Database connection test completed successfully");
            }
        }

        private static void CheckTableExistence(SqlConnection connection)
        {
            Console.WriteLine();
            Console.WriteLine("4. Checking table existence...");

            string[] tables = { "sy_users", "sy_languages", "sy_resources", "ms_units", "ms_employees" };

            foreach (var table in tables)
            {
                using (var command = new SqlCommand(
                    "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName",
                    connection))
                {
                    command.Parameters.AddWithValue("@tableName", table);
                    var exists = (int)command.ExecuteScalar() > 0;

                    if (exists)
                    {
                        // Get row count
                        using (var countCommand = new SqlCommand($"SELECT COUNT(*) FROM [{table}]", connection))
                        {
                            var rowCount = countCommand.ExecuteScalar();
                            Console.WriteLine($"✅ {table}: EXISTS ({rowCount} rows)");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"❌ {table}: NOT FOUND");
                    }
                }
            }
        }

        private static string MaskPassword(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return string.Empty;

            return System.Text.RegularExpressions.Regex.Replace(
                connectionString,
                @"Password=([^;]+)",
                "Password=***",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}