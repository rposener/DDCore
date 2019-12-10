using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Catalog;

namespace ShopData.Configuration
{

    /// <summary>
    /// Configuration for the EntityObject (IAggregateRoot Mapping)
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Details", "Product");
            builder.HasKey("_productId");
            builder.Property("_productId").HasColumnName("ProductId").UseIdentityColumn().UseHiLo();
            builder.Property("_name").HasColumnName("Name");
            builder.Property("_description").HasColumnName("Description");
            builder.Property("_price").HasColumnName("Price");

            // Owned Properties
            builder.OwnsMany<Review>("_reviews", pr =>
            {
                pr.ToTable("Review", "Product");
                pr.WithOwner().HasForeignKey("ProductId");
                pr.HasKey("_reviewId");
                pr.Property("_reviewId").HasColumnName("ReviewId").UseIdentityColumn().UseHiLo();
                pr.Property("_reviewer").HasColumnName("Reviewer");
                pr.Property("_reviewDate").HasColumnName("ReviewDate");
                pr.Property("_reviewText").HasColumnName("ReviewText");
            });
        }
    }
}
