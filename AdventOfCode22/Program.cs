using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    class 
        Cast : ICloneable, IEquatable<Cast>
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int NumberOfTurns { get; set; }
        public int Healing { get; set; }
        public int ManaRecharge { get; set; }
        public Cast(string name, int cost, int damage, int armor, int numberOfTurns=0, int healing=0, int manaRecharge = 0)
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
            return new Cast(Name, Cost, Damage,Armor, NumberOfTurns, Healing, ManaRecharge);
        }

        public bool Equals(Cast other)
        {
            if (other.Name == Name)
                return true;
            return false;
        }
    }

    class Player : ICloneable
    {
        public int HitPoints { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int ManaPoints { get; set; }
        public object Clone()
        {
            return new Player {
                HitPoints = HitPoints,
                Damage = Damage,
                Armor = Armor,
                ManaPoints =  ManaPoints
            };
        }
    }
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

            SimulateFight(instanousSpells, effects);
            System.Console.ReadLine();
        }

        static private void SimulateFight(List<Cast> spells, List<Cast> effects)
        {
            
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

            AddToQueue(spells, effects, player, boss, new List<Cast>());
            while (RunFromQueue()) ;
        }
        static private Queue<Tuple<List<Cast>, List<Cast>, Player, Player, List<Cast>>> SimulationQueue
            = new Queue<Tuple<List<Cast>, List<Cast>, Player, Player, List<Cast>>>();
        static private void AddToQueue(
            List<Cast> spells,
            List<Cast> effects,
            Player player,
            Player boss,
            List<Cast> casted
            )
        {
            SimulationQueue.Enqueue(
                new Tuple<List<Cast>, List<Cast>, Player, Player, List<Cast>>(
                    spells, effects, player, boss, casted
                    ));
        }
        static private bool RunFromQueue()
        {
            if(SimulationQueue.Count<=0)
                return false;
            var first = SimulationQueue.Dequeue();

            //debug
           // first.Item5.ForEach(c => System.Console.Write(c.Name + ","));
           // System.Console.WriteLine();

            SimulateFight(first.Item1, first.Item2, first.Item3, first.Item4, first.Item5);
            return true;
        }
        //static private int min = int.MaxValue;
        static private void SimulateFight(
            List<Cast> spells, 
            List<Cast> effects,
            Player player,
            Player boss,
            List<Cast> casted
            )
        {
            //player round
            player.Armor = 0;

            //PART 2
            player.HitPoints--;
            if (CheckPlayerDeath(player))
            {
                return;
            }
            //END OF MOD FOR PART 2

            //effects works
            var workingEffects = casted.Where(effect => effect.NumberOfTurns > 0);
            foreach (var effect in workingEffects)
            {
                player.ManaPoints += effect.ManaRecharge;
                player.HitPoints += effect.Healing;
                player.Armor += effect.Armor;
                boss.HitPoints -= effect.Damage;
                effect.NumberOfTurns--;
            }
            //check deaths
            if(CheckBossDeath(boss, casted)) { return; }

            //player spells
            var possibleSpells = spells.Concat(effects.Except(workingEffects, new EffectsComparer()))
                .Where(spell => spell.Cost <= player.ManaPoints)
                .ToList().Clone();

            //if player can cast fatal spell...
            if(spells.Any( spell => spell.Damage >= boss.HitPoints && player.ManaPoints >= spell.Cost ))
            {
                //System.Console.WriteLine("Fatality.");
                possibleSpells = spells
                    .Where(spell => spell.Damage >= boss.HitPoints && player.ManaPoints >= spell.Cost)
                    .ToList()
                    .Clone();
            }
            
            var canCastAnything = possibleSpells.Any(spell => spell.Cost <= player.ManaPoints);
            if (!canCastAnything)
            {
                //player failed
                return; 
            }

            foreach (var possibleSpell in possibleSpells)
            {
                var playerCopy = (Player)player.Clone();
                var bossCopy = (Player)boss.Clone();
                var castedAfterPlayersTurn = casted.Clone().ToList();

                playerCopy.ManaPoints -= possibleSpell.Cost;
                if (spells.Contains(possibleSpell))
                {
                    //instant 
                    playerCopy.HitPoints += possibleSpell.Healing;
                    var playerHit = possibleSpell.Damage;
                    bossCopy.HitPoints -= playerHit;
                }
                castedAfterPlayersTurn.Add((Cast)possibleSpell.Clone());

                playerCopy.Armor = 0;
                //boss round
                //effects works
                workingEffects = castedAfterPlayersTurn.Where(effect => effect.NumberOfTurns > 0);
                foreach (var effect in workingEffects)
                {
                    playerCopy.ManaPoints += effect.ManaRecharge;
                    playerCopy.HitPoints += effect.Healing;
                    playerCopy.Armor += effect.Armor;
                    bossCopy.HitPoints -= effect.Damage;
                    effect.NumberOfTurns--;
                }
                if (CheckBossDeath(bossCopy, castedAfterPlayersTurn))
                {
                    return;
                }
                //boss atk
                var bossHit = bossCopy.Damage - playerCopy.Armor;
                playerCopy.HitPoints -= Math.Max(bossHit, 1);

                if (CheckPlayerDeath(playerCopy))
                {
                    continue;
                }
                else
                {
                    //shuffle?
                    //possibleSpells.Shuffle();
                    var newSpells = spells;//.Clone().ToList().Shuffled().ToList();
                    var newEffects = effects;//.Clone().ToList().Shuffled().ToList();
                    AddToQueue(newSpells, newEffects, playerCopy, bossCopy, castedAfterPlayersTurn);
                }
            }
        }
        private static bool CheckPlayerDeath(Player player)
        {
            if(player.HitPoints <= 0)
            {
                return true;
            }
            return false;
        }

        private static int min = int.MaxValue;
        private static bool CheckBossDeath(Player boss, List<Cast> casted)
        {
            if (boss.HitPoints <= 0)
            {
                //player win
                var burnedMana = casted.Sum(spell => spell.Cost);
                if (burnedMana < min)
                {
                    min = burnedMana;
                    System.Console.WriteLine($"New min found: {min}");
                    casted.ForEach(c => System.Console.Write(c.Name + ","));
                    System.Console.WriteLine();
                }
                return true;
            }
            return false;
        }

        private class EffectsComparer : IEqualityComparer<Cast>
        {
            public bool Equals(Cast x, Cast y)
            {
                return x.Name == y.Name;
            }

            public int GetHashCode(Cast obj)
            {
                return obj.Name.GetHashCode();
            }
        }
    }
}
