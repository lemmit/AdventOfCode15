using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode8
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var file = new StreamReader("../../input.txt"))
            {
                var input = file.ForEachLine(line => line).ToList();
                var interpretedSum = FirstPart(input);
                Console.WriteLine($"First Part: {interpretedSum}");
                interpretedSum = SecondPart(input);
                Console.WriteLine($"Second Part: {interpretedSum}");
            }
            Console.ReadLine();
        }

        static int SecondPart(List<string> input)
        {
            var interpretedSum = input.Select((line) => {
                var backslashes = line.AllIndexesOf("\\").Count();
                var backslashQuote = line.AllIndexesOf("\"").Count();
                var interpreted = backslashQuote + backslashes + 2;
                return interpreted;
            }).Sum();
            return interpretedSum;
        }

        static int FirstPart(List<string> input)
        {
            var interpretedSum = input.Select((line) =>
            {
                var backslashes = line.AllIndexesOf("\\\\").Count(); // \\
                line = line.Replace("\\\\", "#");
                var backslashQuote = line.AllIndexesOf("\\\"").Count(); // \"
                var hexValues = new Regex("\\\\x([0-9A-Fa-f]){2}").Matches(line).Count;
                var interpreted = backslashes + backslashQuote + hexValues * 3 + 2; // ""
                return interpreted;
            }).Sum();

            return interpretedSum;
        }
    }
}
