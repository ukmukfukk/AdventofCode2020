using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Day22Help
{
    public class DeckHistory
    {
        private readonly List<Tuple<List<int>, List<int>>> tuples = new List<Tuple<List<int>, List<int>>>();

        internal void Add(List<int> player1, List<int> player2)
        {
            tuples.Add(new Tuple<List<int>, List<int>>(player1, player2));
        }

        internal bool HadAlready(List<int> player1, List<int> player2)
        {
            foreach (var tuple in tuples)
            {
                bool cl = ListsEqual(tuple.Item1, player1);
                if (cl && ListsEqual(tuple.Item2, player2))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ListsEqual(List<int> one, List<int> other)
        {
            if (one.Count != other.Count)
            {
                return false;
            }

            for (int i = 0; i < one.Count; i++)
            {
                if (one[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
