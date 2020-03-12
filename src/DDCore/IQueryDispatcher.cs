using DDCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
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
