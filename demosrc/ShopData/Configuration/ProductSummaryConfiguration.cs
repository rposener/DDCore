using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Configuration
{
    public class ProductSummaryConfiguration : IEntityTypeConfiguration<ProductSummary>
    {
        public void Configure(EntityTypeBuilder<ProductSummary> builder)
        {
            builder.ToView("Summaries", "Product");
            builder.Property("_productId").HasColumnName("ProductId");
            builder.HasKey("_productId");
            builder.Property("_name").HasColumnName("Name");
            builder.Property("_description").HasColumnName("Description");
            builder.Property("_price").HasColumnName("Price");
            builder.Property("_rating").HasColumnName("Rating");
        }
    }
}
