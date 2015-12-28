using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode9
{
    public class CityPairWithDistanceFactory
    {
        public DataWithDistance<CityPair> Create(string line)
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
                return new DataWithDistance<CityPair>(
                        int.Parse(int1),
                        new CityPair(word1, word2)
                    );
            }
            throw new ArgumentException();
        }
    }
}
