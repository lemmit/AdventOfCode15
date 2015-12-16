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

        static int[,] Matrix;
        static Dictionary<string, int> Dict = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            var data = GetInputData().ToList();
            var nrOfCities = data.DistinctBy((a)=> a.Item1).Count();
            System.Console.WriteLine($"Number of cities: {nrOfCities}");
            Matrix = new int[nrOfCities+1, nrOfCities+1];
            var i = 0;
            foreach(var line in data.DistinctBy((a) => a.Item2))
            {
                Dict[line.Item1] = i++;
            }
            foreach(var line in data)
            {
                Matrix[Dict[line.Item1], Dict[line.Item2]] = line.Item3;
            }
            var dijkstraSolver = new Dijkstra(nrOfCities, Matrix);
            dijkstraSolver.Run();

        }



        static IEnumerable<Tuple<string, string, int>> GetInputData()
        {
            using (var reader = new StringReader(InputData))
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
            string re7 = "(65)";    // Integer Number 1

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
    }


}
