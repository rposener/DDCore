using DDCore.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDCore.Domain
{
    /// <summary>
    /// Base Class for all Entities which have an Identity specified by items returned from <see cref="GetIdentityComponents"/>
    /// </summary>
    public abstract class EntityObject : IEquatable<EntityObject>
    {
        /// <summary>
        /// Events that this Entity has Raised
        /// </summary>
        public ICollection<IDomainEvent> Events { get; }

        public EntityObject()
        {
            Events = new List<IDomainEvent>();
        }

        /// <summary>
        /// Provided by Classes that implement EntityObject
        /// Must return properties that Identity the EntityObject
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<IComparable> GetIdentityComponents();

        public override bool Equals(object obj)
        {
            return Equals(obj as EntityObject);
        }

        public override int GetHashCode()
        {
            return GetIdentityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public bool Equals(EntityObject other)
        {
            if (other == null)
                return false;
            return GetIdentityComponents().SequenceEqual(other.GetIdentityComponents());
        }

        public static bool operator ==(EntityObject a, EntityObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityObject a, EntityObject b)
        {
            return !(a == b);
        }
        
        public override string ToString()
        {
            return this.GetType().Name + ":"+ String.Join(".", GetIdentityComponents().Select(c => c.ToString()));
        }
    }
}
