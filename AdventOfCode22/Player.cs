using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Player : ICloneable
    {
        public int HitPoints { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int ManaPoints { get; set; }
        public object Clone()
        {
            return new Player
            {
                HitPoints = HitPoints,
                Damage = Damage,
                Armor = Armor,
                ManaPoints = ManaPoints
            };
        }
    }
}
