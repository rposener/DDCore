using DDCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopData;

namespace ShopServices
{
    public static class ServiceCollectionExtensions
    {
        public static void AddShopServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<ShopContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddDDCore(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
