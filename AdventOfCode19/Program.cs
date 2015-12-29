using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Program
    {
        const string Molecule = "CRnCaSiRnBSiRnFArTiBPTiTiBFArPBCaSiThSiRnTiBPBPMgArCaSiRnTiMgArCaSiThCaSiRnFArRnSiRnFArTiTiBFArCaCaSiRnSiThCaCaSiRnMgArFYSiRnFYCaFArSiThCaSiThPBPTiMgArCaPRnSiAlArPBCaCaSiRnFYSiThCaRnFArArCaCaSiRnPBSiRnFArMgYCaCaCaCaSiThCaCaSiAlArCaCaSiRnPBSiAlArBCaCaCaCaSiThCaPBSiThPBPBCaSiRnFYFArSiThCaSiRnFArBCaCaSiRnFYFArSiThCaPBSiThCaSiRnPMgArRnFArPTiBCaPRnFArCaCaCaCaSiRnCaCaSiRnFYFArFArBCaSiThFArThSiThSiRnTiRnPMgArFArCaSiThCaPBCaSiRnBFArCaCaPRnCaCaPMgArSiRnFYFArCaSiThRnPBPMgAr";
        static void Main(string[] args)
        {
            using (var sr = new StreamReader("../../input.txt"))
            {
                var replacements = sr.ForEachLine((line) => Extract(line)).ToList();

                /*First*/
                var possibleCreatedMolecules = Molecule.Replacements(replacements);
                System.Console.WriteLine($"Possibilities: {possibleCreatedMolecules.Distinct().Count()}");
                
                /*Second - if stops, then restart*/
                replacements.Shuffle();
                replacements = replacements.Select(rep => new Tuple<string, string>(rep.Item2, rep.Item1)).ToList();
                var ffPlant = new FussionFissionPlant(replacements);
                System.Console.WriteLine($"Depth: {ffPlant.Shorten(Molecule)}");
                
            }
            System.Console.ReadLine();
        }

        static Tuple<string, string> Extract(string txt)
        {
            
            string re1 = "((?:[a-z]+))";   // Word 1
            string re2 = ".*?"; // Non-greedy match on filler
            string re3 = "((?:[a-z]+))";   // Word 2

            Regex r = new Regex(re1 + re2 + re3, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String word1 = m.Groups[1].ToString();
                String word2 = m.Groups[2].ToString();
                return new Tuple<string, string>(word1, word2);
            }
            return null;
        }
    }
}
