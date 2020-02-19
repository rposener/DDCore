using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ShopDapperData.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddShopDapperData(this IServiceCollection services, string connectionString)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
            services.AddScoped<ProductRepository>();
        }
    }
}
