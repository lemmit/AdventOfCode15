using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode9
{
    public class DataWithDistance<T>
    {
        public T Data { get; private set; }
        public int Distance { get; private set; }
        public DataWithDistance(int distance, T data)
        {
            Distance = distance;
            Data = data;
        }
    }
}
