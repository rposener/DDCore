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
            builder.UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        }
    }
}
