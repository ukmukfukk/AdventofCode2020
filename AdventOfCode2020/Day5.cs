using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day5 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input5.txt" };

        public string Name => "Day 5";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            SolvePuzzle1(inputs[0]);
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            SolvePuzzle2(inputs[0]);
        }

        private void SolvePuzzle1(IList<string> list)
        {
            List<int> passes = GetPasses(list);

            int maxi = 0;
            for (int i = 1; i < passes.Count; i++)
            {
                if (passes[i] > passes[maxi])
                {
                    maxi = i;
                }
            }

            Helper.Logger.Log(Name, $"max passno: {passes[maxi]}");
        }

        private int GetSeatId(string code)
        {
            int retval = 0;
            for (int i = 0; i < 10; i++)
            {
                if (code[i] == 'B' || code[i] == 'R')
                {
                    retval += (int)Math.Pow(2, 9 - i);
                }
            }

            return retval;
        }

        private void SolvePuzzle2(IList<string> list)
        {
            List<int> passes = GetPasses(list);

            bool[] seats = new bool[1024];

            for (int i = 0; i < 8; i++)
            {
                seats[i] = true;
                seats[1016 + i] = true;
            }

            for (int i = 0; i < passes.Count; i++)
            {
                seats[passes[i]] = true;
            }

            List<int> trios = new List<int>();
            for (int i = 0; i < 1022; i++)
            {
                if (seats[i] && !seats[i + 1] && seats[i + 2])
                {
                    trios.Add(i);
                }
            }

            for (int i = 0; i < trios.Count; i++)
            {
                Helper.Logger.Log(Name, $"trio: {trios[i]}, {trios[i] + 1}, {trios[i] + 2}");
            }
        }

        private List<int> GetPasses(IList<string> list)
        {
            List<int> passes = new List<int>();

            foreach (string s in list)
            {
                passes.Add(GetSeatId(s));
            }

            return passes;
        }
    }
}
