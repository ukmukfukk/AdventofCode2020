using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day15 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input15test.txt", "input15.txt" };

        public string Name => "Day 15";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                foreach (string s in input)
                {
                    SolvePuzzle(s, 2020, 1);
                }
            }
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                foreach (string s in input)
                {
                    SolvePuzzle(s, 30000000, 2);
                }
            }
        }

        private void SolvePuzzle(string input, int rounds, int ex)
        {
            string[] split = input.Split(':');
            int expected = split.Length > ex ? int.Parse(split[ex]) : -1;
            split = split[0].Split(',');
            List<int> numbers = new List<int>() { 0 };
            Dictionary<int, Tuple<int, int>> numberspoken = new Dictionary<int, Tuple<int, int>>();
            int i;
            DateTime startTime = DateTime.Now;
            for (i = 0; i < split.Length; i++)
            {
                int n = int.Parse(split[i]);
                numbers.Insert(i + 1, n);
                SetSpoken(numberspoken, n, i + 1);
            }

            for (i = split.Length + 1; i <= rounds; i++)
            {
                int spokenround = GetSpokenRound(numberspoken, numbers, i - 1);
                if (spokenround != 0)
                {
                    spokenround = (i - 1) - spokenround;
                }

                numbers.Insert(i, spokenround);
                SetSpoken(numberspoken, numbers[i], i);
            }

            Helper.Logger.Log(Name, $"Time: {DateTime.Now.Subtract(startTime).TotalSeconds} seconds");
            if (expected != -1)
            {
                Helper.Logger.Log(Name, $"{rounds} round: {numbers[rounds]}, {(expected == numbers[rounds] ? "TEST PASSED" : "TEST FAILED")}");
            }
            else
            {
                Helper.Logger.Log(Name, $"{rounds} round: {numbers[rounds]}");
            }
        }

        private int GetSpokenRound(Dictionary<int, Tuple<int, int>> numberspoken, List<int> numbers, int r)
        {
            int num = numbers[r];
            if (numberspoken.ContainsKey(num))
            {
                var tup = numberspoken[num];
                if (tup.Item1 != 0)
                {
                    return tup.Item1;
                }
            }

            return 0;
        }

        private void SetSpoken(Dictionary<int, Tuple<int, int>> numberspoken, int n, int i)
        {
            if (numberspoken.ContainsKey(n))
            {
                var spoken = new Tuple<int, int>(numberspoken[n].Item2, i);
                numberspoken[n] = spoken;
            }
            else
            {
                numberspoken.Add(n, new Tuple<int, int>(0, i));
            }
        }
    }
}
