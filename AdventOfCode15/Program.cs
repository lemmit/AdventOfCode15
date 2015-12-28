using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Toolkit;

namespace AdventOfCode15
{
    public class Test
    {
        public static Dictionary<Ingredient, int> Reciept(List<Ingredient> ingredients, List<int> amounts)
        {
            var reciept = new Dictionary<Ingredient, int>();
            for (var i = 0; i < ingredients.Count; i++)
            {
                reciept[ingredients[i]] = amounts[i];
            }
            return reciept;
        }

        public static void Main()
        {
            List<Ingredient> ingredients = PossibleIngredients();
            var ingredientsCount = ingredients.Count;
            var sequenceGenerator = new SequenceGenerator(ingredientsCount);
            var minMaxScoredSequence = sequenceGenerator.GetSequences().MinMaxElement(
                sequence =>
                {
                    var reciept = Reciept(ingredients, sequence);
                    /*Part 2*/
                    if (CalculateCalories(reciept) != 500) {
                        return 0;
                    }
                    return CalculateTotalScore(reciept);
                });
            var winningReciept = Reciept(ingredients, minMaxScoredSequence.Item2);
            var maxScore = CalculateTotalScore(winningReciept);

            System.Console.WriteLine($"\nMaximal total score: {maxScore}");
            winningReciept.Select(elem => $"{elem.Key.Name}: {elem.Value}").ToList().PrintStringList();
            
            System.Console.ReadLine();

        }

        private static List<Ingredient> PossibleIngredients()
        {
            return new List<Ingredient>(){
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
        }

        static int CalculateCalories(Dictionary<Ingredient, int> reciept)
        {
            return reciept.ToList().Select(rec => rec.Key.Calories * rec.Value).Sum();
        }

        static int CalculateTotalScore(Dictionary<Ingredient, int> reciept)
        {
            var tempIngredient = new Ingredient();

            var capacitySum = reciept.Select(rec => rec.Key.Capacity * rec.Value).Sum();
            capacitySum = capacitySum > 0 ? capacitySum : 0;

            var durabilitySum = reciept.Select(rec => rec.Key.Durability * rec.Value).Sum();
            durabilitySum = durabilitySum > 0 ? durabilitySum : 0;

            var flavourSum = reciept.Select(rec => rec.Key.Flavor * rec.Value).Sum();
            flavourSum = flavourSum > 0 ? flavourSum : 0;

            var textureSum = reciept.Select(rec => rec.Key.Texture * rec.Value).Sum();
            textureSum = textureSum > 0 ? textureSum : 0;

            return capacitySum * durabilitySum * flavourSum * textureSum;
        }
    }
}
