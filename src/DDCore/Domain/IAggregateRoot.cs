using DDCore.Data;

namespace DDCore.Domain
{
    /// <summary>
    /// Decorator Interface to Identify Aggregate Roots. 
    /// Each aggregate root should be backed by an <seealso cref="IRepository{T}"/> in the Data project
    /// Only Aggregate Roots are ever materialized with all Owned Properties loaded
    /// </summary>
    public interface IAggregateRoot
    {
    }
}
