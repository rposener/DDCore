using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DDCore.Domain;

namespace DDCore.Data
{
    /// <summary>
    /// Decorator Interface to Identify Queries that return some type of <seealso cref="IQueryResult"/> which is not a <seealso cref="EntityObject"/> or <seealso cref="ValueObject"/>
    /// Types implementing this Interface should use Constructor Injection and/or Property Setters to configure the Query
    /// </summary>
    public interface IQuerySpecification<T> where T:IQueryResult
    {

        /// <summary>
        /// Executes the <see cref="IQuerySpecification{T}"/>
        /// </summary>
        /// <returns>readonly iterable collection of <typeparamref name="T"/></returns>
        Task<IEnumerable<T>> ExecuteAsync();
    }
}
