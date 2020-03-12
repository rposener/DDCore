using DDCore.Events;
using System.Threading.Tasks;

namespace DDCore.Events.Interfaces
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
