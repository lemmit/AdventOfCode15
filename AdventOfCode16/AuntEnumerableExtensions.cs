using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode16
{
    public static class AuntEnumerableExtensions
    {
        public static IEnumerable<Aunt> IfHasPropertyThenWithValue(this IEnumerable<Aunt> aunts, string property, int value)
        {
            return aunts
                .Where(aunt =>
                        (aunt.Item2 != property && aunt.Item4 != property && aunt.Item6 != property)
                        || (aunt.Item2 == property && aunt.Item3 == value)
                        || (aunt.Item4 == property && aunt.Item5 == value)
                        || (aunt.Item6 == property && aunt.Item7 == value)
                    );
        }

        public static IEnumerable<Aunt> IfHasPropertyThenGreaterThanValue(this IEnumerable<Aunt> aunts, string property, int value)
        {
            return aunts
                .Where(aunt =>
                        (aunt.Item2 != property && aunt.Item4 != property && aunt.Item6 != property)
                        || (aunt.Item2 == property && aunt.Item3 > value)
                        || (aunt.Item4 == property && aunt.Item5 > value)
                        || (aunt.Item6 == property && aunt.Item7 > value)
                    );
        }

        public static IEnumerable<Aunt> IfHasPropertyThenLesserThanValue(this IEnumerable<Aunt> aunts, string property, int value)
        {
            return aunts
                .Where(aunt =>
                        (aunt.Item2 != property && aunt.Item4 != property && aunt.Item6 != property)
                        || (aunt.Item2 == property && aunt.Item3 < value)
                        || (aunt.Item4 == property && aunt.Item5 < value)
                        || (aunt.Item6 == property && aunt.Item7 < value)
                    );
        }
    }
}
