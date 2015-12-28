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

    class Program
    {
        static int[,] Lamps = new int[1000, 1000];
        static void Main(string[] args)
        {
            using (var sw = new StreamReader("../../input.txt"))
            {
                var input = sw.ForEachLine(line => line).ToList();
                IGridOfLights gridOfLights = new GridOfLights(1000, 1000);
                SendCommandsToGrid(input, gridOfLights);
                var countFromFirstGrid = gridOfLights.LitLightsCount();
                System.Console.WriteLine($"First Part: {countFromFirstGrid}");

                gridOfLights = new GridOfLightsWithBrightnessControl(1000, 1000);
                SendCommandsToGrid(input, gridOfLights);
                var countFromSecondGrid = gridOfLights.LitLightsCount();
                System.Console.WriteLine($"Second Part: {countFromSecondGrid}");

            }
            System.Console.ReadLine();
        }

        private static void SendCommandsToGrid(List<string> input, IGridOfLights gridOfLights)
        {
            input.ForEach(line =>
            {
                var extracted = new ExtractedData(line);
                if (line.Contains("turn on")) { gridOfLights.TurnOn(extracted.StartPoint, extracted.EndPoint); }
                if (line.Contains("turn off")) { gridOfLights.TurnOff(extracted.StartPoint, extracted.EndPoint); }
                if (line.Contains("toggle")) { gridOfLights.Toggle(extracted.StartPoint, extracted.EndPoint); }
            });
        }
    }
}
