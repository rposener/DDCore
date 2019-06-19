using DDCore.Domain;
using System;
using System.Linq.Expressions;

namespace DDCore.Specification
{
    /// <summary>
    /// A Specification that will Determing if the <typeparamref name="T"/> is met
    /// when <see cref="IsSatisfiedBy(T)"/> is called
    /// </summary>
    /// <typeparam name="T">Type of Aggregate</typeparam>
    public abstract class Specification<T> where T : EntityObject
    {
        /// <summary>
        /// The Specification to Meet
        /// </summary>
        public abstract Expression<Func<T, bool>> Expression { get; }

        public bool IsSatisfiedBy(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }
}
