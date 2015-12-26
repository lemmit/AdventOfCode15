using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode5
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var file = new StreamReader("../../input.txt"))
            {
                var input = file.ForEachLine(line => line).ToList();
                
                RuleChecker ruleChecker = GetRuleCheckerForFirstPart();
                var numberOfNiceStrings = input
                        .Select((line) => ruleChecker.Check(line) ? 1 : 0)
                        .Sum();
                System.Console.WriteLine($"[Part 1] Number of nice strings: {numberOfNiceStrings}");

                ruleChecker = GetRuleCheckerForSecondPart();
                var numberOfNiceStrings2ndPart = input
                        .Select((line) => ruleChecker.Check(line) ? 1 : 0)
                        .Sum();
                System.Console.WriteLine($"[Part 2] Number of nice strings: {numberOfNiceStrings2ndPart}");
            }
            System.Console.ReadLine();
        }

        private static readonly Func<string, bool> DebugRule = 
            (str) => { System.Console.WriteLine($"[DEBUG] {str}"); return true; };

        private static RuleChecker GetRuleCheckerForFirstPart()
        {
            return new RuleChecker()
                .AddRule(str => !str.Contains("ab"))
                .AddRule(str => !str.Contains("cd"))
                .AddRule(str => !str.Contains("pq"))
                .AddRule(str => !str.Contains("xy"))
                .AddRule(str => str.CountOfVowels() >= 3)
                .AddRule(str => str.SubsequentLetterPairs()
                                    .Select(pair => pair.Item1 == pair.Item2 ? 1 : 0)
                                    .Sum() > 0)
                ;
        }

        private static RuleChecker GetRuleCheckerForSecondPart()
        {
            return new RuleChecker()
                    .AddRule((str) => str
                            .SubsequentLetterPairs()
                            // ("a","d"), ("e", "f"), ("g", "e") ...
                            .Select(pair => pair.Item1.ToString() + pair.Item2.ToString())
                            //"ad", "ef", "ge" ...
                            .Any(pair => str.AllIndexesOf(pair).Count() >= 2))
                    .AddRule((str) => str
                            .LettersPairsWithGap()
                            .Count( (pairWithGap) => pairWithGap.Item1 == pairWithGap.Item2) > 0)
                ;
        }

    }
}
