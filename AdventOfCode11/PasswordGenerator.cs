using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode11
{
    public class PasswordGenerator
    {
        private RuleChecker ruleChecker;
        public PasswordGenerator(RuleChecker ruleChecker)
        {
            this.ruleChecker = ruleChecker;
        }
        public string NextPassword(string password)
        {
            do
            {
                password = IncrementPassword(password);
            } while (!ruleChecker.Check(password));
            return password;
        }

        private string IncrementPassword(string password)
        {
            var pass = password.ToCharArray();
            var len = pass.Length;
            bool carry = true;
            int pos = 0;
            while (carry)
            {
                int reversedPos = (len - pos) - 1;
                var character = pass[reversedPos]++;

                if (character >= 'z')
                {
                    pass[reversedPos] = 'a';
                    pos++;
                }
                else
                {
                    carry = false;
                }
            }
            return new string(pass);
        }
    }
}
