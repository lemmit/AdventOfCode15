using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class EffectsComparer : IEqualityComparer<Cast>
    {
        public bool Equals(Cast x, Cast y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Cast obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
