namespace SM.Domain.Model
{
    public class SwitchedRange
    {
        public SwitchedRange(int from, int to, bool disabled)
        {
            From = from;
            To = to;
            Disabled = disabled;
        }

        public int From { get; }
        public int To { get; }
        public bool Disabled { get; }
    }
}
