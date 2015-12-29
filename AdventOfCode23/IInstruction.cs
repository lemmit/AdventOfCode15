using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode23
{
    public interface IInstruction
    {
        int Destination();
        uint Calculate(uint[] registers);
        int NextInstruction(int IP);
    }
}
