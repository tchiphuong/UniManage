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

            // Enable sensitive data logging in development
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
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
                    TrustServerCertificate = bool.Parse(config["Database:TrustServerCertificate"] ?? "true"),
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

        // Auto-discover and register all entities from UniManage.Model.Entities namespace
        RegisterEntitiesFromAssembly(modelBuilder);

        // Apply all entity configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);

        // Configure common conventions
        ConfigureCommonConventions(modelBuilder);

        // Configure relationships and constraints
        ConfigureRelationships(modelBuilder);
    }

    /// <summary>
    /// Automatically discovers and registers all entity types from UniManage.Model.Entities namespace
    /// </summary>
    private void RegisterEntitiesFromAssembly(ModelBuilder modelBuilder)
    {
        var entityTypes = typeof(ad_countries).Assembly.GetTypes()
            .Where(t => t.Namespace == "UniManage.Model.Entities" 
                && t.IsClass 
                && !t.IsAbstract
                && t.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false).Any());

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType);
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

    private void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        // Configure cascade delete behavior to restrict to prevent accidental data loss
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Specific relationship configurations
        ConfigureAddressRelationships(modelBuilder);
        ConfigureHRRelationships(modelBuilder);
        ConfigureInventoryRelationships(modelBuilder);
        ConfigureSalesRelationships(modelBuilder);
        ConfigureSystemRelationships(modelBuilder);
        ConfigureWorkflowRelationships(modelBuilder);
    }

    private void ConfigureAddressRelationships(ModelBuilder modelBuilder)
    {
        // ad_provinces -> ad_countries
        modelBuilder.Entity<ad_provinces>()
            .HasOne(p => p.ad_countries)
            .WithMany(c => c.ad_provincesCollection)
            .HasForeignKey(p => p.CountryCode)
            .OnDelete(DeleteBehavior.Restrict);

        // ad_wards -> ad_provinces  
        modelBuilder.Entity<ad_wards>()
            .HasOne(w => w.ad_provinces)
            .WithMany(p => p.ad_wardsCollection)
            .HasForeignKey(w => w.ProvinceCode)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureHRRelationships(ModelBuilder modelBuilder)
    {
        // hr_employees -> hr_departments
        modelBuilder.Entity<hr_employees>()
            .HasOne(e => e.hr_departments)
            .WithMany(d => d.hr_employeesCollection)
            .HasForeignKey(e => e.DepartmentCode)
            .OnDelete(DeleteBehavior.Restrict);

        // hr_employees -> hr_positions
        modelBuilder.Entity<hr_employees>()
            .HasOne(e => e.hr_positions)
            .WithMany(p => p.hr_employeesCollection)
            .HasForeignKey(e => e.PositionCode)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureInventoryRelationships(ModelBuilder modelBuilder)
    {
        // Skip - entities not found in DatabaseModels.cs
        // Add configurations when entities are available
    }

    private void ConfigureSalesRelationships(ModelBuilder modelBuilder)
    {
        // Skip - entities not found in DatabaseModels.cs
        // Add configurations when entities are available
    }

    private void ConfigureSystemRelationships(ModelBuilder modelBuilder)
    {
        // sy_users -> hr_employees
        modelBuilder.Entity<sy_users>()
            .HasOne(u => u.hr_employees)
            .WithMany(e => e.sy_usersCollection)
            .HasForeignKey(u => u.EmployeeCode)
            .OnDelete(DeleteBehavior.Restrict);

        // sy_users -> sy_roles
        modelBuilder.Entity<sy_users>()
            .HasOne(u => u.sy_roles)
            .WithMany(r => r.sy_usersCollection)
            .HasForeignKey(u => u.RoleCode)
            .OnDelete(DeleteBehavior.Restrict);

        // sy_user_roles composite key (Username + RoleCode)
        modelBuilder.Entity<sy_user_roles>()
            .HasKey(ur => new { ur.Username, ur.RoleCode });
    }

    private void ConfigureWorkflowRelationships(ModelBuilder modelBuilder)
    {
        // Skip - entities not found in DatabaseModels.cs
        // Add configurations when entities are available
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
