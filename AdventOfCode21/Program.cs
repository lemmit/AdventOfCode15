using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode21
{
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

            /* FIRST PART */
            var minMax = weapons
                .Product(armors, rings, rings)
                .Where((items) => new Simulation(items).Winner == Winner.PLAYER)
                .MinMaxElement((items) => ItemsCost(items));
            
            PrintItems(minMax.Item1);
            System.Console.WriteLine($"Minimum: {ItemsCost(minMax.Item1)}");

            /* SECOND PART */
            minMax = weapons
                .Product(armors, rings, rings)
                .Where((items) => new Simulation(items).Winner == Winner.BOSS)
                .MinMaxElement((items) => ItemsCost(items));
            PrintItems(minMax.Item2);
            System.Console.WriteLine($"Maximum: {ItemsCost(minMax.Item2)}");

            System.Console.ReadLine();
        }

        static void PrintItems(Tuple<Item, Item, Item, Item> items) {
            System.Console.WriteLine($"{items.Item1.Name} {items.Item2.Name} {items.Item3.Name} {items.Item4.Name}");
        }

        static int ItemsCost(Tuple<Item, Item, Item, Item> items)
        {
            return items.Item1.Cost + items.Item2.Cost
                    + items.Item3.Cost + items.Item4.Cost;
        }
    }
}
