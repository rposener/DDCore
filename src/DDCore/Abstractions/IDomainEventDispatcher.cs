using DDCore.Domain;
using System.Threading.Tasks;

namespace DDCore.Abstractions
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
