using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode11
{
    public class RuleChecker
    {
        List<Func<string, bool>> Rules = new List<Func<string, bool>>();

        public RuleChecker AddRule(Func<string, bool> rule)
        {
            Rules.Add(rule);
            return this;
        }

        public bool Check(string line)
        {
            foreach (var rule in Rules)
            {
                if (!rule(line))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
