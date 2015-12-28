using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode13
{
    public class MaxHappinessUsingRecurenceTableArrangementFinder : AbstractTableArrangementFinder
    {
        public MaxHappinessUsingRecurenceTableArrangementFinder(List<RelationEntry> inputEntries) 
            : base(inputEntries) { }

        int BestPossibleArrangement(List<string> guests, string firstGuest = null, string lastGuest = null)
        {
            int bestArrangement = int.MinValue;
            if (guests.Count > 0)
            {
                foreach (var guest in guests)
                {
                   var listExceptCurrent = guests
                                                .Except(new List<string> { guest })
                                                .ToList();
                    int possibleArrangement = int.MinValue;
                    if (firstGuest == null)
                    {
                        possibleArrangement = BestPossibleArrangement(listExceptCurrent, guest, guest);
                    }
                    else
                    {
                        possibleArrangement = CountTwoWayHappinessModifier(lastGuest, guest) +
                                             +BestPossibleArrangement(listExceptCurrent, firstGuest, guest);
                    }
                    if (bestArrangement <= possibleArrangement)
                    {
                        //Found better
                        bestArrangement = possibleArrangement;
                    }
                }
            }
            else
            {
                //way to first guest
                bestArrangement = CountTwoWayHappinessModifier(lastGuest, firstGuest);
            }

            return bestArrangement;
        }
        public override int Arrangement()
        {
            return BestPossibleArrangement(Guests);
        }
    }
}
