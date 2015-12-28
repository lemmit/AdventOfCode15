using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode13
{
    public enum HappinessRelation : int
    {
        Lose = -1,
        Gain = 1
    }

    public class RelationEntry
    {
        public GuestPair GuestPair { get; private set; }
        public HappinessRelation Relation { get; private set; }
        public int ValueOfRelation { get; private set; }
        public RelationEntry(GuestPair guestPair, HappinessRelation relation, int valueOfRelation)
        {
            GuestPair = guestPair;
            Relation = relation;
            ValueOfRelation = valueOfRelation;
        }
        public RelationEntry(string txt)
        {
            var extractedData = ExtractDataFromLine(txt);
            GuestPair = new GuestPair(extractedData.Item1, extractedData.Item4);
            Relation = (HappinessRelation)Enum.Parse(typeof(HappinessRelation), 
                                                            extractedData.Item2.Capitalize());
            ValueOfRelation = int.Parse(extractedData.Item3);
        }

        /* Generated using http://txt2re.com/ */
        static Tuple<string, string, string, string> ExtractDataFromLine(string txt)
        {
            string re1 = "((?:[a-z][a-z]+))";   // Word 1
            string re2 = ".*?"; // Non-greedy match on filler
            string re3 = "(?:[a-z][a-z]+)"; // Uninteresting: word
            string re4 = ".*?"; // Non-greedy match on filler
            string re5 = "((?:[a-z][a-z]+))";   // Word 2
            string re6 = ".*?"; // Non-greedy match on filler
            string re7 = "(\\d+)";  // Integer Number 1
            string re8 = ".*?"; // Non-greedy match on filler
            string re9 = "(?:[a-z][a-z]+)"; // Uninteresting: word
            string re10 = ".*?";    // Non-greedy match on filler
            string re11 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re12 = ".*?";    // Non-greedy match on filler
            string re13 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re14 = ".*?";    // Non-greedy match on filler
            string re15 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re16 = ".*?";    // Non-greedy match on filler
            string re17 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re18 = ".*?";    // Non-greedy match on filler
            string re19 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re20 = ".*?";    // Non-greedy match on filler
            string re21 = "((?:[a-z][a-z]+))";  // Word 3

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13 + re14 + re15 + re16 + re17 + re18 + re19 + re20 + re21, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String word1 = m.Groups[1].ToString();
                String word2 = m.Groups[2].ToString();
                String int1 = m.Groups[3].ToString();
                String word3 = m.Groups[4].ToString();
                //Console.Write("EXTRACTION DEBUG: (" + word1.ToString() + ")" + "(" + word2.ToString() + ")" + "(" + int1.ToString() + ")" + "(" + word3.ToString() + ")" + "\n");
                return new Tuple<string, string, string, string>(word1, word2, int1, word3);
            }
            throw new ArgumentException();
        }
    }
}
