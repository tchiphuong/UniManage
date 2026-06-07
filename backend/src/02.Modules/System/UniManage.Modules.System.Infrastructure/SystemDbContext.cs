using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain.Entities;

namespace UniManage.Modules.System.Infrastructure
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {
        }

        public DbSet<SYS_User> Users { get; set; }
        public DbSet<SYS_Role> Roles { get; set; }
        public DbSet<SYS_FileStorage> FileStorages { get; set; }
        public DbSet<SYS_FileReference> FileReferences { get; set; }
        public DbSet<SYS_DocumentNumberRule> DocumentNumberRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<SYS_User>(b =>
            {
                b.ToTable("SYS_User");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.Username).IsUnique();
                b.HasIndex(x => x.Email).IsUnique();
                b.Property(x => x.DataRowVersion).IsRowVersion();
                b.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<SYS_Role>(b =>
            {
                b.ToTable("SYS_Role");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.Code).IsUnique();
                b.Property(x => x.DataRowVersion).IsRowVersion();
                b.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<SYS_FileStorage>(b =>
            {
                b.ToTable("SYS_FileStorage");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.Checksum); // Phục vụ chống trùng file
                b.Property(x => x.DataRowVersion).IsRowVersion();
                b.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<SYS_FileReference>(b =>
            {
                b.ToTable("SYS_FileReference");
                b.HasKey(x => x.Id);
                b.HasIndex(x => new { x.EntityId, x.EntityName });
                b.Property(x => x.DataRowVersion).IsRowVersion();
                b.HasQueryFilter(x => !x.IsDeleted);

                b.HasOne(x => x.File)
                 .WithMany()
                 .HasForeignKey(x => x.FileId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SYS_DocumentNumberRule>(b =>
            {
                b.ToTable("SYS_DocumentNumberRule");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.EntityName).IsUnique();
                b.Property(x => x.DataRowVersion).IsRowVersion();
                b.HasQueryFilter(x => !x.IsDeleted);
            });
        }
    }
}


