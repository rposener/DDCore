using DDCore.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDCore.Extensions
{
    public static class DDBuilderExtensions
    {
        /// <summary>
        /// Configure Databse using IConfiguration
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dbConfig"></param>
        public static DDBuilder ConfigureDatabase(this DDBuilder builder, IConfiguration dbConfig)
        {
            builder.services.Configure<DatabaseOptions>(dbConfig);
            return builder;
        }

        /// <summary>
        /// Configure Databse using Action
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        public static DDBuilder ConfigureDatabase(this DDBuilder builder, Action<DatabaseOptions> options)
        {
            builder.services.Configure<DatabaseOptions>(options);
            return builder;
        }
    }
}
