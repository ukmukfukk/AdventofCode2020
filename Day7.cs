using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Day7Help;

namespace AdventOfCode2020
{
    public class Day7 : IDay
    {
        private List<string> colors; // in the end this was not used

        public IList<string> InputFiles => new List<string> { "input7.txt." };

        public string Name => "Day 7";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            SolvePuzzle1(inputs[0]);
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            SolvePuzzle2(inputs[0]);
        }

        private void SolvePuzzle1(IList<string> list)
        {
            List<BagRule> rules = BuildBagRulesAndFillColors(list);

            List<string> topColors = new List<string>();
            GetAllPossibleTopColors(topColors, "shiny gold", rules);

            Helper.Logger.Log(Name, $"Top colors count: {topColors.Count}");
        }

        private void GetAllPossibleTopColors(List<string> topColors, string color, List<BagRule> rules)
        {
            foreach (BagRule rule in rules)
            {
                if (rule.Rules.Any(r => r.Item2.Equals(color)))
                {
                    if (!topColors.Contains(rule.MainColor))
                    {
                        topColors.Add(rule.MainColor);
                        GetAllPossibleTopColors(topColors, rule.MainColor, rules);
                    }
                }
            }
        }

        private List<BagRule> BuildBagRulesAndFillColors(IList<string> list)
        {
            List<BagRule> retval = new List<BagRule>();
            string[] temp;
            string main;
            BagRule br;
            colors = new List<string>();

            foreach (string s in list)
            {
                temp = s.Split("contain");
                main = temp[0][0..^6];
                temp = temp[1].Split(',');
                br = new BagRule(main);
                AddColor(main);
                int n;
                string color;
                int len;

                foreach (string sub in temp)
                {
                    if (!sub.Contains("no other bags."))
                    {
                        n = int.Parse(sub.Trim()[0..1]);
                        len = 4;

                        if (sub.Contains("bags"))
                        {
                            len++;
                        }

                        if (sub.Contains('.'))
                        {
                            len++;
                        }

                        color = sub[3..^len];
                        AddColor(color);

                        br.AddSub(n, color);
                    }
                }

                retval.Add(br);
            }

            return retval;
        }

        private void AddColor(string color)
        {
            if (!colors.Contains(color))
            {
                colors.Add(color);
            }
        }

        private void SolvePuzzle2(IList<string> list)
        {
            List<BagRule> rules = BuildBagRulesAndFillColors(list);

            int countsub = CountSub("shiny gold", rules);

            Helper.Logger.Log(Name, $"Number of all sub bags: {countsub}");
        }

        private int CountSub(string color, List<BagRule> rules)
        {
            int cnt = 0;
            var rule = rules.Where(r => r.MainColor.Equals(color)).FirstOrDefault();
            if (rule != null)
            {
                foreach (var sub in rule.Rules)
                {
                    cnt += sub.Item1 * (1 + CountSub(sub.Item2, rules));
                }
            }

            return cnt;
        }
    }
}
