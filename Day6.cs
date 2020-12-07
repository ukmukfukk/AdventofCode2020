using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day6 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input6.txt" };

        public string Name => "Day 6";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            List<string> groups = GetGroups(inputs[0]);

            List<bool[]> groupletter = SumGroupLetter(groups);
            int sum = SumGroupLetters(groupletter);

            Helper.Logger.Log(Name, $"Sum: {sum}");
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            List<string> groups = GetGroups(inputs[0]);

            List<bool[]> groupletter = SumEveryOneHasLetter(groups);
            int sum = SumGroupLetters(groupletter);

            Helper.Logger.Log(Name, $"Sum: {sum}");
        }

        private List<bool[]> SumEveryOneHasLetter(List<string> groups)
        {
            List<bool[]> retval = new List<bool[]>();
            bool[] temp;
            for (int g = 0; g < groups.Count; g++)
            {
                string[] persons = groups[g].Trim().Split(" ");

                temp = new bool[26];
                for (byte b = 0; b < 26; b++)
                {
                    bool has = true;
                    foreach (string person in persons)
                    {
                        if (!person.Contains((char)(b + 97)))
                        {
                            has = false;
                            break;
                        }
                    }

                    temp[b] = has;
                }

                retval.Add(temp);
            }

            return retval;
        }

        private int SumGroupLetters(List<bool[]> groupletter)
        {
            int sum = 0;

            foreach (bool[] letters in groupletter)
            {
                foreach (bool letter in letters)
                {
                    if (letter)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        private List<string> GetGroups(IList<string> list)
        {
            string group = string.Empty;
            List<string> retval = new List<string>();

            foreach (string s in list)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    retval.Add(group);
                    group = string.Empty;
                }
                else
                {
                    group += s + " ";
                }
            }

            retval.Add(group);
            return retval;
        }

        private List<bool[]> SumGroupLetter(List<string> groups)
        {
            List<bool[]> retval = new List<bool[]>();
            bool[] temp;
            for (int g = 0; g < groups.Count; g++)
            {
                temp = new bool[26];
                for (byte b = 0; b < 26; b++)
                {
                    temp[b] = groups[g].Contains((char)(b + 97));
                }

                retval.Add(temp);
            }

            return retval;
        }
    }
}
