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

    class StringInKeyValuePair : IEqualityComparer<KeyValuePair<int,string>>
    {
        public bool Equals(KeyValuePair<int, string> x, KeyValuePair<int, string> y)
        {
            var result = x.Value.CompareTo(y.Value);
            return result == 0;
        }

        public int GetHashCode(KeyValuePair<int, string> obj)
        {
            return obj.Value.GetHashCode();
        }
    }
    class InvertedComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var result = y.CompareTo(x); ;
            if (result == 0)
            {
                result = 1;
            }
            return result;
        }
    }

    class Program
    {
        const string Molecule = "CRnCaSiRnBSiRnFArTiBPTiTiBFArPBCaSiThSiRnTiBPBPMgArCaSiRnTiMgArCaSiThCaSiRnFArRnSiRnFArTiTiBFArCaCaSiRnSiThCaCaSiRnMgArFYSiRnFYCaFArSiThCaSiThPBPTiMgArCaPRnSiAlArPBCaCaSiRnFYSiThCaRnFArArCaCaSiRnPBSiRnFArMgYCaCaCaCaSiThCaCaSiAlArCaCaSiRnPBSiAlArBCaCaCaCaSiThCaPBSiThPBPBCaSiRnFYFArSiThCaSiRnFArBCaCaSiRnFYFArSiThCaPBSiThCaSiRnPMgArRnFArPTiBCaPRnFArCaCaCaCaSiRnCaCaSiRnFYFArFArBCaSiThFArThSiThSiRnTiRnPMgArFArCaSiThCaPBCaSiRnBFArCaCaPRnCaCaPMgArSiRnFYFArCaSiThRnPBPMgAr";

        static int Similarity(string str)
        {
            int max = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str.Substring(0, i) == Molecule.Substring(0, i))
                {
                    max = Math.Max(max, i);
                }
                else
                {
                    break;
                }
            }
            return max;
        }


        static void Main(string[] args)
        {
            using (var sr = new StreamReader("../../input.txt"))
            {
                var replacements = sr.ForEachLine((line) => Extract(line)).ToList();
                //var possibleCreatedMolecules = CreatePossibleMolecules(Molecule, replacements); /*First*/
                //replacements.Sort((a,b) => { return b.Item2.Length.CompareTo(a.Item2.Length); });
                replacements.Shuffle();
                Shorter(Molecule, replacements);
                //System.Console.WriteLine($"Final molecule: {molecule}");
                //System.Console.WriteLine($"Possibilities: {possibleCreatedMolecules.Count}");
                var sortedList = new SortedList<string, string>();
                foreach(var el in Fin)
                {
                    sortedList.Add(el.Key, el.Key);
                    System.Console.WriteLine(el.Key);
                }

                { }
            }
            System.Console.ReadLine();
        }
        private static Dictionary<string, int> Fin = new Dictionary<string, int>();
        private static Dictionary<string, int> All = new Dictionary<string, int>();
        private static int min = int.MaxValue;
        private static void Shorter(string molecule, List<Tuple<string, string>> replacements, int depth = 0)
        {
            var possibleCreatedMolecules = new Dictionary<string, int>();
            foreach (var replacement in replacements)
            {
                
                    for (int i = 0; i < molecule.Length; i++)
                {
                        var key = replacement.Item2;
                        if (i > molecule.Length - key.Length)
                        {
                            continue;
                        }
                        var substr = molecule.Substring(i, key.Length);
                        if (key == substr)
                        {
                            var before = molecule.Substring(0, i);
                            var after = molecule.Substring(i + key.Length);
                            //replace molecule with new one
                            var replaced = before + replacement.Item1 + after;
                            int a;
                            bool val = All.TryGetValue(replaced, out a);
                            if (!val)
                            {
                                All[replaced] = 1;
                                possibleCreatedMolecules[replaced] = 1;
                                if (depth < 5)
                                {
                                    System.Console.WriteLine("".PadLeft(depth * 3) + replacement.Item2 + " => " + replacement.Item1);
                                }
                            }
                            else
                            {
                                
                            }
                        }
                }
            }
            string el = molecule;
            if (possibleCreatedMolecules.Count == 0)
            {
                if (el.Length < min)
                {
                    min = el.Length;
                    System.Console.WriteLine($"FOUND MIN at {depth} {el.Length} {el}");
                }
                if (el.Length == 1)
                {
                    System.Console.WriteLine($"FOUND! {depth}");
                }
            }
            foreach (var pos in possibleCreatedMolecules.Keys)
            {
                var newR = replacements.ToList();
                newR.Shuffle();
                Shorter(pos, newR, depth + 1);
            }
        }

        private static Dictionary<string, int> CreatePossibleMolecules(string molecule, List<Tuple<string, string>> replacements)
        {
            var possibleCreatedMolecules = new Dictionary<string, int>();
            for (int i = 0; i < molecule.Length; i++)
            {
                foreach (var replacement in replacements)
                {
                    var key = replacement.Item1;
                    if (i > molecule.Length - key.Length)
                    {
                        continue;
                    }
                    var substr = molecule.Substring(i, key.Length);
                    if (key == substr)
                    {
                        var before = molecule.Substring(0, i);
                        var after = molecule.Substring(i + key.Length);
                        //replace molecule with new one
                        possibleCreatedMolecules[before + replacement.Item2 + after] = 1;
                    }
                }
            }

            return possibleCreatedMolecules;
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
