using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using UniManage.Shared.Infrastructure.Security;

namespace UniManage.Shared.Infrastructure.Database
{
    /// <summary>
    /// Hybrid DbContext supporting both Entity Framework Core and Dapper operations
    /// Auto-discovers all entities from UniManage.Model.Entities namespace
    /// </summary>
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction? _transaction;
        private readonly bool _ownsTransaction;

        /// <summary>
        /// Gets the underlying DbTransaction for Dapper operations
        /// </summary>
        public System.Data.Common.DbTransaction? transaction
        {
            get
            {
                if (_transaction == null)
                    return null;

                return Microsoft.EntityFrameworkCore.Storage.DbContextTransactionExtensions.GetDbTransaction(_transaction);
            }
        }

        #region Constructor & Configuration

        public DbContext(bool openTransaction = false) : base()
        {
            if (openTransaction)
            {
                // Nếu đang dùng cấu trúc Behavior TransactionScope mới thì EF Core tự động liên kết
                if (System.Transactions.Transaction.Current != null)
                {
                    _ownsTransaction = false;
                }
                else
                {
                    _ownsTransaction = true;
                    Database.OpenConnection();
                    _transaction = Database.BeginTransaction();
                }
            }
            else
            {
                _ownsTransaction = false;
            }
        }

        public DbContext(DbContextOptions<DbContext> options, bool openTransaction = false) : base(options)
        {
            if (openTransaction)
            {
                if (System.Transactions.Transaction.Current != null)
                {
                    _ownsTransaction = false;
                }
                else
                {
                    _ownsTransaction = true;
                    Database.OpenConnection();
                    _transaction = Database.BeginTransaction();
                }
            }
            else
            {
                _ownsTransaction = false;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connString = GetConnectionString();
                optionsBuilder.UseSqlServer(connString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                    sqlOptions.CommandTimeout(30);
                });

                var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
                if (isDev)
                {
                    optionsBuilder.EnableSensitiveDataLogging();
                    optionsBuilder.EnableDetailedErrors();
                }
            }
        }

        private static string GetConnectionString()
        {
            try
            {
                var config = UniManage.Shared.Infrastructure.Utilities.ConfigHelper.Configuration;

                var server = config["Database:Server"];
                var database = config["Database:DefaultDatabase"];
                var userId = config["Database:UserId"];
                var password = config["Database:Password"];

                if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(database))
                {
                    // Chú ý: ConfigEncryption.Decrypt() cần được xử lý nếu mật khẩu chưa mã hóa
                    var decryptedPassword = ConfigEncryption.Decrypt(password);

                    var builder = new SqlConnectionStringBuilder
                    {
                        DataSource = server,
                        InitialCatalog = database,
                        UserID = userId,
                        Password = decryptedPassword,
                        TrustServerCertificate = bool.Parse(config["Database:TrustServerCertificate"] ?? "false"),
                        MultipleActiveResultSets = bool.Parse(config["Database:MultipleActiveResultSets"] ?? "true"),
                        ConnectTimeout = int.Parse(config["Database:ConnectionTimeout"] ?? "30")
                    };

                    return builder.ConnectionString;
                }

                throw new InvalidOperationException("Database configuration is missing in appsettings.json");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to read database configuration.", ex);
            }
        }

        #endregion

        #region Model Configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            
            // Tự động quét và áp dụng cấu hình từ tất cả các module Infrastructure
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name != null && a.GetName().Name!.StartsWith("UniManage.Modules."));
            foreach (var assembly in loadedAssemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }

            ConfigureCommonConventions(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private void ConfigureCommonConventions(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var rowVersionProperty = entityType.FindProperty("RowVersion");
                if (rowVersionProperty != null)
                {
                    rowVersionProperty.IsConcurrencyToken = true;
                    rowVersionProperty.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
                }

                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("datetime2(3)");
                    }
                }
            }
        }

        #endregion

        #region Transaction Management

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null && _ownsTransaction)
            {
                await SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null && _ownsTransaction)
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
        }

        public override void Dispose()
        {
            if (_transaction != null && _ownsTransaction)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            base.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            if (_transaction != null && _ownsTransaction)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            await base.DisposeAsync();
        }

        #endregion
    }
}




