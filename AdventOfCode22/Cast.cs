using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class
          Cast : ICloneable, IEquatable<Cast>
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int NumberOfTurns { get; set; }
        public int Healing { get; set; }
        public int ManaRecharge { get; set; }
        public Cast(string name, int cost, int damage, int armor, int numberOfTurns = 0, int healing = 0, int manaRecharge = 0)
        {
            Name = name;
            Cost = cost;
            Damage = damage;
            Armor = armor;
            NumberOfTurns = numberOfTurns;
            Healing = healing;
            ManaRecharge = manaRecharge;
        }
        public Cast(Cast cast)
        {
            Name = cast.Name;
            Cost = cast.Cost;
            Damage = cast.Damage;
            Armor = cast.Armor;
            NumberOfTurns = cast.NumberOfTurns;
            Healing = cast.Healing;
            ManaRecharge = cast.ManaRecharge;
        }

        public object Clone()
        {
            return new Cast(Name, Cost, Damage, Armor, NumberOfTurns, Healing, ManaRecharge);
        }

        public bool Equals(Cast other)
        {
            if (other.Name == Name)
                return true;
            return false;
        }
    }

}
