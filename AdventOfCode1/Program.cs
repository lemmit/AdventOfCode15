using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode1
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("../../input.txt"))
            {
                var input = sr.ReadLine();

                System.Console.WriteLine("** First solver");
                IElevator solver = new Elevator_Functional(input);
                SolveChallenge(solver);

                System.Console.WriteLine("\n** Second(functional) solver");
                solver = new Elevator(input);
                SolveChallenge(solver);
            }
            System.Console.ReadLine();
        }

        private static void SolveChallenge(IElevator solver)
        {
            var final = solver.FinalFloor();
            var gotToBasement = solver.WhenGotToBasement();
            System.Console.WriteLine($"Final floor: {final}");
            System.Console.WriteLine($"First time in basement: {gotToBasement}");
        }
    }
}
