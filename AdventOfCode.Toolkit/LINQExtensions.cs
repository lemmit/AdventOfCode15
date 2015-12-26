using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Toolkit
{
    public static partial class LINQExtensions
    {
        public static IEnumerable<T> DistinctBy<T, TIdentity>(this IEnumerable<T> source, Func<T, TIdentity> identitySelector)
        {
            return source.Distinct(LINQExtensions.By(identitySelector));
        }

        public static IEqualityComparer<TSource> By<TSource, TIdentity>(Func<TSource, TIdentity> identitySelector)
        {
            return new DelegateComparer<TSource, TIdentity>(identitySelector);
        }

        private class DelegateComparer<T, TIdentity> : IEqualityComparer<T>
        {
            private readonly Func<T, TIdentity> identitySelector;

            public DelegateComparer(Func<T, TIdentity> identitySelector)
            {
                this.identitySelector = identitySelector;
            }

            public bool Equals(T x, T y)
            {
                return Equals(identitySelector(x), identitySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return identitySelector(obj).GetHashCode();
            }
        }

        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
        {
            int length = source.Count();
            if (length != 0)
            {
                int index = 0;
                foreach (var item in source)
                {
                    var allOtherItems = source.RemoveAt(index);
                    foreach (var permutation in allOtherItems.Permutations())
                    {
                        yield return new[] { item }.Concat(permutation);
                    }
                    index++;
                }
            }
            else { yield return new T[0]; }
        }

        public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> source, int indexToRemove)
        {
            int index = 0;
            foreach (T item in source)
            {
                if (index != indexToRemove)
                {
                    yield return item;
                }
                index++;
            }
        }

        public static IDictionary<TKey,TValue> ToDictionary<TKey,TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            IDictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach(var keyValuePair in keyValuePairs)
            {
                dict.Add(keyValuePair);
            }
            return dict;
        }

        public static R Fold<T, R>(this IEnumerable<T> enumerable, R state, Func<T, R, R> func)
        {
            foreach(var elem in enumerable)
            {
                state = func(elem, state);
            }
            return state;
        }
    }
}
