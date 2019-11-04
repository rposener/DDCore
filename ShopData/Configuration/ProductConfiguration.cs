using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property("_productId").HasColumnName("ProductId").UseIdentityColumn();
            builder.HasKey("_productId");
            builder.HasMany<Review>("_reviews");
            builder.Property("_name").HasColumnName("Name");
            builder.Property("_description").HasColumnName("Description");
            builder.Property("_price").HasColumnName("Price");
            builder.Property("_description").HasColumnName("Description");
        }
    }
}
