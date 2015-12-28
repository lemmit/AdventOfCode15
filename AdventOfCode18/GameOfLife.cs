using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Toolkit;

namespace AdventOfCode18
{
    public class GameOfLife
    {
        int[,] gameOfLifeFields;
        int X = 0;
        int Y = 0;
        bool stuckedLights = false;
        public GameOfLife(int[,] initializationMatrix, bool stuckedLights = false)
        {
            gameOfLifeFields = initializationMatrix;
            X = gameOfLifeFields.GetLength(1);
            Y = gameOfLifeFields.GetLength(0);
            this.stuckedLights = stuckedLights;
            if (stuckedLights)
            {
                StuckLights();
            }
        }

        private void StuckLights()
        {
            gameOfLifeFields[0, 0] = 1;
            gameOfLifeFields[0, Y-1] = 1;
            gameOfLifeFields[X-1, Y-1] = 1;
            gameOfLifeFields[X-1, 0] = 1;
        }

        public int AliveCreaturesCount()
        {
            return gameOfLifeFields.ForEach((row, col, value) => value).Sum();
        }

        public GameOfLife NextStep()
        {
            var next = new GameOfLife(new int[X,Y], stuckedLights);
            gameOfLifeFields.ForEach((row, col, value) => {
                var neighbours = CountNeighbours(row, col);
                if ((neighbours == 2 || neighbours == 3) && gameOfLifeFields[row, col] == 1)
                {
                    next.gameOfLifeFields[row, col] = 1;
                }
                else if (neighbours == 3 && gameOfLifeFields[row, col] == 0)
                {
                    next.gameOfLifeFields[row, col] = 1;
                }
                else
                {
                    //DIE
                    next.gameOfLifeFields[row, col] = 0;
                }
            });
            if (next.stuckedLights)
            {
                next.StuckLights();
            }
            return next;
        }

        public void Print()
        {
            gameOfLifeFields.ForEach((row, col, value) => {
                System.Console.Write(gameOfLifeFields[row, col] == 1 ? '#' : '.');
                if (col == gameOfLifeFields.GetLength(1) - 1)
                {
                    System.Console.WriteLine();
                }
            });
        }

        int CountNeighbours(int row, int col)
        {
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
            var rowLen = Y;
            var colLen = X;

            var neighbours = positions.Select(pos =>
            {
                if (pos.Item1 >= 0 && pos.Item2 >= 0
                    && pos.Item1 < rowLen && pos.Item2 < colLen)
                {
                    return gameOfLifeFields[pos.Item1, pos.Item2];
                }
                return 0;
            }
            ).Sum();
            //System.Console.WriteLine($"Counting neighbours {row}, {col} = {neighbours}");
            return neighbours;
        }

    }
}
