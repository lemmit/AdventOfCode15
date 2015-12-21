using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode20
{
    class Program
    {

        static IEnumerable<Tuple<int,int>> FirstSequence(int top = 0)
        {
            var tab = new int[top];
            for (int i = 1; i < top; i++)
            {
                for (int j = i - 1; j < top; j += i)
                {
                    tab[j]+=i;
                }
                yield return new Tuple<int,int>(i, tab[i-1]);
            }
        }

        static IEnumerable<Tuple<int, int>> SecondSequence(int top = 0)
        {
            var tab = new int[top];
            for (int i = 1; i < top; i++)
            {
                for (int j = i - 1, nrOfHouses = 0; j < top && nrOfHouses < 50; j += i, nrOfHouses++)
                {
                    tab[j] += i * 11;
                }
                yield return new Tuple<int, int>(i, tab[i - 1]);
            }
        }
        static void Main(string[] args)
        {
            int puzzle = 29000000;

            var first = FirstSequence(puzzle/10).First((h) => h.Item2 >= puzzle/10);
            var second = SecondSequence(puzzle).First((h) => h.Item2 >= puzzle);

            Console.WriteLine($"{first.Item1} : {first.Item2}");
            Console.WriteLine($"{second.Item1} : {second.Item2}");
            Console.ReadLine();
            
        }
    }
}