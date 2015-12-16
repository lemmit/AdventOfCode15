using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode10
{
    class Program
    {
        static void Main(string[] args)
        {
            var lookAndSay = "1113222113";
            //var output = LookAndSay(lookAndSay);
            InitializeTransitionsData();

            /*Test("21");
            //Test("1211");
            //Test("111221");
            */
            var output = lookAndSay;
            //var output = new List<int>() { 31 };
            for (int i=0; i<51; i++)
            {
                output = LookAndSay(output);
                System.Console.WriteLine($"{i}: {output.Length}");

                //SOMETHING IS WRONG HERE :[
                //output = LookAndSaySeriesBased(output);
                //var sum = output.Sum((elem) => Transitions[elem].Item2);
                //System.Console.WriteLine($"{i}: {sum}");
            }

            System.Console.ReadLine();
        }

        static List<int> LookAndSaySeriesBased(List<int> startSeries)
        {
            var outputList = new List<int>();
            foreach(var elem in startSeries)
            {
                outputList.AddRange(Transitions[elem].Item1);
            }
            return outputList;
        }

        static void Test(string test)
        {
            System.Console.WriteLine($"{test}: {LookAndSay(test)}"); 
        }
        static string LookAndSay(string lookAndSay)
        {
            var last = lookAndSay[0];
            string output = "";
            var count = 1;
            for (int i = 1; i < lookAndSay.Length; i++)
            {
                if (lookAndSay[i] == last)
                {
                    count++;
                }
                else
                {
                    output += count;
                    output += last;
                    last = lookAndSay[i];
                    count = 1;
                }
            }
            output += count;
            output += last;
            return output;
        }

        static void InitializeTransitionsData()
        {
            var dict = new Dictionary<int, Tuple<List<int>,int>>() {
                {1, new Tuple<List<int>, int> ( new List<int>(){ 63 },4) },
                {2, new Tuple<List<int>, int> ( new List<int>(){ 64, 62 },7) },
                {3, new Tuple<List<int>, int> ( new List<int>(){ 65 },12) },
                {4, new Tuple<List<int>, int> ( new List<int>(){ 66 },12) },
                {5, new Tuple<List<int>, int> ( new List<int>(){ 68 },4) },
                {6, new Tuple<List<int>, int> ( new List<int>(){ 69 },5) },
                {7, new Tuple<List<int>, int> ( new List<int>(){ 84, 55 },12) },
                {8, new Tuple<List<int>, int> ( new List<int>(){ 70 },6) },
                {9, new Tuple<List<int>, int> ( new List<int>(){ 71 },8) },
                {10, new Tuple<List<int>, int> ( new List<int>(){ 76 },10) },
                {11, new Tuple<List<int>, int> ( new List<int>(){ 77 },10) },
                {12, new Tuple<List<int>, int> ( new List<int>(){ 82 },14) },
                {13, new Tuple<List<int>, int> ( new List<int>(){ 78 },12) },
                {14, new Tuple<List<int>, int> ( new List<int>(){ 79 },14) },
                {15, new Tuple<List<int>, int> ( new List<int>(){ 80 },18) },
                {16, new Tuple<List<int>, int> ( new List<int>(){ 81, 29, 91 },42) },
                {17, new Tuple<List<int>, int> ( new List<int>(){ 81, 29, 90 },42) },
                {18, new Tuple<List<int>, int> ( new List<int>(){ 81, 30 },26) },
                {19, new Tuple<List<int>, int> ( new List<int>(){ 75, 29, 92 },14) },
                {20, new Tuple<List<int>, int> ( new List<int>(){ 75, 32 },28) },
                {21, new Tuple<List<int>, int> ( new List<int>(){ 72 },14) },
                {22, new Tuple<List<int>, int> ( new List<int>(){ 73 },24) },
                {23, new Tuple<List<int>, int> ( new List<int>(){ 74 },24) },
                {24, new Tuple<List<int>, int> ( new List<int>(){ 83 },5) },
                {25, new Tuple<List<int>, int> ( new List<int>(){ 86 },7) },
                {26, new Tuple<List<int>, int> ( new List<int>(){ 87 },10) },
                {27, new Tuple<List<int>, int> ( new List<int>(){ 88 },10) },
                {28, new Tuple<List<int>, int> ( new List<int>(){ 89, 92 },8) },
                {29, new Tuple<List<int>, int> ( new List<int>(){ 1 },2) },
                {30, new Tuple<List<int>, int> ( new List<int>(){ 3 },9) },
                {31, new Tuple<List<int>, int> ( new List<int>(){ 4 },9) },
                {32, new Tuple<List<int>, int> ( new List<int>(){ 2, 61, 29, 85 },23) },
                {33, new Tuple<List<int>, int> ( new List<int>(){ 5 },2) },
                {34, new Tuple<List<int>, int> ( new List<int>(){ 28 },6) },
                {35, new Tuple<List<int>, int> ( new List<int>(){ 24, 33, 61, 29, 91 },32) },
                {36, new Tuple<List<int>, int> ( new List<int>(){ 24, 33, 61, 29, 90 },32) },
                {37, new Tuple<List<int>, int> ( new List<int>(){ 7 },8) },
                {38, new Tuple<List<int>, int> ( new List<int>(){ 8 },3) },
                {39, new Tuple<List<int>, int> ( new List<int>(){ 9 },5) },
                {40, new Tuple<List<int>, int> ( new List<int>(){ 10 },6) },
                {41, new Tuple<List<int>, int> ( new List<int>(){ 21 },10) },
                {42, new Tuple<List<int>, int> ( new List<int>(){ 22 },18) },
                {43, new Tuple<List<int>, int> ( new List<int>(){ 23 },18) },
                {44, new Tuple<List<int>, int> ( new List<int>(){ 11 },6) },
                {45, new Tuple<List<int>, int> ( new List<int>(){ 19 },10) },
                {46, new Tuple<List<int>, int> ( new List<int>(){ 12 },8) },
                {47, new Tuple<List<int>, int> ( new List<int>(){ 13 },7) },
                {48, new Tuple<List<int>, int> ( new List<int>(){ 14 },8) },
                {49, new Tuple<List<int>, int> ( new List<int>(){ 15 },12) },
                {50, new Tuple<List<int>, int> ( new List<int>(){ 18 },20) },
                {51, new Tuple<List<int>, int> ( new List<int>(){ 16 },34) },
                {52, new Tuple<List<int>, int> ( new List<int>(){ 17 },34) },
                {53, new Tuple<List<int>, int> ( new List<int>(){ 20 },20) },
                {54, new Tuple<List<int>, int> ( new List<int>(){ 6, 61, 29, 92 },10) },
                {55, new Tuple<List<int>, int> ( new List<int>(){ 26 },7) },
                {56, new Tuple<List<int>, int> ( new List<int>(){ 27 },7) },
                {57, new Tuple<List<int>, int> ( new List<int>(){ 25, 29, 92 },11) },
                {58, new Tuple<List<int>, int> ( new List<int>(){ 25, 29, 67 },13) },
                {59, new Tuple<List<int>, int> ( new List<int>(){ 25, 29, 85 },21) },
                {60, new Tuple<List<int>, int> ( new List<int>(){ 25, 29, 68, 61, 29, 89 },17) },
                {61, new Tuple<List<int>, int> ( new List<int>(){ 61 },2) },
                {62, new Tuple<List<int>, int> ( new List<int>(){ 33 },1) },
                {63, new Tuple<List<int>, int> ( new List<int>(){ 40 },4) },
                {64, new Tuple<List<int>, int> ( new List<int>(){ 41 },7) },
                {65, new Tuple<List<int>, int> ( new List<int>(){ 42 },14) },
                {66, new Tuple<List<int>, int> ( new List<int>(){ 43 },14) },
                {67, new Tuple<List<int>, int> ( new List<int>(){ 38, 39 },7) },
                {68, new Tuple<List<int>, int> ( new List<int>(){ 44 },4) },
                {69, new Tuple<List<int>, int> ( new List<int>(){ 48 },6) },
                {70, new Tuple<List<int>, int> ( new List<int>(){ 54 },8) },
                {71, new Tuple<List<int>, int> ( new List<int>(){ 49 },10) },
                {72, new Tuple<List<int>, int> ( new List<int>(){ 50 },16) },
                {73, new Tuple<List<int>, int> ( new List<int>(){ 51 },28) },
                {74, new Tuple<List<int>, int> ( new List<int>(){ 52 },28) },
                {75, new Tuple<List<int>, int> ( new List<int>(){ 47, 38 },9) },
                {76, new Tuple<List<int>, int> ( new List<int>(){ 47, 55 },12) },
                {77, new Tuple<List<int>, int> ( new List<int>(){ 47, 56 },12) },
                {78, new Tuple<List<int>, int> ( new List<int>(){ 47, 57 },16) },
                {79, new Tuple<List<int>, int> ( new List<int>(){ 47, 58 },18) },
                {80, new Tuple<List<int>, int> ( new List<int>(){ 47, 59 },24) },
                {81, new Tuple<List<int>, int> ( new List<int>(){ 47, 60 },23) },
                {82, new Tuple<List<int>, int> ( new List<int>(){ 47, 33, 61, 29, 92 },16) },
                {83, new Tuple<List<int>, int> ( new List<int>(){ 45 },6) },
                {84, new Tuple<List<int>, int> ( new List<int>(){ 46 },5) },
                {85, new Tuple<List<int>, int> ( new List<int>(){ 53 },15) },
                {86, new Tuple<List<int>, int> ( new List<int>(){ 38, 29, 89 },6) },
                {87, new Tuple<List<int>, int> ( new List<int>(){ 38, 30 },10) },
                {88, new Tuple<List<int>, int> ( new List<int>(){ 38, 31 },10) },
                {89, new Tuple<List<int>, int> ( new List<int>(){ 34 },3) },
                {90, new Tuple<List<int>, int> ( new List<int>(){ 36 },27) },
                {91, new Tuple<List<int>, int> ( new List<int>(){ 35 },27) },
                {92, new Tuple<List<int>, int> ( new List<int>(){ 37 },5) },
            };
            Transitions = dict;
        }
        static Dictionary<int, Tuple<List<int>, int>> Transitions;
    }

}
