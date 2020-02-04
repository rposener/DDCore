using DDCore.Domain;
using System.Threading.Tasks;

namespace DDCore.Data
{

    /// <summary>
    /// Repository of a <seealso cref="IAggregateRoot"/>
    /// </summary>
    public interface IRepository<T> where T: IAggregateRoot
    {
        /// <summary>
        /// Saves Changes in a single transaction made to items in the Repository
        /// </summary>
        public Task SaveChangesAsync();
    }
}
