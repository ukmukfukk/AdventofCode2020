using System.Collections.Generic;

namespace AdventOfCode2020.Day23Help
{
    public class Ring
    {
        private readonly Dictionary<int, Link> dict = new Dictionary<int, Link>();

        public Ring(int[] cups)
        {
            Head = new Link(cups[0]);
            dict.Add(cups[0], Head);
            var prev = Head;
            for (int i = 1; i < cups.Length; i++)
            {
                prev.Next = new Link(cups[i]);
                prev = prev.Next;
                dict.Add(cups[i], prev);
            }

            Max = cups.Length;
            prev.Next = Head;
        }

        public Link Head { get; private set; }

        public int Max { get; }

        public void MakeMoves(int moves)
        {
            var current = Head;

            for (int i = 0; i < moves; i++)
            {
                var currentv = current.Value;
                int targetv = currentv - 1;
                while (targetv > 0 && NextThreeContainsValue(current, targetv))
                {
                    targetv--;
                }

                if (targetv == 0)
                {
                    targetv = Max;
                    while (NextThreeContainsValue(current, targetv) || currentv == targetv)
                    {
                        targetv--;
                    }
                }

                Link target = dict[targetv];
                Link removed1 = current.Next;

                current.Next = current.Next.Next.Next.Next;
                removed1.Next.Next.Next = target.Next;
                target.Next = removed1;
                current = current.Next;
            }
        }

        public string List1String()
        {
            var list = new List<int>();
            var current = dict[1].Next;
            while (current.Value != 1)
            {
                list.Add(current.Value);
                current = current.Next;
            }

            return string.Join(", ", list);
        }

        public string List1Next2()
        {
            var current = dict[1].Next;
            return current.Value + ", " + current.Next.Value;
        }

        private bool NextThreeContainsValue(Link current, int targetv)
        {
            return current.Next.Value == targetv || current.Next.Next.Value == targetv || current.Next.Next.Next.Value == targetv;
        }
    }
}
