using Microsoft.EntityFrameworkCore;
using UniManage.Modules.Inventory.Domain.Entities;

namespace UniManage.Modules.Inventory.Infrastructure
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<INV_Product> Products { get; set; }
        public DbSet<INV_ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Default Schema
            modelBuilder.HasDefaultSchema("dbo");

            // Cáº¥u hÃ¬nh Product
            modelBuilder.Entity<INV_Product>(b =>
            {
                b.ToTable("INV_Product");
                b.HasKey(x => x.Id);
                b.Property(x => x.Price).HasColumnType("decimal(18,4)");
                
                // Concurrency Token
                b.Property(x => x.DataRowVersion).IsRowVersion();
                
                // Global Query Filter cho Soft Delete
                b.HasQueryFilter(x => !x.IsDeleted);

                b.HasOne(x => x.Category)
                 .WithMany()
                 .HasForeignKey(x => x.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // Cáº¥u hÃ¬nh ProductCategory
            modelBuilder.Entity<INV_ProductCategory>(b =>
            {
                b.ToTable("INV_ProductCategory");
                b.HasKey(x => x.Id);
                
                b.Property(x => x.DataRowVersion).IsRowVersion();
                b.HasQueryFilter(x => !x.IsDeleted);
            });
        }
    }
}

