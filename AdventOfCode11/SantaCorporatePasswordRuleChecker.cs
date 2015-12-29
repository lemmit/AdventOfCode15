using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode11
{
    public class SantaCorporatePasswordRuleChecker : RuleChecker
    {
        public SantaCorporatePasswordRuleChecker()
        {
            InitializeRuleChecker();
        }
        private void InitializeRuleChecker()
        {
            this.AddRule((str) => IncreasingStraightOfThree(str))
                .AddRule((str) => !str.Contains("i"))
                .AddRule((str) => !str.Contains("l"))
                .AddRule((str) => !str.Contains("o"))
                .AddRule((str) => OverlappingPairs(str) == 2)
                ;
        }

        static int OverlappingPairs(string pass)
        {
            int pairs = 0;
            for(int i=0; i<pass.Length - 1; i++)
            {
                if (pass[i] == pass[i + 1])
                {
                    pairs++;
                    i++;
                }
            }
            return pairs;
        } 
        static bool IncreasingStraightOfThree(string pass)
        {
            bool straight = false;
            for (int i = 0; i < pass.Length - 2; i++)
            {
                if (pass[i] + 1 == pass[i + 1]
                    && pass[i + 1] + 1 == pass[i + 2])
                {
                    straight = true;
                }
            }
            return straight;
        }
    }
}
