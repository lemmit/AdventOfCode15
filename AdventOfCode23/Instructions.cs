using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode23
{
    
        public class IncrementInstruction : IInstruction
        {
            int index;

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

        public class RelativeJumpIfEvenInstruction : IInstruction
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

        public class RelativeJumpIfOneInstruction : IInstruction
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

        public class HalfInstruction : IInstruction
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

        public class TripleInstruction : IInstruction
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

        public class UnconditionalRelativeJumpInstruction : IInstruction
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
