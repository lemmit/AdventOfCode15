using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode13
{
    public abstract class AbstractTableArrangementFinder
    {
        protected Dictionary<GuestPair, RelationEntry> Relations
                    = new Dictionary<GuestPair, RelationEntry>();
        protected List<string> Guests = new List<string>();
        public AbstractTableArrangementFinder(List<RelationEntry> inputEntries)
        {
            Relations = inputEntries.ToDictionary((item) => item.GuestPair);
            Guests = GetGuests(inputEntries);
        }
        protected List<string> GetGuests(List<RelationEntry> relations)
        {
            var guardValue = 1;
            var dict = new Dictionary<string, int>();
            foreach (var relation in relations)
            {
                dict[relation.GuestPair.FirstGuest] = guardValue;
            }
            return dict.Keys.ToList();
        }
        protected int CountTwoWayHappinessModifier(string lastGuest, string guest)
        {
            try
            {
                var relationAtoB = Relations[new GuestPair(lastGuest, guest)];
                var relationBtoA = Relations[new GuestPair(guest, lastGuest)];
                return (relationAtoB.ValueOfRelation * (int)relationAtoB.Relation)
                    + (relationBtoA.ValueOfRelation * (int)relationBtoA.Relation);
            }
            catch (Exception e)
            { }
            return 0;
        }
        public abstract int Arrangement();
    }
}
