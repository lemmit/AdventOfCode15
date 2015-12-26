using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode3
{
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
