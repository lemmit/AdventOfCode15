using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode15
{
    public class SequenceGenerator
    {
        private readonly int SequenceLength;
        private readonly int MaxValue;
        private readonly int MinValue;
        private readonly int IncrementStep;
        private readonly int SequenceMaxSum;
        public SequenceGenerator(int sequenceLength,
                                int minValue = 0,
                                int maxValue = 100,
                                int incrementStep = 1,
                                int sequenceMaxSum = 100)
        {
            SequenceLength = sequenceLength;
            MinValue = minValue;
            MaxValue = maxValue;
            IncrementStep = incrementStep;
            SequenceMaxSum = sequenceMaxSum;
        }
        public IEnumerable<List<int>> GetSequences()
        {
            var list = new List<int>();
            Enumerable.Range(0, SequenceLength).ToList().ForEach((_) => list.Add(MinValue));
            var nrOfTries = Math.Pow(MaxValue - MinValue, SequenceLength);
            for (int i = 0; i < nrOfTries; i++)
            {
                try
                {
                    list = IncrementList(list, MaxValue);
                }
                catch (Exception e)
                {
                    //end of sequence
                    yield break;
                }

                var sum = list.Sum();
                if (sum > SequenceMaxSum)
                {
                    continue;
                }

                var outList = new List<int>(list);
                yield return outList;
            }
        }

        public List<int> IncrementList(List<int> list, int maxValue)
        {
            bool carry = true;
            int pos = 0;
            while (carry)
            {
                list[pos] += 1;
                if (list[pos] > maxValue)
                {
                    list[pos] -= maxValue;
                    pos++;
                }
                else carry = false;
                if (pos > list.Count - 1)
                    throw new OverflowException();
            }
            return list;
        }
    }
}
