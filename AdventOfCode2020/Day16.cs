using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Day16Help;

namespace AdventOfCode2020
{
    public class Day16 : SimpleDay
    {
        public override IList<string> InputFiles => new List<string> { "input16test.txt", "input16test2.txt", "input16.txt" };

        public override string Name => "Day 16";

        public override void SolvePuzzle2(IList<IList<string>> inputs)
        {
            for (int i = 1; i < inputs.Count; i++)
            {
                SolvePuzzle2(inputs[i]);
            }
        }

        protected override void SolvePuzzle1(IList<string> input)
        {
            int i = GenerateTicketRules(input, out List<TicketRule> rules);

            i++;
            if (input[i].Contains("your ticket:"))
            {
                i += 2;
            }

            string line;
            long suminvalid = 0;

            for (; i < input.Count; i++)
            {
                line = input[i];
                if (!string.IsNullOrWhiteSpace(line) && !line.Contains("nearby tickets:"))
                {
                    int lineinvalid = LineInvalidSum(rules, line);
                    if (lineinvalid != -1)
                    {
                        suminvalid += lineinvalid;
                    }
                }
            }

            Helper.Logger.Log(Name, $"sum: {suminvalid}");
        }

        protected override void SolvePuzzle2(IList<string> input)
        {
            int i = GenerateTicketRules(input, out List<TicketRule> rules);
            List<int> invalidLines = GetInvalidLines(rules, input, i);

            int r = 0;
            while (r < rules.Count)
            {
                for (int n = 0; n < rules.Count; n++)
                {
                    bool orderok = true;
                    for (int j = i; j < input.Count; j++)
                    {
                        if (!string.IsNullOrWhiteSpace(input[j]) && !input[j].Contains("nearby tickets:") && !input[j].Contains("your ticket:") && !invalidLines.Contains(j))
                        {
                            int num = int.Parse(input[j].Split(',')[n]);
                            if (!rules[r].Check(num))
                            {
                                orderok = false;
                                break;
                            }
                        }
                    }

                    if (orderok)
                    {
                        rules[r].Orders.Add(n);
                    }
                }

                r++;
            }

            Dictionary<TicketRule, int> ruleorder = new Dictionary<TicketRule, int>();
            for (int k = 1; k <= rules.Count; k++)
            {
                TicketRule rule = rules.First(r => r.Orders.Count == k);
                ruleorder.Add(rule, rule.Orders.First(o => !ruleorder.Values.Contains(o)));
            }

            long multi = 1;
            i += 2;
            var yourticket = input[i].Split(',').Select(s => int.Parse(s)).ToList();
            foreach (var ro in ruleorder.Where(r => r.Key.Field.StartsWith("departure")))
            {
                multi *= yourticket[ro.Value];
            }

            Helper.Logger.Log(Name, $"mutli: {multi}");
        }

        private List<int> GetInvalidLines(List<TicketRule> rules, IList<string> input, int i)
        {
            List<int> retval = new List<int>();
            int j = i + 1;
            if (input[j].Contains("your ticket:"))
            {
                j += 2;
            }

            string line;
            for (; j < input.Count; j++)
            {
                line = input[j];
                if (!string.IsNullOrWhiteSpace(line) && !line.Contains("nearby tickets:"))
                {
                    int lineinvalid = LineInvalidSum(rules, line);
                    if (lineinvalid != -1)
                    {
                        retval.Add(j);
                    }
                }
            }

            return retval;
        }

        private int GenerateTicketRules(IList<string> input, out List<TicketRule> ticketRules)
        {
            int i = 0;
            string field;
            string[] split;
            ticketRules = new List<TicketRule>();
            while (!string.IsNullOrWhiteSpace(input[i]))
            {
                split = input[i].Split(':');
                field = split[0];
                split = split[1].Split("or");
                ticketRules.Add(new TicketRule(field, split));
                i++;
            }

            return i;
        }

        private int LineInvalidSum(List<TicketRule> ticketRules, string line)
        {
            int linewrong = 0;
            bool valid = true;
            var values = line.Split(',').Select(s => int.Parse(s)).ToList();
            for (int j = 0; j < values.Count(); j++)
            {
                int r = 0;
                while (r < ticketRules.Count && !ticketRules[r].Check(values[j]))
                {
                    r++;
                }

                if (r == ticketRules.Count)
                {
                    linewrong += values[j];
                    valid = false;
                }
            }

            if (valid)
            {
                return -1;
            }

            return linewrong;
        }
    }
}
