using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscription.Core.Domain;

namespace Subscription.Data.Configurations
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
