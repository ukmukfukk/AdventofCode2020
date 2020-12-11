using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class Day11 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input11test.txt", "input11.txt" };
        public string Name => "Day 11";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            foreach (IList<string> input in inputs)
            {
                SolvePuzzle1(input);
            }
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            foreach (IList<string> input in inputs)
            {
                SolvePuzzle2(input);
            }
        }

        private void SolvePuzzle2(IList<string> input)
        {
            char[,] seats = TurnToCharArray(input);
            bool debug = false;
            int rounds = 1;

            while (ApplyRules2(ref seats))
            {
                if (debug)
                {
                    Helper.Logger.Log(MakeString(seats));
                }

                rounds++;
            }

            long cnt = CountOccupied(seats);

            Helper.Logger.Log(Name, $"Seats occupied: {cnt}, rounds: {rounds}");
        }

        private void SolvePuzzle1(IList<string> input)
        {
            char[,] seats = TurnToCharArray(input);
            bool debug = false;
            int rounds = 1;

            while (ApplyRules(ref seats))
            {
                if (debug)
                {
                    Helper.Logger.Log(MakeString(seats));
                }

                rounds++;
            }

            long cnt = CountOccupied(seats);

            Helper.Logger.Log(Name, $"Seats occupied: {cnt}, rounds: {rounds}");
        }

        private bool ApplyRules2(ref char[,] seats)
        {
            bool hasChange = false;
            char[,] retval = new char[seats.GetLength(0), seats.GetLength(1)];

            for (int i = 0; i < seats.GetLength(0); i++)
            {
                for (int j = 0; j < seats.GetLength(1); j++)
                {
                    switch (seats[i, j])
                    {
                        case 'L':
                            if (CountNeighbours2(seats, i, j) == 0)
                            {
                                retval[i, j] = '#';
                                hasChange = true;
                            }
                            else
                            {
                                retval[i, j] = seats[i, j];
                            }

                            break;
                        case '#':
                            if (CountNeighbours2(seats, i, j) >= 5)
                            {
                                retval[i, j] = 'L';
                                hasChange = true;
                            }
                            else
                            {
                                retval[i, j] = seats[i, j];
                            }

                            break;
                        default: retval[i, j] = seats[i, j]; break;
                    }
                }
            }

            seats = retval;

            return hasChange;
        }

        private int CountNeighbours2(char[,] seats, int i, int j)
        {
            int cnt = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (!(x == 0 && y == 0) && CheckDirection(seats, i, j, x, y))
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }

        private bool CheckDirection(char[,] seats, int i, int j, int xdir, int ydir)
        {
            int newi = i + xdir;
            int newj = j + ydir;
            if (newi < 0 || newi >= seats.GetLength(0) || newj < 0 || newj >= seats.GetLength(1))
            {
                return false;
            }

            if (seats[newi, newj] == 'L')
            {
                return false;
            }

            if (seats[newi, newj] == '#')
            {
                return true;
            }

            return CheckDirection(seats, newi, newj, xdir, ydir);
        }

        private string MakeString(char[,] seats)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < seats.GetLength(1); i++)
            {
                for (int j = 0; j < seats.GetLength(0); j++)
                {
                    sb.Append(seats[j, i]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private long CountOccupied(char[,] seats)
        {
            int cnt = 0;

            for (int i = 0; i < seats.GetLength(0); i++)
            {
                for (int j = 0; j < seats.GetLength(1); j++)
                {
                    if (seats[i, j] == '#')
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }

        private bool ApplyRules(ref char[,] seats)
        {
            bool hasChange = false;
            char[,] retval = new char[seats.GetLength(0), seats.GetLength(1)];

            for (int i = 0; i < seats.GetLength(0); i++)
            {
                for (int j = 0; j < seats.GetLength(1); j++)
                {
                    switch (seats[i, j])
                    {
                        case 'L':
                            if (CountNeighbours(seats, i, j) == 0)
                            {
                                retval[i, j] = '#';
                                hasChange = true;
                            }
                            else
                            {
                                retval[i, j] = seats[i, j];
                            }

                            break;
                        case '#':
                            if (CountNeighbours(seats, i, j) >= 4)
                            {
                                retval[i, j] = 'L';
                                hasChange = true;
                            }
                            else
                            {
                                retval[i, j] = seats[i, j];
                            }

                            break;
                        default: retval[i, j] = seats[i, j]; break;
                    }
                }
            }

            seats = retval;

            return hasChange;
        }

        private int CountNeighbours(char[,] seats, int i, int j)
        {
            int cnt = 0;

            for (int x = i - 1; x <= i + 1; x++)
            {
                for (int y = j - 1; y <= j + 1; y++)
                {
                    if (x >= 0 && x < seats.GetLength(0) && y >= 0 && y < seats.GetLength(1) && !(x == i && y == j) && seats[x, y] == '#')
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }

        private char[,] TurnToCharArray(IList<string> input)
        {
            char[,] retval = new char[input[0].Length, input.Count];
            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    retval[i, j] = input[j][i];
                }
            }

            return retval;
        }
    }
}
