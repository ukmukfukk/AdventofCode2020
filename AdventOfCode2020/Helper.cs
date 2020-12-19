using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Helper
    {
        private static readonly Dictionary<CharRange, char[]> CharRanges;

        static Helper()
        {
            CharRanges = new Dictionary<CharRange, char[]>
            {
                { CharRange.Numeric, new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' } },
                { CharRange.HexaDecimal, new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' } },
            };
        }

        public static Regex EmptyLine => new Regex(@"^\s*$");

        public static Regex WhiteSpace => new Regex(@"\s*");

        public static Regex Digits => new Regex(@"\d+");

        public static Regex Letters => new Regex(@"\p{L}+");

        public static ISimpleLogger Logger { get; internal set; }

        public static bool Between(this int b, int from, int to)
        {
            return b >= from && b <= to;
        }

        public static bool IsOneOf(this string s, params string[] values)
        {
            foreach (string v in values)
            {
                if (s.Equals(v))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsOneOf(this char c, params char[] values)
        {
            foreach (char v in values)
            {
                if (c.Equals(v))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool AllChars(this string s, int from, int to, CharRange charRange)
        {
            for (int i = from; i <= to; i++)
            {
                if (!s[i].IsOneOf(CharRanges[charRange]))
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<IList<int>> GetPermuations(IList<int> values)
        {
            return GetPermuations(values, new List<int>());
        }

        public static bool LastNMatch(this IList<int> main, List<int> lastN, int n)
        {
            for (int i = n - 1; i >= 0; i--)
            {
                if (main[main.Count - n + i] != lastN[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsNotNull(int? nullable, out int value)
        {
            if (nullable.HasValue)
            {
                value = nullable.Value;
                return true;
            }
            else
            {
                value = default(int);
                return false;
            }
        }

        internal static IList<IList<string>> GetInputs(string inputDir, IList<string> inputFiles)
        {
            var retval = new List<IList<string>>();

            foreach (var inputfile in inputFiles)
            {
                retval.Add(ReadFile(Path.Combine(inputDir, inputfile)));
            }

            return retval;
        }

        private static IEnumerable<IList<int>> GetPermuations(IList<int> values, IList<int> beginning)
        {
            List<IList<int>> retval = new List<IList<int>>();
            var freevalues = values.Where(v => !beginning.Contains(v)).ToList();
            if (freevalues.Count() == 0)
            {
                retval.Add(beginning);
                yield return beginning;
            }

            for (int i = 0; i < freevalues.Count(); i++)
            {
                var copy = new List<int>(beginning) { freevalues[i] };
                foreach (var a in GetPermuations(values, copy))
                {
                    yield return a;
                }
            }
        }

        private static IList<string> ReadFile(string fileName)
        {
            var retval = new List<string>();

            using (var sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    retval.Add(line);
                }
            }

            return retval;
        }
    }
}
