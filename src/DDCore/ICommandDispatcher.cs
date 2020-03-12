using DDCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDCore
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches a Command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<Result> DispatchAsync(ICommand command);
    }
}
