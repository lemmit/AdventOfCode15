using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode3
{
    class SantaPositionWithPath
    {
        public Tuple<int, int> Position { get; set; } = new Tuple<int, int>(0, 0);
        public Dictionary<Tuple<int, int>, int> Path { get; set; } = new Dictionary<Tuple<int, int>, int>();
    };

    enum Turn
    {
        Santa,
        Robot
    }

     class SantaAndRobotPositionWithPathsAndTurnInfo
    {
        public Tuple<int, int> SantaPosition { get; set; } = new Tuple<int, int>(0, 0);
        public Tuple<int, int> RobotPosition { get; set; } = new Tuple<int, int>(0, 0);
        public Dictionary<Tuple<int, int>, int> Path { get; set; } = new Dictionary<Tuple<int, int>, int>();
        public Turn Turn { get; set; }
        public void NextTurn()
        {
            if (Turn == Turn.Robot) Turn = Turn.Santa;
            else Turn = Turn.Robot;
        }
    }
    public class FunctionalSantaGridWalker : AbstractSantaGridWalker
    {
        private string input;

        public FunctionalSantaGridWalker(string input)
        {
            this.input = input;
        }

        public override int VisitedHousesIfWalkAlone()
        {
            var sentinel = 1;
            return input.Fold(new SantaPositionWithPath(), (c, s) => {
                s.Path[s.Position] = sentinel;
                s.Position = NextPosition(s.Position, c);
                return s;
            }).Path.Keys.Count();
        }

        public override int VisitedHousesIfWalkWithRobot()
        {
            var sentinel = 1;
            return input.Fold(new SantaAndRobotPositionWithPathsAndTurnInfo(), (c, s) => {
                if (s.Turn == Turn.Robot)
                {
                    s.RobotPosition = NextPosition(s.RobotPosition, c);
                    s.Path[s.RobotPosition] = sentinel;
                }
                else
                {
                    s.SantaPosition = NextPosition(s.SantaPosition, c);
                    s.Path[s.SantaPosition] = sentinel;
                }
                s.NextTurn();
                return s;
            }).Path.Keys.Count();
        }
        
    }
}
