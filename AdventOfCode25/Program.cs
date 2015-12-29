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
            System.Console.ReadLine();
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
