using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var file = new StreamReader("../../input.txt"))
            {
                var fsum = FirstPart(file);
                System.Console.WriteLine($"[1 part] Elves will need {fsum} sq. feet");
                var ssum = SecondPart(file);
                System.Console.WriteLine($"[2 part] Elves will need {ssum} sq. feet");
            }
            System.Console.ReadLine();
        }

        static int SecondPart(StreamReader file)
        {
            return file.ForEachLine((line) => {
                var splitted = line.Split('x').Select(x => int.Parse(x)).ToList();
                splitted.Sort();
                var a = splitted[0];
                var b = splitted[1];
                var c = splitted[2];
                var needed = a * b * c + 2 * a + 2 * b;
                return needed;
            }).Sum();
        }

        static int FirstPart(StreamReader file)
        {
            return file.ForEachLine((line) => {
                var splitted = line.Split('x');
                var l = int.Parse(splitted[0]);
                var w = int.Parse(splitted[1]);
                var h = int.Parse(splitted[2]);
                var min = Math.Min(l * w, w * h);
                min = Math.Min(min, h * l);
                //2*l*w + 2*w*h + 2*h*l. 
                var needed = 2 * l * w + 2 * w * h + 2 * h * l + min;
                // System.Console.WriteLine($"2*{l}*{w} + 2*{w}*{h} + 2*{h}*{l} + {min} = {needed}");
                return needed;
            }).Sum();
        }
    }
}
