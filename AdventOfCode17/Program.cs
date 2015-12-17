using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Toolkit;
using System.IO;

namespace AdventOfCode17
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var sw = new StreamReader("../../input.txt"))
            {
                var list = sw.ForEachLine((line) => int.Parse(line) ).ToList();

                var result = Enumerable
                  .Range(1, (1 << list.Count) - 1)
                  .Select(index => list.Where((item, idx) => ((1 << idx) & index) != 0).ToList());

                //PART 1
                var combinationsSatysfying = result.Where(comb => comb.Sum() == 150);

                //PART 2
                var minCount = combinationsSatysfying.Min(comb => comb.Count());
                var minCombinations = combinationsSatysfying.Where(comb => comb.Count() == minCount);

                System.Console.WriteLine($"Number of combinations(Part 1): {combinationsSatysfying.Count()}");
                System.Console.WriteLine($"Different ways of minimal (Part 2): {minCombinations.Count()}");
            }
            System.Console.ReadLine();
        }
    }
}
