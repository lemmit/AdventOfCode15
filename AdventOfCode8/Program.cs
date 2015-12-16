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
                //var interpretedSum = FirstPart(file);
                var interpretedSum = SecondPart(file);
                Console.WriteLine($"Sub: {interpretedSum}");
            }

            Console.ReadLine();
        }

        static int SecondPart(StreamReader file)
        {
            var interpretedSum = file.ForEachLine((line) => {
                //var hexValues = new Regex("\\\\x([0-9A-Fa-f]){2}").Matches(line).Count;
                var backslashes = line.AllIndexesOf("\\").Count();
                var backslashQuote = line.AllIndexesOf("\"").Count();
                var interpreted = backslashQuote + backslashes + 2;
                //Console.WriteLine(line);
                //Console.WriteLine($"{backslashes} + {backslashQuote} + 2 + 2 = {interpreted}");
                return interpreted;
            }).Sum();
            return interpretedSum;
        }

        static int FirstPart(StreamReader file)
        {
            var interpretedSum = file.ForEachLine((line) =>
            {
                //counter += line.Length;
                var interpreted = 0;
                var backslashes = line.AllIndexesOf("\\\\").Count(); // \\
                line = line.Replace("\\\\", "#");
                var backslashQuote = line.AllIndexesOf("\\\"").Count(); // \"
                var hexValues = new Regex("\\\\x([0-9A-Fa-f]){2}").Matches(line).Count;
                var backHexValues = 0;// new Regex("\\\\\\x([0-9A-Fa-f]){2}").Matches(line).Count;
                                      //var hexValues = line.AllIndexesOf("\\x").Count()*3; // \x66
                interpreted += backslashes;
                interpreted += backslashQuote;
                interpreted += hexValues * 3 - backHexValues * 3;
                interpreted += 2; // ""
                Console.WriteLine(line);
                Console.WriteLine($"{backslashes} + {backslashQuote} + {hexValues} - {backHexValues} = {interpreted}");
                return interpreted;
            }).Sum();

            return interpretedSum;
        }
    }
}
