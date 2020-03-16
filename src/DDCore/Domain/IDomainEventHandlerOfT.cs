using System.Threading.Tasks;

namespace DDCore.Domain
{
    /// <summary>
    /// Handler for a Domain Event. Domain Events occur just before persistence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        /// <summary>
        /// Handles <typeparamref name="T"/>
        /// </summary>
        /// <param name="domainEvent">The event that occurred</param>
        /// <returns></returns>
        Task HandleEventAsync(T domainEvent);
    }
}
