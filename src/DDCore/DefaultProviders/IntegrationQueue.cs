using DDCore.Abstractions;
using DDCore.IntegrationEvents;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDCore.DefaultProviders
{
    public class IntegrationQueue : IIntegrationQueue
    {
        private readonly List<IIntegrationEvent> events;

        /// <summary>
        /// List of Integration Events that were Queued
        /// </summary>
        public IReadOnlyCollection<IIntegrationEvent> IntegrationEvents
        {
            get
            {
                return events.AsReadOnly();
            }
        }

        public IntegrationQueue()
        {
            this.events = new List<IIntegrationEvent>();
        }

        /// <summary>
        /// Queues an Event in the Integration Queue
        /// </summary>
        /// <param name="event"></param>
        public void QueueEvent(IIntegrationEvent @event)
        {
            events.Add(@event);
        }

    }
}
