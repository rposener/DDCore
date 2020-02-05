using DDCore.Domain;
using System.Threading.Tasks;

namespace DDCore.Data
{

    /// <summary>
    /// Repository of a <seealso cref="IAggregateRoot"/>
    /// </summary>
    public interface IRepository<T> where T: IAggregateRoot
    {
    }
}
