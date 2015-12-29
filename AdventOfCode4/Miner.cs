using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode4
{
    public class Miner
    {
        public Miner()
        {
        }

        public int Mine(string start, int zeroedBytes)
        {
            int possiblePoW = 0;
            while (true)
            {
                var result = GetHash(start + possiblePoW);
                var hexResult = ByteArrayToHexString(result);
                bool difference = false;
                for(int i=0; i<zeroedBytes; i++)
                {
                    if(hexResult[i] != '0')
                    {
                        possiblePoW++;
                        difference = true;
                        break;
                    }
                }
                if (difference) { continue; }
                return possiblePoW;
            }
        }
        private string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private byte[] GetHash(string val)
        {
            var md5Provider = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(val);
            byte[] result = md5Provider.ComputeHash(textToHash);
            return result;
        }
    }
}