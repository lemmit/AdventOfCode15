using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode3
{

    public class SantaPositionWithPath
    {
        public Tuple<int, int> Position { get; set; } = new Tuple<int, int>(0, 0);
        public Dictionary<Tuple<int, int>, int> Path { get; set; } = new Dictionary<Tuple<int, int>, int>();
    };

    public enum Turn
    {
        Santa,
        Robot
    }

    public class SantaAndRobotPositionWithPathsAndTurnInfo
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

    class Program
    {
        static void Main(string[] args)
        {
            using (var sw = new StreamReader("../../input.txt"))
            {
                var line = sw.ReadLine();
                System.Console.WriteLine("** Solution");

                AbstractSantaGridWalker walker = new SantaGridWalker(line);
                Solve(walker);
                System.Console.WriteLine("\n** Solution(functional)");
                walker = new FunctionalSantaGridWalker(line);
                Solve(walker);

            }
            System.Console.ReadLine();
        }

        private static void Solve(AbstractSantaGridWalker walker)
        {
            var alone = walker.VisitedHousesIfWalkAlone();
            var withRobot = walker.VisitedHousesIfWalkWithRobot();
            System.Console.WriteLine($"[Walks alone] {alone} houses received at least one present");
            System.Console.WriteLine($"[Walks with robot] {withRobot} houses received at least one present");
        }
    }
}
