using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Toolkit;
using System.IO;

namespace AdventOfCode23
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("../../input.txt"))
            {
                var instructions = sr.ForEachLine(line =>
                    InstructionFactory.FromString(line)
                    ).ToArray();
                var proc = new Processor(instructions);

                // PART 2
                proc.SetRegister(0, 1);

                var value = proc.Execute();
                System.Console.WriteLine(value);
                System.Console.ReadLine();
            }
        }

    }
}
