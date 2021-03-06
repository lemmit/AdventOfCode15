﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Toolkit
{
    public static class ListExtensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        private static Random rng = new Random(Guid.NewGuid().GetHashCode());
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IList<T> Shuffled<T>(this IList<T> listToShuffle)
        {
            var list = listToShuffle.ToList();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public static long Product(this List<int> list) 
        {
            long product = 1;
            foreach(var elem in list)
            {
                product *= elem;
            }
            return product;
        }

        public static void PrintStringList(this List<string> list, int padding = 45)
        {
            var strBuilder = new StringBuilder();
            foreach (var str in list)
            {
                strBuilder.Append(str);
                if (str != list.Last())
                {
                    strBuilder.Append("-> ");
                }
            }
            Console.WriteLine(strBuilder.ToString().PadLeft(padding));
        }
        
    }
}
