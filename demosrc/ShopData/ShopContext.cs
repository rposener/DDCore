using Microsoft.EntityFrameworkCore;
using ShopData.Configuration;
using ShopDomain.Catalog;

namespace ShopData
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) 
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSummary> ProductSummaries { get; set; }

        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSummaryConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }
    }
}
