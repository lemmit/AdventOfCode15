using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode9
{
    public class CityPairEqualityComparer : IEqualityComparer<CityPair>
    {
        public bool Equals(CityPair x, CityPair y)
        {
            return x.FirstCity == y.FirstCity && x.SecondCity == y.SecondCity;
        }

        public int GetHashCode(CityPair obj)
        {
            return obj.FirstCity.GetHashCode() + obj.SecondCity.GetHashCode();
        }
    }
}
