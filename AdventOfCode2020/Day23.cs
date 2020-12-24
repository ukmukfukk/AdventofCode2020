using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2020
{
    public class Day23 : DayUsingInputNames
    {
        private List<int> cups;
        public override IList<string> InputFiles => new List<string> { "inputcustom.txt", "input23test.txt", "input23.txt" };

        public override string Name => "Day 23";

        public List<int> Cups { get => cups; private set => cups = value; }

        public int Current { get; private set; }

        public string ListString => string.Join(", ", Cups);

        public string Window
        {
            get
            {
                int i = Cups.IndexOf(1);
                if (i < Cups.Count - 21 && i > 20)
                {
                    return string.Join(", ", Cups.Skip(i - 20).Take(40));
                }

                return string.Empty;
            }
        }

        public string List1String
        {
            get
            {
                return GetList1String(Cups);
            }
        }

        private string GetList1String(List<int> list)
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

        private List<int> GetList1(List<int> list)
        {
            int i = list.IndexOf(1);
            List<int> re = new List<int>(1);
            if (i < list.Count - 1)
            {
                re.AddRange(list.Skip(i + 1));
            }

            if (i > 0)
            {
                re.AddRange(list.Take(i));
            }

            return re;
        }

        public string GetOrder()
        {
            int i = Cups.IndexOf(1);
            string s = string.Empty;
            if (i < Cups.Count - 1)
            {
                s = string.Join(string.Empty, Cups.Skip(i + 1));
            }

            if (i > 0)
            {
                s += string.Join(string.Empty, Cups.Take(i));
            }

            return s;
        }

        protected override void SolvePuzzle1(string inputname, IList<string> input)
        {
            if (inputname == "inputcustom.txt")
            {
                return;
            }

            Helper.Logger = new ConsoleLogger(LogLevel.Error);
            Cups = input[0].Select(c => int.Parse(c.ToString())).ToList();
            var cupsarray = Cups.ToArray();
            Log($"start: {DateTime.Now}", LogLevel.Critical);
            for (int i = 0; i < 100; i++)
            {
                if (i == 10)
                {
                    Log($"{inputname} {GetList1String(cupsarray)}");
                    //Log($"{inputname} {GetOrder()}");
                }
                else
                {
                    Log($"{inputname} {GetList1String(cupsarray)}", LogLevel.Information);
                }
                int[] cnt = new int[10];
                for (int j = 0; j < cupsarray.Length; j++)
                {
                    cnt[cupsarray[j]]++;
                }

                for (int j = 0; j < cnt.Length; j++)
                {
                    if (cnt[j] > 1)
                    {
                        Log($"duplicate {j}, step {i}", LogLevel.Critical);
                    }
                }

                //MakeMove(ref cups);
                MakeMove2(ref cupsarray);
            }

            Log($"end: {DateTime.Now}", LogLevel.Critical);

            Log($"{inputname} {GetList1String(cupsarray)}", LogLevel.Critical);
            //Log($"{inputname} {GetOrder()}");
            Log(string.Empty, LogLevel.Critical);
        }

        private string GetList1String(int[] arr)
        {
            return GetList1String(arr.ToList());
        }

        protected override void SolvePuzzle2(string inputname, IList<string> input)
        {
            Helper.Logger = new ConsoleLogger(LogLevel.Error);
            Cups = input[0].Select(c => int.Parse(c.ToString())).ToList();
            if (inputname == "inputcustom.txt")
            {
                return;
            }

            Cups.AddRange(Enumerable.Range(Cups.Max(), 1_000_000 - Cups.Max()));
            var cupsarray = Cups.ToArray();

            int i = 0;
            //Tuple<int, List<int>> predict = Predict();
            while (i < 10000000)
            {
                MakeMove2(ref cupsarray);
                //if (predict.Item1 == i)
                //{

                //}

                if ((i % 1000) == 0)
                {
                    Log($"{i}. {inputname} {GetOrder2(cupsarray)}");
                }
                i++;
            }

            Log($"{inputname} {GetOrder2(cupsarray)}");
            Log(string.Empty, LogLevel.Critical);
        }

        private Tuple<int, List<int>> Predict()
        {
            int steps = 0;
            var cups = new List<int>(Cups);
            int i;

            for (steps = 0; steps < 4; steps++)
            {
                MakeMove(ref cups);
            }

            i = 0;
            var list1 = GetList1(cups);
            while (list1[i] < 10)
            {
                i++;
            }

            int m = list1[i] % 4;

            var newlist = new List<int>() { 1 };
            newlist.AddRange(list1.Take(i));
            var listrest = new List<int>();
            for (int j = 10; j <= cups.Max(); j++)
            {
                if (j % 4 == m)
                {
                    newlist.Add(j);
                    steps++;
                }
                else
                {
                    listrest.Add(j);
                }
            }

            int maxi = list1.IndexOf(list1.Max());

            newlist.AddRange(list1.Skip(maxi + 1).Take(9 - i - 1));
            newlist.AddRange(listrest);

            return new Tuple<int, List<int>>(steps - 1, newlist);
        }

        private void MakeMove(ref List<int> cups)
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

            cups = new List<int>(before);
            cups.Add(target);
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

        private string GetOrder2()
        {
            int i = Cups.IndexOf(1);
            if (i < Cups.Count - 2)
            {
                return Cups[i + 1] + ", " + Cups[i + 2];
            }

            if (i == Cups.Count - 1)
            {
                return Cups[0] + ", " + Cups[1];
            }

            return Cups.Last() + ", " + Cups.First();
        }

        private string GetOrder2(int[] arr)
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

        private void AddNextN(int n)
        {
            int max = Cups.Max();
            for (int i = max + 1; i <= max + n; i++)
            {
                Cups.Add(i);
            }
        }
    }
}
