using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode9
{


    class Program
    {

        static Dictionary<Tuple<string, string>, int> Distances = new Dictionary<Tuple<string, string>, int>();
        static List<string> Cities;

        static int CheckLength(List<string> cities)
        {
            var sum = 0;
            for(int i=0; i<cities.Count-1; i++)
            {
                sum += Distances[new Tuple<string, string>(cities[i], cities[i + 1])];
            }
            return sum;
        }
        static void PrintStringList(List<string> list)
        {
            var strBuilder = new StringBuilder();
            foreach (var str in list)
            {
                strBuilder.Append(str);
                if(str != list.Last())
                    strBuilder.Append("-> ");
            }
            Console.WriteLine(strBuilder.ToString().PadLeft(45));
        }
        static void Main(string[] args)
        {
            LoadData(InputData);

            var min = int.MaxValue;
            var max = int.MinValue;
            List<string> maxList = null;
            List<string> minList = null;
            foreach(var perm in Cities.Permutations())
            {
                var permOfCities = perm.ToList();
                var len = CheckLength(permOfCities);
                if(len < min)
                {
                    min = len;
                    minList = permOfCities;
                }
                if (len > max)
                {
                    max = len;
                    maxList = permOfCities;
                }
            }

            PrintStringList(minList);
            System.Console.WriteLine($"Length: {min}");

            PrintStringList(maxList);
            System.Console.WriteLine($"Length: {max}");

            System.Console.ReadLine();
        }

        #region Data Loading & Initialization
        static void LoadData(string inputData)
        {
            var data = GetInputData(inputData).ToList();
            foreach (var info in data)
            {
                Distances[new Tuple<string, string>(info.Item1, info.Item2)] = info.Item3;
                Distances[new Tuple<string, string>(info.Item2, info.Item1)] = info.Item3;
            }
            var cities = data.Select(x => x.Item1).ToList();
            cities.AddRange(data.Select(x => x.Item2));
            Cities = cities.Distinct().ToList();

        }
        static IEnumerable<Tuple<string, string, int>> GetInputData(string inputData)
        {
            using (var reader = new StringReader(inputData))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return ExtractData(line);
                }
            }
        }
        static Tuple<string, string, int> ExtractData(string line)
        {
            string re1 = "((?:[a-z][a-z]+))";   // Word 1
            string re2 = ".*?"; // Non-greedy match on filler
            string re3 = "(?:[a-z][a-z]+)"; // Uninteresting: word
            string re4 = ".*?"; // Non-greedy match on filler
            string re5 = "((?:[a-z][a-z]+))";   // Word 2
            string re6 = ".*?"; // Non-greedy match on filler
            string re7 = "(\\d+)";	// Integer Number 1    // Integer Number 1

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(line);
            if (m.Success)
            {
                String word1 = m.Groups[1].ToString();
                String word2 = m.Groups[2].ToString();
                String int1 = m.Groups[3].ToString();
                return new Tuple<string, string, int>(word1, word2, int.Parse(int1));
            }
            return null;
        }

        const string TestData = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

        const string InputData = @"Faerun to Tristram = 65
Faerun to Tambi = 129
Faerun to Norrath = 144
Faerun to Snowdin = 71
Faerun to Straylight = 137
Faerun to AlphaCentauri = 3
Faerun to Arbre = 149
Tristram to Tambi = 63
Tristram to Norrath = 4
Tristram to Snowdin = 105
Tristram to Straylight = 125
Tristram to AlphaCentauri = 55
Tristram to Arbre = 14
Tambi to Norrath = 68
Tambi to Snowdin = 52
Tambi to Straylight = 65
Tambi to AlphaCentauri = 22
Tambi to Arbre = 143
Norrath to Snowdin = 8
Norrath to Straylight = 23
Norrath to AlphaCentauri = 136
Norrath to Arbre = 115
Snowdin to Straylight = 101
Snowdin to AlphaCentauri = 84
Snowdin to Arbre = 96
Straylight to AlphaCentauri = 107
Straylight to Arbre = 14
AlphaCentauri to Arbre = 46";
        #endregion
    }
}
