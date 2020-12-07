using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day7Help
{
    public class BagRule
    {
        public BagRule(string main)
        {
            MainColor = main;
            Rules = new List<Tuple<int, string>>();
        }

        public string MainColor { get; }

        public List<Tuple<int, string>> Rules { get; }

        internal void AddSub(int n, string color)
        {
            Rules.Add(new Tuple<int, string>(n, color));
        }
    }
}
