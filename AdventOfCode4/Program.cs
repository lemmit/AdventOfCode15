using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace AdventOfCode4
{
    class Program
    {
        static void Main(string[] args)
        {
            var previous = "yzbqklnj";
            var miner = new Miner();
            var PoW = miner.Mine(previous, zeroedBytes: 5);
            System.Console.WriteLine($"PoW[1]: {PoW}");
            PoW = miner.Mine(previous, zeroedBytes: 6);
            System.Console.WriteLine($"PoW[2]: {PoW}");
            System.Console.ReadLine();
        }

        
        
    }
}
