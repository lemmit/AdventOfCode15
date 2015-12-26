using System;

namespace AdventOfCode1
{
    public class Elevator : IElevator
    {
        private string input;
        private int finalFloor = 0;
        private int gotToBasement = -1;
        private bool solved = false;
        public Elevator(string input)
        {
            this.input = input;
        }

        private void Solve()
        {
            int closing = 0;
            int opening = 0;
            bool inBasement = false;
            for (int i = 0; i < input.Length; i++)
            {
                var character = input[i];
                switch (character)
                {
                    case '(':
                        opening++;
                        break;
                    case ')':
                        closing++;
                        break;
                }
                if (opening - closing < 0 && !inBasement)
                {
                    gotToBasement = i + 1;
                    inBasement = true;
                }
            }
            finalFloor = opening - closing;
        }

        public int FinalFloor()
        {
            if (!solved) { Solve(); }
            return finalFloor;
        }

        public int WhenGotToBasement()
        {
            if (!solved) { Solve(); }
            return gotToBasement;
        }
    }
}