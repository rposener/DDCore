using DDCore;
using DDCore.Configuration;
using DDCore.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Adds Domain Driven Core to the Service Collection 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static DDBuilder AddDDCore(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWork>();
            var rootTypes = GetAllTypesOf<IAggregateRoot>();
            foreach (var type in rootTypes)
            {
                var iType = typeof(IRepository<>).MakeGenericType(type);
                var cType = typeof(Repository<>).MakeGenericType(type);
                services.AddScoped(iType, cType);
            }
            return new DDBuilder(services);
        }

        /// <summary>
        /// Uses Reflection over the <seealso cref="DependencyContext"/> to find Implemented Types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IEnumerable<Type> GetAllTypesOf<T>()
        {
            var platform = Environment.OSVersion.Platform.ToString();
            var runtimeAssemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(platform);

            return runtimeAssemblyNames
                .Select(Assembly.Load)
                .SelectMany(a => a.ExportedTypes)
                .Where(t => typeof(T).IsAssignableFrom(t));
        }

    }
}
