using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode13
{
    enum HappinessRelation : int{
        Lose = -1,
        Gain = 1
    }

    class RelationEntry
    {
        public string GuestName { get; set; }
        public HappinessRelation Relation { get; set; }
        public int ValueOfRelation { get; set; }
        public string RelatedGuestName { get; set; }
        public RelationEntry(string guestName, HappinessRelation relation, int valueOfRelation, string relatedGuestName)
        {
            GuestName = guestName;
            Relation = relation;
            ValueOfRelation = valueOfRelation;
            RelatedGuestName = relatedGuestName;
        }
    }

    static class StringExtensions
    {
        public static string Capitalize(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException();
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }
    }

    class Program
    {
        static Dictionary<Tuple<string, string>, Tuple<HappinessRelation, int>> Relations = null; 

        static void Main(string[] args)
        {
            var input = GetInputData().ToList();
            var guests = GetGuests(input);
            //add me for second part
            guests.Add("Me");
            //
            var nrOfGuests = guests.Count;
            System.Console.WriteLine($"There is {nrOfGuests} guests.");
            Relations = new Dictionary<Tuple<string, string>, Tuple<HappinessRelation, int>>();
            
            foreach(var relation in input)
            {
                Relations.Add(
                        new Tuple<string, string>(relation.GuestName, relation.RelatedGuestName), 
                        new Tuple<HappinessRelation, int>(relation.Relation, relation.ValueOfRelation)
                        );
            }

            int bestOption = BestPossibleArrangement(guests);
            System.Console.WriteLine($"Best option is {bestOption}");
            System.Console.ReadLine();
        }

        static int BestPossibleArrangement(List<string> guests, string firstGuest = null, string lastGuest = null)
        {
            int bestArrangement = int.MinValue;
            if (guests.Count > 0)
            {
                foreach (var guest in guests)
                {
                   // var padding = "".PadLeft(30 - (guests.Count * 2));
                   // System.Console.Write(padding);
                   // System.Console.WriteLine($">{lastGuest}, {guest}");

                    var listExceptCurrent = guests
                                                .Except(new List<string> { guest })
                                                .ToList();
                    int possibleArrangement = int.MinValue;
                    if(firstGuest == null)
                    {
                        possibleArrangement = BestPossibleArrangement(listExceptCurrent, guest, guest);
                    }
                    else
                    {
                        possibleArrangement = CountTwoWayHappinessModifier(lastGuest, guest) +
                                             + BestPossibleArrangement(listExceptCurrent, firstGuest, guest);
                    }
                    if(bestArrangement <= possibleArrangement)
                    {
                        //Found better
                        bestArrangement = possibleArrangement;
                    }
                }
            }
            else {
                //way to first guest
                bestArrangement = CountTwoWayHappinessModifier(lastGuest, firstGuest);
            }
            
            return bestArrangement;
        }

        private static int CountTwoWayHappinessModifier(string lastGuest, string guest)
        {
            try {
                var relationAtoB = Relations[new Tuple<string, string>(lastGuest, guest)];
                var relationBtoA = Relations[new Tuple<string, string>(guest, lastGuest)];
                return (relationAtoB.Item2 * (int)relationAtoB.Item1)
                    + (relationBtoA.Item2 * (int)relationBtoA.Item1);
            }
            catch(Exception e)
            {}
            return 0;
        }

        static List<string> GetGuests(List<RelationEntry> relations)
        {
            var guardValue = 1;
            var dict = new Dictionary<string, int>();
            foreach(var relation in relations)
            {
                dict[relation.GuestName] = guardValue;
            }
            return dict.Keys.ToList();
        }

        static IEnumerable<RelationEntry> GetInputData()
        {
            using (var reader = new StringReader(InputData))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var extractedData = ExtractDataFromLine(line);
                    var parsedData = new RelationEntry(
                                            extractedData.Item1,
                                            (HappinessRelation)Enum.Parse(typeof(HappinessRelation), extractedData.Item2.Capitalize()),
                                            int.Parse(extractedData.Item3),
                                            extractedData.Item4
                                            );
                    yield return parsedData;
                }
            }
        }


        /* Generated using http://txt2re.com/ */
        static Tuple<string, string, string, string> ExtractDataFromLine(string txt)
        {
            string re1 = "((?:[a-z][a-z]+))";   // Word 1
            string re2 = ".*?"; // Non-greedy match on filler
            string re3 = "(?:[a-z][a-z]+)"; // Uninteresting: word
            string re4 = ".*?"; // Non-greedy match on filler
            string re5 = "((?:[a-z][a-z]+))";   // Word 2
            string re6 = ".*?"; // Non-greedy match on filler
            string re7 = "(\\d+)";  // Integer Number 1
            string re8 = ".*?"; // Non-greedy match on filler
            string re9 = "(?:[a-z][a-z]+)"; // Uninteresting: word
            string re10 = ".*?";    // Non-greedy match on filler
            string re11 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re12 = ".*?";    // Non-greedy match on filler
            string re13 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re14 = ".*?";    // Non-greedy match on filler
            string re15 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re16 = ".*?";    // Non-greedy match on filler
            string re17 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re18 = ".*?";    // Non-greedy match on filler
            string re19 = "(?:[a-z][a-z]+)";    // Uninteresting: word
            string re20 = ".*?";    // Non-greedy match on filler
            string re21 = "((?:[a-z][a-z]+))";  // Word 3

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13 + re14 + re15 + re16 + re17 + re18 + re19 + re20 + re21, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String word1 = m.Groups[1].ToString();
                String word2 = m.Groups[2].ToString();
                String int1 = m.Groups[3].ToString();
                String word3 = m.Groups[4].ToString();
                //Console.Write("EXTRACTION DEBUG: (" + word1.ToString() + ")" + "(" + word2.ToString() + ")" + "(" + int1.ToString() + ")" + "(" + word3.ToString() + ")" + "\n");
                return new Tuple<string, string, string, string>(word1, word2, int1, word3);
            }
            throw new ArgumentException();
        }

        const string InputData = @"Alice would lose 2 happiness units by sitting next to Bob.
