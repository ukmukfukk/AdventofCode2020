using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day25 : DayUsingInputNames
    {
        public override IList<string> InputFiles => new List<string> { "input25test.txt", "input25.txt" };

        public override string Name => "Day 25";

        protected override void SolvePuzzle1(string inputname, IList<string> input)
        {
            long key1 = long.Parse(input[0]);
            long key2 = long.Parse(input[1]);

            int subloop = TryAndGetLoopAndKey(7, 100000000, key1);
            if (subloop == 0)
            {
                Log($"{inputname} key1 not found");
            }
            else
            {
                Log($"{inputname} key1 loops: {subloop}");
            }

            subloop = TryAndGetLoopAndKey(7, 100000000, key2);
            if (subloop == 0)
            {
                Log($"{inputname} key2 not found");
            }
            else
            {
                Log($"{inputname} key2 loops: {subloop}");
            }

            var ek = GeneratePK(key1, subloop);

            Log($"{inputname} ek: {ek}");
        }

        protected override void SolvePuzzle2(string inputname, IList<string> input)
        {
            throw new NotImplementedException();
        }

        private int TryAndGetLoopAndKey(int sub, int maxloop, long key)
        {
            long re = 1;

            for (int i = 1; i <= maxloop; i++)
            {
                re = (re * sub) % 20201227;
                if (re == key)
                {
                    return i;
                }
            }

            return 0;
        }

        private long GeneratePK(long subject, int loops)
        {
            long re = 1;
            for (int i = 0; i < loops; i++)
            {
                re = (re * subject) % 20201227;
            }

            return re;
        }
    }
}
