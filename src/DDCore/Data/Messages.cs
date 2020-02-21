using DDCore.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DDCore.Data
{
    public sealed class Messages
    {
        private readonly IServiceProvider provider;
        private readonly ILogger<Messages> logger;

        public Messages(IServiceProvider provider, ILogger<Messages> logger)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Dispatches a Command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<Result> Dispatch(ICommand command)
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
        /// Dispatches a Query
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<TResult> Dispatch<TResult>(IQuery<TResult> query)
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
