using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManage.Shared.Domain;

namespace UniManage.Shared.Infrastructure.Database.Configurations
{
    public class SyUsersCustomConfiguration : IEntityTypeConfiguration<SyUsers>
    {
        public void Configure(EntityTypeBuilder<SyUsers> builder)
        {
            builder.HasOne(u => u.HrEmployees)
                   .WithMany()
                   .HasForeignKey(u => u.EmployeeCode)
                   .HasPrincipalKey(e => e.EmployeeCode)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
