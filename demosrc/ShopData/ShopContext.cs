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

        /// <summary>
        /// Configuration for the Context
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .UseHiLo()
                .ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);
        }

        /// <summary>
        /// Aggregate Roots Go Here 
        /// note: Owned Value Types do not appear here
        /// </summary>
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSummaryResult> ProductSummaries { get; set; }
    }
}
