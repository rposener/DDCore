using DDCore.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDCore
{
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches a Domain Event
        /// Failures in Handlers will Throw
        /// </summary>
        /// <param name="domainEvent"></param>
        /// <returns></returns>
        Task DispatchAsync(IDomainEvent domainEvent);
    }
}
