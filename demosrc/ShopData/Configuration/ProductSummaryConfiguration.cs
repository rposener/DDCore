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
            builder.HasKey(p => p.ProductId);
            builder.UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        }
    }
}
