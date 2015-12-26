using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode3
{
    public class SantaGridWalker : AbstractSantaGridWalker
    {
        private string input;

        public SantaGridWalker(string input)
        {
            this.input = input;
        }

        public override int VisitedHousesIfWalkAlone()
        {
            var dict = new Dictionary<Tuple<int, int>, int>();
            var sentinel = 1;
            var santaPosition = new Tuple<int, int>(0, 0);
            dict[santaPosition] = sentinel;
            for (int i = 0; i < input.Length; i++)
            {
                Tuple<int, int> position = santaPosition;
                var c = input[i];
                position = NextPosition(position, c);
                dict[position] = sentinel;
                santaPosition = position;
            }
            return dict.Keys.Count();
        }

        public override int VisitedHousesIfWalkWithRobot()
        {
            var dict = new Dictionary<Tuple<int, int>, int>();
            var sentinel = 1;
            var santaPosition = new Tuple<int, int>(0, 0);
            var roboPosition = new Tuple<int, int>(0, 0);
            dict[santaPosition] = sentinel;
            var santa = true;
            for (int i = 0; i < input.Length; i++)
            {
                Tuple<int, int> position;
                if (santa)
                {
                    position = santaPosition;
                }
                else
                {
                    position = roboPosition;
                }
                position = NextPosition(position, input[i]);
                dict[position] = sentinel;
                if (santa)
                {
                    santaPosition = position;
                }
                else
                {
                    roboPosition = position;
                }

                santa = !santa;

            }
            return dict.Keys.Count();
        }

    }
}
