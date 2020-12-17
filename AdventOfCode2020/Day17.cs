using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day17 : SimpleDay
    {
        public override IList<string> InputFiles => new List<string> { "input17test.txt", "input17.txt" };

        public override string Name => "Day 17";

        protected override void SolvePuzzle1(IList<string> input)
        {
            bool[,,] cubes = new bool[input[0].Length, input.Count, 1];
            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    cubes[i, j, 0] = input[j][i] == '#';
                }
            }

            PrintCubes(cubes);

            for (int i = 0; i < 6; i++)
            {
                IterateCubes(ref cubes);
                PrintCubes(cubes);
            }

            int sum = 0;
            for (int z = 0; z < cubes.GetLength(2); z++)
            {
                for (int x = 0; x < cubes.GetLength(0); x++)
                {
                    for (int y = 0; y < cubes.GetLength(1); y++)
                    {
                        if (cubes[x, y, z])
                        {
                            sum++;
                        }
                    }
                }
            }

            Helper.Logger.Log(Name, $"cubes active: {sum}");
        }

        protected override void SolvePuzzle2(IList<string> input)
        {
            bool[,,,] cubes = new bool[input[0].Length, input.Count, 1, 1];
            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    cubes[i, j, 0, 0] = input[j][i] == '#';
                }
            }

            for (int i = 0; i < 6; i++)
            {
                IterateCubes4(ref cubes);
            }

            int sum = 0;
            for (int w = 0; w < cubes.GetLength(3); w++)
            {
                for (int z = 0; z < cubes.GetLength(2); z++)
                {
                    for (int x = 0; x < cubes.GetLength(0); x++)
                    {
                        for (int y = 0; y < cubes.GetLength(1); y++)
                        {
                            if (cubes[x, y, z, w])
                            {
                                sum++;
                            }
                        }
                    }
                }
            }

            Helper.Logger.Log(Name, $"cubes active: {sum}");
        }

        private void IterateCubes(ref bool[,,] cubes)
        {
            bool[,,] re = new bool[cubes.GetLength(0) + 2, cubes.GetLength(1) + 2, cubes.GetLength(2) + 2];
            bool active;
            int cnt;
            for (int z = 0; z < re.GetLength(2); z++)
            {
                for (int x = 0; x < re.GetLength(0); x++)
                {
                    for (int y = 0; y < re.GetLength(1); y++)
                    {
                        cnt = CountNeighbours(cubes, x - 1, y - 1, z - 1);
                        if (x > 0 && y > 0 && z > 0 && x < re.GetLength(0) - 1 && y < re.GetLength(1) - 1 && z < re.GetLength(2) - 1)
                        {
                            active = cubes[x - 1, y - 1, z - 1];
                        }
                        else
                        {
                            active = false;
                        }

                        re[x, y, z] = (active && cnt == 2) || cnt == 3;
                    }
                }
            }

            cubes = re;
        }

        private int CountNeighbours(bool[,,] cubes, int x, int y, int z)
        {
            int cnt = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (!(i == 0 && j == 0 && k == 0)
                            && x + i >= 0 && y + j >= 0 && z + k >= 0
                            && x + i < cubes.GetLength(0) && y + j < cubes.GetLength(1) && z + k < cubes.GetLength(2)
                            && cubes[x + i, y + j, z + k])
                        {
                            cnt++;
                        }
                    }
                }
            }

            return cnt;
        }

        private void PrintCubes(bool[,,] cubes)
        {
            for (int z = 0; z < cubes.GetLength(2); z++)
            {
                Console.WriteLine($"z={z}");
                for (int x = 0; x < cubes.GetLength(0); x++)
                {
                    for (int y = 0; y < cubes.GetLength(1); y++)
                    {
                        Console.Write(cubes[x, y, z] ? '#' : '.');
                    }

                    Console.WriteLine();
                }
            }
        }

        private void IterateCubes4(ref bool[,,,] cubes)
        {
            bool[,,,] re = new bool[cubes.GetLength(0) + 2, cubes.GetLength(1) + 2, cubes.GetLength(2) + 2, cubes.GetLength(3) + 2];
            bool active;
            int cnt;
            for (int w = 0; w < re.GetLength(3); w++)
            {
                for (int z = 0; z < re.GetLength(2); z++)
                {
                    for (int x = 0; x < re.GetLength(0); x++)
                    {
                        for (int y = 0; y < re.GetLength(1); y++)
                        {
                            cnt = CountNeighbours(cubes, x - 1, y - 1, z - 1, w - 1);
                            if (x > 0 && y > 0 && z > 0 && w > 0
                                && x < re.GetLength(0) - 1 && y < re.GetLength(1) - 1 && z < re.GetLength(2) - 1 && w < re.GetLength(3) - 1)
                            {
                                active = cubes[x - 1, y - 1, z - 1, w - 1];
                            }
                            else
                            {
                                active = false;
                            }

                            re[x, y, z, w] = (active && cnt == 2) || cnt == 3;
                        }
                    }
                }
            }

            cubes = re;
        }

        private int CountNeighbours(bool[,,,] cubes, int x, int y, int z, int w)
        {
            int cnt = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            if (!(i == 0 && j == 0 && k == 0 && l == 0)
                                && x + i >= 0 && y + j >= 0 && z + k >= 0 && w + l >= 0
                                && x + i < cubes.GetLength(0) && y + j < cubes.GetLength(1) && z + k < cubes.GetLength(2) && w + l < cubes.GetLength(3)
                                && cubes[x + i, y + j, z + k, w + l])
                            {
                                cnt++;
                            }
                        }
                    }
                }
            }

            return cnt;
        }
    }
}
