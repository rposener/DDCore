using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DDCore
{
    public class UnitOfWork : IDisposable
    {
        private readonly DatabaseOptions databaseOptions;
        private readonly ILogger<UnitOfWork> logger;

        internal DbConnection Connection { get; private set; }

        internal DbTransaction Transaction { get; private set; }

        public UnitOfWork(IOptions<DatabaseOptions> options, ILogger<UnitOfWork> logger)
        {
            this.databaseOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Begins a Unit of Work within the <seealso cref="IServiceScope"/>
        /// </summary>
        /// <param name="isolation"></param>
        public void Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            if (Connection != null)
                logger.LogError("Work has already begun and cannot be restarted in this scope.");
            else
            {
                logger.LogInformation("Starting a Unit Of Work");
                Connection = new SqlConnection(databaseOptions.ConnectionString);
                Connection.Open();
                Transaction = Connection.BeginTransaction(isolation);
            }
        }

        /// <summary>
        /// Commits the changes performed in this <seealso cref="IServiceScope"/>
        /// </summary>
        public void Commit()
        {
            if (Connection == null)
                logger.LogError("Call Begin() first - no work has started.");
            else
            {
                logger.LogInformation("Committing a Unit Of Work");
                Transaction?.Commit();
                Connection?.Close();
            }
        }

        /// <summary>
        /// Will either <see cref="Commit"/> or <see cref="Rollback"/> depending on <seealso cref="DatabaseOptions.AutoCommitWork"/>
        /// </summary>
        public void Dispose()
        {
            if (databaseOptions.AutoCommitWork)
                Commit();
            else
                Rollback();
        }

        /// <summary>
        /// Rolls back the changes performed in this <seealso cref="IServiceScope"/>
        /// </summary>
        public void Rollback()
        {
            if (Connection != null)
            {
                logger.LogInformation("Rolling back a Unit Of Work");
                Transaction?.Rollback();
                Connection?.Close();
            }
        }
    }
}
