using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Toolkit
{
    public static partial class StreamReaderExtentions
    {
        public static void ForEachLine(this System.IO.StreamReader streamReader, Action<string> action)
        {
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                action(line);
            }
        }
        public static IEnumerable<T> ForEachLine<T>(this System.IO.StreamReader streamReader, Func<string, T> function)
        {
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                yield return function(line);
            }
        }
        public static IEnumerable<T> ForEachLine<T>(this System.IO.StreamReader streamReader, Func<string, int, T> function)
        {
            int lineNumber = 0;
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var result = function(line, lineNumber);
                lineNumber++;
                yield return result;
            }
        }

        public static void ForEachLine(this System.IO.StreamReader streamReader, Action<string, int> action)
        {
            int lineNumber = 0;
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                action(line, lineNumber);
                lineNumber++;
            }
        }
    }
}
