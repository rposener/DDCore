using System;
using System.Collections.Generic;
using System.Linq;

namespace DDCore.Domain
{
    /// <summary>
    /// Base Abstract Class for all Value Objects
    /// Supports a Default Sort and Equality Comparison by the returns from <see cref="GetEqualityComponents"/>
    /// </summary>
    public abstract class ValueObject : IComparable, IEquatable<ValueObject>
    {
        /// <summary>
        /// Provided by the implementing Class should provide a set of properties that
        /// are used for Comparison to understand if 2 Values are Equal and how to sort them
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<IComparable> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            return Equals(obj as ValueObject);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public int CompareTo(object obj)
        {
            if (obj is ValueObject otherValue)
            {
                using (IEnumerator<IComparable> myEnum = this.GetEqualityComponents().GetEnumerator())
                using (IEnumerator<IComparable> otherEnum = otherValue?.GetEqualityComponents().GetEnumerator())
                {
                    bool myNext = myEnum.MoveNext();
                    bool otherNext = otherEnum.MoveNext();
                    if (myNext != otherNext)
                        throw new InvalidOperationException("One of the collections ran out of values before the other");
                    var elemResult = myEnum.Current.CompareTo(otherEnum.Current);
                    if (elemResult != 0)
                        return elemResult;
                }

                return 0;
            }
            throw new ArgumentException("Cannot compare these types");
        }

        public bool Equals(ValueObject other)
        {
            // Do a Simple Comparison of these Objects
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
