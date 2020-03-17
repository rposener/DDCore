﻿using DDCore.Queries;
using System.Threading.Tasks;

namespace DDCore.Abstractions
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