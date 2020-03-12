using DDCore.Data;
using System.Threading.Tasks;

namespace DDCore
{
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatches a Query
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query);
    }
}
