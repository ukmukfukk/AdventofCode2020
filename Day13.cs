using System.Collections.Generic;
using System.Linq;

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
            Dictionary<long, List<int>> modClasses = new Dictionary<long, List<int>>();

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
                Helper.Logger.Log("Name", $"FirstTimestamp: {timestamp}, {(expected == timestamp ? "TEST PASSED" : "TEST FAILED")}");
            }
            else
            {
                Helper.Logger.Log("Name", $"FirstTimestamp: {timestamp}");
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
            /*retval = 0;
             * while (retval < ts)
            {
                retval += bus;
            }*/

            long div = ts / bus;
            retval = (div + 1) * bus;

            return retval;
        }

        private void SolvePuzzle2(IList<string> input, long start = 0)
        {
            string[] split = input[1].Split(',');
            var buses = split.Select(s => int.TryParse(s, out int i) ? i : -1).ToList();

            long timestamp = start;
            bool found = false;
            long step = 1000000000;
            long milestone = start;

            while (!found)
            {
                if (timestamp > step + milestone)
                {
                    Helper.Logger.Log(timestamp.ToString());
                    milestone += step;
                }

                found = CheckTS(buses, timestamp);

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

        private bool CheckTS(List<int> buses, long timestamp)
        {
            bool found;
            long runner = FindDepart(buses[0], timestamp);
            int b = 1;
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
