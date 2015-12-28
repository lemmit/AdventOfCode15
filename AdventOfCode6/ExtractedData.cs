using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode6
{
    public class ExtractedData
    {
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public ExtractedData(string txt)
        {
            /* Code generated using txt2re.com */

            string re1 = ".*?"; // Non-greedy match on filler
            string re2 = "(\\d+)";  // Integer Number 1
            string re3 = ".*?"; // Non-greedy match on filler
            string re4 = "(\\d+)";  // Integer Number 2
            string re5 = ".*?"; // Non-greedy match on filler
            string re6 = "(\\d+)";  // Integer Number 3
            string re7 = ".*?"; // Non-greedy match on filler
            string re8 = "(\\d+)";  // Integer Number 4

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String int1 = m.Groups[1].ToString();
                String int2 = m.Groups[2].ToString();
                String int3 = m.Groups[3].ToString();
                String int4 = m.Groups[4].ToString();
                StartPoint = new Point(int.Parse(int1), int.Parse(int2));
                EndPoint = new Point(int.Parse(int3), int.Parse(int4));
                return;
            }
            throw new ArgumentException();
        }
    }
}
