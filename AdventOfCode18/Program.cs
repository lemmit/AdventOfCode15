using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode18
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var sw = new StreamReader("../../input.txt"))
            {
                var gameOfLifeFields = new int[100, 100];
                sw.ForEachLine((line, row) =>
                {
                    for (int col = 0; col < line.Length; col++)
                    {
                        switch (line[col])
                        {
                            case '#':
                                gameOfLifeFields[row, col] = 1;
                                break;
                            case '.':
                                gameOfLifeFields[row, col] = 0;
                                break;
                        }
                    }
                });

                var gameOfLife = new GameOfLife(gameOfLifeFields
                                                /*PART 2*/ 
                                                ,stuckedLights: true); 

                for (int i=0; i<100; i++)
                {
                    //System.Console.WriteLine($"Grid after {i} steps:");
                    //gameOfLife.Print();
                    gameOfLife = gameOfLife.NextStep();
                }
                var on = gameOfLife.AliveCreaturesCount();
                System.Console.WriteLine(on);
                System.Console.ReadLine();
            }
        }
    }
}
