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
                //var sum = FirstPart(file);
                var sum = SecondPart(file);
                System.Console.WriteLine(sum);
            }
            System.Console.ReadLine();
            System.Console.ReadLine();
        }

        static IEnumerable<string> GetPairs(string str)
        {
            for(int i=0; i<str.Length-1; i++)
            {
                yield return str[i].ToString() + str[i + 1].ToString();
            }
        }

        static int SecondPart(StreamReader sw)
        {
            return sw.ForEachLine((line) =>
            {
                var strings = GetPairs(line);
                var pairs = strings.ToList()
                        .Select((pair) => line.AllIndexesOf(pair).Count() >= 2)
                        .Count( (pair) => pair);
                var lettersWithAnotherBetween = false;
                for(int i=0; i<line.Length - 2; i++)
                {
                    if(line[i] == line[i + 2])
                    {
                        lettersWithAnotherBetween = true;
                        break;
                    }
                }
                var nice = (pairs > 0 && lettersWithAnotherBetween) ? 1 : 0;
                System.Console.WriteLine($"{line} is nice={nice} because { pairs > 0 } and { lettersWithAnotherBetween } ");
                return nice;
            }).Sum();
        }

        static int FirstPart(StreamReader sw)
        {
            return sw.ForEachLine((line) =>
            {
                if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
                    return 0;
                var vowels = 0;
                "aeiou".ToList().ForEach((c) => vowels += line.AllIndexesOf(c.ToString()).Count());
                var anyDouble = false;
                for (var i = 0; i < line.Length - 1; i++)
                {
                    if (line[i] == line[i + 1])
                    {
                        anyDouble = true;
                        break;
                    }
                }
                if (vowels >= 3 && anyDouble)
                {
                    return 1;
                }
                return 0;
            }).Sum();
        }
    }
}
