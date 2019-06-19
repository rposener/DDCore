using System.Data.Common;
using System.Threading.Tasks;

namespace DDCore.Domain
{
    /// <summary>
    /// Base Class for a Command that Affects the State/Storage of an <seealso cref="IAggregateRoot"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Command<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Executes a Command Affecting one or more <typeparamref name="T"/> entities
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public abstract Task ExecuteAsync(DbConnection connection, DbTransaction transaction);
    }
}
