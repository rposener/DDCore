using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ShopData
{
    public static class ShopDataServiceCollectionExtensions
    {
        public static void AddShopData(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<ShopContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductSummaryQuery>();
        }
    }
}
