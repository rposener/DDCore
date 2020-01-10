using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDapperData.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddShopDapperData(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
