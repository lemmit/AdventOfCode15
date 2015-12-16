using System;

namespace AdventOfCode11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lastPassword = "hxbxxyzz";
            var password = IncrementPassword(lastPassword);
            while (!CheckRequirements(password))
            {
                password = IncrementPassword(password);
            }
            System.Console.WriteLine(password);
            System.Console.ReadLine();
        }

        static bool CheckRequirements(string password)
        {
            char[] pass = password.ToCharArray();
            var increasing = IncreasingStraightOfThree(pass);
            var noForbidden = NoForbiddenLetters(pass);
            var noOverlapping = NoOverlappingPairs(pass) == 2;
            return increasing && noForbidden && noOverlapping;
        }

        static string IncrementPassword(string pass)
        {
            return new string(IncrementPassword(pass.ToCharArray()));
        }

        private static int NoOverlappingPairs(char[] pass)
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

        private static bool NoForbiddenLetters(char[] pass)
        {
            var passStr = pass.ToString();
            if (passStr.Contains("i")
                || passStr.Contains("I")
                || passStr.Contains("o"))
                return false;
            return true;
        }


        static bool IncreasingStraightOfThree(char[] pass)
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

        static char[] IncrementPassword(char[] pass)
        {
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
            return pass;
        }

    }
}
