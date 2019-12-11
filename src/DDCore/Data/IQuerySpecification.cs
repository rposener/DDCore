using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDCore.Data
{
    /// <summary>
    /// Decorator Interface to Identify Queries that return some type of <seealso cref="IQueryResult"/>
    /// Types implementing this Interface should use Constructor Injection and/or Property Setters to configure the Query
    /// </summary>
    public interface IQuerySpecification<T> where T:IQueryResult
    {

        /// <summary>
        /// Executes the <see cref="IQuerySpecification{T}"/>
        /// </summary>
        /// <returns>readonly list of <typeparamref name="T"/></returns>
        Task<IReadOnlyList<T>> ExecuteAsync();
    }
}
