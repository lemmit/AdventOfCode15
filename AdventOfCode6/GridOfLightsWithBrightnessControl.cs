using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode6
{
    public class GridOfLightsWithBrightnessControl : IGridOfLights
    {
        private int[,] grid;

        public GridOfLightsWithBrightnessControl(int width, int height)
        {
            grid = new int[width, height];
        }

        public long LitLightsCount()
        {
            var sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    sum += grid[j, i];
                }
            }
            return sum;
        }

        public void Toggle(Point start, Point end)
        {
            for (int i = start.Y; i <= end.Y; i++)
            {
                for (int j = start.X; j <= end.X; j++)
                {
                    grid[j, i] += 2;
                }
            }
        }

        public void TurnOff(Point start, Point end)
        {
            for (int i = start.Y; i <= end.Y; i++)
            {
                for (int j = start.X; j <= end.X; j++)
                {
                    grid[j, i] = grid[j,i] > 0 ? grid[j,i] - 1 : 0;
                }
            }
        }

        public void TurnOn(Point start, Point end)
        {
            for (int i = start.Y; i <= end.Y; i++)
            {
                for (int j = start.X; j <= end.X; j++)
                {
                    grid[j, i] += 1;
                }
            }
        }
    }
}
