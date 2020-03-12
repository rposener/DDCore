using DDCore.Data;
using DDCore.Domain;
using DDCore.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        static bool ClassImplementsGenericInterface(this Type classType, Type interfaceType) 
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
