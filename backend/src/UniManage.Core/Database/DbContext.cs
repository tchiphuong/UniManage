using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using UniManage.Core.Security;

namespace UniManage.Core.Database
{
    public class DbContext : IDisposable, IAsyncDisposable
    {
        public readonly SqlConnection connection;
        public SqlTransaction transaction;
        private bool _disposed;

        public DbContext(bool openTransaction = false)
        {
            var connString = GetConnectionString();
            connection = new SqlConnection(connString);

            try
            {
                connection.Open();

                if (openTransaction)
                {
                    transaction = connection.BeginTransaction();
                }
            }
            catch (Exception ex)
            {
                connection?.Dispose();
                throw new InvalidOperationException($"Failed to connect to database. Error: {ex.Message}. Check connection string configuration.", ex);
            }
        }

        public DbContext(DbContext _dbContext)
        {
            connection = _dbContext.connection;
            transaction = _dbContext.transaction;
        }

        private static string GetConnectionString()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile("appsettings.Development.json", optional: true)
                    .Build();

                // Try Database section with individual keys (recommended for encryption)
                var server = config["Database:Server"];
                var database = config["Database:DefaultDatabase"];
                var userId = config["Database:UserId"];
                var password = config["Database:Password"];

                if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(database))
                {
                    // Decrypt password if encrypted (add your decryption logic here)
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

                // No connection info found
                throw new InvalidOperationException("Database configuration is missing in appsettings.json");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to read database configuration.", ex);
            }
        }

        /// <summary>
        /// Decrypt password if it's encrypted using ConfigEncryption service
        /// </summary>
        private static string? DecryptIfNeeded(string? encryptedValue)
        {
            return ConfigEncryption.Decrypt(encryptedValue);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            return await connection.ExecuteScalarAsync<T>(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            return await connection.ExecuteAsync(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (transaction != null)
            {
                await transaction.CommitAsync(cancellationToken);
                await transaction.DisposeAsync();
                transaction = null;
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync(cancellationToken);
                await transaction.DisposeAsync();
                transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_disposed) return;

            if (transaction != null)
            {
                await transaction.DisposeAsync();
            }

            if (connection != null)
            {
                await connection.DisposeAsync();
            }

            _disposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                transaction?.Dispose();
                connection?.Dispose();
            }

            _disposed = true;
        }
    }
}