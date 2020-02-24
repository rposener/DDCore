using DDCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopData;
using System;

namespace ShopServices
{
    public static class ServiceCollectionExtensions
    {
        private static ILoggerFactory GetLoggerFactory(Action<ILoggingBuilder> sqlLogConfig)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(sqlLogConfig);
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        public static void AddShopServices(this IServiceCollection services, string connectionString, Action<ILoggingBuilder> sqlLogConfig = null)
        {
            services.AddDbContextPool<ShopContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseLoggerFactory(GetLoggerFactory(sqlLogConfig));
            });


            services.AddDDCore(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
