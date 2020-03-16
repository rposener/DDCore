using DDCore.IntegrationEvents;
using System.Threading.Tasks;

namespace DDCore.Abstractions
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
