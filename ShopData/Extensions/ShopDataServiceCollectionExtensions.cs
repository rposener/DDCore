using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopData;
using System;
using System.Collections.Generic;
using System.Text;

public static class ShopDataServiceCollectionExtensions
{
    public static void AddShopData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<ShopContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}
