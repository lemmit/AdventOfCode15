using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode21
{

    class Item
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public Item(string name, int cost, int damage, int armor)
        {
            Name = name;
            Cost = cost;
            Damage = damage;
            Armor = armor;
        }
    }

    class Player
    {
        public int HitPoints { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var weapons = new List<Item>() {
                new Item ("Dagger",      8,      4,      0),
                new Item ("Shortsword",  10,     5,      0),
                new Item ("Warhammer",   25,     6,      0),
                new Item ("Longsword",   40,     7,      0),
                new Item ("Greataxe",    74,     8,      0)
            };

            var armors = new List<Item>() {
                new Item ("None",       0,     0,       0),
                new Item ("Leather",      13,     0,       1),
                new Item ("Chainmail",    31,     0,       2),
                new Item ("Splintmail",   53,     0,       3),
                new Item ("Bandedmail",   75,     0,       4),
                new Item ("Platemail",   102,     0,       5)
            };

            var rings = new List<Item>() {
                new Item ("None",    0,     0,       0),
                new Item ("None",    0,     0,       0),
                new Item ("Damage + 1",    25,     1,       0),
                new Item ("Damage + 2",    50,     2,       0),
                new Item ("Damage + 3",   100,     3,       0),
                new Item ("Defense + 1",   20,     0,       1),
                new Item ("Defense + 2",   40,     0,       2),
                new Item ("Defense + 3",   80,     0,       3)
            };

            var min = int.MaxValue;
            var max = int.MinValue;
            weapons.ForEach(weapon =>
              armors.ForEach(armor =>
               rings.ForEach(firstRing =>
                rings.ForEach(secondRing =>
                {
                    if (firstRing == secondRing)
                    {
                        return;
                    }
                    var moneySum = weapon.Cost + armor.Cost + firstRing.Cost + secondRing.Cost;
                    if (Simulate(weapon, armor, firstRing, secondRing))
                    {
                        //fight won
                        if (min > moneySum)
                        {
                            min = moneySum;
                            System.Console.WriteLine($"New min: {weapon.Name}, {armor.Name}, {firstRing.Name}, {secondRing.Name} : {moneySum}");
                        }
                    }
                    else
                    {
                        //fight lost
                        if (max < moneySum)
                        {
                            max = moneySum;
                            System.Console.WriteLine($"New max: {weapon.Name}, {armor.Name}, {firstRing.Name}, {secondRing.Name} : {moneySum}");
                        }
                    }
                }))));

            System.Console.WriteLine($"Minimum: {min}");
            System.Console.WriteLine($"Maximum: {max}");
            System.Console.ReadLine();
        }

        private static bool Simulate(Item weapon, Item armor, Item firstRing, Item secondRing)
        { 
            var player = new Player {
                HitPoints = 100,
                Damage = weapon.Damage + armor.Damage + firstRing.Damage + secondRing.Damage,
                Armor = weapon.Armor + armor.Armor + firstRing.Armor + secondRing.Armor
            };
            var boss = new Player {
                HitPoints = 104,
                Damage = 8,
                Armor = 1
            };
            var playing = true;
            int round = 0;
            while(playing)
            {
                round++;
                //System.Console.WriteLine($"ROUND {round}");
                if(player.HitPoints > 0)
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
                else {
                    //boss is dead
                    return true;
                }
            }
            return false;
        }
    }
}
