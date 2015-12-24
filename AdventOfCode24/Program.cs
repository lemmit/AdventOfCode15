using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode24
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<int>();
            using(var sr = new StreamReader("../../input.txt"))
            {
                sr.ForEachLine((line) =>
                    input.Add(int.Parse(line))
                );
                input.Reverse();
                //input.Shuffle();
                var output = Solve(input, 4);
                PrintSolution(output);
                System.Console.ReadLine();
            }
        }

        private static void PrintSolution(List<List<int>> output)
        {
            foreach (var bucket in output)
            {
                foreach (var elem in bucket)
                {
                    System.Console.Write($"{elem},");
                }
                System.Console.Write($" - ");
            }
            System.Console.WriteLine($"QE: {output.First().Product()}");
        }

        static List<List<int>> Solution = null;
        static List<List<int>> Solve(List<int> packages, int numberOfBuckets)
        {
            Solution = null;
            SearchForSolutions(packages, packages.Sum() / numberOfBuckets);
            return Solution;
        }

        private static void SearchForSolutions(List<int> packages, 
            int sum, 
            int index = 0, 
            List<List<int>> solutionProposal = null)
        {
            if (solutionProposal == null)
            {
                solutionProposal = new List<List<int>>();
                solutionProposal.Add(new List<int>());
            }
            else if (Solution != null)
            {
                //TRY TO CUT OFF 
                var c = solutionProposal.Count;
                for(int i=0; i < c; i++)
                {
                    if(Solution[i].Count < solutionProposal[i].Count)
                    {
                        return;
                    }
                }
            }
            var partialSum = solutionProposal[index].Sum();
           foreach(var package in packages)
            {
                var newProposal = new List<List<int>>();
                solutionProposal.ForEach(list => {
                    newProposal.Add(list.ToList());
                });
                newProposal[index].Add(package);
                var packagesExceptElem = packages.Except(new List<int> { package }).ToList();
                var sumWithNewElement = partialSum + package;
                if (sumWithNewElement > sum)
                {
                    return;
                }else if(sumWithNewElement == sum)
                {
                    if (packagesExceptElem.Count == 0)
                    {
                        if (BetterSolution(newProposal))
                        {
                            newProposal.Sort(new ListCountComparer());
                            Solution = newProposal;
                            PrintSolution(newProposal);
                        }
                    }
                    else
                    {
                        newProposal.Add(new List<int>());
                        SearchForSolutions(packagesExceptElem, sum, index + 1, newProposal);
                    }
                }else if(sumWithNewElement < sum)
                {
                    SearchForSolutions(packagesExceptElem, sum, index, newProposal);
                }
            }
        }

        private static bool BetterSolution(List<List<int>> proposal)
        {
            var newProposal = proposal.ToList();
            newProposal.Sort(new ListCountComparer());
            var c = newProposal[0].Count;
            return
                Solution == null
                ||
                Solution[0].Count > c
                ||
                (
                    Solution[0].Count == c
                    &&
                    Solution[0].Product() > newProposal[0].Product()
                );
        }

        private class ListCountComparer : IComparer<List<int>>
        {
            public int Compare(List<int> x, List<int> y)
            {
                var countCompare = x.Count.CompareTo(y.Count);
                if (countCompare == 0)
                {
                    var xQE = x.Product();
                    var yQE = y.Product();
                    return xQE.CompareTo(yQE);
                }
                else
                {
                    return countCompare;
                }
            }
        }
    }
}
