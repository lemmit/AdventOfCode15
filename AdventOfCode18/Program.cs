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
                var gameOfLifeFields = new int[100, 100]; //possible outofbound exception for other data
                int maxY = 0;
                int maxX = 0;
                
                sw.ForEachLine((line, row) =>
                {
                    maxY = Math.Max(maxY, row);
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
                        maxX = Math.Max(maxX, col);
                    }
                });
                //gameOfLifeFields = ResizeField(gameOfLifeFields, maxX+1, maxY+1);

                StuckLights(gameOfLifeFields); /*PART 2*/
                for (int i=0; i<100; i++)
                {
                    //PrintGameOfLifeTable(gameOfLifeFields);
                    gameOfLifeFields = LifeStep(gameOfLifeFields);
                    StuckLights(gameOfLifeFields); /*PART 2*/

                }
                var on = 0;
                gameOfLifeFields.ForEach((row, col, value) => {
                    on += value;
                });
                System.Console.WriteLine(on);
                System.Console.ReadLine();
            }
        }

        private static void StuckLights(int[,] gameOfLifeFields)
        {
            gameOfLifeFields[0, 0] = 1;
            gameOfLifeFields[0, 99] = 1;
            gameOfLifeFields[99, 0] = 1;
            gameOfLifeFields[99, 99] = 1;
        }

        private static int[,] ResizeField(int[,] gameOfLifeFields, int rows, int cols)
        {
            var field = new int[rows, cols];
            for(int i=0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    field[i,j] = gameOfLifeFields[i, j];
                }
            }
            return field;
        }

        static int CountNeighbours(int [,] table, int row, int col)
        {
            var neighbours = 0;
            var positions = new List<Tuple<int, int>>() {
                new Tuple<int,int>(row - 1,  col - 1),
                new Tuple<int,int>(row - 1,  col),
                new Tuple<int,int>(row - 1,  col + 1),
                new Tuple<int,int>(row + 1,  col - 1 ),
                new Tuple<int,int>(row + 1,  col),
                new Tuple<int,int>(row + 1,  col + 1),
                new Tuple<int,int>(row,      col - 1),
                new Tuple<int,int>(row,      col + 1),
            };
            var rowLen = table.GetLength(0);
            var colLen = table.GetLength(1);

            positions.ForEach(pos =>
            {
                if (pos.Item1 >= 0 && pos.Item2 >= 0
                    && pos.Item1 < rowLen && pos.Item2 < colLen)
                {
                    neighbours += table[pos.Item1, pos.Item2];
                }
            }
            );
            //System.Console.WriteLine($"Counting neighbours {row}, {col} = {neighbours}");
            return neighbours;
        }

        static int[,] LifeStep(int[,] table)
        {
            var next = new int[table.GetLength(0), table.GetLength(1)];
            table.ForEach((row, col, value) => {
                var neighbours = CountNeighbours(table, row, col);
                if ((neighbours == 2 || neighbours == 3) && table[row, col] == 1)
                {
                    next[row, col] = 1;
                }
                else if (neighbours == 3 && table[row, col] == 0)
                {
                    next[row, col] = 1;
                }
                else
                {
                    //DIE
                    next[row, col] = 0;
                }
                //var died = table[row, col] == 1 && (table[row, col] == next[row, col]);
                //System.Console.WriteLine($"Light on {row}, {col} died? {died}");
            });
            return next;
        }

        static void PrintGameOfLifeTable(int[,] table)
        {
            table.ForEach((row, col, value) => {
                System.Console.Write(table[row, col]);
                if (col == table.GetLength(1) - 1)
                {
                    System.Console.WriteLine();
                }
            });
        }
    }
}
