using System.Threading.Tasks;

namespace DDCore.IntegrationEvents
{
    /// <summary>
    /// Handler for an Integration Event.  Integration Events occur after successful persistence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIntegrationEventHandler<T> where T: IIntegrationEvent
    {
        /// <summary>
        /// Handles <typeparamref name="T"/> integration event
        /// </summary>
        /// <param name="integrationEvent">The integration event that occurred</param>
        /// <returns></returns>
        Task HandleEventAsync(T integrationEvent);
    }
}
