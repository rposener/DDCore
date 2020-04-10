using DDCore.Abstractions;
using DDCore.Commands;
using DDCore.Configuration;
using DDCore.Domain;
using DDCore.IntegrationEvents;
using DDCore.Queries;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DDCore.DefaultProviders
{
    public class Dispatcher : ICommandDispatcher, IQueryDispatcher, IDomainEventDispatcher, IIntegrationEventDispatcher
    {
        private readonly IServiceProvider provider;
        private readonly IIntegrationQueue integrationQueue;
        private readonly ILogger<Dispatcher> logger;
        private readonly DDCoreOptions options;

        public Dispatcher(IServiceProvider provider, IIntegrationQueue integrationQueue, ILogger<Dispatcher> logger, IOptions<DDCoreOptions> options)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.integrationQueue = integrationQueue ?? throw new ArgumentNullException(nameof(integrationQueue));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Dispatches a Command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<Result> DispatchAsync(ICommand command)
        {
            var commandName = command.GetType().Name;
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            var handler = (dynamic)provider.GetService(handlerType);
            if (handler == null)
            {
                logger.LogError("The Command Handler {0} could not be resolved.", handlerType.FullName);
            }
            logger.LogDebug("{0} starting execution using {1}", commandName, ((Type)handler.GetType()).Name);
            var sw = Stopwatch.StartNew();
            Result result;
            try
            {
                result = await handler.HandleAsync((dynamic)command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{0} failed to execute", commandName);
                throw;
            }
            sw.Stop();
            if (result.IsSuccess)
            {
                logger.LogInformation("complete {0} execution took {1} using {2}.", commandName, sw.Elapsed, ((Type)handler.GetType()).Name);
                if (options.DispatchIntegrationEventsOnSuccessfulCommand)
                {
                    logger.LogInformation("starting integration events for {0}.", commandName);
                    sw.Restart();
                    await DispatchAllIntegrationEventsAsync();
                    sw.Stop();
                    logger.LogInformation("integration events for {0} took {1}.", commandName, sw.Elapsed);
                }
            }
            else
            {
                logger.LogError("{0} failed with error: {1}", commandName, result.Error);
            }
            return result;
        }

        /// <summary>
        /// Dispatches a Domain Event
        /// Failures in Handlers will Throw
        /// </summary>
        /// <param name="domainEvent"></param>
        /// <returns></returns>
        public async Task DispatchAsync(IDomainEvent domainEvent)
        {
            var eventName = domainEvent.GetType().Name;
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlersListType = typeof(IEnumerable<>).MakeGenericType(handlerType);
            var handlers = (IEnumerable<dynamic>)provider.GetService(handlersListType);
            foreach (var handler in handlers)
            {
                logger.LogDebug("Dispatching {0} event using {1}", eventName, ((Type)handler.GetType()).Name);
                var sw = Stopwatch.StartNew();
                Result result;
                try
                {
                    result = await handler.HandleEventAsync((dynamic)domainEvent);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{0} event failed", eventName);
                    throw;
                }
                sw.Stop();
            }
        }


        /// <summary>
        /// Dispatches all events in the queue
        /// </summary>
        /// <param name="runConcurrently">Default is false 
        /// if true all <seealso cref="IIntegrationEventHandler{T}"/>s will run concurrently.</param>
        /// <returns></returns>
        public async Task DispatchAllIntegrationEventsAsync()
        {
            // Validate we have any Integration Events
            if (!(integrationQueue.IntegrationEvents?.Any() ?? false))
                return;

            if (options.DispatchIntegrationsConcurrently)
            {
                await Task.WhenAll(integrationQueue.IntegrationEvents.Select(ev => DispatchAsync(ev)));
            }
            else
            {
                foreach (var ev in integrationQueue.IntegrationEvents)
                {
                    await DispatchAsync(ev);
                }
            }
        }


        /// <summary>
        /// Dispatches an Integration Event
        /// Failures in Handlers are logged but ignored
        /// </summary>
        /// <param name="integrationEvent"></param>
        /// <returns></returns>
        public async Task DispatchAsync(IIntegrationEvent integrationEvent)
        {
            var eventName = integrationEvent.GetType().Name;
            var handlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEvent.GetType());
            var handlersListType = typeof(IEnumerable<>).MakeGenericType(handlerType);
            var handlers = (IEnumerable<dynamic>)provider.GetService(handlersListType);
            foreach (var handler in handlers)
            {
                logger.LogDebug("Starting {0} integration event using {1}", eventName, ((Type)handler.GetType()).Name);
                var sw = Stopwatch.StartNew();
                Result result;
                try
                {
                    result = await handler.HandleEventAsync((dynamic)integrationEvent);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{0} integration event failed", eventName);
                }
                sw.Stop();
            }
        }

        /// <summary>
        /// Dispatches a Query
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
        {
            var queryName = query.GetType().Name;
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = provider.GetService(handlerType);
            if (handler == null)
            {
                logger.LogError("The Command Handler for {0} could not be resolved.", queryName);
            }
            logger.LogDebug("{0} starting execution using {1}", queryName, ((Type)handler.GetType()).Name);
            var sw = Stopwatch.StartNew();
            TResult result;
            try
            {
                result = await handler.ExecuteAsync((dynamic)query);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{0} failed to execute", queryName);
                throw;
            }
            sw.Stop();
            logger.LogInformation("{0} successful execution took {1} using {2}.", queryName, sw.Elapsed, ((Type)handler.GetType()).Name);
            return result;
        }
    }
}
