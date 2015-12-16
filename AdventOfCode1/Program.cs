using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode1
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("../../input.txt"))
            {
                var input = sr.ReadLine();
                int closing = 0;
                int opening = 0;
                bool inBasement = false;
                for(int i=0; i<input.Length; i++)
                {
                    var character = input[i];
                    switch (character)
                    {
                        case '(':
                            opening++;
                            break;
                        case ')':
                            closing++;
                            break;
                    }
                    if(opening-closing < 0 && !inBasement)
                    {
                        System.Console.WriteLine($"Santa entered the basement: {i+1}");
                        inBasement = true;
                    }
                }
                System.Console.WriteLine($"Final floor: {opening - closing}");
            }
            System.Console.ReadLine();
        }
    }
}
