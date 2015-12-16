using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode15
{
    public class Ingredient
    {
        public string Name { get; set; }
        public int Capacity { get; set; } = 0;
        public int Durability { get; set; } = 0;
        public int Flavor { get; set; } = 0;
        public int Texture { get; set; } = 0;
        public int Calories { get; set; } = 0;
    };

    public class SequenceGenerator
    {
        private readonly int SequenceLength;
        private readonly int MaxValue;
        private readonly int MinValue;
        private readonly int IncrementStep;
        private readonly int SequenceMaxSum;
        public SequenceGenerator(int sequenceLength,
                                int minValue = 0,
                                int maxValue = 100,
                                int incrementStep = 1,
                                int sequenceMaxSum = 100)
        {
            SequenceLength = sequenceLength;
            MinValue = minValue;
            MaxValue = maxValue;
            IncrementStep = incrementStep;
            SequenceMaxSum = sequenceMaxSum;
        }
        public IEnumerable<List<int>> GetSequences()
        {
            var list = new List<int>();
            Enumerable.Range(0, SequenceLength).ToList().ForEach((_) => list.Add(MinValue));
            var nrOfTries = Math.Pow(MaxValue - MinValue, SequenceLength);
            for (int i = 0; i < nrOfTries; i++)
            {
                try
                {
                    list = IncrementList(list, MaxValue);
                }
                catch (Exception e)
                {
                    //end of sequence
                    yield break;
                }

                var sum = list.Sum();
                if (sum > SequenceMaxSum)
                {
                    continue;
                }

                var outList = new List<int>(list);
                yield return outList;
            }
        }

        public List<int> IncrementList(List<int> list, int maxValue)
        {
            bool carry = true;
            int pos = 0;
            while (carry)
            {
                list[pos] += 1;
                if (list[pos] > maxValue)
                {
                    list[pos] -= maxValue;
                    pos++;
                }
                else carry = false;
                if (pos > list.Count - 1)
                    throw new InvalidOperationException();
            }
            return list;
        }
    }

    public class Test
    {

        public static void Main()
        {
            var ingredients = new List<Ingredient>(){
            new Ingredient{
                Name = "Frosting",
                Capacity = 4,
                Durability = -2,
                Flavor = 0,
                Texture = 0,
                Calories = 5
            },
            new Ingredient{
                Name = "Candy",
                Capacity = 0,
                Durability = 5,
                Flavor = -1,
                Texture = 0,
                Calories = 8
            },
            new Ingredient{
                Name = "Butterscotch",
                Capacity = -1,
                Durability = 0,
                Flavor = 5,
                Texture = 0,
                Calories = 6
            },
            new Ingredient{
                Name = "Sugar",
                Capacity = 0,
                Durability = 0,
                Flavor = -2,
                Texture = 2,
                Calories = 1
            }
        };
            var ingredientsCount = ingredients.Count;
            var sequenceGenerator = new SequenceGenerator(ingredientsCount);
            var maxScore = 0;
            List<Tuple<int, Ingredient>> winningReciept = null;

            var perfCount = 0;
            foreach (var sequence in sequenceGenerator.GetSequences())
            {
                perfCount++;
                var reciept = new List<Tuple<int, Ingredient>>();
                for (var i = 0; i < ingredientsCount; i++)
                {
                    reciept.Add(new Tuple<int, Ingredient>(sequence[i], ingredients[i]));
                }
                var calories = CalculateCalories(reciept);
                if (calories != 500)
                {
                    continue;
                }
                var totalScore = CalculateTotalScore(reciept);
                if (maxScore < totalScore)
                {
                    maxScore = totalScore;
                    winningReciept = reciept;
                }
                if (perfCount % (100 * 100) == 0)
                {
                    System.Console.Write(".");
                }

            }

            System.Console.WriteLine($"\nMaximal total score: {maxScore}");
            foreach (var amountAndIngredient in winningReciept)
            {
                System.Console.WriteLine($"{amountAndIngredient.Item2.Name}: {amountAndIngredient.Item1}");
            }
            System.Console.ReadLine();

        }

        static int CalculateCalories(List<Tuple<int, Ingredient>> reciept)
        {
            return reciept.Sum((amountAndIngredient) => amountAndIngredient.Item1 * amountAndIngredient.Item2.Calories);
        }

        static int CalculateTotalScore(List<Tuple<int, Ingredient>> reciept)
        {
            var tempIngredient = new Ingredient();
            foreach (var amountAndIngredient in reciept)
            {
                var amount = amountAndIngredient.Item1;
                var ingredient = amountAndIngredient.Item2;
                tempIngredient.Capacity += amount * ingredient.Capacity;
                tempIngredient.Durability += amount * ingredient.Durability;
                tempIngredient.Flavor += amount * ingredient.Flavor;
                tempIngredient.Texture += amount * ingredient.Texture;
            }
            if (tempIngredient.Capacity <= 0
                || tempIngredient.Durability <= 0
                || tempIngredient.Flavor <= 0
                || tempIngredient.Texture <= 0
            )
                return 0;
            else
                return tempIngredient.Capacity
                       * tempIngredient.Durability
                       * tempIngredient.Flavor
                       * tempIngredient.Texture;
        }
    }
}
