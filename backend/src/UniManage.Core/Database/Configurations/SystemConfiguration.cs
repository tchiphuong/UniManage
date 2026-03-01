using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManage.Model.Entities;

namespace UniManage.Core.Database.Configurations
{
    public class SystemConfiguration : IEntityTypeConfiguration<SystemConfig>
    {
        public void Configure(EntityTypeBuilder<SystemConfig> builder)
        {
            builder.ToTable("sy_configs");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            
            builder.Property(x => x.ConfigCode).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ConfigValue).HasMaxLength(4000);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.DataType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IsSystem).IsRequired();
            
            builder.HasIndex(x => x.ConfigCode).IsUnique();
        }
    }
}
