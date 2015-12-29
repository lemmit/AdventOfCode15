using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode23
{
    public class Processor
    {
        uint[] registers;
        IInstruction[] instructions;
        public Processor(IInstruction[] lines)
        {
            instructions = lines;
            registers = new uint[2];
        }

        private int IP { get; set; }

        public void SetRegister(int index, uint value)
        {
            registers[index] = value;
        }

        private bool ExecuteStep()
        {
            var currentInstruction = instructions[IP];
            //System.Console.WriteLine($"Executing {currentInstruction.GetType()}");
            registers[currentInstruction.Destination()] = currentInstruction.Calculate(registers);
            IP = currentInstruction.NextInstruction(IP);
            if (IP < instructions.Length)
            {
                return true;
            }
            return false;
        }

        public uint Execute()
        {
            while (ExecuteStep())
            {
               // System.Console.Write($"IP {IP} | Reg(");
                foreach (var reg in registers)
                {
               //     System.Console.Write($"{reg}, ");
                }
               // System.Console.WriteLine($")");
            }
            return registers[1];
        }
    }
}
