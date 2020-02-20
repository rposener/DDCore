using DDCore.Data;
using DDCore.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DDCore
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all DDDCore Types to the <paramref name="services"/> collection.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/> to add Services to</param>
        /// <param name="assemblies">List of <seealso cref="Assembly"/>s to scan for <seealso cref="IRepository{T}"/>, <seealso cref="ICommandHandler{TCommand}"/> and <seealso cref="IQueryHandler{TQuery, TResult}"/> implementations</param>
        public static void AddDDCore(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                throw new Exception("Call to DDDCore() did not specify any Assemblies.");
            }

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.Where(t => typeof(ICommandHandler<>).IsAssignableFrom(t)), true)
                .As(T => new[] { typeof(ICommandHandler<>).MakeGenericType(T) })
                .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.Where(t => typeof(IQueryHandler<,>).IsAssignableFrom(t)), true)
                .As(T =>
                {
                    var queryTypes = T.GetGenericArguments();
                    return new[] {
                        typeof(IQueryHandler<,>).MakeGenericType(queryTypes)
                    };
                })
                .WithTransientLifetime());

            services.AddScoped<Messages>();
        }
    }
}
