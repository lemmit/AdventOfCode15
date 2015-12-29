using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Toolkit
{
    public static class IEnumerableProductExtensions
    {
        public static IEnumerable<Tuple<T1, T2>> Product<T1, T2>(
           this IEnumerable<T1> @this,
           IEnumerable<T2> enum1)
        {
            foreach (var elem in @this)
            {
                foreach (var elem1 in enum1)
                {
                    yield return new Tuple<T1, T2>(elem, elem1);
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3>> Product<T1, T2, T3>(
            this IEnumerable<T1> @this,
            IEnumerable<T2> enum1,
            IEnumerable<T3> enum2)
        {
            foreach (var elem in @this)
            {
                foreach (var elem1 in enum1)
                {
                    foreach (var elem2 in enum2)
                    {
                        yield return new Tuple<T1, T2, T3>(elem, elem1, elem2);
                    }
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4>> Product<T1, T2, T3, T4>(
            this IEnumerable<T1> @this,
            IEnumerable<T2> enum1,
            IEnumerable<T3> enum2,
            IEnumerable<T4> enum3)
        {
            foreach (var elem in @this)
            {
                foreach (var elem1 in enum1)
                {
                    foreach (var elem2 in enum2)
                    {
                        foreach (var elem3 in enum3)
                        {
                            yield return new Tuple<T1, T2, T3, T4>(elem, elem1, elem2, elem3);
                        }
                    }
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5>> Product<T1, T2, T3, T4, T5>(
           this IEnumerable<T1> @this,
           IEnumerable<T2> enum1,
           IEnumerable<T3> enum2,
           IEnumerable<T4> enum3,
           IEnumerable<T5> enum4)
        {
            foreach (var elem in @this)
            {
                foreach (var elem1 in enum1)
                {
                    foreach (var elem2 in enum2)
                    {
                        foreach (var elem3 in enum3)
                        {
                            foreach (var elem4 in enum4)
                            {
                                yield return new Tuple<T1, T2, T3, T4, T5>(elem, elem1, elem2, elem3, elem4);
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5, T6>> Product<T1, T2, T3, T4, T5, T6>(
          this IEnumerable<T1> @this,
          IEnumerable<T2> enum1,
          IEnumerable<T3> enum2,
          IEnumerable<T4> enum3,
          IEnumerable<T5> enum4,
          IEnumerable<T6> enum5)
        {
            foreach (var elem in @this)
            {
                foreach (var elem1 in enum1)
                {
                    foreach (var elem2 in enum2)
                    {
                        foreach (var elem3 in enum3)
                        {
                            foreach (var elem4 in enum4)
                            {
                                foreach (var elem5 in enum5)
                                {
                                    yield return new Tuple<T1, T2, T3, T4, T5, T6>(elem, elem1, elem2, elem3, elem4, elem5);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5, T6, T7>> Product<T1, T2, T3, T4, T5, T6, T7>(
          this IEnumerable<T1> @this,
          IEnumerable<T2> enum1,
          IEnumerable<T3> enum2,
          IEnumerable<T4> enum3,
          IEnumerable<T5> enum4,
          IEnumerable<T6> enum5,
          IEnumerable<T7> enum6)
        {
            foreach (var elem in @this)
            {
                foreach (var elem1 in enum1)
                {
                    foreach (var elem2 in enum2)
                    {
                        foreach (var elem3 in enum3)
                        {
                            foreach (var elem4 in enum4)
                            {
                                foreach (var elem5 in enum5)
                                {
                                    foreach (var elem6 in enum6)
                                    {
                                        yield return new Tuple<T1, T2, T3, T4, T5, T6, T7>(elem, elem1, elem2, elem3, elem4, elem5, elem6);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
