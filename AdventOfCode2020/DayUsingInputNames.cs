using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public abstract class DayUsingInputNames : LoggingDay
    {
        public override void SolvePuzzle1(IList<IList<string>> inputs)
        {
            throw new Exception("These days we expect dictionaries");
        }

        public override void SolvePuzzle2(IList<IList<string>> inputs)
        {
            throw new Exception("These days we expect dictionaries");
        }

        public virtual void SolvePuzzle1(Dictionary<string, IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle1(input.Key, input.Value);
            }
        }

        public virtual void SolvePuzzle2(Dictionary<string, IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle2(input.Key, input.Value);
            }
        }

        protected abstract void SolvePuzzle1(string inputname, IList<string> input);

        protected abstract void SolvePuzzle2(string inputname, IList<string> input);
    }
}
