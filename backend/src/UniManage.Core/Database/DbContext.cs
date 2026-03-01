using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UniManage.Core.Security;
using UniManage.Model.Entities;
using System.Reflection;

namespace UniManage.Core.Database;

/// <summary>
/// Hybrid DbContext supporting both Entity Framework Core and Dapper operations
/// Auto-discovers all entities from UniManage.Model.Entities namespace
/// </summary>
/// <remarks>
/// Usage examples:
/// 
/// 1. Read-only query (no transaction):
///    using (var db = new DbContext())
///    {
///        var users = await db.Set&lt;sy_users&gt;().Where(u => u.Status == "active").ToListAsync();
///    }
///    
/// 2. Write operation with transaction:
///    using (var db = new DbContext(openTransaction: true))
///    {
///        var user = await db.Set&lt;sy_users&gt;().FindAsync(userId);
///        user.Status = "inactive";
///        await db.SaveChangesAsync();
///        await db.CommitAsync(); // or will auto-rollback on dispose if not committed
///    }
///    
/// 3. Mix EF Core and Dapper in transaction:
///    using (var db = new DbContext(openTransaction: true))
///    {
///        // EF Core
///        var user = await db.Set&lt;sy_users&gt;().FindAsync(userId);
///        user.Status = "inactive";
///        await db.SaveChangesAsync();
///        
///        // Dapper
///        await db.ExecuteAsync("INSERT INTO sy_api_logs (Action) VALUES (@Action)", new { Action = "UpdateUser" });
///        
///        await db.CommitAsync();
///    }
/// </remarks>
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
            
            // Extract DbTransaction from IDbContextTransaction using reflection
            var dbTransactionProp = _transaction.GetType().GetProperty("DbTransaction");
            return dbTransactionProp?.GetValue(_transaction) as System.Data.Common.DbTransaction;
        }
    }

    #region Constructor & Configuration

    public DbContext(bool openTransaction = false) : base()
    {
        _ownsTransaction = openTransaction;
        if (openTransaction)
        {
            Database.OpenConnection();
            _transaction = Database.BeginTransaction();
        }
    }

    public DbContext(DbContextOptions<DbContext> options, bool openTransaction = false) : base(options)
    {
        _ownsTransaction = openTransaction;
        if (openTransaction)
        {
            Database.OpenConnection();
            _transaction = Database.BeginTransaction();
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

            // ===========================================
            // [SECURITY] C5 — Sensitive data logging ONLY in Development
            // In production, this would log passwords, connection strings, PII
            // ===========================================
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
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var server = config["Database:Server"];
            var database = config["Database:DefaultDatabase"];
            var userId = config["Database:UserId"];
            var password = config["Database:Password"];

            if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(database))
            {
                var decryptedPassword = ConfigEncryption.Decrypt(password);

                var builder = new System.Data.SqlClient.SqlConnectionStringBuilder
                {
                    DataSource = server,
                    InitialCatalog = database,
                    UserID = userId,
                    Password = decryptedPassword,
                    // [SECURITY] H8 — Default to false to enforce certificate validation
                    // Only set to true if you have a self-signed cert AND understand the risk
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

        // Configure common conventions
        ConfigureCommonConventions(modelBuilder);

        // Dynamic configuration for all entities
        ConfigureDynamicEntities(modelBuilder);
    }

    /// <summary>
    /// Dynamically registers entities and applies configurations based on attributes
    /// </summary>
    private void ConfigureDynamicEntities(ModelBuilder modelBuilder)
    {
        var entityTypes = typeof(ad_countries).Assembly.GetTypes()
            .Where(t => t.Namespace == "UniManage.Model.Entities"
                && t.IsClass
                && !t.IsAbstract
                && t.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false).Any())
            .ToList();

        foreach (var type in entityTypes)
        {
            var entityBuilder = modelBuilder.Entity(type);

            // Configure relationships based on ReferencedKeyAttribute
            // This handles cases where FK points to a non-PK column (e.g. Code vs Id)
            foreach (var prop in type.GetProperties())
            {
                var refKeyAttr = prop.GetCustomAttribute<UniManage.Model.Attributes.ReferencedKeyAttribute>();
                if (refKeyAttr != null)
                {
                    // Found a property with [ReferencedKey] -> configure HasPrincipalKey
                    // Convention: Navigation property name is usually the Referenced Table Name
                    
                    // We need to find the navigation property associated with this FK
                    // Usually properties like "ac_accounts" for "GLAccountCode"
                    // But T4 generates navigation properties based on table name
                    
                    // Let's inspect the T4 generated navigation properties
                    // It generates: public virtual ReferencedTable? ReferencedTable { get; set; }
                    // OR public virtual ReferencedTable? fkColumnRefTable { get; set; }
                    
                    // Since this is complex to guess via reflection generic invocation,
                    // we will use the string-based API which is much easier for dynamic code
                    
                    // 1. Find the target entity type (from the attribute logic if we had it, but here we can infer from navigation props)
                    // Actually, simpler: loop over Navigation Properties instead of FK properties?
                    // No, attribute is on the FK property (GLAccountCode).
                    
                    // Let's find the navigation prop that matches this FK
                    // This is hard without metadata.
                    
                    // ALTERNATIVE: Just use the string-based configuration safely
                    // entityBuilder.HasOne(targetType).WithMany(collectionName).HasForeignKey(propName).HasPrincipalKey(targetKey)
                    // We need: TargetType, CollectionName, PrincipalKeyName.
                    // The attribute only has "TargetEntity.KeyName".
                    
                    // Actually, T4 generated: [ReferencedKey("ac_accounts.AccountCode")]
                    // KeyName = "ac_accounts.AccountCode"
                    
                    var parts = refKeyAttr.KeyName.Split('.');
                    if (parts.Length == 2)
                    {
                        var targetTableName = parts[0]; // e.g. "ac_accounts"
                        var targetKeyName = parts[1];   // e.g. "AccountCode"
                        
                        var targetType = entityTypes.FirstOrDefault(t => t.Name == targetTableName);
                        if (targetType != null)
                        {
                            // Try to find the navigation property on THIS entity that points to TargetType
                            var navProp = type.GetProperties()
                                .FirstOrDefault(p => p.PropertyType == targetType);
                                
                            // Try to find the collection property on TARGET entity that points to THIS entity
                            // Convention from T4: TableName + "Collection" -> "ac_bank_accountsCollection"
                            var collectionName = type.Name + "Collection";
                            
                            if (navProp != null)
                            {
                                // Dynamic configuration using string-based API
                                entityBuilder.HasOne(targetType, navProp.Name)
                                    .WithMany(collectionName)
                                    .HasForeignKey(prop.Name)
                                    .HasPrincipalKey(targetKeyName)
                                    .OnDelete(DeleteBehavior.Restrict);
                            }
                        }
                    }
                }
            }
        }

        // Configure cascade delete behavior globally
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    private void ConfigureCommonConventions(ModelBuilder modelBuilder)
    {
        // Set default schema
        modelBuilder.HasDefaultSchema("dbo");

        // Configure all entities with RowVersion for concurrency
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var rowVersionProperty = entityType.FindProperty("RowVersion");
            if (rowVersionProperty != null)
            {
                // Set as concurrency token (RowVersion/Timestamp)
                rowVersionProperty.IsConcurrencyToken = true;
                rowVersionProperty.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
            }

            // Configure datetime columns to datetime2(3)
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

    /// <summary>
    /// Commits the current transaction (if opened with openTransaction: true)
    /// </summary>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null && _ownsTransaction)
        {
            await SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Rolls back the current transaction (if opened with openTransaction: true)
    /// </summary>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null && _ownsTransaction)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Dispose and auto-rollback if transaction was not committed
    /// </summary>
    public override void Dispose()
    {
        if (_transaction != null && _ownsTransaction)
        {
            // Auto-rollback if not committed
            _transaction.Dispose();
            _transaction = null;
        }
        base.Dispose();
    }

    /// <summary>
    /// Async dispose and auto-rollback if transaction was not committed
    /// </summary>
    public override async ValueTask DisposeAsync()
    {
        if (_transaction != null && _ownsTransaction)
        {
            // Auto-rollback if not committed
            await _transaction.DisposeAsync();
            _transaction = null;
        }
        await base.DisposeAsync();
    }

    #endregion
}
