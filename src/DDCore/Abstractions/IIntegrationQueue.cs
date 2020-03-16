using DDCore.IntegrationEvents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDCore.Abstractions
{
    public interface IIntegrationQueue
    {
        /// <summary>
        /// Adds an Integration Event to the queue.  The Queued Events are only dispatched
        /// after the Command is Successfully Executed.
        /// </summary>
        /// <param name="event">Integration Event to Dispatch</param>
        void QueueEvent(IIntegrationEvent @event);

        /// <summary>
        /// Returns all Queued Integration Events
        /// </summary>
        IReadOnlyCollection<IIntegrationEvent> IntegrationEvents { get; }
    }
}
