using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Day18Help
{
    public class ExpressionPrecedence : ExpressionBase
    {
        public ExpressionPrecedence(string s)
        {
            int opeq = s.IndexOf('=');
            if (opeq != -1)
            {
                string[] split = s.Split('=');
                Expected = int.Parse(split[2]);
                s = split[0];
            }

            if (int.TryParse(s, out int p))
            {
                First = p;
                return;
            }

            int level0opindex = -1;
            s = "X" + s + "X";
            while (level0opindex == -1)
            {
                s = s[1..^1];

                level0opindex = GetLevel0OpIndex(s, '+');
                if (level0opindex == -1)
                {
                    level0opindex = GetLevel0OpIndex(s, '*');
                }
            }

            string before;
            string after;
            Op = s[level0opindex];

            if (Op == '*')
            {
                before = s[0..level0opindex];
                after = s[(level0opindex + 1)..];
                if (int.TryParse(before, out int b))
                {
                    First = b;
                }
                else
                {
                    FirstEx = new ExpressionPrecedence(before);
                }

                if (int.TryParse(after, out int a))
                {
                    Next = a;
                }
                else
                {
                    NextEx = new ExpressionPrecedence(after);
                }
            }
            else
            {
                int beforei = GetOperandBefore(s, level0opindex);
                int afteri = GetOperandAfter(s, level0opindex);

                if (afteri < s.Length - 1)
                {
                    Op = '*';
                    FirstEx = new ExpressionPrecedence(s[0..(afteri + 1)]);
                    NextEx = new ExpressionPrecedence(s[(afteri + 2)..]);
                }
                else
                {
                    if (beforei > 0)
                    {
                        Op = '*';
                        FirstEx = new ExpressionPrecedence(s[0..(beforei - 1)]);
                        NextEx = new ExpressionPrecedence(s[beforei..]);
                    }
                    else
                    {
                        before = s[0..level0opindex];
                        after = s[(level0opindex + 1)..];
                        if (int.TryParse(before, out int b))
                        {
                            First = b;
                        }
                        else
                        {
                            FirstEx = new ExpressionPrecedence(before);
                        }

                        if (int.TryParse(after, out int a))
                        {
                            Next = a;
                        }
                        else
                        {
                            NextEx = new ExpressionPrecedence(after);
                        }
                    }
                }
            }
        }

        private int GetOperandAfter(string s, int start)
        {
            int depth = 0;
            for (int i = start + 1; i < s.Length; i++)
            {
                if (s[i] == ')')
                {
                    depth++;
                }

                if (s[i] == '(')
                {
                    depth--;
                }

                if (depth == 0)
                {
                    if (i == s.Length - 1)
                    {
                        return i;
                    }
                    else if (s[i] == '*')
                    {
                        return i - 1;
                    }
                }
            }

            throw new Exception("Something went terribly worng when trying to find operand after");
        }

        private int GetOperandBefore(string s, int end)
        {
            int depth = 0;
            for (int i = end - 1; i >= 0; i--)
            {
                if (s[i] == ')')
                {
                    depth++;
                }

                if (s[i] == '(')
                {
                    depth--;
                }

                if (depth == 0)
                {
                    if (i == 0)
                    {
                        return i;
                    }
                    else if (s[i] == '*')
                    {
                        return i + 1;
                    }
                }
            }

            throw new Exception("Something went terribly worng when trying to find operand before");
        }
    }
}