Alice would lose 62 happiness units by sitting next to Carol.
Alice would gain 65 happiness units by sitting next to David.
Alice would gain 21 happiness units by sitting next to Eric.
Alice would lose 81 happiness units by sitting next to Frank.
Alice would lose 4 happiness units by sitting next to George.
Alice would lose 80 happiness units by sitting next to Mallory.
Bob would gain 93 happiness units by sitting next to Alice.
Bob would gain 19 happiness units by sitting next to Carol.
Bob would gain 5 happiness units by sitting next to David.
Bob would gain 49 happiness units by sitting next to Eric.
Bob would gain 68 happiness units by sitting next to Frank.
Bob would gain 23 happiness units by sitting next to George.
Bob would gain 29 happiness units by sitting next to Mallory.
Carol would lose 54 happiness units by sitting next to Alice.
Carol would lose 70 happiness units by sitting next to Bob.
Carol would lose 37 happiness units by sitting next to David.
Carol would lose 46 happiness units by sitting next to Eric.
Carol would gain 33 happiness units by sitting next to Frank.
Carol would lose 35 happiness units by sitting next to George.
Carol would gain 10 happiness units by sitting next to Mallory.
David would gain 43 happiness units by sitting next to Alice.
David would lose 96 happiness units by sitting next to Bob.
David would lose 53 happiness units by sitting next to Carol.
David would lose 30 happiness units by sitting next to Eric.
David would lose 12 happiness units by sitting next to Frank.
David would gain 75 happiness units by sitting next to George.
David would lose 20 happiness units by sitting next to Mallory.
Eric would gain 8 happiness units by sitting next to Alice.
Eric would lose 89 happiness units by sitting next to Bob.
Eric would lose 69 happiness units by sitting next to Carol.
Eric would lose 34 happiness units by sitting next to David.
Eric would gain 95 happiness units by sitting next to Frank.
Eric would gain 34 happiness units by sitting next to George.
Eric would lose 99 happiness units by sitting next to Mallory.
Frank would lose 97 happiness units by sitting next to Alice.
Frank would gain 6 happiness units by sitting next to Bob.
Frank would lose 9 happiness units by sitting next to Carol.
Frank would gain 56 happiness units by sitting next to David.
Frank would lose 17 happiness units by sitting next to Eric.
Frank would gain 18 happiness units by sitting next to George.
Frank would lose 56 happiness units by sitting next to Mallory.
George would gain 45 happiness units by sitting next to Alice.
George would gain 76 happiness units by sitting next to Bob.
George would gain 63 happiness units by sitting next to Carol.
George would gain 54 happiness units by sitting next to David.
George would gain 54 happiness units by sitting next to Eric.
George would gain 30 happiness units by sitting next to Frank.
George would gain 7 happiness units by sitting next to Mallory.
Mallory would gain 31 happiness units by sitting next to Alice.
Mallory would lose 32 happiness units by sitting next to Bob.
Mallory would gain 95 happiness units by sitting next to Carol.
Mallory would gain 91 happiness units by sitting next to David.
Mallory would lose 66 happiness units by sitting next to Eric.
Mallory would lose 75 happiness units by sitting next to Frank.
Mallory would lose 99 happiness units by sitting next to George.";
    }
}
