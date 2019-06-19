using DDCore.Domain;
using DDCore.Specification;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDCore
{
    internal class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ILogger logger;

        public Repository(UnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = loggerFactory?.CreateLogger(typeof(Repository<T>)) ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public Task ExecuteCommandAsync(Command<T> command)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Executing {0} Command: {1}", command.GetType().FullName, JsonConvert.SerializeObject(command));
            }
            else
            {
                logger.LogInformation("Executing {0} Command", command.GetType().FullName);
            }
            return command.ExecuteAsync(unitOfWork.Connection, unitOfWork.Transaction);
        }

        public Task<T> LookupAsync(LookupSpecification<T> specification)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Executing {0} Lookup: {1}", specification.GetType().FullName, JsonConvert.SerializeObject(specification));
            }
            else
            {
                logger.LogInformation("Executing {0} Lookup", specification.GetType().FullName);
            }
            return specification.LookupAsync(unitOfWork.Connection, unitOfWork.Transaction);
        }

        public Task<IEnumerable<T>> QueryAsync(QuerySpecification<T> specification)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Executing {0} Query: {1}", specification.GetType().FullName, JsonConvert.SerializeObject(specification));
            }
            else
            {
                logger.LogInformation("Executing {0} Query", specification.GetType().FullName);
            }
            return specification.QueryAsync(unitOfWork.Connection, unitOfWork.Transaction);
        }
    }
}
