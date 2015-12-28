using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode13
{
    public class MaxHappinessTableArrangementFinder : AbstractTableArrangementFinder
    {
        public MaxHappinessTableArrangementFinder(List<RelationEntry> inputEntries) 
            : base(inputEntries) { }
        public override int Arrangement()
        {
            return Guests.Permutations().Select(perm =>
            {
                var permList = perm.ToList();
                var previousGuest = "";
                var firstGuest = "";
                var sum = 0;
                for (int i = 0; i < permList.Count; i++)
                {
                    var guest = permList[i];
                    if (i == 0)
                    {
                        firstGuest = guest;
                        previousGuest = firstGuest;
                    }
                    else if (i == permList.Count - 1)
                    {
                        sum += CountTwoWayHappinessModifier(guest, firstGuest);
                    }
                    else
                    {
                        sum += CountTwoWayHappinessModifier(previousGuest, guest);
                        previousGuest = guest;
                    }
                }
                return sum;
            }).Max();
        }
    }
}
