using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode1
{
    public class Elevator_Functional : IElevator
    {
        private string input;

        public Elevator_Functional(string input)
        {
            this.input = input;
        }

        public int FinalFloor()
        {
            var finalFloor = input.Select(c => ChangeFloorToInt(c)).Sum();
            return finalFloor;
        }

        public int WhenGotToBasement()
        {
            var getsToBasement = FindWhenGetsToBasement(input.Select(c => c).ToList());
            return getsToBasement;
        }

        private static int FindWhenGetsToBasement(List<char> seq, int pos = 0, int sum = 0)
        {
            if (pos >= seq.Count) { return -1; }
            if (sum < 0) { return pos; }
            var nextSum = sum + ChangeFloorToInt( seq[pos] );
            return FindWhenGetsToBasement(seq, pos + 1, nextSum);
        }

        private static int ChangeFloorToInt(char c)
        {
            switch (c)
            {
                case '(':
                    return 1;
                case ')':
                    return -1;
                default:
                    return 0;
            }
        }
    }
}