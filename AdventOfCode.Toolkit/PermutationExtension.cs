﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Toolkit
{
    public static class PermutationExtensions
    {
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
    }
}
