namespace SM.Domain.Model
{
    public class SwitchedProperty
    {

        public SwitchedProperty(int value, bool disabled)
        {
            Value = value;
            Disabled = disabled;
        }

        public int Value { get; }
        public bool Disabled { get; }
    }
}
