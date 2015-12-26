using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Toolkit
{
    public static partial class StringExtensions
    {
        public static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }

        public static int CountOfVowels(this string str)
        {
            var vowels = "aeiou";
            return vowels.Select(vowel => str.AllIndexesOf(vowel.ToString()).Count()).Sum();
        }

        public static IEnumerable<Tuple<char, char>> SubsequentLetterPairs(this string str)
        {
            for (var i = 0; i < str.Length - 1; i++)
            {
                yield return new Tuple<char, char>(str[i], str[i + 1]);
            }
        }

        public static IEnumerable<Tuple<char, char>> LettersPairsWithGap(this string str)
        {
            for (var i = 0; i < str.Length - 2; i++)
            {
                yield return new Tuple<char, char>(str[i], str[i + 2]);
            }
        }
    }
}
