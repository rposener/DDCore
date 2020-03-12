using DDCore.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDCore
{
    public interface IIntegrationQueue
    {
        /// <summary>
        /// Adds an Integration Event to the queue.  The Queued Events are only dispatched
        /// after the Command is Successfully Executed.
        /// </summary>
        /// <param name="event">Integration Event to Dispatch</param>
        void QueueEvent(IIntegrationEvent @event);
    }
}
