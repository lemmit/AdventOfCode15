using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode3
{
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
