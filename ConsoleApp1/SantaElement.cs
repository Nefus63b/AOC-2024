namespace ConsoleApp1
{
    public class SantaElement<T>
    {
        public long Index { get; set; }

        public T? Value { get; set; }

        public bool BoolValue { get; set; }

        public SantaElement()
        {
            Index = 0;
            Value = default;
            BoolValue = false;
        }

        public SantaElement(long index, T? value, bool boolValue)
        {
            Index = index;
            Value = value;
            BoolValue = boolValue;
        }
    }
}
