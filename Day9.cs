using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day9 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input9.txt" };

        public string Name => "Day 9";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            SolvePuzzle1(inputs[0]);
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            SolvePuzzle2(inputs[0]);
        }

        private void SolvePuzzle2(IList<string> list)
        {
            List<long> numbers = list.Select(s => long.Parse(s)).ToList();
            FindListToSum(numbers, 85848519);
        }

        private void FindListToSum(List<long> numbers, int target)
        {
            for (int start = 0; start < numbers.Count; start++)
            {
                List<long> selected = new List<long>();
                int i = start;
                while (selected.Sum() != target && i < numbers.Count)
                {
                    selected.Add(numbers[i]);
                    i++;
                }

                if (selected.Sum() == target)
                {
                    Helper.Logger.Log(Name, $"the sum: {selected.Min() + selected.Max()}");
                    return;
                }
            }
        }

        private void SolvePuzzle1(IList<string> list)
        {
            List<long> numbers = list.Select(s => long.Parse(s)).ToList();
            CheckNumbers(numbers, 25);
        }

        private void CheckNumbers(List<long> numbers, int lookback)
        {
            for (int i = lookback; i < numbers.Count; i++)
            {
                if (!NumberValid(numbers, i, lookback))
                {
                    Helper.Logger.Log(Name, $"Invalid: {numbers[i]}");
                }
            }
        }

        private bool NumberValid(List<long> numbers, int n, int lookback)
        {
            for (int i = n - lookback; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (numbers[i] != numbers[j] && numbers[i] + numbers[j] == numbers[n])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
