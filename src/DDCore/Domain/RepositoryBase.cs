using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDCore.Domain
{

    /// <summary>
    /// Base class for Repository of a <seealso cref="IAggregateRoot"/>
    /// </summary>
    public abstract class RepositoryBase<T> where T: IAggregateRoot
    {
        /// <summary>
        /// Saves Changes in a single transaction made to items in the Repository
        /// </summary>
        public abstract Task SaveChangesAsync();
    }
}
