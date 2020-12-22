using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    internal class Day2 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input2.txt" };

        public string Name => "Day 2";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            int validcount = 0;
            string[] temp;

            string pnumbers = @"\d+-\d+";
            string pchar = @"\w:";
            string ppassword = @":\s\w+";

            Match match;
            foreach (string line in inputs[0])
            {
                match = Regex.Match(line, pnumbers);
                temp = match.Captures[0].Value.Split("-");
                int from = int.Parse(temp[0]);
                int to = int.Parse(temp[1]);

                match = Regex.Match(line, pchar);
                char letter = match.Captures[0].Value[0];

                match = Regex.Match(line, ppassword);
                string password = match.Captures[0].Value[2..];

                int count = Regex.Matches(password, letter.ToString()).Count;

                if (count >= from && count <= to)
                {
                    validcount++;
                    Helper.Logger.Log(Name, $"line {line} valid");
                }
                else
                {
                    Helper.Logger.Log(Name, $"line {line} invalid");
                }
            }

            Helper.Logger.Log(Name, $"Count of valid passwords is {validcount}");
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            int validcount = 0;
            string[] temp;

            string pnumbers = @"\d+-\d+";
            string pchar = @"\w:";
            string ppassword = @":\s\w+";

            Match match;
            foreach (string line in inputs[0])
            {
                match = Regex.Match(line, pnumbers);
                temp = match.Captures[0].Value.Split("-");
                int pos1 = int.Parse(temp[0]);
                int pos2 = int.Parse(temp[1]);

                match = Regex.Match(line, pchar);
                char letter = match.Captures[0].Value[0];

                match = Regex.Match(line, ppassword);
                string password = match.Captures[0].Value[2..];

                int count = 0;

                if (password[pos1 - 1] == letter)
                {
                    count++;
                }

                if (password[pos2 - 1] == letter)
                {
                    count++;
                }

                if (count == 1)
                {
                    validcount++;
                    Helper.Logger.Log(Name, $"line {line} valid");
                }
                else
                {
                    Helper.Logger.Log(Name, $"line {line} invalid");
                }
            }

            Helper.Logger.Log(Name, $"Count of valid passwords is {validcount}");
        }
    }
}
