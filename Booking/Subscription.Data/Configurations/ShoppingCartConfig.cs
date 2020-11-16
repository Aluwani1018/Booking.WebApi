using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscription.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscription.Data.Configurations
{
    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("ShoppingCart");

            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.CreateDateTime).IsRequired();
        }
    }
}
