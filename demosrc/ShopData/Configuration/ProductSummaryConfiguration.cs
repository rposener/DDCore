using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Catalog;

namespace ShopData.Configuration
{
    public class ProductSummaryConfiguration : IEntityTypeConfiguration<ProductSummaryResult>
    {
        public void Configure(EntityTypeBuilder<ProductSummaryResult> builder)
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
