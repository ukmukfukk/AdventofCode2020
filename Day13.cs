using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            // SolvePuzzle2Alternative(inputs[2]);
            foreach (var input in inputs)
            {
                SolvePuzzle2Alternative(input);
                SolvePuzzle2(input);
            }
        }

        private void SolvePuzzle2Alternative(IList<string> input)
        {
            string[] split = input[1].Split(',');
            Dictionary<int, int> dividers = new Dictionary<int, int>();
            for (int i = 0; i < split.Length; i++)
            {
                if (int.TryParse(split[i], out int div))
                {
                    dividers.Add(div, i);
                }
            }

            long prod = 1;
            foreach (var k in dividers.Keys)
            {
                prod *= k;
            }

            List<int> mods;
            long timestamp = 0;
            bool found = false;
            long step = 100000000;
            long milestone = 0;
            Dictionary<long, List<int>> modClasses = new Dictionary<long, List<int>>();

            while (!found)
            {
                if (timestamp > step + milestone)
                {
                    Helper.Logger.Log(timestamp.ToString());
                    milestone += step;
                }

                if (modClasses.ContainsKey(timestamp % prod))
                {
                    timestamp++;
                    break;
                }

                mods = new List<int>();
                foreach (var k in dividers)
                {
                    int mod = k.Key - (int)(timestamp % k.Key);
                    mod %= k.Key;
                    mods.Add(mod);
                }

                found = true;
                int n = 0;
                foreach (var k in dividers)
                {
                    if (mods[n] != k.Value)
                    {
                        found = false;
                        break;
                    }

                    n++;
                }

                if (!found)
                {
                    modClasses.Add(timestamp, mods);
                    timestamp++;
                }
            }

            if (input.Count > 2)
            {
                long expected = long.Parse(input[2]);
                Helper.Logger.Log("Name", $"FirstTimestamp: {timestamp}, {(expected == timestamp ? "TEST PASSED" : "TEST FAILED")}");
            }
            else
            {
                Helper.Logger.Log("Name", $"FirstTimestamp: {timestamp}");
            }
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
            /*retval = 0;
             * while (retval < ts)
            {
                retval += bus;
            }*/

            long div = ts / bus;
            retval = (div + 1) * bus;

            return retval;
        }

        private void SolvePuzzle2(IList<string> input)
        {
            string[] split = input[1].Split(',');
            var buses = split.Select(s => int.TryParse(s, out int i) ? i : -1).ToList();

            long timestamp = 0;
            bool found = false;
            long step = 100000000;
            long milestone = 0;

            while (!found)
            {
                if (timestamp > step + milestone)
                {
                    Helper.Logger.Log(timestamp.ToString());
                    milestone += step;
                }

                found = CheckTS(buses, ref timestamp);

                if (!found)
                {
                    timestamp++;
                }
            }

            if (input.Count > 2)
            {
                long expected = long.Parse(input[2]);
                Helper.Logger.Log("Name", $"FirstTimestamp: {timestamp}, {(expected == timestamp ? "TEST PASSED" : "TEST FAILED")}");
            }
            else
            {
                Helper.Logger.Log("Name", $"FirstTimestamp: {timestamp}");
            }
        }

        private bool CheckTS(List<int> buses, ref long timestamp)
        {
            bool found;
            timestamp = FindDepart(buses[0], timestamp);
            int b = 1;
            long runner = timestamp;
            found = true;
            int diff = 1;

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
