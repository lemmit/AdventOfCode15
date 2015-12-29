using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode23
{
    public class InstructionFactory
    {
        public static IInstruction FromString(string line)
        {
            var reg = line.Substring(4);
            if (line.Contains("hlf"))
            {
                var index = reg[0] - 'a';
                return new HalfInstruction(index);
            }
            if (line.Contains("tpl"))
            {
                var index = reg[0] - 'a';
                return new TripleInstruction(index);
            }
            if (line.Contains("inc"))
            {
                var index = reg[0] - 'a';
                return new IncrementInstruction(index);
            }
            if (line.Contains("jmp"))
            {
                var jumpOffset = int.Parse(reg);
                return new UnconditionalRelativeJumpInstruction(jumpOffset);
            }
            if (line.Contains("jie"))
            {
                var splitted = reg.Split(',');
                var registerIndex = splitted[0][0] - 'a';
                var jumpOffset = int.Parse(splitted[1]);
                return new RelativeJumpIfEvenInstruction(registerIndex, jumpOffset);
            }
            if (line.Contains("jio"))
            {
                var splitted = reg.Split(',');
                var registerIndex = splitted[0][0] - 'a';
                var jumpOffset = int.Parse(splitted[1]);
                return new RelativeJumpIfOneInstruction(registerIndex, jumpOffset);
            }
            throw new ArgumentException();
        }
    }
}
