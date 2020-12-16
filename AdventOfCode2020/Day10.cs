using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day10 : IDay
    {
        private long maxsum;
        private long[] maxsubcomb;

        public IList<string> InputFiles => new List<string> { "input10test.txt", "input10.txt" };

        public string Name => "Day 10";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle1(input);
            }
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle2(input);
            }
        }

        private void SolvePuzzle2(IList<string> input)
        {
            var numbers = input.Select(s => int.Parse(s));
            maxsubcomb = new long[numbers.Max() + 1];
            for (int i = 0; i <= numbers.Max(); i++)
            {
                maxsubcomb[i] = -1;
            }

            long combinations = CountCombination(numbers, 0);

            Helper.Logger.Log(Name, $"Result: {combinations}");
        }

        private long CountCombination(IEnumerable<int> numbers, int current)
        {
            if (current == numbers.Max())
            {
                return 1;
            }

            var temp = FindNext(numbers, current);
            long sum = 0;

            foreach (int next in temp)
            {
                if (maxsubcomb[next] == -1)
                {
                    maxsubcomb[next] = CountCombination(numbers, next);
                }

                sum += maxsubcomb[next];
            }

            if (Debugger.IsAttached)
            {
                if (sum > maxsum)
                {
                    Helper.Logger.Log(sum.ToString());
                    maxsum = sum;
                }
            }

            return sum;
        }

        private void SolvePuzzle1(IList<string> input)
        {
            var numbers = input.Select(s => int.Parse(s));
            int current = 0;
            int diff1 = 0;
            int diff3 = 1;
            int temp;

            while (current < numbers.Max())
            {
                temp = FindNext(numbers, current).Min();
                if (temp - current == 1)
                {
                    diff1++;
                }

                if (temp - current == 3)
                {
                    diff3++;
                }

                current = temp;
            }

            Helper.Logger.Log(Name, $"Result: {diff1 * diff3}");
        }

        private IEnumerable<int> FindNext(IEnumerable<int> numbers, int current)
        {
            return numbers.Where(n => n > current && n <= current + 3);
        }
    }
}
