using DDCore.Domain;
using System.Threading.Tasks;

namespace DDCore.Queries
{
    /// <summary>
    /// Decorator Interface to Identify Queries that return some type of <seealso cref="IQueryResult"/> which is not a <seealso cref="EntityObject"/> or <seealso cref="ValueObject"/>
    /// Types implementing this Interface should use Constructor Injection and/or Property Setters to configure the Query
    /// </summary>
    public interface IQueryHandler<TQuery, TResult> where TQuery:IQuery<TResult>
    {

        /// <summary>
        /// Executes the <see cref="IQuery{TResult}"/>
        /// </summary>
        /// <param name="query">the query to execute</param>
        /// <returns>a <typeparamref name="TResult"/> from the Query Execution</returns>
        Task<TResult> ExecuteAsync(TQuery query);
    }
}
