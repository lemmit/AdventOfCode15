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
            foreach(var pair in str.LettersPairsWithGap(0)) {
                yield return pair;
            }
        }

        public static IEnumerable<Tuple<char, char>> LettersPairsWithGap(this string str, int gap = 1)
        {
            for (var i = 0; i < str.Length - gap - 1; i++)
            {
                yield return new Tuple<char, char>(str[i], str[i + gap + 1]);
            }
        }

        public static string Capitalize(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException();
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public static IEnumerable<string> Replacements(this string molecule, IEnumerable<Tuple<string, string>> replacements)
        {
            for (int i = 0; i < molecule.Length; i++)
            {
                foreach (var replacement in replacements)
                {
                    var key = replacement.Item1;
                    if (i > molecule.Length - key.Length)
                    {
                        continue;
                    }
                    var substr = molecule.Substring(i, key.Length);
                    if (key == substr)
                    {
                        var before = molecule.Substring(0, i);
                        var after = molecule.Substring(i + key.Length);
                        //replace molecule with new one
                        yield return before + replacement.Item2 + after;
                    }
                }
            }
        }
    }
}
