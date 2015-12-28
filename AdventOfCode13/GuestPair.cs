using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode13
{
    public class GuestPair : Tuple<string, string>
    {
        public string FirstGuest { get { return Item1; } }
        public string SecondGuest { get { return Item2; }}
        public GuestPair(string firstGuest, string secondGuest) : base(firstGuest, secondGuest)
        {
        }
    }
}
