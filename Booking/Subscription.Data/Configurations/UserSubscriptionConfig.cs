using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscription.Core.Domain;

namespace Subscription.Data.Configurations
{
    public class UserSubscriptionConfig : IEntityTypeConfiguration<UserSubscriptionType>
    {
        public void Configure(EntityTypeBuilder<UserSubscriptionType> builder)
        {
            builder.ToTable("UserSubscription");

            builder.HasKey(x => new { x.SubscriptionTypeId, x.UserId });
            builder.HasOne(x => x.User).WithMany(x => x.UserSubscriptions).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.SubscriptionType).WithMany(x => x.UserSubscriptions).HasForeignKey(x => x.SubscriptionTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
