using System.Collections.Generic;

namespace AdventOfCode2020
{
    public abstract class SimpleDay : LoggingDay
    {
        public override void SolvePuzzle1(IList<IList<string>> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                SolvePuzzle1(inputs[i]);
            }
        }

        public override void SolvePuzzle2(IList<IList<string>> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                SolvePuzzle2(inputs[i]);
            }
        }

        protected abstract void SolvePuzzle1(IList<string> input);

        protected abstract void SolvePuzzle2(IList<string> input);
    }
}
