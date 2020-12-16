using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    internal class Day1 : IDay
    {
        public IList<string> InputFiles => new List<string> { "Input1.txt", };

        public string Name => "Day 1";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            var intList = inputs[0].Select(s => int.Parse(s)).ToList();
            var pair = Find2NumbersToSum2020(intList);

            if (pair == null)
            {
                Helper.Logger.Log(Name, "Did not find 2 numbers to sum 2020");
            }
            else
            {
                Helper.Logger.Log(Name, $"Indexes: {pair.Item1},{pair.Item2} Items: {intList[pair.Item1]},{intList[pair.Item2]} Multiplies: {intList[pair.Item1] * intList[pair.Item2]}");
            }
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            var intList = inputs[0].Select(s => int.Parse(s)).ToList();
            var trio = Find3NumbersToSum2020(intList);

            if (trio == null)
            {
                Helper.Logger.Log(Name, "Did not find 3 numbers to sum 2020");
            }
            else
            {
                Helper.Logger.Log(Name, $"Indexes: {trio.Item1},{trio.Item2},{trio.Item3} Items: {intList[trio.Item1]},{intList[trio.Item2]},{intList[trio.Item3]} Multiplies: {intList[trio.Item1] * intList[trio.Item2] * intList[trio.Item3]}");
            }
        }

        private Tuple<int, int, int> Find3NumbersToSum2020(List<int> list)
        {
            {
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        for (int k = j + 1; k < list.Count; k++)
                        {
                            if (list[i] + list[j] + list[k] == 2020)
                            {
                                return new Tuple<int, int, int>(i, j, k);
                            }
                        }
                    }
                }

                return null;
            }
        }

        private Tuple<int, int> Find2NumbersToSum2020(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i] + list[j] == 2020)
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }

            return null;
        }
    }
}
