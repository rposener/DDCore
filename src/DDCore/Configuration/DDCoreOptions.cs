using System;
using System.Collections.Generic;
using System.Text;

namespace DDCore.Configuration
{
    /// <summary>
    /// Configures how DDCore Executes
    /// </summary>
    public class DDCoreOptions
    {
        public DDCoreOptions()
        {
            // Set Defaults
            DispatchIntegrationsConcurrently = true;
            DispatchIntegrationEventsOnSuccessfulCommand = true;
        }

        /// <summary>
        /// Should <seealso cref="IntegrationEvents.IIntegrationEvent"/>s be dispatched concurrently
        /// when <seealso cref="Commands.ICommand"/> is return <seealso cref="Result.IsSuccess"/>.
        /// Defaults to true
        /// </summary>
        public bool DispatchIntegrationsConcurrently { get; set; }

        /// <summary>
        /// Should <seealso cref="Commands.ICommand"/> automatically trigger all <seealso cref="IntegrationEvents.IIntegrationEvent"/>s
        /// in the <seealso cref="Abstractions.IIntegrationQueue"/> when the return is <seealso cref="Result.IsSuccess"/>.
        /// Defaults to true
        /// </summary>
        public bool DispatchIntegrationEventsOnSuccessfulCommand { get; set; }
    }
}
