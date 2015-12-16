using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode3
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<Tuple<int, int>, int>();
            var sentinel = 24122015;
            using (var sw = new StreamReader("../../input.txt"))
            {
                var line = sw.ReadLine();
                var santaPosition = new Tuple<int, int>(0, 0);
                var roboPosition = new Tuple<int, int>(0, 0);
                dict[santaPosition] = sentinel;
                var santa = true;
                for (int i=0; i < line.Length; i++)
                {
                    Tuple<int, int> position;
                    if (santa)
                    {
                        position = santaPosition;
                    }else
                    {
                        position = roboPosition;
                    }
                    var c = line[i];
                    switch (c)
                    {
                        case '<':
                            position = new Tuple<int, int>(position.Item1 - 1, position.Item2);
                            break;
                        case '>':
                            position = new Tuple<int, int>(position.Item1 + 1, position.Item2);
                            break;
                        case '^':
                            position = new Tuple<int, int>(position.Item1, position.Item2 + 1);
                            break;
                        case 'v':
                            position = new Tuple<int, int>(position.Item1, position.Item2 - 1);
                            break;
                    }
                    dict[position] = sentinel;
                    if (santa)
                    {
                        santaPosition = position;
                    }
                    else
                    {
                        roboPosition = position;
                    }

                    santa = !santa;
                    
                }
                var housesCount = dict.Keys.Count();
                System.Console.WriteLine($"{housesCount} houses received at least one present");
            }
            System.Console.ReadLine();
        }
    }
}
