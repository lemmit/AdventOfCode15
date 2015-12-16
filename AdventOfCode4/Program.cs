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
            var start = "yzbqklnj";
            //var start = "abcdef";
            foreach(var val in GetHash(start + 609043))
            {
                System.Console.Write(val.ToString("x2") + " ");
            }
            System.Console.WriteLine();
            //            System.Console.WriteLine($"{GetHash(start+609043)}");

            for (int i=0; i < 1000 * 1000 * 1000; i++)
            {
                var result = GetHash(start + i);
                //System.Console.WriteLine($"{start+i}: {result}");
                if (result[0] == 0
                    && result[1] == 0
                    && (result[2] /*& 0xF0*/) == 0)
                {
                    System.Console.WriteLine(i);
                    break;
                }
            }
            System.Console.ReadLine();
        }

        static byte[] GetHash(string val)
        {
            //var md5 = MD5.Create();
            //var stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(val));
            //var stream = new MemoryStream(GetBytes(val));
            //var hash = md5.ComputeHash(stream);
            //return hash;
            var md5_2 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(val);
            byte[] result = md5_2.ComputeHash(textToHash);
            return result;
            
        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
