using System;

namespace AdventOfCode3
{
    public abstract class AbstractSantaGridWalker
    {
        public abstract int VisitedHousesIfWalkAlone();
        public abstract int VisitedHousesIfWalkWithRobot();

        public static Tuple<int, int> NextPosition(Tuple<int, int> position, char c)
        {
            switch (c)
            {
                case '<':
                    position = new Tuple<int, int>(position.Item1 - 1, position.Item2);
                    break;
                case '>':
                    position = new Tuple<int, int>(position.Item1 + 1, position.Item2);
                    break;
                case '^':
                    position = new Tuple<int, int>(position.Item1, position.Item2 + 1);
                    break;
                case 'v':
                    position = new Tuple<int, int>(position.Item1, position.Item2 - 1);
                    break;
            }

            return position;
        }
    }
}