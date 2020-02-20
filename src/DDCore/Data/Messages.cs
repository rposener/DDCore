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
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            logger.LogDebug("Finding Command Handler for {0}", handlerType.FullName);
            dynamic handler = provider.GetService(handlerType);
            var sw = Stopwatch.StartNew();
            Result result = await handler.Execute(command);
            sw.Stop();
            logger.LogDebug("Command Handler {0} execution took {1}.", handlerType.FullName, sw.Elapsed);
            if (result.IsSuccess)
            {
                logger.LogDebug("Command Handler {0} was successful", handlerType.FullName);
            }
            else
            {
                logger.LogDebug("Command Handler {0} failed with error: {1}", handlerType.FullName, result.Error);
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
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            logger.LogDebug("Finding Query Handler for {0}", handlerType.FullName);
            dynamic handler = provider.GetService(handlerType);
            var sw = Stopwatch.StartNew();
            TResult result = await handler.Execute(query);
            sw.Stop();
            logger.LogDebug("Query Handler {0} execution took {1}.", handlerType.FullName, sw.Elapsed);
            logger.LogDebug("Query Handler {0} was successful", handlerType.FullName);
            return result;
        }
    }
}
