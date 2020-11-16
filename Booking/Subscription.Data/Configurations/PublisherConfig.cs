using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscription.Core.Domain;

namespace Subscription.Data.Configurations
{
    public class PublisherConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publisher");

            builder.HasKey(x => new { x.Id });
            builder.HasMany(x => x.Books).WithOne(y => y.Publisher).HasForeignKey(z => z.PublisherId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
