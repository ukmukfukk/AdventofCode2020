namespace AdventOfCode2020.Day18Help
{
    public class ExpressionBase
    {
        public int? First { get; protected set; }

        public int? Next { get; protected set; }

        public ExpressionBase FirstEx { get; protected set; }

        public ExpressionBase NextEx { get; protected set; }

        public char? Op { get; set; }

        public int? Expected { get; protected set; }

        public long Evaluate()
        {
            long? firstValue = GetFirst();
            long? nextValue = GetNext();
            if (Op == null)
            {
                if (firstValue != null)
                {
                    return firstValue.Value;
                }
                else
                {
                    return nextValue.Value;
                }
            }
            else
            {
                if (Op.Value == '*')
                {
                    return firstValue.Value * nextValue.Value;
                }
                else
                {
                    return firstValue.Value + nextValue.Value;
                }
            }
        }

        public override string ToString()
        {
            string firststring = GetFirstString();
            string nextstring = GetNextString();
            if (Op == null)
            {
                if (firststring != null)
                {
                    return firststring;
                }
                else
                {
                    return nextstring;
                }
            }
            else
            {
                if (Op.Value == '*')
                {
                    return '(' + firststring + '*' + nextstring + ')';
                }
                else
                {
                    return '(' + firststring + '+' + nextstring + ')';
                }
            }
        }

        protected int GetLevel0OpIndex(string s, params char[] ops)
        {
            int depth = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '(')
                {
                    depth++;
                }

                if (s[i] == ')')
                {
                    depth--;
                }

                if (depth == 0 && s[i].IsOneOf(ops))
                {
                    return i;
                }
            }

            return -1;
        }

        private long? GetFirst()
        {
            if (First != null)
            {
                return First.Value;
            }

            if (FirstEx != null)
            {
                return FirstEx.Evaluate();
            }

            return null;
        }

        private string GetFirstString()
        {
            if (First != null)
            {
                return First.ToString();
            }

            if (FirstEx != null)
            {
                return FirstEx.ToString();
            }

            return null;
        }

        private long? GetNext()
        {
            if (Next != null)
            {
                return Next.Value;
            }

            if (NextEx != null)
            {
                return NextEx.Evaluate();
            }

            return null;
        }

        private string GetNextString()
        {
            if (Next != null)
            {
                return Next.ToString();
            }

            if (NextEx != null)
            {
                return NextEx.ToString();
            }

            return null;
        }
    }
}