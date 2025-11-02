using System.Collections.Generic;

namespace ReactiveVariables
{
    public abstract class EquallityComparer<T> : IEqualityComparer<T>
    {
        protected EquallityComparer() { }

        public static EquallityComparer<T> Default { get; }

        public abstract bool Equals(T x, T y);
        public abstract int GetHashCode(T obj);
    }
}