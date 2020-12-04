using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    internal static class Helper
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

        internal static IList<IList<string>> GetInputs(string inputDir, IList<string> inputFiles)
        {
            var retval = new List<IList<string>>();

            foreach (var inputfile in inputFiles)
            {
                retval.Add(ReadFile(Path.Combine(inputDir, inputfile)));
            }

            return retval;
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
