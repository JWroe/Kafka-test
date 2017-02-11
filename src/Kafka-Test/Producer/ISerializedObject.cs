using System.Text;

namespace Producer
{
    public interface ISerializedObject
    {
        byte[] Value { get; }
    }

    public class SerializedString : ISerializedObject
    {
        private byte[] _value;
        private readonly string _input;
        private readonly Encoding _encoding;

        public SerializedString(string input) : this(input, Encoding.UTF8)
        {
        }

        public SerializedString(string input, Encoding encoding)
        {
            _input = input;
            _encoding = encoding;
        }

        public byte[] Value => _value ?? (_value = _encoding.GetBytes(_input));
    }
}