using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode15
{
    public class Ingredient
    {
        public string Name { get; set; }
        public int Capacity { get; set; } = 0;
        public int Durability { get; set; } = 0;
        public int Flavor { get; set; } = 0;
        public int Texture { get; set; } = 0;
        public int Calories { get; set; } = 0;
    };
}
