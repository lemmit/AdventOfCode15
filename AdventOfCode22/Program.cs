using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    class Program
    {
        static void Main(string[] args)
        {
            var instanousSpells = new List<Cast>() {
                new Cast("Magic Missile",   53, 4, 0, 0, 0, 0),
                new Cast("Drain",           73, 2, 0, 0, 2, 0)
            };

            var effects = new List<Cast>() {
                new Cast("Poison",          173, 3, 0, 6, 0, 0),
                new Cast("Shield",          113, 0, 7, 6, 0, 0),
                new Cast("Recharge",        229, 0, 0, 5, 0, 101)
            };
            var player = new Player
            {
                HitPoints = 50,
                ManaPoints = 500
            };
            var boss = new Player
            {
                HitPoints = 58,
                Damage = 9
            };

            /*var player = new Player
            {
                HitPoints = 10,
                ManaPoints = 250
            };
            var boss = new Player
            {
                HitPoints = 14,
                Damage = 8
            };*/

            var mana = new Simulation(player, boss, instanousSpells, effects)
                .LeastAmountOfMana;
            System.Console.WriteLine(mana);

            mana = new Simulation(player, boss, instanousSpells, effects, levelHard: true)
                .LeastAmountOfMana;
            System.Console.WriteLine(mana);

            System.Console.ReadLine();
        }

    }
}
