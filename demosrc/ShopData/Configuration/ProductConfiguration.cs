﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Catalog;

namespace ShopData.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Details", "Product");
            builder.Property("_productId").HasColumnName("ProductId").UseIdentityColumn().UseHiLo();
            builder.HasKey("_productId");
            builder.OwnsMany<Review>("_reviews");
            builder.Property("_name").HasColumnName("Name");
            builder.Property("_description").HasColumnName("Description");
            builder.Property("_price").HasColumnName("Price");
        }
    }
}
