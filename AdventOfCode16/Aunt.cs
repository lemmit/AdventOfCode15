using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode16
{
    public class Aunt : Tuple<int, string, int, string, int, string, int>
    {
        public Aunt(int auntNumber,
            string propertyName1, int value1,
            string propertyName2, int value2,
            string propertyName3, int value3
            ) : base(auntNumber,
                propertyName1, value1,
                propertyName2, value2,
                propertyName3, value3)
        { }

        static public Aunt FromString(string line)
        {
            string re1 = ".*?"; // Non-greedy match on filler
            string re2 = "(\\d+)";  // Integer Number 1
            string re3 = ".*?"; // Non-greedy match on filler
            string re4 = "((?:[a-z][a-z]+))";   // Word 1
            string re5 = ".*?"; // Non-greedy match on filler
            string re6 = "(\\d+)";  // Integer Number 2
            string re7 = ".*?"; // Non-greedy match on filler
            string re8 = "((?:[a-z][a-z]+))";   // Word 2
            string re9 = ".*?"; // Non-greedy match on filler
            string re10 = "(\\d+)"; // Integer Number 3
            string re11 = ".*?";    // Non-greedy match on filler
            string re12 = "((?:[a-z][a-z]+))";  // Word 3
            string re13 = ".*?";    // Non-greedy match on filler
            string re14 = "(\\d+)"; // Integer Number 4

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13 + re14, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(line);
            if (m.Success)
            {
                String int1 = m.Groups[1].ToString();
                String word1 = m.Groups[2].ToString();
                String int2 = m.Groups[3].ToString();
                String word2 = m.Groups[4].ToString();
                String int3 = m.Groups[5].ToString();
                String word3 = m.Groups[6].ToString();
                String int4 = m.Groups[7].ToString();
                return new Aunt(
                    int.Parse(int1),
                    word1,
                    int.Parse(int2),
                    word2,
                    int.Parse(int3),
                    word3,
                    int.Parse(int4)
                    );
            }
            throw new ArgumentException();
        }
    }
}
