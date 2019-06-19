using DDCore.Domain;
using System.Data.Common;
using System.Threading.Tasks;

namespace DDCore.Specification
{
    /// <summary>
    /// A Specification that will find exactly 1 <typeparamref name="T"/>
    /// when submitted into <seealso cref="IRepository{T}.LookupAsync(LookupSpecification{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of Aggregate</typeparam>
    public abstract class LookupSpecification<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Execute returns first <typeparamref name="T"/> which match the Specification
        /// Returns null if none are found
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public abstract Task<T> LookupAsync(DbConnection connection, DbTransaction transaction);
    }
}
