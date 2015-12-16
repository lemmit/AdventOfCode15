using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode6
{

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Program
    {
        static int[,] Lamps = new int[1000, 1000];
        static void Main(string[] args)
        {
            for(int i=0; i<1000; i++)
            {
                for(int j=0; j<1000;j++)
                {
                    Lamps[j, i] = 0;
                }
            }
            using(var sw = new StreamReader("../../input.txt"))
            {
               // FirstPart(sw);
                SecondPart(sw);
                var sum = 0;
                for (int i = 0; i < 1000; i++)
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        sum+=Lamps[j, i];
                    }
                }
                System.Console.WriteLine(sum);
            }
            System.Console.ReadLine();
        }

        static void SecondPart(StreamReader sw)
        {
            sw.ForEachLine((line) => {
                var data = Extract(line);
                if (line.Contains("turn on"))
                {
                    for (int i = data.Item1.Y; i <= data.Item2.Y; i++)
                    {
                        for (int j = data.Item1.X; j <= data.Item2.X; j++)
                        {
                            Lamps[j, i] += 1;
                        }
                    }
                }
                else if (line.Contains("turn off"))
                {
                    for (int i = data.Item1.Y; i <= data.Item2.Y; i++)
                    {
                        for (int j = data.Item1.X; j <= data.Item2.X; j++)
                        {
                            if (Lamps[j, i] > 0)
                            {
                                Lamps[j, i] -= 1;
                            }
                        }
                    }
                }
                else if (line.Contains("toggle"))
                {
                    for (int i = data.Item1.Y; i <= data.Item2.Y; i++)
                    {
                        for (int j = data.Item1.X; j <= data.Item2.X; j++)
                        {
                            Lamps[j, i] += 2;
                        }
                    }
                }
            });
        }

        static void FirstPart(StreamReader sw)
        {
            sw.ForEachLine((line) => {
                var data = Extract(line);
                if (line.Contains("turn on"))
                {
                    for (int i = data.Item1.Y; i <= data.Item2.Y; i++)
                    {
                        for (int j = data.Item1.X; j <= data.Item2.X; j++)
                        {
                            Lamps[j, i] = 1;
                        }
                    }
                }
                else if (line.Contains("turn off"))
                {
                    for (int i = data.Item1.Y; i <= data.Item2.Y; i++)
                    {
                        for (int j = data.Item1.X; j <= data.Item2.X; j++)
                        {
                            Lamps[j, i] = 0;
                        }
                    }
                }
                else if (line.Contains("toggle"))
                {
                    for (int i = data.Item1.Y; i <= data.Item2.Y; i++)
                    {
                        for (int j = data.Item1.X; j <= data.Item2.X; j++)
                        {
                            Lamps[j, i] = (Lamps[j, i] == 1) ? 0 : 1;
                        }
                    }
                }
            });
        }

        static Tuple<Point, Point> Extract(string txt)
        {
            string re1 = ".*?"; // Non-greedy match on filler
            string re2 = "(\\d+)";  // Integer Number 1
            string re3 = ".*?"; // Non-greedy match on filler
            string re4 = "(\\d+)";  // Integer Number 2
            string re5 = ".*?"; // Non-greedy match on filler
            string re6 = "(\\d+)";  // Integer Number 3
            string re7 = ".*?"; // Non-greedy match on filler
            string re8 = "(\\d+)";  // Integer Number 4

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String int1 = m.Groups[1].ToString();
                String int2 = m.Groups[2].ToString();
                String int3 = m.Groups[3].ToString();
                String int4 = m.Groups[4].ToString();
                return new Tuple<Point, Point>(
                                                new Point { X = int.Parse(int1), Y = int.Parse(int2) },
                                                new Point { X = int.Parse(int3), Y = int.Parse(int4) }
                                                );
            }
            return null;
        }
    }
}
