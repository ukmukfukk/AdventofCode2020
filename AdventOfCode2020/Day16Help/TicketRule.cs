using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Day16Help
{
    internal class TicketRule
    {
        public TicketRule(string field, string[] split)
        {
            Field = field;
            Orders = new List<int>();
            Rules = new List<Tuple<int, int>>();
            string[] splitd;
            foreach (string s in split)
            {
                splitd = s.Trim().Split('-');
                Rules.Add(new Tuple<int, int>(int.Parse(splitd[0]), int.Parse(splitd[1])));
            }
        }

        public string Field { get; }

        public IList<Tuple<int, int>> Rules { get; private set; }

        public List<int> Orders { get; set; }

        internal bool Check(int value)
        {
            for (int i = 0; i < Rules.Count; i++)
            {
                if (value >= Rules[i].Item1 && value <= Rules[i].Item2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
