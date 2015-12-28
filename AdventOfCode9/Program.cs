using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode9
{
    class Program
    {
        static Dictionary<CityPair, int> Distances = new Dictionary<CityPair, int>(new CityPairEqualityComparer());
        static List<string> Cities;

        static void Main(string[] args)
        {
            var data = ReadDistances();
            LoadDataToDistancesDictionary(data);
            InitializeDistinctCities(data);

            var allPossiblePaths = Cities
                                .Permutations();
            var minAndMax = allPossiblePaths
                                .MinMaxElement((path) => CheckLength(path.ToList()));
            var minList = minAndMax.Item1.ToList();
            var maxList = minAndMax.Item2.ToList();
            var min = CheckLength(minList);
            var max = CheckLength(maxList);

            minList.PrintStringList();
            System.Console.WriteLine($"[Part 1] Min path length: {min}");

            maxList.PrintStringList();
            System.Console.WriteLine($"[Part 2] Max path length: {max}");

            System.Console.ReadLine();
        }

        static int CheckLength(List<string> cities)
        {
            var sum = 0;
            for (int i = 0; i < cities.Count - 1; i++)
            {
                sum += Distances[new CityPair(cities[i], cities[i + 1])];
            }
            return sum;
        }

        private static void LoadDataToDistancesDictionary(List<DataWithDistance<CityPair>> data)
        {
            foreach (var info in data)
            {
                Distances[info.Data] = info.Distance;
                Distances[info.Data.Exchange()] = info.Distance;
            }
        }

        private static List<DataWithDistance<CityPair>> ReadDistances()
        {
            using (var reader = new StreamReader("../../input.txt"))
            {
                var cityPairWithDistanceFactory = new CityPairWithDistanceFactory();
                return reader.ForEachLine(line =>
                    cityPairWithDistanceFactory.Create(line)).ToList();
            }
        }

        private static void InitializeDistinctCities(IEnumerable<DataWithDistance<CityPair>> data)
        {
            var cities = data.Select(x => x.Data.FirstCity).ToList();
            cities.AddRange(data.Select(x => x.Data.SecondCity));
            Cities = cities.Distinct().ToList();
        }

    }
}
