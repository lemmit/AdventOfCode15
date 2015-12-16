using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Vixen can fly 8 km/s for 8 seconds, but then must rest for 53 seconds.
    Blitzen can fly 13 km/s for 4 seconds, but then must rest for 49 seconds.
    Rudolph can fly 20 km/s for 7 seconds, but then must rest for 132 seconds.
    Cupid can fly 12 km/s for 4 seconds, but then must rest for 43 seconds.
    Donner can fly 9 km/s for 5 seconds, but then must rest for 38 seconds.
    Dasher can fly 10 km/s for 4 seconds, but then must rest for 37 seconds.
    Comet can fly 3 km/s for 37 seconds, but then must rest for 76 seconds.
    Prancer can fly 9 km/s for 12 seconds, but then must rest for 97 seconds.
    Dancer can fly 37 km/s for 1 seconds, but then must rest for 36 seconds.
*/
namespace AdventOfCode14
{
    public class Reindeer {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int FlyingTimeBeforeRest { get; set; }
        public int RestTime { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var reindeers = new List<Reindeer> {
                new Reindeer { Name = "Vixen", Speed = 8, FlyingTimeBeforeRest = 8, RestTime = 53 },
                new Reindeer { Name = "Blitzen", Speed = 12, FlyingTimeBeforeRest = 4, RestTime = 49 },
                new Reindeer { Name = "Rudolph", Speed = 20, FlyingTimeBeforeRest = 7, RestTime = 132 },
                new Reindeer { Name = "Cupid", Speed = 12, FlyingTimeBeforeRest = 4, RestTime = 43 },
                new Reindeer { Name = "Donner", Speed = 9, FlyingTimeBeforeRest = 5, RestTime = 38 },
                new Reindeer { Name = "Dasher", Speed = 10, FlyingTimeBeforeRest = 4, RestTime = 37 },
                new Reindeer { Name = "Comet", Speed = 3, FlyingTimeBeforeRest = 37, RestTime = 76 },
                new Reindeer { Name = "Prancer", Speed = 9, FlyingTimeBeforeRest = 12, RestTime = 97 },
                new Reindeer { Name = "Dancer", Speed = 37, FlyingTimeBeforeRest = 1, RestTime = 36 },
            };
            int seconds = 2503;

            
            //var fastestReindeer = FindReindeerWithHighestDistanceTraveledAfterSeconds(reindeers, seconds).First();
            //System.Console.WriteLine($"Fastest reindeer is {fastestReindeer.Item1.Name}, and traveled {fastestReindeer.Item2}km");
             
            var mostScoredReindeer = FindReindeerWithHighestScoreAfterSeconds(reindeers, seconds);
            System.Console.WriteLine($"Reindeer with highest score is {mostScoredReindeer.Item1.Name}, and have {mostScoredReindeer.Item2} points");

            System.Console.ReadLine();
        }

        static Tuple<Reindeer, int> FindReindeerWithHighestScoreAfterSeconds(List<Reindeer> reindeers, int seconds)
        {
            /*dictionary reindeer-> score*/
            var dict = new Dictionary<Reindeer, int>();
                reindeers.ForEach((reindeer) => dict[reindeer] = 0);

            foreach(var i in Enumerable.Range(1, seconds))
            {
                var topReindeers = FindReindeerWithHighestDistanceTraveledAfterSeconds(reindeers, i);
                foreach(var topReindeer in topReindeers)
                {
                    dict[topReindeer.Item1]++;
                    System.Console.WriteLine($"[{i}] Reindeer {topReindeer.Item1.Name} scored. Sum: {dict[topReindeer.Item1]}");
                }
            }

            var max = dict.Max((pair) => pair.Value);
            var maxKeyValue = dict.FirstOrDefault(x => x.Value == max);
            return new Tuple<Reindeer, int>(maxKeyValue.Key, max);
        }

        static List<Tuple<Reindeer, int>> FindReindeerWithHighestDistanceTraveledAfterSeconds(List<Reindeer> reindeers, int seconds)
        {
            var scores = new List< Tuple<Reindeer, int> >();
            foreach (var reindeer in reindeers)
            {
                int cycle = reindeer.FlyingTimeBeforeRest + reindeer.RestTime;
                int numberOfCycles = seconds / cycle;
                int distance = numberOfCycles * reindeer.FlyingTimeBeforeRest * reindeer.Speed;
                int leftSeconds = seconds - numberOfCycles * cycle;
                if (leftSeconds >= reindeer.FlyingTimeBeforeRest)
                {
                    distance += reindeer.FlyingTimeBeforeRest * reindeer.Speed;
                }else if(leftSeconds > 0)
                {
                    distance += leftSeconds * reindeer.Speed;
                }
                scores.Add(new Tuple<Reindeer, int>(reindeer, distance));
                //System.Console.WriteLine($"Reindeer {reindeer.Name} traveled {distance}kms after {seconds}s.");
            }
            //sort reindeers to get those with highest score
            scores.Sort((r1, r2) => r1.Item2.CompareTo(r2.Item2));
            int maxDistance = scores.Last().Item2;
            var fastestReindeers = scores.Where(reindeerScored => reindeerScored.Item2 == maxDistance).ToList();
            return fastestReindeers;
        }
    }
}
