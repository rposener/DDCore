using DDCore.Abstractions;
using DDCore.Commands;
using DDCore.Configuration;
using DDCore.DefaultProviders;
using DDCore.Domain;
using DDCore.IntegrationEvents;
using DDCore.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
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
            // Add Configuration
            services.AddOptions<DDCoreOptions>();

            if (assemblies == null || assemblies.Length == 0)
            {
                throw new Exception("Call to DDDCore() did not specify any Assemblies.");
            }

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.Where(t => t.ClassImplementsGenericInterface((typeof(ICommandHandler<>)))), true)
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.Where(t => t.ClassImplementsGenericInterface((typeof(IDomainEventHandler<>)))), true)
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.Where(t => t.ClassImplementsGenericInterface((typeof(IIntegrationEventHandler<>)))), true)
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.Where(t =>t.ClassImplementsGenericInterface(typeof(IQueryHandler<,>))), true)
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            // Attempt to Add these if an implementation is not already provided
            services.TryAddScoped<ICommandDispatcher, Dispatcher>();
            services.TryAddScoped<IQueryDispatcher, Dispatcher>();
            services.TryAddScoped<IDomainEventDispatcher, Dispatcher>();
            services.TryAddScoped<IIntegrationEventDispatcher, Dispatcher>();

            // Add the Integration Queue as Scoped
            services.AddScoped<IIntegrationQueue, IntegrationQueue>();
            services.AddScoped<IntegrationQueue>();
        }

        /// <summary>
        /// Configures DDCore from an IConfiguration provided
        /// </summary>
        /// <param name="services">service collection with DDCore</param>
        /// <param name="config">configuration section</param>
        public static void ConfigureDDCore(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<DDCoreOptions>(config.GetSection("Dispatcher"));
        }

        /// <summary>
        /// Configured DDCore from an action
        /// </summary>
        /// <param name="services">service collection with DDCore</param>
        /// <param name="config">action to configure DDCore</param>
        public static void ConfigureDDCore(this IServiceCollection services, Action<DDCoreOptions> config)
        {
            services.Configure(config);
        }

        private static bool ClassImplementsGenericInterface(this Type classType, Type interfaceType) 
        {
            var interfacesImplemented = classType.GetInterfaces();
            foreach (var inter in interfacesImplemented)
            {
                if (inter.IsGenericType)
                {
                    if (inter.GetGenericTypeDefinition() == interfaceType.GetGenericTypeDefinition())
                        return true;
                }
            }
            return false;
        }
    }
}
