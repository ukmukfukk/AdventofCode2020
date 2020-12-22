using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode2020.Day19Help;

namespace AdventOfCode2020
{
    public class Day19 : SimpleDay
    {
        public override IList<string> InputFiles => new List<string> { "input19test.txt", "input19.txt", "input19test2.txt", "input19test3.txt", "input19_2.txt" };

        public override string Name => "Day 19";

        public override void SolvePuzzle1(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle1(input);
            }
        }

        public Rule[] GetRules(IList<string> input, int el, int rulecnt)
        {
            Rule[] retval = new Rule[rulecnt + 1];
            for (int i = 0; i < el; i++)
            {
                string[] split = input[i].Split(':');
                int id = int.Parse(split[0]);
                retval[id] = new Rule(id, split[1]);
            }

            return retval;
        }

        public void GetEmptyLineAndRuleCnt(IList<string> input, out int el, out int rulecnt)
        {
            rulecnt = 0;
            for (el = 0; !Helper.EmptyLine.IsMatch(input[el]); el++)
            {
                int temp = int.Parse(input[el].Split(':')[0]);
                if (temp > rulecnt)
                {
                    rulecnt = temp;
                }
            }
        }

        protected override void SolvePuzzle1(IList<string> input)
        {
            GetEmptyLineAndRuleCnt(input, out int el, out int rulecnt);
            Rule.Rules = GetRules(input, el, rulecnt);
            Regex regex = new Regex("^" + Rule.Rules[0].Pattern + "$");
            int validcnt = 0;
            for (int i = el + 1; i < input.Count; i++)
            {
                string s = input[i];
                bool? valid = null;
                if (s.Contains(':'))
                {
                    string[] split = s.Split(':');
                    s = split[0];
                    valid = bool.Parse(split[1]);
                }

                Match match = regex.Match(s);
                bool ismatch = match.Groups.Count == 1 ? match.Success :
                    match.Groups[3].Captures.Count > 0 &&
                    match.Groups[1].Captures.Count + match.Groups[2].Captures.Count - match.Groups[3].Captures.Count >= 1;

                if (ismatch)
                {
                    validcnt++;
                }

                if (valid.HasValue)
                {
                    Helper.Logger.Log(Name, $"{s}: {ismatch}, {(ismatch == valid.Value ? "Pass" : "Fail")}");
                }
                else
                {
                    Helper.Logger.Log(Name, $"{s}: {ismatch}");
                }
            }

            Helper.Logger.Log(Name, $"validcnt: {validcnt}{Environment.NewLine}{Environment.NewLine}");
        }

        protected override void SolvePuzzle2(IList<string> input)
        {
            SolvePuzzle1(input);
        }
    }
}
