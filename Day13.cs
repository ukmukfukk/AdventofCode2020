using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day13 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input13test.txt", "input13test21.txt", "input13test22.txt", "input13test23.txt", "input13test24.txt", "input13test25.txt", "input13.txt" };

        public string Name => "Day 13";

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
                SolvePuzzle2Alternative(input);
                SolvePuzzle2(input, input[0] == "1008169" ? 100000000000000 : 0);
            }
        }

        private void SolvePuzzle2Alternative(IList<string> input)
        {
            string[] split = input[1].Split(',');
            List<int> dividers = new List<int>();
            List<int> targetmods = new List<int>();
            for (int i = 0; i < split.Length; i++)
            {
                if (int.TryParse(split[i], out int divider))
                {
                    dividers.Add(divider);
                    int mod = i == 0 ? 0 : divider - i;
                    while (mod < 0)
                    {
                        mod += divider;
                    }

                    targetmods.Add(mod);
                }
            }

            long timestamp = 0;
            for (int i = 0; i < dividers.Count; i++)
            {
                long product = GetProductForFirstN(dividers, i);

                while (timestamp % dividers[i] != targetmods[i])
                {
                    timestamp += product;
                }
            }

            if (input.Count > 2)
            {
                long expected = long.Parse(input[2]);
                Helper.Logger.Log(Name, $"FirstTimestamp: {timestamp}, {(expected == timestamp ? "TEST PASSED" : "TEST FAILED")}");
            }
            else
            {
                Helper.Logger.Log(Name, $"FirstTimestamp: {timestamp}");
            }
        }

        private long GetProductForFirstN(List<int> dividers, int n)
        {
            long prod = 1;
            for (int i = 0; i < n; i++)
            {
                prod *= dividers[i];
            }

            return prod;
        }

        private void SolvePuzzle1(IList<string> input)
        {
            long timestamp = long.Parse(input[0]);
            string[] split = input[1].Split(',');
            var buses = split.Where(s => s != "x").Select(s => int.Parse(s)).ToList();

            long[] departs = GetDeparts(buses, ref timestamp);

            long dep = departs.Min();
            int b = 0;
            while (b < buses.Count)
            {
                if (departs[b] == dep)
                {
                    break;
                }

                b++;
            }

            Helper.Logger.Log(Name, $"BusId: {buses[b]}, departs: {dep}, solution:{(dep - timestamp) * buses[b]}");
        }

        private long[] GetDeparts(IList<int> buses, ref long ts)
        {
            var retval = new long[buses.Count()];

            for (int i = 0; i < buses.Count(); i++)
            {
                retval[i] = FindDepart(buses[i], ts);
            }

            return retval;
        }

        private long FindDepart(int bus, long ts)
        {
            long retval;
            long div = ts / bus;
            retval = (div + 1) * bus;

            return retval;
        }

        private void SolvePuzzle2(IList<string> input, long start = 0)
        {
            string[] split = input[1].Split(',');
            var buses = split.Select(s => int.TryParse(s, out int i) ? i : -1).ToList();
            int taskcnt = 8;
            long timestamp = start;
            long step = 1000000000;
            long range = 1000000000;
            long milestone = start;
            List<Task<long>> tasks = new List<Task<long>>();
            long[] results = new long[taskcnt];
            for (int i = 0; i < taskcnt; i++)
            {
                object param = new Tuple<List<int>, long, long>(buses, timestamp, range);
                tasks.Add(Task.Factory.StartNew(new Func<object, long>(CheckTSRange), param));
                timestamp += range;
            }

            while (true)
            {
                if (timestamp > step + milestone)
                {
                    Helper.Logger.Log(timestamp.ToString());
                    milestone += step;
                }

                int? id = GetFinishedTask(tasks);
                if (id != null)
                {
                    if (tasks[id.Value].Result != -1)
                    {
                        timestamp = tasks[id.Value].Result;
                        for (int i = 0; i < taskcnt; i++)
                        {
                            while (i != id && !tasks[i].IsCompleted)
                            {
                                tasks[i].Wait();
                            }

                            results[i] = tasks[i].Result;
                        }

                        timestamp = results.Where(r => r != -1).Min();
                        break;
                    }

                    object param = new Tuple<List<int>, long, long>(buses, timestamp, range);
                    tasks[id.Value] = Task.Factory.StartNew(new Func<object, long>(CheckTSRange), param);
                    timestamp += range;
                }

                Thread.Sleep(100);
            }

            if (input.Count > 2)
            {
                long expected = long.Parse(input[2]);
                Helper.Logger.Log(Name, $"FirstTimestamp: {timestamp}, {(expected == timestamp ? "TEST PASSED" : "TEST FAILED")}");
            }
            else
            {
                Helper.Logger.Log(Name, $"FirstTimestamp: {timestamp}");
            }
        }

        private int? GetFinishedTask(List<Task<long>> tasks)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].IsCompleted)
                {
                    return i;
                }
            }

            return null;
        }

        private long CheckTSRange(object param)
        {
            var paramT = (Tuple<List<int>, long, long>)param;
            var buses = paramT.Item1;
            var timestamp = paramT.Item2;
            var range = paramT.Item3;
            long retval = timestamp;
            while (retval < timestamp + range)
            {
                if (CheckTS(buses, ref retval))
                {
                    return retval;
                }
            }

            return -1;
        }

        private bool CheckTS(List<int> buses, ref long timestamp)
        {
            bool found;
            timestamp = FindDepart(buses[0], timestamp);
            int b = 1;
            found = true;
            int diff = 1;
            long runner = timestamp;
            while (found && b < buses.Count)
            {
                if (buses[b] == -1)
                {
                    diff++;
                }
                else
                {
                    long temp = FindDepart(buses[b], runner);
                    if (temp - runner != diff)
                    {
                        found = false;
                        break;
                    }

                    diff = 1;
                    runner = temp;
                }

                b++;
            }

            return found;
        }
    }
}
