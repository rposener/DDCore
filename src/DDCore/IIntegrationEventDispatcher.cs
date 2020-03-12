using DDCore.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDCore
{
    public interface IIntegrationEventDispatcher
    {
        /// <summary>
        /// Dispatches an Integration Event
        /// Failures in Handlers are ignored
        /// </summary>
        /// <param name="integrationEvent"></param>
        /// <returns></returns>
        Task DispatchAsync(IIntegrationEvent integrationEvent);
    }
}
