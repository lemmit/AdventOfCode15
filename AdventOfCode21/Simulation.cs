using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode21
{
    public enum Winner{
        BOSS,
        PLAYER
    }

    public class Simulation
    {
        Item weapon;
        Item armor; 
        Item firstRing;
        Item secondRing;
        Player player;
        Player boss;

        public Winner Winner { get
            {
                return Simulate() ? Winner.PLAYER : Winner.BOSS;
            }
        }

        public Simulation(Tuple<Item, Item, Item, Item> items) : this(items.Item1, items.Item2, items.Item3, items.Item4)
        {
        }

        public Simulation(Item weapon, Item armor, Item firstRing, Item secondRing)
        {
            this.weapon = weapon;
            this.armor = armor;
            this.firstRing = firstRing;
            this.secondRing = secondRing;
            player = new Player
            {
                HitPoints = 100,
                Damage = weapon.Damage + armor.Damage + firstRing.Damage + secondRing.Damage,
                Armor = weapon.Armor + armor.Armor + firstRing.Armor + secondRing.Armor
            };
            boss = new Player
            {
                HitPoints = 104,
                Damage = 8,
                Armor = 1
            };
        }

        private bool Simulate()
        {
            var playing = true;
            int round = 0;
            while (playing)
            {
                round++;
                //System.Console.WriteLine($"ROUND {round}");
                if (player.HitPoints > 0)
                {
                    var playerHit = player.Damage - boss.Armor;
                    var takenHP = Math.Max(1, playerHit);
                    boss.HitPoints -= takenHP;
                    // System.Console.WriteLine($"Player deals {takenHP}, boss is now {boss.HitPoints}");
                }
                else
                {
                    //last turn was fatal for our player
                    return false;
                }

                if (boss.HitPoints > 0)
                {
                    var bossHit = boss.Damage - player.Armor;
                    var takenHP = Math.Max(1, bossHit);
                    player.HitPoints -= Math.Max(1, bossHit);
                    // System.Console.WriteLine($"Boss deals {takenHP}, player is now {player.HitPoints}");
                }
                else
                {
                    //boss is dead
                    return true;
                }
            }
            return false;
        }
    }
}
