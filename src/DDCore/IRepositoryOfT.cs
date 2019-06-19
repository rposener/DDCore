using DDCore.Domain;
using DDCore.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDCore
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Finds the first <typeparamref name="T"/> that matches the <paramref name="specification"/>
        /// Returns Null if none are found
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>First <typeparamref name="T"/> that meets the <paramref name="specification"/> or null</returns>
        Task<T> LookupAsync(LookupSpecification<T> specification);

        /// <summary>
        /// Returns all <typeparamref name="T"/> that match the <paramref name="specification"/>
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>All <typeparamref name="T"/> that meet the <paramref name="specification"/> or an empty set</returns>
        Task<IEnumerable<T>> QueryAsync(QuerySpecification<T> specification);

        /// <summary>
        /// Executes a <paramref name="command"/> that updates data of one or more <typeparamref name="T"/>.
        /// </summary>
        /// <param name="command">Command to Execute</param>
        /// <returns></returns>
        Task ExecuteCommandAsync(Command<T> command);
    }
}
