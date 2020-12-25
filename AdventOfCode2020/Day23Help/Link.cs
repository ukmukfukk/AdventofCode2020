namespace AdventOfCode2020.Day23Help
{
    public class Link
    {
        public Link(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public Link Next { get; internal set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}