using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode2020.Day18Help;

namespace AdventOfCode2020
{
    public class Day18 : SimpleDay
    {
        public override IList<string> InputFiles => new List<string> { "input18test.txt", "input18.txt" };

        public override string Name => "Day 18";

        protected override void SolvePuzzle1(IList<string> input)
        {
            var expressions = new List<Expression>();
            Regex whitespace = new Regex(@"\s*");
            foreach (string s in input)
            {
                expressions.Add(new Expression(whitespace.Replace(s, string.Empty)));
            }

            long sum = 0;
            foreach (var e in expressions)
            {
                long value = e.Evaluate();
                sum += value;
                if (e.Expected == null)
                {
                    Helper.Logger.Log(Name, $"e:{e}    Evaluated to {value}");
                }
                else
                {
                    Helper.Logger.Log(Name, $"e:{e}    Evaluated to {value}, {(value == e.Expected.Value ? "Pass" : "Fail")}");
                }
            }

            Helper.Logger.Log(Name, $"sum: {sum}");
        }

        protected override void SolvePuzzle2(IList<string> input)
        {
            var expressions = new List<ExpressionPrecedence>();
            Regex whitespace = new Regex(@"\s*");
            foreach (string s in input)
            {
                expressions.Add(new ExpressionPrecedence(whitespace.Replace(s, string.Empty)));
            }

            long sum = 0;
            foreach (var e in expressions)
            {
                long value = e.Evaluate();
                sum += value;
                if (e.Expected == null)
                {
                    Helper.Logger.Log(Name, $"e:{e}    Evaluated to {value}");
                }
                else
                {
                    Helper.Logger.Log(Name, $"e:{e}    Evaluated to {value}, {(value == e.Expected.Value ? "Pass" : "Fail")}");
                }
            }

            Helper.Logger.Log(Name, $"sum: {sum}");
        }
    }
}
