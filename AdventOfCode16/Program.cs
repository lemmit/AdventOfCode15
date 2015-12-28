using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode16
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            children: 3
            cats: 7
            samoyeds: 2
            pomeranians: 3
            akitas: 0
            vizslas: 0
            goldfish: 5
            trees: 3
            cars: 2
            perfumes: 1
            */

            using (var reader = new StreamReader("../../input.txt"))
            {
                var input = reader.ForEachLine(line => Aunt.FromString(line)).ToList();

                var aunts = input.IfHasPropertyThenWithValue("children", 3)
                    .IfHasPropertyThenGreaterThanValue("cats", 7)
                    .IfHasPropertyThenWithValue("samoyeds", 2)
                    .IfHasPropertyThenLesserThanValue("pomeranians", 3)
                    .IfHasPropertyThenWithValue("akitas", 0)
                    .IfHasPropertyThenWithValue("vizslas", 0)
                    .IfHasPropertyThenLesserThanValue("goldfish", 5)
                    .IfHasPropertyThenGreaterThanValue("trees", 3)
                    .IfHasPropertyThenWithValue("cars", 2)
                    .IfHasPropertyThenWithValue("perfumes", 1);

                aunts.Select(aunt => aunt.Item1.ToString()).ToList().PrintStringList(0);
            }
            System.Console.ReadLine();
        }
    }
}
