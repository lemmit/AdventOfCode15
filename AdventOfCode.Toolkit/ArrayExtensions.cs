using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Toolkit
{
    public static class ArrayExtensions
    {

        public static void ForEach<T>(this T[,] table, Action<int, int, T> action)
        {
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    action(row, col, table[row, col]);
                }
            }
        }

        public static IEnumerable<E> ForEach<T, E>(this T[,] table, Func<int,int, T, E> func)
        {
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    yield return func(row, col, table[row, col]);
                }
            }
        }

    }
}
