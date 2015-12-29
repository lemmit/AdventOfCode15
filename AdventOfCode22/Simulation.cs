using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode22
{
    public class Simulation
    {
        private List<Cast> effects;
        private List<Cast> spells;
        private Player player;
        private Player boss;
        private int mana = 0;
        private bool levelHard;
        private Queue<Tuple<Player, Player, IList<Cast>>> SimulationQueue
            = new Queue<Tuple<Player, Player, IList<Cast>>>();

        public int LeastAmountOfMana
        {
            get
            {
                mana = int.MaxValue;
                SimulateFight();
                return mana;
            }
        }
        public Simulation(
            Player player, 
            Player boss, 
            List<Cast> instanousSpells, 
            List<Cast> effects,
            bool levelHard = false
            )
        {
            this.player = player;
            this.boss = boss;
            this.spells = instanousSpells;
            this.effects = effects;
            this.levelHard = levelHard;
        }
        void SimulateFight()
        {
            AddToQueue(player, boss, new List<Cast>());
            while (RunFromQueue()) ;
        }
        private void AddToQueue(
            Player player,
            Player boss,
            IList<Cast> casted
            )
        {
            SimulationQueue.Enqueue(
                new Tuple<Player, Player, IList<Cast>>(
                    player, boss, casted
                    ));
        }

        private bool RunFromQueue()
        {
            if (SimulationQueue.Count <= 0)
            {
                return false;
            }
            var first = SimulationQueue.Dequeue();

            //debug
            //first.Item3.ToList().ForEach(c => System.Console.Write(c.Name + ","));
            //System.Console.WriteLine();


            SimulateFight(first.Item1, first.Item2, first.Item3);
            return true;
        }
        //static private int min = int.MaxValue;
        private void SimulateFight(
            Player player,
            Player boss,
            IList<Cast> casted
            )
        {

            var burnedMana = casted.Sum(spell => spell.Cost);
            if(burnedMana > mana)
            {
                //we have already found better solution than that
                return;
            }

            //player round
            player.Armor = 0;

            //PART 2
            if (levelHard)
            {
                player.HitPoints--;
            }
            //END OF MOD FOR PART 2

            if (CheckPlayerDeath(player))
            {
                return;
            }
            
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
            if (CheckBossDeath(boss, casted)) { return; }

            //player spells
            var possibleSpells = spells.Concat(effects.Except(workingEffects, new EffectsComparer()))
                .Where(spell => spell.Cost <= player.ManaPoints)
                .ToList().Clone();

            //if player can cast fatal spell...
            if (spells.Any(spell => spell.Damage >= boss.HitPoints && player.ManaPoints >= spell.Cost))
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
                    AddToQueue(playerCopy, bossCopy, castedAfterPlayersTurn);
                }
            }
        }
        bool CheckPlayerDeath(Player player)
        {
            if (player.HitPoints <= 0)
            {
                return true;
            }
            return false;
        }

        bool CheckBossDeath(Player boss, IList<Cast> casted)
        {
            if (boss.HitPoints <= 0)
            {
                //player win
                var burnedMana = casted.Sum(spell => spell.Cost);
                if (burnedMana < mana)
                {
                    mana = burnedMana;
                    /*
                    System.Console.WriteLine($"New min found: {mana}");
                    casted.ToList().ForEach(c => System.Console.Write(c.Name + ","));
                    System.Console.WriteLine();
                    */
                }
                return true;
            }
            return false;
        }
    }
}