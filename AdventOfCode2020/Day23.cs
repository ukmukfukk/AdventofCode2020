using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2020
{
    public class Day23 : DayUsingInputNames
    {
        public override IList<string> InputFiles => new List<string> { "input23test.txt", "input23.txt" };

        public override string Name => "Day 23";

        public int Current { get; private set; }

        public string GetList1String(int[] arr)
        {
            return GetList1String(arr.ToList());
        }

        public string GetList1String(List<int> list)
        {
            int i = list.IndexOf(1);
            string s = "1, ";
            if (i < list.Count - 1)
            {
                s += string.Join(", ", list.Skip(i + 1));
                s += ", ";
            }

            if (i > 0)
            {
                s += string.Join(", ", list.Take(i));
            }

            return s;
        }

        public string GetOrder2(int[] arr)
        {
            int i = 0;
            while (arr[i] != 1)
            {
                i++;
            }

            if (i < arr.Length - 2)
            {
                return arr[i + 1] + ", " + arr[i + 2];
            }

            if (i == arr.Length - 1)
            {
                return arr[0] + ", " + arr[1];
            }

            return arr.Last() + ", " + arr.First();
        }

        protected override void SolvePuzzle1(string inputname, IList<string> input)
        {
            Helper.Logger = new ConsoleLogger(LogLevel.Error);
            var cupsarray = input[0].Select(c => int.Parse(c.ToString())).ToArray();
            Log($"start: {DateTime.Now}", LogLevel.Critical);
            bool alter = false;
            string result = string.Empty;
            if (alter)
            {
                throw new NotImplementedException();
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i == 10)
                    {
                        Log($"{inputname} {GetList1String(cupsarray)}");
                    }
                    else
                    {
                        Log($"{inputname} {GetList1String(cupsarray)}", LogLevel.Information);
                    }

                    MakeMove2(ref cupsarray);
                }

                result = GetList1String(cupsarray);
            }

            Log($"end: {DateTime.Now}", LogLevel.Critical);
            Log($"{inputname} {result}", LogLevel.Critical);
            Log(string.Empty, LogLevel.Critical);
        }

        protected override void SolvePuzzle2(string inputname, IList<string> input)
        {
            Helper.Logger = new ConsoleLogger(LogLevel.Error);
            var cups = input[0].Select(c => int.Parse(c.ToString())).ToList();
            cups.AddRange(Enumerable.Range(cups.Max(), 1_000_000 - cups.Max()));
            var cupsarray = cups.ToArray();
            bool alter = false;
            string result = string.Empty;
            if (alter)
            {
                throw new NotImplementedException();
            }
            else
            {
                int i = 0;
                while (i < 10000000)
                {
                    MakeMove2(ref cupsarray);
                    if ((i % 1000) == 0)
                    {
                        Log($"{i}. {inputname} {GetOrder2(cupsarray)}");
                    }

                    i++;
                }

                result = GetOrder2(cupsarray);
            }

            Log($"{inputname} {result}");
            Log(string.Empty, LogLevel.Critical);
        }

        protected void MakeMove(ref List<int> cups)
        {
            var taken = cups.Skip(1).Take(3);
            int current = cups[0];
            int target = current - 1;

            var possibleTargets = cups.Skip(4);
            while (target > 0)
            {
                if (possibleTargets.Contains(target))
                {
                    break;
                }

                target--;
            }

            if (target == 0)
            {
                target = possibleTargets.Max();
            }

            IEnumerable<int> before;
            IEnumerable<int> after;
            int ti = cups.IndexOf(target);
            if (ti > 4)
            {
                before = cups.Skip(4).Take(ti - 4);
            }
            else
            {
                before = new List<int>();
            }

            if (ti < cups.Count - 1)
            {
                after = cups.Skip(ti + 1);
            }
            else
            {
                after = new List<int>();
            }

            cups = new List<int>(before)
            {
                target,
            };
            cups.AddRange(taken);
            cups.AddRange(after);
            cups.Add(current);
        }

        private void MakeMove2(ref int[] cupsa)
        {
            var taken = cupsa[1..4];
            int currentv = cupsa[0];
            int target = currentv - 1;

            int maxv = 0;
            int? smaller = null;
            int targeti = 0;
            int maxi = 0;
            int i;

            for (i = 4; i < cupsa.Length; i++)
            {
                if (cupsa[i] == target)
                {
                    targeti = i;
                    break;
                }

                if (cupsa[i] < target)
                {
                    if (smaller != null)
                    {
                        if (cupsa[i] > smaller.Value)
                        {
                            smaller = cupsa[i];
                            targeti = i;
                        }
                    }
                    else
                    {
                        smaller = cupsa[i];
                        targeti = i;
                    }
                }

                if (targeti == 0)
                {
                    if (cupsa[i] > maxv)
                    {
                        maxv = cupsa[i];
                        maxi = i;
                    }
                }
            }

            if (targeti == 0)
            {
                targeti = maxi;
            }

            if (targeti == cupsa.Length - 1)
            {
                for (i = 4; i < cupsa.Length; i++)
                {
                    cupsa[i - 4] = cupsa[i];
                }

                cupsa[i - 4] = taken[0];
                cupsa[i - 3] = taken[1];
                cupsa[i - 2] = taken[2];
                cupsa[i - 1] = currentv;

                return;
            }

            for (i = 4; i < cupsa.Length - 1; i++)
            {
                if (i <= targeti)
                {
                    cupsa[i - 4] = cupsa[i];
                }

                if (i == targeti)
                {
                    cupsa[i - 3] = taken[0];
                    cupsa[i - 2] = taken[1];
                    cupsa[i - 1] = taken[2];
                }

                if (i >= targeti)
                {
                    cupsa[i] = cupsa[i + 1];
                }
            }

            cupsa[i] = currentv;
        }
    }
}
