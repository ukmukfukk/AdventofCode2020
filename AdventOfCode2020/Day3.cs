using System.Collections.Generic;

namespace AdventOfCode2020
{
    internal class Day3 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input3.txt" };

        public string Name => "Day 3";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            int xdim = inputs[0][0].Length;
            int ydim = inputs[0].Count;
            bool[,] map = new bool[xdim, ydim];

            for (int j = 0; j < ydim; j++)
            {
                for (int i = 0; i < xdim; i++)
                {
                    map[i, j] = inputs[0][j][i] == '#';
                }
            }

            int count = CountSlopes(map, 3, 1);

            Helper.Logger.Log(Name, $"{count} trees");
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            int xdim = inputs[0][0].Length;
            int ydim = inputs[0].Count;
            bool[,] map = new bool[xdim, ydim];

            for (int j = 0; j < ydim; j++)
            {
                for (int i = 0; i < xdim; i++)
                {
                    map[i, j] = inputs[0][j][i] == '#';
                }
            }

            List<int> count = new List<int>
            {
                CountSlopes(map, 1, 1),
                CountSlopes(map, 3, 1),
                CountSlopes(map, 5, 1),
                CountSlopes(map, 7, 1),
                CountSlopes(map, 1, 2),
            };

            long multi = 1;
            foreach (int c in count)
            {
                multi *= c;
            }

            Helper.Logger.Log(Name, $"{multi} trees");
        }

        private int CountSlopes(bool[,] map, int xstep, int ystep)
        {
            int xdim = map.GetLength(0);
            int ydim = map.GetLength(1);
            int count = 0;
            int x = 0;
            int y = 0;

            while (y < ydim)
            {
                if (map[x, y])
                {
                    count++;
                }

                x = (x + xstep) % xdim;
                y += ystep;
            }

            return count;
        }
    }
}
