using DDCore.Data;
using DDCore.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DDCore
{
    public class Dispatcher : ICommandDispatcher, IQueryDispatcher, IDomainEventDispatcher, IIntegrationEventDispatcher
    {
        private readonly IServiceProvider provider;
        private readonly ILogger<Dispatcher> logger;

        public Dispatcher(IServiceProvider provider, ILogger<Dispatcher> logger)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            dynamic handler = provider.GetService(handlerType);
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
                logger.LogInformation("{0} successful execution took {1} using {2}.", commandName, sw.Elapsed, ((Type)handler.GetType()).Name);
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
            var handlers = (IEnumerable<dynamic>) provider.GetService(handlersListType);
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
        /// Dispatches an Integration Event
        /// Failures in Handlers are ignored
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
            catch(Exception ex)
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
