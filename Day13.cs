using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class Day13 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input13test.txt", "input13.txt" };

        public string Name => "Day 13";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle1(input);
            }
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle2(input);
            }
        }

        private void SolvePuzzle1(IList<string> input)
        {
            throw new NotImplementedException();
        }

        private void SolvePuzzle2(IList<string> input)
        {
            throw new NotImplementedException();
        }
    }
}
