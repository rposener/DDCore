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
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductId).UseIdentityColumn();
            builder.Property(p => p.Name);
            builder.Property(p => p.Description);
            builder.Property(p => p.Price);

            // Owned Properties
            builder.OwnsMany(p => p.Reviews, pr =>
            {
                pr.ToTable("Review", "Product");
                pr.WithOwner().HasForeignKey("ProductId");
                pr.HasKey(r => r.ReviewId);
                pr.Property(r => r.ReviewId).UseIdentityColumn();
                pr.Property(r => r.Reviewer);
                pr.Property(r => r.ReviewDate);
                pr.Property(r => r.ReviewText);
            });
        }
    }
}
