using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    public class FussionFissionPlant
    {
        private Dictionary<string, int> Cache;
        private int min;
        private int depth;
        private List<Tuple<string, string>> replacements;
        private bool found;
        public FussionFissionPlant(List<Tuple<string, string>> replacementsList)
        {
            replacements = replacementsList;
        }

        public int Shorten(string molecule)
        {
            Cache = new Dictionary<string, int>();
            HSCache = new HashSet<string>();
            min = int.MaxValue;
            depth = int.MaxValue;
            found = false;
            Shorten(molecule, replacements, 0);
            return depth;
        }
        private HashSet<string> HSCache;
        private void Shorten(string molecule, List<Tuple<string, string>> replacements, int searchDepth)
        {
            if (found) { return; }

            var possibleCreatedMolecules = molecule.Replacements(replacements).Distinct().Except(HSCache).ToList();
            HSCache.UnionWith(possibleCreatedMolecules);
            
            if (possibleCreatedMolecules.Count() == 0)
            {
                min = Math.Min(molecule.Length, min);
                if (molecule.Length == 1)
                {
                    this.depth = searchDepth;
                    //System.Console.WriteLine($"FOUND! {searchDepth}");
                    found = true;
                    return;
                }
            }
            foreach (var pos in possibleCreatedMolecules)
            {
                var newR = replacements.ToList();
                newR.Shuffle();
                Shorten(pos, newR, searchDepth + 1);
            }
        }

    }
}
