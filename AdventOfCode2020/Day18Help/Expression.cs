using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Day18Help
{
    public class Expression : ExpressionBase
    {
        public Expression(string s)
        {
            int opeq = s.IndexOf('=');
            if (opeq != -1)
            {
                string[] split = s.Split('=');
                Expected = int.Parse(split[1]);
                s = split[0];
            }

            int level0opindex = GetLevel0OpIndex(s);
            while (level0opindex == -1)
            {
                s = s[1..^1];
                level0opindex = GetLevel0OpIndex(s);
            }

            Op = s[level0opindex];
            string before = s[0..level0opindex];
            string after = s[(level0opindex + 1)..];
            if (int.TryParse(before, out int b))
            {
                First = b;
            }
            else
            {
                FirstEx = new Expression(before);
            }

            if (int.TryParse(after, out int a))
            {
                Next = a;
            }
            else
            {
                NextEx = new Expression(after);
            }
        }

        private int GetLevel0OpIndex(string s)
        {
            return GetLevel0OpIndex(s, '+', '*');
        }
    }
}
