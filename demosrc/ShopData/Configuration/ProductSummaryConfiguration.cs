using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopData.ViewTypes;

namespace ShopData.Configuration
{
    public class ProductSummaryConfiguration : IEntityTypeConfiguration<ProductResult>
    {
        public void Configure(EntityTypeBuilder<ProductResult> builder)
        {
            builder.ToView("Summaries", "Product");
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.Rating).HasColumnType("decimal(3,2)");
            builder.Property(p => p.Price).HasColumnType("money");
            builder.UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        }
    }
}
