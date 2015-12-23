using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Toolkit;
using System.IO;

namespace AdventOfCode23
{
    interface Instruction
    {
        int Destination();
        uint Calculate(uint[] registers);
        int NextInstruction(int IP);
    }
    
    class Processor
    {
        uint[] registers;
        Instruction[] instructions;
        public Processor(Instruction[] lines)
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
            System.Console.WriteLine($"Executing {currentInstruction.GetType()}");
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
                System.Console.Write($"IP {IP} | Reg(");
                foreach(var reg in registers)
                {
                    System.Console.Write($"{reg}, ");
                }
                System.Console.WriteLine($")");
            }
            return registers[1];
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("../../input.txt"))
            {
                var instructions = new List<Instruction>();
                sr.ForEachLine(line => {

                    var reg = line.Substring(4);
                    if (line.Contains("hlf"))
                    {
                        var index = reg[0] - 'a';
                        instructions.Add(new HalfInstruction(index));
                        return;
                    }
                    if (line.Contains("tpl"))
                    {
                        var index = reg[0] - 'a';
                        instructions.Add(new TripleInstruction(index));
                        return;
                    }
                    if (line.Contains("inc"))
                    {
                        var index = reg[0] - 'a';
                        instructions.Add(new IncrementInstruction(index));
                        return;
                    }
                    if (line.Contains("jmp"))
                    {
                        var jumpOffset = int.Parse(reg);
                        instructions.Add(new UnconditionalRelativeJumpInstruction(jumpOffset));
                        return;
                    }
                    if (line.Contains("jie"))
                    {
                        var splitted = reg.Split(',');
                        var registerIndex = splitted[0][0] - 'a';
                        var jumpOffset = int.Parse(splitted[1]);
                        instructions.Add(new RelativeJumpIfEvenInstruction(registerIndex, jumpOffset));
                        return;
                    }
                    if (line.Contains("jio"))
                    {
                        var splitted = reg.Split(',');
                        var registerIndex = splitted[0][0] - 'a';
                        var jumpOffset = int.Parse(splitted[1]);
                        instructions.Add(new RelativeJumpIfOneInstruction(registerIndex, jumpOffset));
                        return;
                    }
                });
                var proc = new Processor(instructions.ToArray());
                proc.SetRegister(0, 1);// PART 2
                var value = proc.Execute();
                System.Console.WriteLine(value);
                
                System.Console.ReadLine();
            }
        }

        private class IncrementInstruction : Instruction
        {
            private int index;

            public IncrementInstruction(int index)
            {
                this.index = index;
            }

            public uint Calculate(uint[] registers)
            {
                return registers[index]+1;
            }

            public int Destination() => index;

            public int NextInstruction(int IP)
            {
                return IP + 1;
            }
        }

        private class RelativeJumpIfEvenInstruction : Instruction
        {
            private int jumpOffset;
            private int registerIndex;
            private int calculatedJumpOffset;
            public RelativeJumpIfEvenInstruction(int registerIndex, int jumpOffset)
            {
                this.registerIndex = registerIndex;
                this.jumpOffset = jumpOffset;
            }

            public uint Calculate(uint[] registers)
            {
                if (registers[registerIndex] % 2 == 0)
                {
                    calculatedJumpOffset = jumpOffset;
                }
                else
                {
                    calculatedJumpOffset = 1;
                }
                return registers[registerIndex];
            }

            public int Destination() => registerIndex;

            public int NextInstruction(int IP)
            {
                
                return IP + calculatedJumpOffset;
            }
        }

        private class RelativeJumpIfOneInstruction : Instruction
        {
            private int jumpOffset;
            private int registerIndex;
            private int calculatedJumpOffset;
            public RelativeJumpIfOneInstruction(int registerIndex, int jumpOffset)
            {
                this.registerIndex = registerIndex;
                this.jumpOffset = jumpOffset;
            }

            public uint Calculate(uint[] registers)
            {
                if (registers[registerIndex] == 1)
                {
                    calculatedJumpOffset = jumpOffset;
                }
                else
                {
                    calculatedJumpOffset = 1;
                }
                return registers[registerIndex];
            }

            public int Destination() => registerIndex;

            public int NextInstruction(int IP)
            {

                return IP + calculatedJumpOffset;
            }
        }

        private class HalfInstruction : Instruction
        {
            int _destRegister = 0;
            public HalfInstruction(int register)
            {
                _destRegister = register;
            }

            public uint Calculate(uint[] registers)
            {
                return registers[_destRegister] / 2;
            }

            public int Destination() => _destRegister;

            public int NextInstruction(int IP)
            {
                return IP + 1;
            }
        }

        private class TripleInstruction : Instruction
        {
            private int index;

            public TripleInstruction(int index)
            {
                this.index = index;
            }

            public uint Calculate(uint[] registers)
            {
                return registers[index] * 3;
            }

            public int Destination()
            {
                return index;
            }

            public int NextInstruction(int IP)
            {
                return IP + 1;
            }
        }

        private class UnconditionalRelativeJumpInstruction : Instruction
        {
            private int jumpOffset;

            public UnconditionalRelativeJumpInstruction(int jumpOffset)
            {
                this.jumpOffset = jumpOffset;
            }

            public uint Calculate(uint[] registers)
            {
                return registers[0];
            }

            public int Destination() => 0;

            public int NextInstruction(int IP)
            {
                return IP + jumpOffset;
            }
        }
    }
}
