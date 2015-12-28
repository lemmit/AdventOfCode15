using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode9
{
    public class CityPair
    {
        public string FirstCity { get; private set; }
        public string SecondCity { get; private set; }
        public CityPair(string firstCity, string secondCity)
        {
            FirstCity = firstCity;
            SecondCity = secondCity;
        }
        public CityPair Exchange()
        {
            return new CityPair(SecondCity, FirstCity);
        }
    }
}
