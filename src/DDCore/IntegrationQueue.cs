using DDCore.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDCore
{
    public class IntegrationQueue
    {
        private readonly ICollection<IIntegrationEvent> events;
        private readonly IIntegrationEventDispatcher dispatcher;

        public IntegrationQueue(IIntegrationEventDispatcher dispatcher)
        {
            this.events = new List<IIntegrationEvent>();
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Queues an Event in the Integration Queue
        /// </summary>
        /// <param name="event"></param>
        public void QueueEvent(IIntegrationEvent @event)
        {
            events.Add(@event);
        }

        /// <summary>
        /// Dispatches all events in the queue
        /// </summary>
        /// <param name="runConcurrently">Default is false 
        /// if true all <seealso cref="IIntegrationEventHandler{T}"/>s will run concurrently.</param>
        /// <returns></returns>
        public async Task DispatchAllAsync(bool runConcurrently = false)
        {
            if (runConcurrently)
            {
                await Task.WhenAll(events.Select(ev => dispatcher.DispatchAsync(ev)));
            }
            else
            {
                foreach(var ev in events)
                {
                    await dispatcher.DispatchAsync(ev);
                }
            }
        }
    }
}
