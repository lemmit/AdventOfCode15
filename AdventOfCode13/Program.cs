using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode13
{

    class Program
    {
        static Dictionary<GuestPair, RelationEntry> Relations 
                    = new Dictionary<GuestPair, RelationEntry>();

        static void Main(string[] args)
        {
            using(var sr = new StreamReader("../../input.txt"))
            {
                var input = sr.ForEachLine(line => new RelationEntry(line)).ToList();

                //add me for second part
                input.Add(new RelationEntry(new GuestPair("Me", "Me"), HappinessRelation.Gain, 0));
                System.Console.WriteLine(DateTime.Now);

                int bestOption = new MaxHappinessTableArrangementFinder(input)
                                .Arrangement();
                System.Console.WriteLine($"Best option is {bestOption}");
                System.Console.WriteLine(DateTime.Now);

                bestOption = new MaxHappinessUsingRecurenceTableArrangementFinder(input)
                                .Arrangement();
                System.Console.WriteLine($"Best option [solution 2] is {bestOption}");
                System.Console.WriteLine(DateTime.Now);
            }
            System.Console.ReadLine();
        }
    }
}
