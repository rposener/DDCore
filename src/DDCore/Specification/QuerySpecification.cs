using DDCore.Domain;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace DDCore.Specification
{
    /// <summary>
    /// A Specification that will find all matching <typeparamref name="T"/> 
    /// when submitted into <seealso cref="IRepository{T}.QueryAsync(QuerySpecification{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of Aggregate</typeparam>
    public abstract class QuerySpecification<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Execute Query to Get all <typeparamref name="T"/> which match the Specification
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public abstract Task<IEnumerable<T>> QueryAsync(DbConnection connection, DbTransaction transaction);
    }
}
