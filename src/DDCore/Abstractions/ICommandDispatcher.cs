﻿using DDCore.Commands;
using System.Threading.Tasks;

namespace DDCore.Abstractions
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