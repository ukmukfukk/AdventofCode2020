using System.Collections.Generic;

namespace AdventOfCode2020
{
    internal interface IDay
    {
        IList<string> InputFiles { get; }

        string Name { get; }

        void SolvePuzzle1(IList<IList<string>> inputs);

        void SolvePuzzle2(IList<IList<string>> inputs);
    }
}
