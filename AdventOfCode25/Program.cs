using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode25
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(FirstPart(3010, 3019));
            System.Console.WriteLine(FirstPart_Functional(3010, 3019));
            for (int y = 1; y < 5; y++)
            {
                for (int x = 1; x < 5; x++)
                {
                    System.Console.Write($"{FirstPart_Functional(x, y)} ");
                }
                System.Console.WriteLine();
            }
            System.Console.ReadLine();
        }

        private static long FirstPart_Functional(int col, int row)
        {
            int fromRow = 1 + row * (row - 1) / 2;
            int fromCol = (col+row-1)* (col+row-1 - 1) / 2
                        - (row)*(row-1)/2;
            int index = fromRow + fromCol;
            long value = 20151125;
            while (--index > 0)
            {
                value = NextValue(value);
            }
            return value;
        }

        private static long FirstPart(int row, int col)
        {
            int x = 0;
            int y = 0;
            long index = 20151125;
            while (true)
            {
                // System.Console.WriteLine($"[{x},{y}] = {index}");
                index = NextValue(index);
                x++;
                y--;
                if (y < 0)
                {
                    y = x;
                    x = 0;
                }
                if (y == row - 1 
                    && x == col - 1)
                {
                    return index;
                }
            }
        }

        private static long NextValue(long index)
        {
            index *= 252533;
            index %= 33554393;
            return index;
        }
    }
}
