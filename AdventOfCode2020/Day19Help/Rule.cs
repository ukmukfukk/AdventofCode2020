using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day19Help
{
    public class Rule
    {
        private string pattern;

        public Rule(int id, string s)
        {
            Id = id;
            var m = Helper.Letters.Match(s);
            if (m.Success)
            {
                Term = m.Value;
            }
            else

            if (s.Contains('|'))
            {
                RuleIsOr = true;
                SubRules = new List<Rule>();
                foreach (string s2 in s.Split('|'))
                {
                    SubRules.Add(new Rule(id, s2));
                }
            }
            else
            {
                if (s.Contains(' '))
                {
                    RuleList = s.Trim().Split(' ').Select(s => int.Parse(s)).ToList();
                }
                else
                {
                    SubRuleId = int.Parse(s.Trim());
                }
            }
        }

        public static Rule[] Rules { get; set; }

        public bool RuleIsOr { get; private set; }

        public List<Rule> SubRules { get; private set; }

        public List<int> RuleList { get; private set; }

        public int? SubRuleId { get; private set; }

        public string Term { get; private set; }

        public bool HasTerm => !string.IsNullOrEmpty(Term);

        public int Id { get; }

        public string Pattern
        {
            get
            {
                if (pattern == null)
                {
                    if (HasTerm)
                    {
                        return pattern = Term;
                    }

                    if (Helper.IsNotNull(SubRuleId, out int id))
                    {
                        return pattern = Rules[id].Pattern;
                    }

                    if (RuleIsOr)
                    {
                        if (Id == 11 || Id == 8)
                        {
                            return pattern = SubRules[1].Pattern;
                        }

                        string sub = "(?:";
                        foreach (Rule rule in SubRules)
                        {
                            sub += rule.Pattern + "|";
                        }

                        return pattern = sub[..^1] + ")";
                    }

                    if (RuleList != null && RuleList.Count > 0)
                    {
                        if (RuleList.Contains(Id))
                        {
                            List<int> beforelist = new List<int>();
                            List<int> afterlist = new List<int>();

                            bool isbefore = true;
                            for (int i = 0; i < RuleList.Count; i++)
                            {
                                if (RuleList[i] == Id)
                                {
                                    isbefore = false;
                                }
                                else
                                {
                                    if (isbefore)
                                    {
                                        beforelist.Add(RuleList[i]);
                                    }
                                    else
                                    {
                                        afterlist.Add(RuleList[i]);
                                    }
                                }
                            }

                            string before = string.Empty;
                            string after = string.Empty;
                            if (beforelist.Count > 0)
                            {
                                before = "(";
                                for (int i = 0; i < beforelist.Count; i++)
                                {
                                    before += Rules[beforelist[i]].Pattern;
                                }

                                before += ")+";
                            }

                            if (afterlist.Count > 0)
                            {
                                after = "(";
                                for (int i = 0; i < afterlist.Count; i++)
                                {
                                    after += Rules[afterlist[i]].Pattern;
                                }

                                after += ")+";
                            }

                            return pattern = before + after;
                        }
                        else
                        {
                            string sub = string.Empty;
                            for (int i = 0; i < RuleList.Count; i++)
                            {
                                sub += Rules[RuleList[i]].Pattern;
                            }

                            return pattern = sub;
                        }
                    }
                }

                return pattern;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (RuleIsOr)
            {
                return string.Join(" | ", values: SubRules.Select(sr => sr.ToString()));
            }

            if (RuleList != null)
            {
                return string.Join(" ", RuleList);
            }

            if (SubRuleId != null)
            {
                return SubRuleId.ToString();
            }

            if (Term != null)
            {
                return Term;
            }

            return string.Empty;
        }
    }
}
